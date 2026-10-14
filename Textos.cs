using UnityEngine;

namespace SatsumaPronto
{
    // Textos do mod. A versão "amigos" (SO_PT) é sempre em português;
    // a versão multilíngue usa o idioma do Windows e cai para inglês.
    public static class Textos
    {
        public static string Descricao, Explicacao, Botao, SoNoMenu, Confirmar, SemSave, Pronto, Backup, Erro;

        static Textos()
        {
#if SO_PT
            Usar(SystemLanguage.Portuguese);
#else
            Usar(Application.systemLanguage);
#endif
        }

        static void Usar(SystemLanguage lingua)
        {
            switch (lingua)
            {
                case SystemLanguage.Portuguese:
                    Def("Um botão que monta o Satsuma (stock) no seu save, mantendo o resto do jogo.",
                        "Monta o Satsuma no seu save, com bateria e tanque cheio. Dinheiro, itens e progresso continuam iguais. Faz backup antes. Use no menu principal.",
                        "Montar o Satsuma no meu save",
                        "Só funciona no MENU PRINCIPAL.\nSaia para o menu e tente de novo.",
                        "Trocar o carro do seu save pelo Satsuma montado?\nO resto do save continua igual e é feito um backup antes.",
                        "Não achei o save. Comece um jogo novo, salve, volte ao menu e tente de novo.",
                        "Pronto! Clique em CONTINUE para jogar com o Satsuma montado.",
                        "Backup do save antigo:",
                        "Deu erro, o save não foi alterado:");
                    break;
                case SystemLanguage.Spanish:
                    Def("Un botón que monta el Satsuma (de serie) en tu partida, sin tocar el resto del juego.",
                        "Monta el Satsuma en tu partida, con batería y depósito lleno. Dinero, objetos y progreso no cambian. Hace una copia de seguridad antes. Úsalo en el menú principal.",
                        "Montar el Satsuma en mi partida",
                        "Solo funciona en el MENÚ PRINCIPAL.\nVuelve al menú e inténtalo de nuevo.",
                        "¿Cambiar el coche de tu partida por el Satsuma montado?\nEl resto de la partida no cambia y se hace una copia de seguridad antes.",
                        "No encontré la partida. Empieza un juego nuevo, guarda, vuelve al menú e inténtalo de nuevo.",
                        "¡Listo! Pulsa CONTINUE para jugar con el Satsuma montado.",
                        "Copia de seguridad de la partida anterior:",
                        "Ocurrió un error, la partida no se modificó:");
                    break;
                case SystemLanguage.Finnish:
                    Def("Yksi nappi, joka kokoaa Satsuman (vakio) tallennukseesi muuttamatta muuta peliä.",
                        "Kokoaa Satsuman tallennukseesi, akku ja täysi tankki mukaan lukien. Rahat, tavarat ja edistyminen pysyvät ennallaan. Tekee varmuuskopion ensin. Käytä päävalikossa.",
                        "Kokoa Satsuma tallennukseeni",
                        "Toimii vain PÄÄVALIKOSSA.\nPalaa valikkoon ja yritä uudelleen.",
                        "Vaihdetaanko tallennuksesi auto koottuun Satsumaan?\nMuu tallennus pysyy ennallaan ja varmuuskopio tehdään ensin.",
                        "Tallennusta ei löytynyt. Aloita uusi peli, tallenna, palaa valikkoon ja yritä uudelleen.",
                        "Valmis! Paina CONTINUE ja aja kootulla Satsumalla.",
                        "Vanhan tallennuksen varmuuskopio:",
                        "Tapahtui virhe, tallennusta ei muutettu:");
                    break;
                case SystemLanguage.German:
                    Def("Ein Knopf, der den Satsuma (Serie) in deinem Spielstand zusammenbaut, ohne den Rest zu ändern.",
                        "Baut den Satsuma in deinem Spielstand zusammen, mit Batterie und vollem Tank. Geld, Gegenstände und Fortschritt bleiben gleich. Macht vorher ein Backup. Im Hauptmenü benutzen.",
                        "Satsuma in meinem Spielstand bauen",
                        "Funktioniert nur im HAUPTMENÜ.\nGeh zurück ins Menü und versuche es erneut.",
                        "Das Auto in deinem Spielstand durch den fertigen Satsuma ersetzen?\nDer Rest bleibt gleich, vorher wird ein Backup gemacht.",
                        "Kein Spielstand gefunden. Starte ein neues Spiel, speichere, geh ins Menü und versuche es erneut.",
                        "Fertig! Klicke auf CONTINUE, um mit dem fertigen Satsuma zu spielen.",
                        "Backup des alten Spielstands:",
                        "Fehler, der Spielstand wurde nicht geändert:");
                    break;
                case SystemLanguage.French:
                    Def("Un bouton qui assemble la Satsuma (d'origine) dans ta sauvegarde, sans toucher au reste du jeu.",
                        "Assemble la Satsuma dans ta sauvegarde, avec batterie et réservoir plein. Argent, objets et progression restent identiques. Fait une copie de secours avant. À utiliser dans le menu principal.",
                        "Assembler la Satsuma dans ma sauvegarde",
                        "Fonctionne uniquement dans le MENU PRINCIPAL.\nRetourne au menu et réessaie.",
                        "Remplacer la voiture de ta sauvegarde par la Satsuma assemblée ?\nLe reste ne change pas et une copie de secours est faite avant.",
                        "Sauvegarde introuvable. Commence une nouvelle partie, sauvegarde, reviens au menu et réessaie.",
                        "Terminé ! Clique sur CONTINUE pour jouer avec la Satsuma assemblée.",
                        "Copie de secours de l'ancienne sauvegarde :",
                        "Erreur, la sauvegarde n'a pas été modifiée :");
                    break;
                case SystemLanguage.Italian:
                    Def("Un pulsante che monta la Satsuma (di serie) nel tuo salvataggio, senza toccare il resto del gioco.",
                        "Monta la Satsuma nel tuo salvataggio, con batteria e serbatoio pieno. Soldi, oggetti e progressi restano uguali. Fa prima un backup. Usalo nel menu principale.",
                        "Monta la Satsuma nel mio salvataggio",
                        "Funziona solo nel MENU PRINCIPALE.\nTorna al menu e riprova.",
                        "Sostituire l'auto del tuo salvataggio con la Satsuma montata?\nIl resto non cambia e prima viene fatto un backup.",
                        "Salvataggio non trovato. Inizia una nuova partita, salva, torna al menu e riprova.",
                        "Fatto! Clicca CONTINUE per giocare con la Satsuma montata.",
                        "Backup del vecchio salvataggio:",
                        "Errore, il salvataggio non è stato modificato:");
                    break;
                case SystemLanguage.Polish:
                    Def("Jeden przycisk, który składa Satsumę (seryjną) w twoim zapisie, bez zmiany reszty gry.",
                        "Składa Satsumę w twoim zapisie, z akumulatorem i pełnym bakiem. Pieniądze, przedmioty i postęp zostają bez zmian. Najpierw robi kopię zapasową. Używaj w menu głównym.",
                        "Złóż Satsumę w moim zapisie",
                        "Działa tylko w MENU GŁÓWNYM.\nWróć do menu i spróbuj ponownie.",
                        "Zamienić auto w twoim zapisie na złożoną Satsumę?\nReszta zapisu się nie zmienia, najpierw zostanie zrobiona kopia zapasowa.",
                        "Nie znaleziono zapisu. Zacznij nową grę, zapisz, wróć do menu i spróbuj ponownie.",
                        "Gotowe! Kliknij CONTINUE, aby grać złożoną Satsumą.",
                        "Kopia zapasowa starego zapisu:",
                        "Wystąpił błąd, zapis nie został zmieniony:");
                    break;
                case SystemLanguage.Russian:
                    Def("Одна кнопка собирает Satsuma (сток) в твоём сохранении, не меняя остальную игру.",
                        "Собирает Satsuma в твоём сохранении, с аккумулятором и полным баком. Деньги, предметы и прогресс не меняются. Сначала делает резервную копию. Используй в главном меню.",
                        "Собрать Satsuma в моём сохранении",
                        "Работает только в ГЛАВНОМ МЕНЮ.\nВыйди в меню и попробуй снова.",
                        "Заменить машину в сохранении на собранную Satsuma?\nОстальное не изменится, сначала будет сделана резервная копия.",
                        "Сохранение не найдено. Начни новую игру, сохранись, выйди в меню и попробуй снова.",
                        "Готово! Нажми CONTINUE, чтобы играть на собранной Satsuma.",
                        "Резервная копия старого сохранения:",
                        "Ошибка, сохранение не изменено:");
                    break;
                case SystemLanguage.Ukrainian:
                    Def("Одна кнопка збирає Satsuma (сток) у твоєму збереженні, не змінюючи решту гри.",
                        "Збирає Satsuma у твоєму збереженні, з акумулятором і повним баком. Гроші, предмети та прогрес не змінюються. Спочатку робить резервну копію. Використовуй у головному меню.",
                        "Зібрати Satsuma у моєму збереженні",
                        "Працює лише в ГОЛОВНОМУ МЕНЮ.\nВийди в меню і спробуй знову.",
                        "Замінити машину у збереженні на зібрану Satsuma?\nРешта не зміниться, спочатку буде зроблено резервну копію.",
                        "Збереження не знайдено. Почни нову гру, збережися, вийди в меню і спробуй знову.",
                        "Готово! Натисни CONTINUE, щоб грати на зібраній Satsuma.",
                        "Резервна копія старого збереження:",
                        "Помилка, збереження не змінено:");
                    break;
                case SystemLanguage.Czech:
                    Def("Jedno tlačítko, které složí Satsumu (sériovou) ve tvé uložené hře, bez změny zbytku hry.",
                        "Složí Satsumu ve tvé uložené hře, s baterií a plnou nádrží. Peníze, předměty a postup zůstanou stejné. Nejdřív udělá zálohu. Použij v hlavním menu.",
                        "Složit Satsumu v mé uložené hře",
                        "Funguje jen v HLAVNÍM MENU.\nVrať se do menu a zkus to znovu.",
                        "Nahradit auto ve tvé uložené hře složenou Satsumou?\nZbytek se nezmění a nejdřív se udělá záloha.",
                        "Uložená hra nenalezena. Začni novou hru, ulož, vrať se do menu a zkus to znovu.",
                        "Hotovo! Klikni na CONTINUE a hraj se složenou Satsumou.",
                        "Záloha staré uložené hry:",
                        "Chyba, uložená hra nebyla změněna:");
                    break;
                case SystemLanguage.Turkish:
                    Def("Satsuma'yı (orijinal) kaydında toplayan tek bir düğme; oyunun geri kalanı değişmez.",
                        "Satsuma'yı kaydında akü ve dolu depoyla toplar. Para, eşyalar ve ilerleme aynı kalır. Önce yedek alır. Ana menüde kullan.",
                        "Satsuma'yı kaydımda topla",
                        "Sadece ANA MENÜDE çalışır.\nMenüye dön ve tekrar dene.",
                        "Kaydındaki araba toplanmış Satsuma ile değiştirilsin mi?\nKaydın geri kalanı değişmez ve önce yedek alınır.",
                        "Kayıt bulunamadı. Yeni oyun başlat, kaydet, menüye dön ve tekrar dene.",
                        "Tamam! Toplanmış Satsuma ile oynamak için CONTINUE'ya tıkla.",
                        "Eski kaydın yedeği:",
                        "Hata oluştu, kayıt değiştirilmedi:");
                    break;
                case SystemLanguage.Swedish:
                    Def("En knapp som bygger ihop Satsuman (original) i ditt sparade spel, utan att ändra resten.",
                        "Bygger ihop Satsuman i ditt sparade spel, med batteri och full tank. Pengar, saker och framsteg är oförändrade. Gör en säkerhetskopia först. Använd i huvudmenyn.",
                        "Bygg Satsuman i mitt sparade spel",
                        "Fungerar bara i HUVUDMENYN.\nGå tillbaka till menyn och försök igen.",
                        "Byta bilen i ditt sparade spel mot den färdiga Satsuman?\nResten ändras inte och en säkerhetskopia görs först.",
                        "Hittade inget sparat spel. Starta ett nytt spel, spara, gå till menyn och försök igen.",
                        "Klart! Klicka på CONTINUE för att spela med den färdiga Satsuman.",
                        "Säkerhetskopia av det gamla sparade spelet:",
                        "Ett fel uppstod, det sparade spelet ändrades inte:");
                    break;
                case SystemLanguage.Dutch:
                    Def("Eén knop die de Satsuma (standaard) in je savegame in elkaar zet, zonder de rest te veranderen.",
                        "Zet de Satsuma in je savegame in elkaar, met accu en volle tank. Geld, spullen en voortgang blijven hetzelfde. Maakt eerst een back-up. Gebruik in het hoofdmenu.",
                        "Satsuma bouwen in mijn savegame",
                        "Werkt alleen in het HOOFDMENU.\nGa terug naar het menu en probeer opnieuw.",
                        "De auto in je savegame vervangen door de complete Satsuma?\nDe rest blijft hetzelfde en er wordt eerst een back-up gemaakt.",
                        "Geen savegame gevonden. Start een nieuw spel, sla op, ga naar het menu en probeer opnieuw.",
                        "Klaar! Klik op CONTINUE om met de complete Satsuma te spelen.",
                        "Back-up van de oude savegame:",
                        "Er ging iets mis, de savegame is niet gewijzigd:");
                    break;
                case SystemLanguage.Hungarian:
                    Def("Egy gomb, ami összerakja a Satsumát (gyári) a mentésedben, a játék többi része nem változik.",
                        "Összerakja a Satsumát a mentésedben, akkumulátorral és teli tankkal. A pénz, a tárgyak és a haladás változatlan marad. Előbb biztonsági mentést készít. A főmenüben használd.",
                        "Satsuma összerakása a mentésemben",
                        "Csak a FŐMENÜBEN működik.\nLépj vissza a menübe, és próbáld újra.",
                        "Lecseréled a mentésed autóját az összerakott Satsumára?\nA többi nem változik, előbb biztonsági mentés készül.",
                        "Nem található mentés. Kezdj új játékot, ments, lépj a menübe, és próbáld újra.",
                        "Kész! Kattints a CONTINUE gombra, és játssz az összerakott Satsumával.",
                        "A régi mentés biztonsági másolata:",
                        "Hiba történt, a mentés nem változott:");
                    break;
                default:
                    Def("One button that builds the Satsuma (stock) in your save, keeping the rest of the game.",
                        "Builds the Satsuma in your save, with battery and a full tank. Money, items and progress stay the same. Makes a backup first. Use it in the main menu.",
                        "Build the Satsuma in my save",
                        "Only works in the MAIN MENU.\nGo back to the menu and try again.",
                        "Replace the car in your save with the built Satsuma?\nThe rest of the save stays the same and a backup is made first.",
                        "Save not found. Start a new game, save, go back to the menu and try again.",
                        "Done! Click CONTINUE to play with the built Satsuma.",
                        "Backup of the old save:",
                        "Something went wrong, the save was not changed:");
                    break;
            }
        }

        static void Def(string descricao, string explicacao, string botao, string soNoMenu, string confirmar,
                        string semSave, string pronto, string backup, string erro)
        {
            Descricao = descricao; Explicacao = explicacao; Botao = botao; SoNoMenu = soNoMenu;
            Confirmar = confirmar; SemSave = semSave; Pronto = pronto; Backup = backup; Erro = erro;
        }
    }
}
