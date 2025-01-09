using GameLogic.Implementations;
using GameLogic.Interfaces;

namespace UnitTests
{
    public class MockDisplayer : IGameDisplayer
    {
        public void Display(GameField gameField)
        {
            // Do nothing for tests
        }
    }
}
