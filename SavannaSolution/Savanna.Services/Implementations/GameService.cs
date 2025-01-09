using GameLogic.Implementations;
using Savanna.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savanna.Services.Implementations
{
    public class GameService : IGameService
    {
        public async Task<List<Animal>> GetInitialAnimals()
        {
            return new List<Animal>();
        }

        public async Task AddAnimal(List<Animal> animals, Animal animal)
        {
            throw new NotImplementedException();
        }

        public async Task Update(List<Animal> animals)
        {
            throw new NotImplementedException();
        }
    }
}
