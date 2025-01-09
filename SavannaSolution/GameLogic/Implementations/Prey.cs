namespace GameLogic.Implementations
{
    public class Prey : Animal
    {
        public Prey(int speed, int visionRange, double health) : base(speed, visionRange, health) { }

        /// <summary>
        /// Moves the antelope away from the closest lion within its vision range.
        /// Searches through a list of animals to find the nearest lion,
        /// and if one is found within range, the antelope moves in the opposite direction.
        /// </summary>
        /// <param name="width"> The width of the game field. </param>
        /// <param name="height"> The height of the game field. </param>
        /// <param name="animals"> The list of animals on the game field. </param>
        public override void Move(int width, int height, List<Animal> animals)
        {
            if (!this.IsAlive)
            {
                return;
            }

            Animal closestPredator = null;
            double closestDistance = double.MaxValue;

            foreach (var animal in animals)
            {
                if (animal is Predator)
                {
                    double distance = this.GetDistance(animal);
                    if (distance < closestDistance && distance <= VisionRange)
                    {
                        closestDistance = distance;
                        closestPredator = animal;
                    }
                }
            }

            if (closestPredator != null)
            {
                if (closestPredator.X > X) X -= Speed;
                else if (closestPredator.X < X) X += Speed;
                if (closestPredator.Y > Y) Y -= Speed;
                else if (closestPredator.Y < Y) Y += Speed;
            }
            else
            {
                MoveRandomly(width, height);
            }

            X = Math.Clamp(X, 0, width - 1);
            Y = Math.Clamp(Y, 0, height - 1);

            Health -= TextConstants.HealthDamage;

            if (Health <= 0)
            {
                Kill();
            }
        }
    }
}
