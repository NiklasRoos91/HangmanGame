using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanGame.Classes
{
    public class Word
    {
        public string Value { get; set; }
        public string Category { get; set; }

        public Word(string value, string category)
        {
            Value = value;
            Category = category;
        }
    }
}
