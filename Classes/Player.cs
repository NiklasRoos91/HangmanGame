using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanGame.Classes
{
    public class Player
    {
        public string Name { get; set; }
        public int GamesPlayed { get; set; }
        public int GamesWon { get; set; }

        public Player(string name, int gamesPlayed, int gamesWon) 
        {
            Name = name;
            GamesPlayed = gamesPlayed;
            GamesWon = gamesWon;
        }
    }
}
