using GameLogic.Interfaces;
using Newtonsoft.Json;

namespace GameLogic.Implementations
{
    public class GameField
    {
        private List<Animal> _animals = new List<Animal>();
        private readonly int _width;
        private readonly int _height;
        public int Width => _width;
        public int Height => _height;
        public List<Animal> Animals => _animals;
        private readonly IGameDisplayer _displayer;

        public GameField(int width, int height, IGameDisplayer displayer)
        {
            _width = width;
            _height = height;
            _displayer = displayer;
        }

        public GameField(int width, int height, IGameDisplayer displayer, List<Animal> animals)
        {
            _width = width;
            _height = height;
            _displayer = displayer;
            _animals = animals;
        }

        /// <summary>
        /// Adds a new animal to the game field at a random position within the field boundaries.
        /// </summary>
        /// <param name="animal"> The animal to be added to the game field. </param>
        public void AddAnimal(Animal animal)
        {
            Random random = new Random();

            animal.X = random.Next(0, _width);
            animal.Y = random.Next(0, _height);

            _animals.Add(animal);
        }

        /// <summary>
        /// Updates the state of the game field by moving each animal according to its behavior and then displaying the updated field.
        /// </summary>
        public void Update()
        {
            foreach (var animal in _animals)
            {
                animal.Move(_width, _height, _animals);
            }
            CheckForReproduction();
            _animals = _animals.Where(animal => animal.IsAlive).ToList();
            _displayer.Display(this);
        }

        /// <summary>
        /// Checks for pairs of animals that can reproduce and adds new animals to the game field if reproduction conditions are met.
        /// </summary>
        public void CheckForReproduction()
        {
            List<Animal> newAnimals = new List<Animal>();

            for (int i = 0; i < _animals.Count; i++)
            {
                for (int j = i + 1; j < _animals.Count; j++)
                {
                    var parentA = _animals[i];
                    var parentB = _animals[j];

                    if (parentA.IsAlive && parentB.IsAlive && parentA.CanReproduce(parentB))
                    {
                        Animal newAnimal = CreateAnimal(parentA.GetType());

                        if (newAnimal != null)
                        {
                            newAnimals.Add(newAnimal);
                        }
                    }
                }
            }

            foreach (var newAnimal in newAnimals)
            {
                AddAnimal(newAnimal);
            }
        }

        /// <summary>
        /// Creates a new instance of an animal based on the specified type.
        /// </summary>
        /// <param name="animalType"> The type of the animal to create. </param>
        /// <returns> A new animal instance of the specified type, or null if the type is not supported. </returns>
        private Animal CreateAnimal(Type animalType)
        {
            var config = _animals.FirstOrDefault(a => a.GetType() == animalType)?.GetAnimalConfig();
            if (config != null)
            {
                return GameUtils.CreateAnimalFromConfig(config);
            }

            return null;
        }
    }
}
