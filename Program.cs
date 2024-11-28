using HangmanGame.Classes;

namespace HangmanGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameMenus hangmanGameMenu = new GameMenus();

            hangmanGameMenu.MainMenu();
        }
    }
}
