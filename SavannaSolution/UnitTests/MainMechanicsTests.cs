using GameLogic.Implementations;

namespace UnitTests
{
    [TestClass]
    public class MainMechanics
    {
        #region MovementMechanicsTests
        [TestMethod]
        public void Antelope_RandomMovement()
        {
            // Arrange
            var antelope = new Prey(speed: 1, visionRange: 5, health: 100)
            {
                X = 3,
                Y = 3
            };

            var listOfAnimals = new List<Animal>();

            // Act
            antelope.Move(5, 5, listOfAnimals);

            // Assert 
            var resultX = antelope.X == 3;
            var resultY = antelope.Y == 3;

            Assert.IsFalse(resultX && resultY);
        }

        [TestMethod]
        public void Lion_RandomMovement()
        {
            // Arrange
            var lion = new Predator(speed: 1, visionRange: 5, health: 100)
            {
                X = 3,
                Y = 3
            };

            var listOfAnimals = new List<Animal>();

            // Act
            lion.Move(5, 5, listOfAnimals);

            // Assert 
            var resultX = lion.X == 3;
            var resultY = lion.Y == 3;

            Assert.IsFalse(resultX && resultY);
        }

        [TestMethod]
        public void Lion_Change_Position_When_Hunting()
        {
            // Arrange
            var lion = new Predator(speed: 5, visionRange: 5, health: 100)
            {
                X = 2,
                Y = 1
            };

            var antelope = new Prey(speed: 0, visionRange: 5, health: 100)
            {
                X = 2,
                Y = 2
            };

            var listOfAnimals = new List<Animal>();
            listOfAnimals.Add(antelope);

            // Act
            lion.Move(3,3, listOfAnimals);

            // Assert
            Assert.AreEqual(2, lion.X);
            Assert.AreEqual(2, lion.Y);
        }

        [TestMethod]
        public void Antelope_Change_Position_When_Escaping()
        {
            // Arrange
            var antelope = new Prey(speed: 1, visionRange: 5, health: 100)
            {
                X = 3,
                Y = 3
            };

            var lion = new Predator(speed: 0, visionRange: 5, health: 100)
            {
                X = 2,
                Y = 2
            };

            var listOfAnimals = new List<Animal>();
            listOfAnimals.Add(lion);

            // Act
            antelope.Move(5, 5, listOfAnimals);

            // Assert 
            var resultX = antelope.X == 3;
            var resultY = antelope.Y == 3;

            Assert.IsFalse(resultX && resultY);
        }

        #endregion MovementMechanicsTests

        #region HealthMechanicsTests
        [TestMethod]
        public void Health_Deacreases_On_Move()
        {
            //Arrange
            var antelope = new Prey(speed: 1, visionRange: 5, health: 10);
            var animals = new List<Animal>();

            //Act
            antelope.Move(20, 20, animals);

            //Assert
            Assert.AreEqual(9.5, antelope.Health);
        }

        [TestMethod]
        public void Animal_Dies_When_Health_Reaches_Zero()
        {
            //Arrange
            var antelope = new Prey(speed: 1, visionRange: 5, health: 0.5);

            //Act
            antelope.Move(20, 20, new List<Animal>());

            //Assert
            Assert.IsFalse(antelope.IsAlive);
        }

        [TestMethod]
        public void Animal_Disappears_When_Dead()
        {
            //Arrange
            var displayer = new MockDisplayer();
            var gameField = new GameField(width: 2, height: 2, displayer);

            var lion = new Predator(speed: 1, visionRange: 5, health: 0.5);
            gameField.AddAnimal(lion);
            gameField.Update();

            //Act
            lion.Health -= 0.5;
            gameField.Update();

            //Assert
            Assert.IsFalse(gameField.Animals.Contains(lion));
        }
        #endregion HealthMechanicsTests

        #region BirthMechanicsTest
        [TestMethod]
        public void Antelope_Birth_Funnction_Should_Add_New_Antelope()
        {
            //Arrange
            var antelope1 = new Prey(speed: 0, visionRange: 5, health: 100)
            {
                X = 1,
                Y = 2,
            };

            var antelope2 = new Prey(speed: 0, visionRange: 5, health: 100)
            {
                X = 1,
                Y = 1,
            };

            var gameField = new GameField(width: 2, height: 2, new GameDisplayer());
            gameField.AddAnimal(antelope1);
            gameField.AddAnimal(antelope2);

            //Act
            antelope1.CanReproduce(antelope2);
            antelope1.CanReproduce(antelope2);
            gameField.CheckForReproduction();

            //Assert
            Assert.AreEqual(3, gameField.Animals.Count);
            Assert.IsInstanceOfType(gameField.Animals[2], typeof(Prey));
        }

        [TestMethod]
        public void Lione_Birth_Funnction_Should_Add_New_Lion()
        {
            //Arrange
            var lion1 = new Predator(speed: 0, visionRange: 5, health: 100)
            {
                X = 1,
                Y = 2,
            };

            var lion2 = new Predator(speed: 0, visionRange: 5, health: 100)
            {
                X = 1,
                Y = 1,
            };

            var gameField = new GameField(width: 2, height: 2, new GameDisplayer());
            gameField.AddAnimal(lion1);
            gameField.AddAnimal(lion2);

            //Act
            lion1.CanReproduce(lion2);
            lion1.CanReproduce(lion2);
            gameField.CheckForReproduction();

            //Assert
            Assert.AreEqual(3, gameField.Animals.Count);
            Assert.IsInstanceOfType(gameField.Animals[2], typeof(Predator));
        }

        #endregion BirthMechanicsTest
    }
}