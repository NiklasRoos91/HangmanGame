using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace HangmanGame.Classes
{
    public class GameMenus
    {
        public HangmanGame HangmanGame { get; set; }

        public GameMenus()
        {
            HangmanGame = new HangmanGame();
        }

        public void MainMenu()
        {
            while (true)
            {
                AnsiConsole.MarkupLine("[purple]Hangman[/]");

                var userChoice = PromptForChoices(
                    "[green]Välj ett alternativ[/]",
                    new List<string> { "Spela", "Visa spelar statistik", "Avsluta" });

                switch (userChoice)
                {
                    case "Spela":
                        ShowPlayerMenu();
                        HangmanGame.StartGame();
                        PromptPlayAgain();
                        break;
                    case "Visa spelar statistik":
                        DisplayLeaderboard();
                        break;
                    case "Avsluta":
                        AnsiConsole.MarkupLine("[red]Avslutar...[/]");
                        return;
                    default:
                        AnsiConsole.MarkupLine("[yellow]Inte ett giltigt alternativ, välj igen[/]");
                        break;
                }
                AnsiConsole.MarkupLine("[green]Tryck på en knapp för att återgå till första menyn[/]");
                Console.ReadKey();
                Console.Clear();
            }
        }

        public void ShowPlayerMenu()
        {
            while (true)
            {
                var players = HangmanGame.PlayerManager.PlayerList;

                List<string> playerNames = new List<string>();

                foreach (var player in players)
                {
                    playerNames.Add(player.Name);
                }

                playerNames.Add("Lägg till ny spelare");

                var userChoice = PromptForChoices(
                    "[green]Välj en spelare eller lägg till en ny[/]",
                    new List<string>(playerNames));

                if (userChoice == "Lägg till ny spelare")
                {
                    HangmanGame.PlayerManager.AddNewPlayer();
                }
                else
                {
                    var selectedPlayer = HangmanGame.PlayerManager.SelectPlayer(userChoice);
                    
                    if (selectedPlayer != null)
                    {
                        AnsiConsole.MarkupLine($"[green]Du har valt spelaren: {selectedPlayer.Name}[/]");
                        HangmanGame.SelectedPlayer = selectedPlayer;
                        break;
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[red]Den valda spelaren finns inte, försök igen.[/]");
                    }
                }
            }
        }

        public void PromptPlayAgain()
        {
            while (true)
            {
                var userChoice = PromptForChoices(
                    "[green]Vill du spela igen[/]",
                    new List<string> { "Ja", "Nej" });

                switch (userChoice)
                {
                    case "Ja":
                        HangmanGame.StartGame();
                        break;
                    case "Nej":
                        return;
                    default:
                        AnsiConsole.MarkupLine("[yellow]Inte ett giltigt alternativ, välj igen[/]");
                        break;
                }
            }
        }

        public void DisplayLeaderboard()
        {
            var leaderboard = HangmanGame.PlayerManager.GenerateLeaderboard();

            var table = new Table();
            table.AddColumn("Rank");
            table.AddColumn("Name");
            table.AddColumn("Win %");
            table.AddColumn("Games Played");
            table.AddColumn("Games Won");

            int rank = 1;
            foreach (var player in leaderboard)
            {
                table.AddRow(rank.ToString(), player.Name, player.WinRatio.ToString("0.00"), player.GamesPlayed.ToString(), player.GamesWon.ToString());
                rank++;
            }
            AnsiConsole.Write(table);
        }
        private string PromptForChoices(string title, List<string> choices )
        {
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title(title)
                .AddChoices(choices));
        }
    }
}