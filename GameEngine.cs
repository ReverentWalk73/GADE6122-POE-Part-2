using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE6122_POE_Part_1
{
    internal class GameEngine
    {
        // Fields
        private Level currentLevel;
        private Random random;
        private int numberOfLevels;

        private const int MIN_SIZE = 10;
        private const int MAX_SIZE = 20;

        // Constructor
        public GameEngine(int numberOfLevels)
        {
            this.numberOfLevels = numberOfLevels;
            this.random = new Random();

            int width = random.Next(MIN_SIZE, MAX_SIZE + 1);
            int height = random.Next(MIN_SIZE, MAX_SIZE + 1);
            this.currentLevel = new Level(width, height);
        }

        public override string ToString()
        {
            return currentLevel.ToString();
        }
    }
}
