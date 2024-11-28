using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HangmanGame.Classes
{
    public class WordManager
    {
        FileManager FileManager;

        public List<Word> WordList { get; set; } = new List<Word>()
            .Select(word => new Word(word.Value.ToLower(), word.Category.ToLower())).ToList();

        public WordManager()
        {
            FileManager = new FileManager();
            WordList = FileManager.LoadDataFromJson<Word>("words");
        }

        public void ShowWords()
        {
            foreach(Word word in WordList)
            {
                Console.WriteLine($"{word.Value} + {word.Category}");
            }
        }

        public string GetRandomWord()
        {
            if (WordList.Count == 0)
            {
                AnsiConsole.MarkupLine("[yellow}Ordlistan är tom.Lägg till ord innan du genererar ett slumpmässigt ord.[/]");
            }
            Random random = new Random();
            int randomIndex = random.Next(WordList.Count);
            return WordList[randomIndex].Value;
        }
    }
}
