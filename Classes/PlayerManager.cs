using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HangmanGame.Classes
{
    public class PlayerManager
    {
        public FileManager FileManager;

        public List<Player> PlayerList { get; set; } = new List<Player>();

        public PlayerManager()
        {
            FileManager = new FileManager();
            PlayerList = FileManager.LoadDataFromJson<Player>("players");
        }

        public List<(string Name, int GamesPlayed, int GamesWon, double WinRatio)> GenerateLeaderboard()
        {
            var leaderboard = new List<(string Name, int GamesPlayed, int GamesWon, double WinRatio)>();

            try
            {
                foreach (var player in PlayerList)
                {
                    double winPercentage;

                    if (player.GamesPlayed > 0)
                    {
                        winPercentage = ((double)player.GamesWon / player.GamesPlayed) * 100;
                        leaderboard.Add((player.Name, player.GamesPlayed, player.GamesWon, winPercentage));
                    }
                    else
                    {
                        winPercentage = 0;
                        leaderboard.Add((player.Name, player.GamesPlayed, player.GamesWon, winPercentage));
                    }
                }
            }
            catch (Exception e)
            {
                AnsiConsole.MarkupLine($"[yellow]Ett fel inträffade vid generering av leaderboard: {e.Message}[/]");

            }
            var sortedLeaderboard = leaderboard
                .OrderByDescending(player => player.WinRatio)
                .ThenByDescending(player => player.GamesWon)
                .ToList();

            return sortedLeaderboard;
        }

        public void AddNewPlayer()
        {
            AnsiConsole.MarkupLine("[green]Ange namnet på den nya spelaren[/]");
            string newName = Console.ReadLine()!;

            if (!string.IsNullOrWhiteSpace(newName) && !PlayerList.Any(player => player.Name.Equals(newName, StringComparison.OrdinalIgnoreCase)))
            {
                PlayerList.Add(new Player(newName, 0, 0));
            }
            else
            {
                AnsiConsole.MarkupLine("[yellow]Ogiltigt namn eller spelaren finns redan![/]");
            }
        }

        public Player SelectPlayer(string name)
        {
            Player selectedPlayer = null!;
            foreach (var player in PlayerList)
            {
                if (player.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    selectedPlayer = player;
                    break;
                }
            }
            return selectedPlayer;
        }
    }
}
