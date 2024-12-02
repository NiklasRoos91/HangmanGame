using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanGame.Classes
{
    public class HangmanGame
    {
        public PlayerManager PlayerManager { get; set; }
        public WordManager WordManager { get; set; }
        public Player SelectedPlayer { get; set; }

        public HangmanGame()
        {
            WordManager = new WordManager();
            PlayerManager = new PlayerManager();
        }

        public void StartGame()
        {
            Console.Clear();
            string wordToGuess = WordManager.GetRandomWord();
            List<char> wordInProgress = wordToGuess.Select(character => '_').ToList();
            int numberOfWrongGuesses = 0;
            int maxNumberOfWrongGuesses = 8;
            List<char> guessedLetters = new List<char>();

            try
            {
                while (numberOfWrongGuesses < maxNumberOfWrongGuesses && wordInProgress.Contains('_'))
                {
                    string wordInProgressString = string.Join("", wordInProgress);

                    HangmanVisuals.DrawHangman(numberOfWrongGuesses);
                    AnsiConsole.MarkupLine($"[green] Ord att gissa: {wordInProgressString}[/]");
                    
                    if (guessedLetters.Count > 0)
                    {
                        AnsiConsole.Markup("[green]Gissade bokstäver: [/]");
                        foreach (char character in guessedLetters)
                        {
                            AnsiConsole.Markup($"[green]{character}, [/]");
                        }
                        Console.WriteLine();
                    }

                    AnsiConsole.MarkupLine("[yellow]Ange en bokstav: [/]");

                    char guess = Console.ReadKey(true).KeyChar;

                    if (char.IsLetter(guess))
                    {
                        guess = Char.ToLower(guess);

                        if (guessedLetters.Contains(guess))
                        {
                            Console.Clear();
                            AnsiConsole.MarkupLine("[yellow]Du har redan gissat den bokstaven! Försök igen.[/]");
                        }
                        else
                        {
                            guessedLetters.Add(guess);
                            numberOfWrongGuesses = ProcessIfGuessIsRightOrWrong(wordToGuess, wordInProgress, numberOfWrongGuesses, maxNumberOfWrongGuesses, guess);
                        }
                    }
                    else
                    {
                        Console.Clear();
                        AnsiConsole.MarkupLine("[yellow]Ogiltig inmatning! Vänligen ange en giltig bokstav.[/]");
                    }
                    AnsiConsole.MarkupLine($"[yellow]Antal gissningar kvar: {maxNumberOfWrongGuesses - numberOfWrongGuesses}[/]");
                    AnsiConsole.MarkupLine($"[yellow]Felaktiga gissningar: {numberOfWrongGuesses}[/]");

                }

                if (numberOfWrongGuesses >= maxNumberOfWrongGuesses)
                {
                    HangmanVisuals.DrawHangman(numberOfWrongGuesses);
                    SelectedPlayer.GamesPlayed++;
                    AnsiConsole.MarkupLine("[red]Du förlorade spelet. Försök igen![/]");
                }
                else
                {
                    SelectedPlayer.GamesPlayed++;
                    SelectedPlayer.GamesWon++;
                    HangmanVisuals.ShowVictory();
                    AnsiConsole.MarkupLine($"[green]Grattis! Du vann! Du har gissat ordet: {wordToGuess}[/]");
                }

                PlayerManager.FileManager.SaveDataToJson<Player>(PlayerManager.PlayerList, "players");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private static int ProcessIfGuessIsRightOrWrong(string wordToGuess, List<char> wordInProgress, int numberOfWrongGuesses, int MaxNumberOfWrongGuesses, char guess)
        {
            if (wordToGuess.Contains(guess))
            {
                for (int i = 0; i < wordToGuess.Length; i++)
                {
                    if (wordToGuess[i] == guess)
                    {
                        wordInProgress[i] = guess;
                        Console.Clear();
                    }
                }
            }
            else
            {
                numberOfWrongGuesses++;
                Console.Clear();
                AnsiConsole.MarkupLine($"[red]Felaktig gissning[/]");
            }
            return numberOfWrongGuesses;
        }
    }
}
