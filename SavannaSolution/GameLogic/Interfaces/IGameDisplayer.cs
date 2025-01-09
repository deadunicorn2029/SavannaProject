using GameLogic.Implementations;

namespace GameLogic.Interfaces
{
    public interface IGameDisplayer
    {
        /// <summary>
        /// Displays the current state of the game field in the console.
        /// This method creates a grid representation of the field, using '.' for empty cells, 
        /// 'L' for lions, and 'A' for antelopes, then prints it to the console. 
        /// Each animal is placed at its respective position on the grid if within bounds.
        /// </summary>
        public void Display(GameField gameField);
    }
}
