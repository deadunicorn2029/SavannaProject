using GameLogic;
using GameLogic.Implementations;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new GameLogic.GameLogic(width: 20, height: 10);
            game.Run();
        }
    }
}
