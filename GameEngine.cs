using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GADE6122_POE_Part_1
{
    internal class GameEngine
    {
        public enum GameState
        {
            InProgress,
            Complete,
            GameOver
        }
        // Fields
        private Level currentLevel;
        private Random random;
        private int numberOfLevels;
        public int numEnemies;
        
        private GameState _gameState = GameState.InProgress;
        private int _currentLevelNumber = 1;
        private int v;
        private const int MIN_SIZE = 10;
        private const int MAX_SIZE = 20;

        // Constructor
        public GameEngine(int numberOfLevels, int numEnemies)
        {
            this.numberOfLevels = numberOfLevels;
            this.random = new Random();
            _currentLevelNumber = 1;
            int width = random.Next(MIN_SIZE, MAX_SIZE + 1);
            int height = random.Next(MIN_SIZE, MAX_SIZE + 1);
            this.currentLevel = new Level( width, height, numEnemies);
            _gameState = GameState.InProgress;
        }
        public GameEngine(int v)
        {
            this.v = v;
        }

        public void StartGame()
        {
            _currentLevelNumber = 1;
            currentLevel = new Level(_currentLevelNumber);
            _gameState = GameState.InProgress;
        }
        public void NextLevel()
        {
            _currentLevelNumber++;

            // Store the current hero
            HeroTile oldHero = currentLevel.Hero;

            // Generate new level size
            int width = random.Next(MIN_SIZE, MAX_SIZE + 1);
            int height = random.Next(MIN_SIZE, MAX_SIZE + 1);

            // Create new level and pass the hero
            currentLevel = new Level(width, height, oldHero);
        }
        public override string ToString()
        {
            if (currentLevel == null)
                return "No level has been generated";
            return currentLevel.ToString();
        }
        private bool MoveHero(Direction.DirectionType direction)
        {
            if (currentLevel == null || currentLevel.Hero == null)
            {
                return false;
            }
            HeroTile hero = currentLevel.Hero;
            Tile target = hero.Vision[(int)direction];

            if (target is EmptyTile)
            {
                currentLevel.SwopTiles(hero, target);
                hero.Updatevision(currentLevel);
                return true;
            }
            if (target is ExitTile)
            {
                hero._position= target._position;
                if (_currentLevelNumber < numberOfLevels)
                {
                    NextLevel();
                    _gameState = GameState.InProgress;
                }
                else
                {
                    _gameState = GameState.Complete;
                }
                return true;
            }
            return false;
        }
        public bool TriggerMovement(Direction.DirectionType direction)
        {
            return MoveHero(direction);
        }
        public GameState CurrentGameState { get { return _gameState; } }
    }
}
