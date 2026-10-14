using System;
using System.IO;
using System.Reflection;
using MSCLoader;
using UnityEngine;

namespace SatsumaPronto
{
    public class SatsumaPronto : Mod
    {
        public override string ID => "SatsumaPronto";
        public override string Name => "Satsuma Pronto";
        public override string Author => "mariozads";
        public override string Version => "1.5";
        public override string Description => Textos.Descricao;

        public override void ModSetup()
        {
            SetupFunction(Setup.ModSettings, Mod_Settings);
        }

        void Mod_Settings()
        {
            Settings.AddText(Textos.Explicacao);
            Settings.AddButton(Textos.Botao, Confirmar);
        }

        void Confirmar()
        {
            if (ModLoader.CurrentScene != CurrentScene.MainMenu)
            {
                ModUI.ShowMessage(Textos.SoNoMenu, "Satsuma Pronto");
                return;
            }
            ModUI.ShowYesNoMessage(Textos.Confirmar, "Satsuma Pronto", Aplicar);
        }

        static byte[] Recurso(string nome)
        {
            using (Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream("SaveModelo." + nome))
            using (var ms = new MemoryStream())
            {
                byte[] buf = new byte[81920];
                int n;
                while ((n = s.Read(buf, 0, buf.Length)) > 0)
                    ms.Write(buf, 0, n);
                return ms.ToArray();
            }
        }

        void Aplicar()
        {
            try
            {
                string pastaSave = Application.persistentDataPath;
                string arquivoSave = Path.Combine(pastaSave, "defaultES2File.txt");
                if (!File.Exists(arquivoSave))
                {
                    ModUI.ShowMessage(Textos.SemSave, "Satsuma Pronto");
                    return;
                }

                string backup = Path.Combine(pastaSave, "backup_" + DateTime.Now.ToString("yyyy-MM-dd_HHmmss"));
                Directory.CreateDirectory(backup);
                foreach (string f in Directory.GetFiles(pastaSave))
                    File.Copy(f, Path.Combine(backup, Path.GetFileName(f)));

                // Monta tudo na memória antes de gravar: se der erro, nada muda no save
                int trocadas, novas;
                byte[] novo = SaveMerge.Mesclar(File.ReadAllBytes(arquivoSave), Recurso("defaultES2File.txt"), out trocadas, out novas);

                string arquivoItens = Path.Combine(pastaSave, "items.txt");
                byte[] itens = File.Exists(arquivoItens) ? File.ReadAllBytes(arquivoItens) : new byte[0];
                byte[] novosItens = SaveMerge.InstalarItens(itens, Recurso("items.txt"));

                File.WriteAllBytes(arquivoSave, novo);
                File.WriteAllBytes(arquivoItens, novosItens);

                ModConsole.Print("[Satsuma Pronto] OK (" + trocadas + " / " + novas + "). Backup: " + backup);
                ModUI.ShowMessage(Textos.Pronto + "\n\n" + Textos.Backup + "\n" + backup, "Satsuma Pronto");
            }
            catch (Exception e)
            {
                ModConsole.LogError("[Satsuma Pronto] " + e);
                ModUI.ShowMessage(Textos.Erro + "\n" + e.Message, "Satsuma Pronto");
            }
        }
    }
}
