using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanGame.Classes
{
    public static class HangmanVisuals
    {
        public static void DrawHangman(int wrongGuesses)
        {
            // Array med varje steg i hänga gubben
            string[] stages = new string[]
            {
            @"
             +---+
                 |
                 |
                 |
                 |
                 |
            =========",
            @"
             +---+
             |   |
                 |
                 |
                 |
                 |
            =========",
            @"
             +---+
             |   |
             O   |
                 |
                 |
                 |
            =========",
            @"
             +---+
             |   |
             O   |
             |   |
                 |
                 |
            =========",
            @"
             +---+
             |   |
             O   |
             |   |
                 |
                 |
            =========",
            @"
             +---+
             |   |
             O   |
            /|   |
                 |
                 |
            =========",
            @"
             +---+
             |   |
             O   |
            /|\  |
                 |
                 |
            =========",
            @"
             +---+
             |   |
             O   |
            /|\  |
            /    |
                 |
            =========",
            @"
             +---+
             |   |
             O   |
            /|\  |
            / \  |
                 |
            ========="
            };
            Console.WriteLine();
            AnsiConsole.Write(new Panel(stages[wrongGuesses])
                .Header("HängaGubben", Justify.Center)
                .BorderColor(Color.Red));
        }
        public static void ShowVictory()
        {
            Console.Clear();
            Console.WriteLine("    \\o/   Hurra!");
            Console.WriteLine("     |     ");
            Console.WriteLine("    / \\   ");
            Console.WriteLine("   ------  ");
        }
    }
}

