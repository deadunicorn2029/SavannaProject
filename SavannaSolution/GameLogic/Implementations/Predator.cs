
namespace GameLogic.Implementations
{
    public class Predator : Animal
    {
        public Predator(int speed, int visionRange, double health) : base(speed, visionRange, health) { }

        /// <summary>
        /// Moves the lion towards the closest antelope within its vision range.
        /// Searches through a list of animals to find the nearest antelope,
        /// and if one is found within range, the lion moves in the direction of the antelope.
        /// </summary>
        /// <param name="width"> The width of the game field. </param>
        /// <param name="height"> The height of the game field. </param>
        /// <param name="animals"> The list of animals on the game field. </param>
        public override void Move(int width, int height, List<Animal> animals)
        {
            if (!IsAlive) return;

            Animal closestPrey = null;
            double closestDistance = double.MaxValue;

            foreach (var animal in animals)
            {
                if (animal is Prey && animal.IsAlive)
                {
                    double distance = this.GetDistance(animal); // Using the extension method
                    if (distance < closestDistance && distance <= VisionRange)
                    {
                        closestDistance = distance;
                        closestPrey = animal;
                    }
                }
            }

            if (closestPrey != null)
            {
                if (closestPrey.X > X) X += Speed;
                else if (closestPrey.X < X) X -= Speed;
                if (closestPrey.Y > Y) Y += Speed;
                else if (closestPrey.Y < Y) Y -= Speed;

                if (X == closestPrey.X && Y == closestPrey.Y)
                {
                    closestPrey.Kill();
                    Health += TextConstants.HealthBoostForPredator;
                }
            }
            else
            {
                MoveRandomly(width, height);
            }

            X = Math.Clamp(X, 0, width - 1);
            Y = Math.Clamp(Y, 0, height - 1);

            Health -= TextConstants.HealthDamage;

            if(Health <= 0)
            {
                Kill();
            }
        }
    }
}
