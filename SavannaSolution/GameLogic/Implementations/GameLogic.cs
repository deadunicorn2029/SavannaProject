using GameLogic.Implementations;

namespace GameLogic
{
    public class GameLogic
    {
        private readonly GameField _gameField;
        private readonly Dictionary<ConsoleKey, AnimalConfig> _animalConfigs;

        public GameLogic(int width, int height)
        {
            GameDisplayer displayer = new GameDisplayer();
            _gameField = new GameField(width, height, displayer);
            _animalConfigs = LoadAnimalConfigs();
        }

        /// <summary>
        /// Starts the main game loop, displaying game rules and continuously updating the game field. 
        /// </summary>
        public void Run()
        {
            Console.WriteLine(TextConstants.GameRuleText);

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var input = Console.ReadKey(true).Key;
                    if (_animalConfigs.TryGetValue(input, out var config))
                    {
                        _gameField.AddAnimal(GameUtils.CreateAnimalFromConfig(config));
                    }
                }
                _gameField.Update();
                Thread.Sleep(500);
            }
        }

        /// <summary>
        /// Loads animal configurations from JSON files in the plugin directory.
        /// </summary>
        /// <returns>Dictionary mapping input keys to their respective animal configurations.</returns>
        private Dictionary<ConsoleKey, AnimalConfig> LoadAnimalConfigs()
        {
            var configs = PluginLoader.LoadAnimalConfigs();
            return configs.ToDictionary(
                config => GameUtils.GetKeyFromAnimalReadKey(config.AnimalReadKey),
                config => config
            );
        }

    }
}