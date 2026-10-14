# Satsuma Pronto

Mod for **My Summer Car** ([MSCLoader](https://www.nexusmods.com/mysummercar/mods/147)): one button that builds the stock Satsuma into your own save, keeping your money, items and progress.

Mod para **My Summer Car** ([MSCLoader](https://www.nexusmods.com/mysummercar/mods/147)): um botão que monta o Satsuma original no seu próprio save, mantendo dinheiro, itens e progresso.

## ⬇️ Download

| Versão / Version | Arquivo / File |
|---|---|
| 🇧🇷 Português | [download/SatsumaPronto_PT.zip](download/SatsumaPronto_PT.zip) |
| 🌍 Multilingual (15 languages, follows Windows language) | [download/SatsumaPronto_Multilingual.zip](download/SatsumaPronto_Multilingual.zip) |

Languages: English, Português, Español, Suomi, Deutsch, Français, Italiano, Polski, Русский, Українська, Čeština, Türkçe, Svenska, Nederlands, Magyar.

## Como usar / How to use

1. Instale o MSCLoader e copie `SatsumaPronto.dll` para a pasta `Mods` do jogo.
   Install MSCLoader and copy `SatsumaPronto.dll` into the game's `Mods` folder.
2. No **menu principal**: configurações de mods → Satsuma Pronto → botão.
   In the **main menu**: mod settings → Satsuma Pronto → button.
3. Confirme e clique em **Continue**. / Confirm and click **Continue**.

O mod faz backup do save antes (`...\LocalLow\Amistech\My Summer Car\backup_<data>`).
The mod backs up your save first (`...\LocalLow\Amistech\My Summer Car\backup_<date>`).

## O que vem no carro / What the car gets

All stock parts installed and bolted, charged battery with tight terminals, 4 spark plugs, oil filter, alternator belt, rear seat, full fuel tank (36 L), 5 new tires, tight wheel nuts, and the spare wheel placed next to the player.

## Como funciona / How it works

The game save (`defaultES2File.txt`, `items.txt`) uses the EasySave2 binary format. The mod copies only the Satsuma-related keys from an embedded template save into your save and fixes a few values; everything else stays as it was.

## Compilar / Build

```
dotnet build -c Release -p:MSCDir="C:\...\My Summer Car"            # multilingual
dotnet build -c Release -p:MSCDir="C:\...\My Summer Car" -p:SoPT=true  # Portuguese only
```

Requires the .NET SDK and MSCLoader installed in the game folder.

## Créditos / Credits

- Template save with the built Satsuma: [churris-x/MSC-backup](https://github.com/churris-x/MSC-backup).
- [MSCLoader](https://github.com/piotrulos/MSCModLoader) by piotrulos.
- Made with [Claude Code](https://claude.com/claude-code).
