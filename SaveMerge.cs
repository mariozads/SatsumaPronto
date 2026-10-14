using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SatsumaPronto
{
    // Lê e grava o formato ES2 do defaultES2File.txt / items.txt:
    // cada entrada = 0x7E, chave (string .NET com tamanho 7-bit), int32 tamanho, dados.
    // Só usa tipos básicos: o Mono do jogo não tem tudo do .NET 3.5.
    public static class SaveMerge
    {
        // Peças do carro: "nome(Clone)...", "nome(xxxxx)...", "antenna(leftx)..." etc.
        static readonly string[] Peca = { "(Clone)", "(xxxxx)", "(leftx)", "(right)", "(rlxxx)", "(rrxxx)", "(flxxx)", "(frxxx)" };
        static readonly string[] Prefixo =
        {
            "Wear", "wheel_", "hubcap", "halfshaft_", "IK_wishbone_",
            "WheelFLDamageVector", "WheelFRDamageVector", "WheelRLDamageVector", "WheelRRDamageVector"
        };
        static readonly string[] Extras =
        {
            "CarbChamberLevel", "SatsumaHandbrakePosition", "SatsumaLightsSelection",
            "AlternatorRotation", "GearboxFinalGearRatio", "KeySatsuma",
            "WindshieldBroken", "WindshieldBrokenPos"
        };

        public static bool EhDoCarro(string chave)
        {
            if (chave.StartsWith("envelope(")) return false;     // carta do correio, não é peça
            if (chave.StartsWith("MotorHoist")) return false;    // guincho do motor, fica com o jogador
            if (chave.EndsWith("Purchased")) return false;       // compras continuam as do jogador
            foreach (string p in Peca) if (chave.Contains(p)) return true;
            foreach (string p in Prefixo) if (chave.StartsWith(p)) return true;
            if (Array.IndexOf(Extras, chave) >= 0) return true;
            return chave.EndsWith("Bolts") || chave.EndsWith("Tightness");
        }

        public static List<KeyValuePair<string, byte[]>> Ler(byte[] dados)
        {
            var lista = new List<KeyValuePair<string, byte[]>>();
            using (var r = new BinaryReader(new MemoryStream(dados), Encoding.UTF8))
            {
                while (r.BaseStream.Position < r.BaseStream.Length)
                {
                    byte marca = r.ReadByte();
                    if (marca != 0x7E)
                        throw new Exception("Save em formato inesperado na posição " + (r.BaseStream.Position - 1));
                    string chave = r.ReadString();
                    int tamanho = r.ReadInt32();
                    lista.Add(new KeyValuePair<string, byte[]>(chave, r.ReadBytes(tamanho)));
                }
            }
            return lista;
        }

        public static byte[] Gravar(List<KeyValuePair<string, byte[]>> lista)
        {
            var ms = new MemoryStream();
            using (var w = new BinaryWriter(ms, Encoding.UTF8))
            {
                foreach (var e in lista)
                {
                    w.Write((byte)0x7E);
                    w.Write(e.Key);
                    w.Write(e.Value.Length);
                    w.Write(e.Value);
                }
            }
            return ms.ToArray();
        }

        // Valores no formato ES2: FF + hash do tipo + valor + 7B
        static byte[] Valor(string hashTipo, byte[] valor)
        {
            var b = new List<byte> { 0xFF };
            for (int i = 0; i < 8; i += 2) b.Add(Convert.ToByte(hashTipo.Substring(i, 2), 16));
            b.AddRange(valor);
            b.Add(0x7B);
            return b.ToArray();
        }
        public static byte[] Bool(bool v) { return Valor("9c7c4dad", new[] { (byte)(v ? 1 : 0) }); }
        public static byte[] Int(int v) { return Valor("5608a8e2", BitConverter.GetBytes(v)); }
        public static byte[] Float(float v) { return Valor("6bd73e6e", BitConverter.GetBytes(v)); }

        // Lista de parafusos apertados: n vezes "int(8)"
        static byte[] ParafusosApertados(int n)
        {
            var b = new List<byte> { 0x53, 0xFF, 0xEE, 0xF1, 0xE9, 0xFD, 0x00 };
            b.AddRange(BitConverter.GetBytes(n));
            for (int i = 0; i < n; i++)
                b.AddRange(new byte[] { 0x06, (byte)'i', (byte)'n', (byte)'t', (byte)'(', (byte)'8', (byte)')' });
            b.Add(0x7B);
            return b.ToArray();
        }

        public static void Definir(List<KeyValuePair<string, byte[]>> lista, string chave, byte[] valor)
        {
            for (int i = 0; i < lista.Count; i++)
                if (lista[i].Key == chave) { lista[i] = new KeyValuePair<string, byte[]>(chave, valor); return; }
            lista.Add(new KeyValuePair<string, byte[]>(chave, valor));
        }

        static byte[] Pegar(List<KeyValuePair<string, byte[]>> lista, string chave)
        {
            foreach (var e in lista) if (e.Key == chave) return e.Value;
            return null;
        }

        static byte[] PegarModelo(List<KeyValuePair<string, byte[]>> modelo, string chave)
        {
            byte[] v = Pegar(modelo, chave);
            if (v == null) throw new Exception("Save modelo sem " + chave);
            return v;
        }

        // Peça parafusada: Installed, Bolted, todos os parafusos em int(8) e aperto = 8 × parafusos
        static void Parafusar(List<KeyValuePair<string, byte[]>> lista, string peca, string chaveParafusos, int parafusos)
        {
            Definir(lista, peca + "Installed", Bool(true));
            Definir(lista, peca + "Bolted", Bool(true));
            Definir(lista, chaveParafusos + "Bolts", ParafusosApertados(parafusos));
            Definir(lista, chaveParafusos + "Tightness", Float(8f * parafusos));
        }

        // Transform no ES2: posição (3 floats) começa no byte 6
        static void PorPertoDoJogador(List<KeyValuePair<string, byte[]>> lista, string chave)
        {
            byte[] jogador = Pegar(lista, "PlayerTransform");
            byte[] objeto = Pegar(lista, chave);
            if (jogador == null || objeto == null || jogador.Length < 18 || objeto.Length < 18) return;

            byte[] novo = (byte[])objeto.Clone();
            float[] desvio = { 1f, 0.5f, 0f };   // 1 m para o lado, meio metro acima
            for (int i = 0; i < 3; i++)
            {
                float v = BitConverter.ToSingle(jogador, 6 + 4 * i) + desvio[i];
                Buffer.BlockCopy(BitConverter.GetBytes(v), 0, novo, 6 + 4 * i, 4);
            }
            Definir(lista, chave, novo);
        }

        // Garante que o contador de itens (ex.: sparkplugID) chegue pelo menos a n
        static void IdMinimo(List<KeyValuePair<string, byte[]>> lista, string chave, int n)
        {
            byte[] id = Pegar(lista, chave);
            int atual = id != null && id.Length == 10 ? BitConverter.ToInt32(id, 5) : 0;
            if (atual < n) Definir(lista, chave, Int(n));
        }

        // items.txt: bateria, velas, correia e filtro de óleo ficam aqui, não no defaultES2File.
        public static byte[] InstalarItens(byte[] itensUsuario, byte[] itensModelo)
        {
            var lista = Ler(itensUsuario);
            var modelo = Ler(itensModelo);

            // Bateria do modelo, instalada e com carga cheia
            foreach (string k in new[] { "Transform", "Discharge", "ChargeMax" })
                Definir(lista, "battery1" + k, PegarModelo(modelo, "battery1" + k));
            Definir(lista, "battery1Charge", PegarModelo(modelo, "battery1ChargeMax"));
            Definir(lista, "battery1Installed", Bool(true));
            Definir(lista, "battery1Consumed", Bool(false));
            Definir(lista, "battery1OnCharged", Bool(false));
            IdMinimo(lista, "batteryID", 1);

            // 4 velas novas
            for (int i = 1; i <= 4; i++)
            {
                string p = "spark plug" + i;
                Definir(lista, p + "Transform", PegarModelo(modelo, p + "Transform"));
                Definir(lista, p + "TriggerID", PegarModelo(modelo, p + "TriggerID"));
                Definir(lista, p + "Installed", Bool(true));
                Definir(lista, p + "Consumed", Bool(false));
                Definir(lista, p + "Tightness", Float(8f));
                Definir(lista, p + "Wear", Float(100f));
            }
            IdMinimo(lista, "sparkplugID", 4);

            // Correia do alternador nova
            Definir(lista, "alternator belt1Transform", PegarModelo(modelo, "alternator belt1Transform"));
            Definir(lista, "alternator belt1Installed", Bool(true));
            Definir(lista, "alternator belt1Wear", Float(100f));
            IdMinimo(lista, "alternatorbeltID", 1);

            // Filtro de óleo original do jogo (oilfilter0) instalado no motor, limpo
            Definir(lista, "oilfilter0Transform", PegarModelo(modelo, "oil filter1Transform"));
            Definir(lista, "oilfilter0Installed", Bool(true));
            Definir(lista, "oilfilter0Consumed", Bool(false));
            Definir(lista, "oilfilter0Tightness", Float(8f));
            Definir(lista, "oilfilter0Dirt", Float(0f));

            return Gravar(lista);
        }

        // Copia as chaves do carro do modelo para o save do jogador; o resto fica igual.
        public static byte[] Mesclar(byte[] usuario, byte[] modelo, out int trocadas, out int novas)
        {
            var lista = Ler(usuario);
            var indice = new Dictionary<string, int>();
            for (int i = 0; i < lista.Count; i++)
                indice[lista[i].Key] = i;

            trocadas = novas = 0;
            foreach (var e in Ler(modelo))
            {
                if (!EhDoCarro(e.Key)) continue;
                int i;
                if (indice.TryGetValue(e.Key, out i))
                {
                    lista[i] = e;
                    trocadas++;
                }
                else
                {
                    indice[e.Key] = lista.Count;
                    lista.Add(e);
                    novas++;
                }
            }

            // Ajustes ao save modelo: cabos da bateria e banco traseiro estavam soltos,
            // o tanque tinha ~2 L e o cano de abastecimento estava solto.            Definir(lista, "wiring_battery_harness(xxxxx)Installed", Bool(true));
            Definir(lista, "wiring_battery_harness(xxxxx)Bolted", Bool(true));
            Parafusar(lista, "battery_terminal_plus(xxxxx)", "WiringBatteryPlus", 1);
            Parafusar(lista, "battery_terminal_minus(xxxxx)", "WiringBatteryMinus", 1);
            Parafusar(lista, "seat rear(Clone)", "Seat_Rear", 2);
            Definir(lista, "fuel tank(Clone)FuelLevel", Float(36f));
            Definir(lista, "fuel tank pipe(xxxxx)Bolted", Bool(true));
            // Dois pneus do modelo estavam quase no fim (vida 1): todos novos
            for (int i = 1; i <= 5; i++)
                Definir(lista, "wheel_steel" + i + "TireWear", Float(100f));
            // Rodas montadas (1 a 4) com as 4 porcas apertadas; no modelo a traseira esquerda tinha 2 frouxas
            for (int i = 1; i <= 4; i++)
            {
                Definir(lista, "wheel_steel" + i + "Bolts", ParafusosApertados(4));
                Definir(lista, "wheel_steel" + i + "Tightness", Float(32f));
            }
            // O estepe do modelo ficou a 1,4 km de casa: põe ao lado do jogador
            PorPertoDoJogador(lista, "wheel_steel5Transform");

            return Gravar(lista);
        }
    }
}
