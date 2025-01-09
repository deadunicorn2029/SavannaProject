using GameLogic.Interfaces;

namespace GameLogic.Implementations
{
    public class GameDisplayer : IGameDisplayer
    {
        public void Display(GameField gameField)
        {
            int width = gameField.Width;
            int height = gameField.Height;
            List<Animal> animals = gameField.Animals;

            char[,] grid = new char[height, width];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    grid[i, j] = TextConstants.EmptyCell;
                }
            }

            foreach (var animal in animals)
            {
                char animalSymbol = animal.AnimalInGameSymbol[0];

                if (animal.X >= 0 && animal.X < width && animal.Y >= 0 && animal.Y < height)
                {
                    grid[animal.Y, animal.X] = animalSymbol;
                }
            }

            Console.Clear();
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.Write(grid[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
