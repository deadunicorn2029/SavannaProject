namespace GameLogic.Implementations
{
    public static class GameUtils
    {
        /// <summary>
        /// Calculates the Euclidean distance between the current animal and another animal.
        /// </summary>
        /// <param name="animal">The current animal.</param>
        /// <param name="other">The other animal to calculate the distance to.</param>
        /// <returns>The Euclidean distance between the two animals.</returns>
        public static double GetDistance(this Animal animal, Animal other)
        {
            return Math.Sqrt(Math.Pow(other.X - animal.X, 2) + Math.Pow(other.Y - animal.Y, 2));
        }

        /// <summary>
        /// Creates an animal from the provided AnimalConfig.
        /// </summary>
        /// <param name="config">Animal configuration loaded from a plugin.</param>
        /// <returns>A new Animal instance with properties set from the configuration.</returns>
        public static Animal CreateAnimalFromConfig(AnimalConfig config)
        {
            if (config.AnimalBehaviour == "Predator")
            {
                return new Predator(config.Speed, config.VisionRange, config.Health)
                {
                    AnimalType = config.AnimalType,
                    AnimalReadKey = config.AnimalReadKey,
                    AnimalInGameSymbol = config.AnimalInGameSimbol
                };
            }
            else if (config.AnimalBehaviour == "Prey")
            {
                return new Prey(config.Speed, config.VisionRange, config.Health)
                {
                    AnimalType = config.AnimalType,
                    AnimalReadKey = config.AnimalReadKey,
                    AnimalInGameSymbol = config.AnimalInGameSimbol
                };
            }

            throw new Exception($"Unsupported animal behavior: {config.AnimalBehaviour}");
        }

        /// <summary>
        /// Converts a string read key from JSON to a ConsoleKey.
        /// </summary>
        /// <param name="readKey">The read key from the animal configuration.</param>
        /// <returns>Corresponding ConsoleKey.</returns>
        public static ConsoleKey GetKeyFromAnimalReadKey(string readKey)
        {
            return readKey.ToUpper() switch
            {
                "A" => ConsoleKey.A,
                "B" => ConsoleKey.B,
                "C" => ConsoleKey.C,
                "D" => ConsoleKey.D,
                "E" => ConsoleKey.E,
                "F" => ConsoleKey.F,
                "G" => ConsoleKey.G,
                "H" => ConsoleKey.H,
                "I" => ConsoleKey.I,
                "J" => ConsoleKey.J,
                "K" => ConsoleKey.K,
                "L" => ConsoleKey.L,
                "M" => ConsoleKey.M,
                "N" => ConsoleKey.N,
                "O" => ConsoleKey.O,
                "P" => ConsoleKey.P,
                "Q" => ConsoleKey.Q,
                "R" => ConsoleKey.R,
                "S" => ConsoleKey.S,
                "T" => ConsoleKey.T,
                "U" => ConsoleKey.U,
                "V" => ConsoleKey.V,
                "W" => ConsoleKey.W,
                "X" => ConsoleKey.X,
                "Y" => ConsoleKey.Y,
                "Z" => ConsoleKey.Z,
                _ => throw new Exception($"Unsupported key mapping: {readKey}")
            };
        }
    }
}
