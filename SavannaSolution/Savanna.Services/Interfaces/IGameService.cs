using GameLogic.Implementations;

namespace Savanna.Services.Interfaces
{
    public interface IGameService
    {
        Task<List<Animal>> GetInitialAnimals();

        Task AddAnimal(List<Animal> animals, Animal animal);

        Task Update(List<Animal> animals);
    }
}
