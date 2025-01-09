namespace GameLogic.Implementations
{
    public abstract class Animal
    {
        public int X { get; set; }
        public int Y { get; set; }
        public (int X, int Y) Position { get; set; }
        public int Speed { get; set; }
        public int VisionRange { get; set; }
        public bool IsAlive {  get; private set; }
        public double Health { get; set; }
        private int _reproductionCounter = 0;
        public string AnimalType { get; set; }
        public string AnimalReadKey { get; set; }
        public string AnimalInGameSymbol { get; set; }

        protected Animal(int speed, int visionRange, double health)
        {
            Speed = speed;
            VisionRange = visionRange;
            Health = health;
            IsAlive = true;
        }

        /// <summary>
        /// Generates and returns an instance of `AnimalConfig` containing the current animal's behavior, type, read key, speed, vision range, and health.
        /// </summary>
        /// <returns> A new `AnimalConfig` instance representing the current animal's configuration. </returns>
        public AnimalConfig GetAnimalConfig()
        {
            return new AnimalConfig
            {
                AnimalBehaviour = this is Predator ? TextConstants.Predator : TextConstants.Prey,
                AnimalType = this.AnimalType,
                AnimalReadKey = this.AnimalReadKey,
                Speed = this.Speed,
                VisionRange = this.VisionRange,
                Health = this.Health
            };
        }

        /// <summary>
        /// Defines the movement logic for an animal, which will be implemented in derived classes.
        /// </summary>
        /// <param name="width"> The width of the game field. </param>
        /// <param name="height"> The height of the game field. </param>
        /// <param name="animals"> The list of animals on the game field, used to determine interactions with nearby animals. </param>
        public abstract void Move(int width, int height, List<Animal> animals);

        /// <summary>
        /// Setting animal status to dead.
        /// </summary>
        public void Kill() => IsAlive = false;

        /// <summary>
        /// Defines the random movement logic for an animal, which will be implemented in derived classes.
        /// </summary>
        /// <param name="width"> The width of the game field. </param>
        /// <param name="height"> The height of the game field. </param>
        protected void MoveRandomly(int width, int height)
        {
            Random random = new Random();
            int direction = random.Next(0, 4);

            switch (direction)
            {
                case 0: X += Speed; break;
                case 1: X -= Speed; break;
                case 2: Y += Speed; break;
                case 3: Y -= Speed; break;
            }

            X = Math.Clamp(X, 0, width - 1);
            Y = Math.Clamp(Y, 0, height - 1);
        }

        /// <summary>
        /// Determines whether the current animal can reproduce with another animal.
        /// Checks if the two animals are of the same type and within the reproduction distance.
        /// If conditions are met multiple times in sequence, reproduction is allowed.
        /// </summary>
        /// <param name="otherAnimal"> The other animal to check for reproduction compatibility. </param>
        /// <returns> True if the reproduction conditions are met; otherwise, false. </returns>
        public bool CanReproduce(Animal otherAnimal)
        {
            if(otherAnimal.AnimalType.Equals(this.AnimalType) && this.GetDistance(otherAnimal) <= 1)
            {
                _reproductionCounter++;
            }
            else if (_reproductionCounter > 0)
            {
                _reproductionCounter = 0;
            }

            return _reproductionCounter >= TextConstants.ReproductionCounter;
        }
    }
}
