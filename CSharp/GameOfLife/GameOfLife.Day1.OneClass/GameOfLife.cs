using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Day1.OneClass
{
    class GameOfLife
    {
        private static readonly int width = 10;
        private static readonly int height = 10;

        static void Main(string[] args)
        {
            if(args.Length == 0)
            {
                new GameOfLife().Run();
                Console.ReadLine();
                return;
            }
            if (args[0].Equals("g"))
            {
                new GameOfLife('g').Run();
                Console.ReadLine();
                return;
            }
            try
            {
                int seed = int.Parse(args[0]);
                new GameOfLife(seed).Run();
                Console.ReadLine();
                return;
            }
            catch (Exception e)
            {

            }
            Console.WriteLine("Wrong params: use none, a number or g");
            Console.ReadLine();
        }

        bool[][] gamefield = new bool[width][];
        private char? glider;
        private int? seed;

        public GameOfLife(char glider)
        {
            this.glider = glider;
        }

        public GameOfLife(int seed)
        {
            this.seed = seed;
        }

        public GameOfLife()
        {

        }

        private void CreateGameField()
        {
            for (int i = 0; i < width; i++)
            {
                gamefield[i] = new bool[height];
            }
            if (glider.HasValue)
            {
                gamefield[0][1] = true;
                gamefield[1][2] = true;
                gamefield[2][0] = true;
                gamefield[2][1] = true;
                gamefield[2][2] = true;
                return;
            }
            Random random = new Random();
            if (seed.HasValue)
            {
                random = new Random(seed.Value);
            }

            int counter = 0;
            while (counter <= 20)
            {
                int x = random.Next(0, width);
                int y = random.Next(0, height);
                if (!gamefield[x][y])
                {
                    gamefield[x][y] = true;
                    counter++;
                }
            }
        }

        private void EvolutionStep()
        {
            bool[][] tempGameField = new bool[width][];

            for (int w = 0; w < width; w++)
            {
                tempGameField[w] = new bool[height];

                for (int h = 0; h < height; h++)
                {
                    int n = LivingNeighbours(w, h);
                    tempGameField[w][h] = IsAlive(n, gamefield[w][h]);
                }
            }

            gamefield = tempGameField;
        }

        private bool IsAlive(int n, bool self)
        {
            if (n == 3)
            {
                return true;
            }
            if (n == 2 && self)
            {
                return true;
            }
            return false;
        }

        private int LivingNeighbours(int w, int h)
        {
            int counter = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int tempX = (w + i) % width;
                    int tempY = (h + j) % height;
                    if (tempX < 0)
                    {
                        tempX = width + tempX;
                    }
                    if (tempY < 0)
                    {
                        tempY = height + tempY;
                    }

                    if (gamefield[tempX][tempY])
                    {
                        counter++;
                    }
                }
            }
            if (gamefield[w][h])
            {
                counter--;
            }
            return counter;
        }

        private void PrintGameField()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            foreach (var line in gamefield)
            {
                foreach (var cell in line)
                {
                    Console.Write(' ');
                    if (cell)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write('x');
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write('o');
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private void Run()
        {
            CreateGameField();
            foreach (var i in Enumerable.Range(0, 10))
            {
                PrintGameField();
                EvolutionStep();
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("----------Finish----------");
        }
    }
}
