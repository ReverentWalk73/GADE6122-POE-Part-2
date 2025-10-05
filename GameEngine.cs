using GADE6122_POE_Part_1.GADE6122_POE_Part_1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GADE6122_POE_Part_1.GameEngine;

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
        private readonly Random random = new Random();
        private int numberOfLevels;
        public int numEnemies;
        private int heroMoveCount = 0;
        private GameState _gameState = GameState.InProgress;
        private int _currentLevelNumber = 1;
        private int v;
        private Tile tile;

        // constants
        private const int MIN_SIZE = 10;
        private const int MAX_SIZE = 20;

        // Properties
        public string HeroStats
        {
            get
            {
                return $"{currentLevel.Hero.HitPoints  }/{currentLevel.Hero.MaxHitPoints}";
            }
        }
        public GameState CurrentGameState => _gameState;

        // Constructor
        public GameEngine(int numberOfLevels, int numEnemies)
        {
            this.numberOfLevels = numberOfLevels;
            this.numEnemies = numEnemies;
           
            StartGame();
        }
      
        // Starting at level 1
        public void StartGame()
        {
            _currentLevelNumber = 1;
            int width = random.Next(MIN_SIZE, MAX_SIZE + 1);
            int height = random.Next(MIN_SIZE, MAX_SIZE + 1);
            currentLevel = new Level(width, height, numEnemies);
            _gameState = GameState.InProgress;
        }

        public void NextLevel()
        {
            _currentLevelNumber++;

            HeroTile oldHero = currentLevel.Hero;

            List<EnemyTile> survivors = new List<EnemyTile>();
            foreach (var e in currentLevel.Enemies)
            {
                if (!e.IsDead)
                    survivors.Add(e);
            }

            int width = random.Next(MIN_SIZE, MAX_SIZE + 1);
            int height = random.Next(MIN_SIZE, MAX_SIZE + 1);

            currentLevel = new Level(width, height, survivors.ToArray(), oldHero);
            currentLevel.UpdateVision();
        }

        private bool MoveHero(Direction.DirectionType direction, PickupTile pickup)
        {
            if (currentLevel?.Hero == null) return false;

            HeroTile hero = currentLevel.Hero;
            Tile target = hero.Vision[(int)direction];

            if (target is PickupTile pickups)
            {
                pickups.ApplyEffect(hero);
                currentLevel.SwopTiles(hero, target);
                currentLevel.UpdateVision();
                return true;
            }

            if (target is EmptyTile)
            {
                currentLevel.SwopTiles(hero, target);
                currentLevel.UpdateVision();
                return true;
            }

            if (target is ExitTile)
            {
                hero._position = target._position;
                if (_currentLevelNumber < numberOfLevels)
                {
                    NextLevel();
                    _gameState = GameState.Complete;
                }
                return true;
            }
            return false;
        }

        public bool TriggerMovement(Direction.DirectionType direction)
        {
            bool moved = MoveHero(direction, null); // Pass null if no pickup
            if (moved)
            {
                heroMoveCount++;
                if (heroMoveCount % 2 == 0)
                {
                    MoveEnemies();
                }
            }
            return moved;
        }
        public void TriggerAttack(Direction.DirectionType direction)
        {
            // Call hero attack
            bool success = HeroAttack(direction);

            // If attack successful, enemies attack
            if (success)
            {
                EnemiesAttack();
                if (currentLevel.Hero.IsDead)
                {
                    _gameState = GameState.GameOver;
                }
            }
            currentLevel.UpdateVision();
        }
        private bool HeroAttack(Direction.DirectionType direction)
        {
            Tile targetTile = currentLevel.Hero.Vision[(int)direction];

            if (targetTile is CharacterTiles characterTarget)
            {
                // Hero attacks the enemy
                currentLevel.Hero.Attack(characterTarget);
                return true;   // if attack is successful
            }

            return false;  // No valid target
        }
        private void EnemiesAttack()
        {
            foreach (EnemyTile enemy in currentLevel.Enemies)
            {
                if (!enemy.IsDead)
                {
                    CharacterTiles[] targets = enemy.GetTargets();

                    foreach (CharacterTiles target in targets)
                    {
                        enemy.Attack(target);
                    }
                }
            }
        }
        private void MoveEnemies()
        {
            foreach (var enemy in currentLevel.Enemies)
            {
                if (enemy.IsDead) continue;
                if (enemy.GetMove(out Tile targetTile))
                {
                    // Swaps if target is empty, enabling movement.
                    if (targetTile is EmptyTile)
                        currentLevel.SwopTiles(enemy, targetTile);
                }
            }
        }
     
        public override string ToString()
        {
            if (_gameState == GameState.GameOver)
            {
                return "Game Over!";
            }
            return currentLevel.ToString();
        }
    }

    public enum MoveDirection
    {
        None,
        Up,
        Down,
        Left,
        Right
    }
}