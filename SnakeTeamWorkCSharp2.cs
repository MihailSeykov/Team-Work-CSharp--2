using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;

namespace SnakeTeamWorkCSharp2
{
    struct Position
    {
        public int row;
        public int col;
        public Position(int row, int col)
        {
            this.row = row;
            this.col = col;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            byte right = 0;
            byte left = 1;
            byte down = 2;
            byte up = 3;
            int lastFoodTime = 0;
            int foodDissapearTime = 8000;
            int negativePoints = 0;
            Position[] directions = new Position[]
            {

                new Position(0, 1), // right
                new Position(0, -1), // left
                new Position(1, 0), // down
                new Position(-1, 0), // up
            };
            double sleepTime = 100;
            int direction = right;
            Random randomNumbersGenerator = new Random();
            Console.BufferHeight = Console.WindowHeight;
            lastFoodTime = Environment.TickCount;

            List<Position> obstacles = new List<Position>()
            {
                new Position(3, 10),
                new Position(3, 11),
                new Position(3, 12),
                new Position(3, 13),
                new Position(3, 14),
                new Position(4, 10),
                new Position(5, 10),
                new Position(6, 10),
                new Position(7, 10),
                new Position(8, 10),
                new Position(4, 11),
                new Position(5, 11),
                new Position(6, 11),
                new Position(7, 11),
                new Position(8, 11),
                new Position(4, 12),
                new Position(5, 12),
                new Position(6, 12),
                new Position(7, 12),
                new Position(8, 12),
                new Position(4, 13),
                new Position(5, 13),
                new Position(6, 13),
                new Position(7, 13),
                new Position(8, 13),
                new Position(4, 14),
                new Position(5, 14),
                new Position(6, 14),
                new Position(7, 14),
                new Position(8, 14),        
                new Position(3, 63),
                new Position(3, 64),
                new Position(3, 65),
                new Position(3, 66),
                new Position(3, 67),
                new Position(4, 63),
                new Position(5, 63),
                new Position(6, 63),
                new Position(7, 63),
                new Position(8, 63),
                new Position(4, 64),
                new Position(5, 64),
                new Position(6, 64),
                new Position(7, 64),
                new Position(8, 64),
                new Position(4, 65),
                new Position(5, 65),
                new Position(6, 65),
                new Position(7, 65),
                new Position(8, 65),
                new Position(4, 66),
                new Position(5, 66),
                new Position(6, 66),
                new Position(7, 66),
                new Position(8, 66),
                new Position(4, 67),
                new Position(5, 67),
                new Position(6, 67),
                new Position(7, 67),
                new Position(8, 67),
                new Position(16, 10),
                new Position(16, 11),
                new Position(16, 12),
                new Position(16, 13),
                new Position(16, 14),
                new Position(17, 10),
                new Position(18, 10),
                new Position(19, 10),
                new Position(20, 10),
                new Position(21, 10),
                new Position(17, 11),
                new Position(18, 11),
                new Position(19, 11),
                new Position(20, 11),
                new Position(21, 11),
                new Position(17, 12),
                new Position(18, 12),
                new Position(19, 12),
                new Position(20, 12),
                new Position(21, 12),
                new Position(17, 13),
                new Position(18, 13),
                new Position(19, 13),
                new Position(20, 13),
                new Position(21, 13),
                new Position(17, 14),
                new Position(18, 14),
                new Position(19, 14),
                new Position(20, 14),
                new Position(21, 14), 
                new Position(16, 63),
                new Position(16, 64),
                new Position(16, 65),
                new Position(16, 66),
                new Position(16, 67),
                new Position(17, 63),
                new Position(18, 63),
                new Position(19, 63),
                new Position(20, 63),
                new Position(21, 63),
                new Position(17, 64),
                new Position(18, 64),
                new Position(19, 64),
                new Position(20, 64),
                new Position(21, 64),
                new Position(17, 65),
                new Position(18, 65),
                new Position(19, 65),
                new Position(20, 65),
                new Position(21, 65),
                new Position(17, 66),
                new Position(18, 66),
                new Position(19, 66),
                new Position(20, 66),
                new Position(21, 66),
                new Position(17, 67),
                new Position(18, 67),
                new Position(19, 67),
                new Position(20, 67),
                new Position(21, 67),
                new Position(10, 37),
                new Position(10, 38),
                new Position(10, 39),
                new Position(10, 40),
                new Position(10, 41),
                new Position(11, 37),
                new Position(12, 37),
                new Position(13, 37),
                new Position(14, 37),
                new Position(15, 37),
                new Position(11, 38),
                new Position(12, 38),
                new Position(13, 38),
                new Position(14, 38),
                new Position(15, 38),
                new Position(11, 39),
                new Position(12, 39),
                new Position(13, 39),
                new Position(14, 39),
                new Position(15, 39),
                new Position(11, 40),
                new Position(12, 40),
                new Position(13, 40),
                new Position(14, 40),
                new Position(15, 40),
                new Position(11, 41),
                new Position(12, 41),
                new Position(13, 41),
                new Position(14, 41),
                new Position(15, 41),
            };
            foreach (Position obstacle in obstacles)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(obstacle.col, obstacle.row);
                Console.Write("=");
            }

            Queue<Position> snakeElements = new Queue<Position>();
            for (int i = 0; i <= 5; i++)
            {
                snakeElements.Enqueue(new Position(0, i));
            }

            Position food;
            do
            {
                food = new Position(randomNumbersGenerator.Next(0, Console.WindowHeight),
                    randomNumbersGenerator.Next(0, Console.WindowWidth));
            }
            while (snakeElements.Contains(food) || obstacles.Contains(food));
            Console.SetCursorPosition(food.col, food.row);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("@");

            foreach (Position position in snakeElements)
            {
                Console.SetCursorPosition(position.col, position.row);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("*");
            }

            while (true)
            {
                negativePoints++;

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo userInput = Console.ReadKey();
                    if (userInput.Key == ConsoleKey.LeftArrow)
                    {
                        if (direction != right) direction = left;
                    }
                    if (userInput.Key == ConsoleKey.RightArrow)
                    {
                        if (direction != left) direction = right;
                    }
                    if (userInput.Key == ConsoleKey.UpArrow)
                    {
                        if (direction != down) direction = up;
                    }
                    if (userInput.Key == ConsoleKey.DownArrow)
                    {
                        if (direction != up) direction = down;
                    }
                }

                Position snakeHead = snakeElements.Last();
                Position nextDirection = directions[direction];

                Position snakeNewHead = new Position(snakeHead.row + nextDirection.row,
                    snakeHead.col + nextDirection.col);

                if (snakeNewHead.col < 0) snakeNewHead.col = Console.WindowWidth - 1;
                if (snakeNewHead.row < 0) snakeNewHead.row = Console.WindowHeight - 1;
                if (snakeNewHead.row >= Console.WindowHeight) snakeNewHead.row = 0;
                if (snakeNewHead.col >= Console.WindowWidth) snakeNewHead.col = 0;

                if (snakeElements.Contains(snakeNewHead) || obstacles.Contains(snakeNewHead))
                {
                    //Console.SetCursorPosition(0, 0);
                    //Console.ForegroundColor = ConsoleColor.Red;
                    //Console.WriteLine("Game over!");
                    //Console.WriteLine("Your points are: {0}", userPoints);

                    Console.Clear();
                    int userPoints = (snakeElements.Count - 6) * 100 - negativePoints;
                    if (userPoints < 0) userPoints = 0;
                    userPoints = Math.Max(userPoints, 0);
                    Console.WriteLine("Top 10 all time results");
                    Console.WriteLine();
                    int count = 1;
                    bool isAdded = false;
                    string[] lines = System.IO.File.ReadAllLines(@"F:\Snake.txt");
                    var result = new List<int>();
                    for (int i = 0; i < lines.Length; i++)
                    {
                        result.Add(int.Parse(lines[i]));
                    }
                    for (int i = 0; i < result.Count; i++)
                    {
                        if (userPoints > result[i])
                        {
                            result.Add(userPoints);
                            isAdded = true;
                            break;
                        }
                    }
                    if (isAdded)
                    {
                        result.Sort();
                        result.Reverse();
                        result.RemoveAt(result.Count - 1);
                    }
                    var write = new List<string>();
                    foreach (var item in result)
                    {
                        write.Add(item.ToString());
                    }
                    foreach (var item in result)
                    {
                        Console.WriteLine("{0} - {1}", count, item);
                        count++;
                    }


                    Console.WriteLine();

                    Console.WriteLine("Your points are: {0}", userPoints);
                    System.IO.File.WriteAllLines(@"F:\Snake.txt", write);
                    return;

                }

                Console.SetCursorPosition(snakeHead.col, snakeHead.row);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("*");

                snakeElements.Enqueue(snakeNewHead);
                Console.SetCursorPosition(snakeNewHead.col, snakeNewHead.row);
                Console.ForegroundColor = ConsoleColor.Gray;
                if (direction == right) Console.Write(">");
                if (direction == left) Console.Write("<");
                if (direction == up) Console.Write("^");
                if (direction == down) Console.Write("v");


                if (snakeNewHead.col == food.col && snakeNewHead.row == food.row)
                {
                    // feeding the snake
                    do
                    {
                        food = new Position(randomNumbersGenerator.Next(0, Console.WindowHeight),
                            randomNumbersGenerator.Next(0, Console.WindowWidth));
                    }
                    while (snakeElements.Contains(food) || obstacles.Contains(food));
                    lastFoodTime = Environment.TickCount;
                    Console.SetCursorPosition(food.col, food.row);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("@");
                    sleepTime--;               
                }
                else
                {
                    // moving...
                    Position last = snakeElements.Dequeue();
                    Console.SetCursorPosition(last.col, last.row);
                    Console.Write(" ");
                }

                if (Environment.TickCount - lastFoodTime >= foodDissapearTime)
                {
                    negativePoints = negativePoints + 50;
                    Console.SetCursorPosition(food.col, food.row);
                    Console.Write(" ");
                    do
                    {
                        food = new Position(randomNumbersGenerator.Next(0, Console.WindowHeight),
                            randomNumbersGenerator.Next(0, Console.WindowWidth));
                    }
                    while (snakeElements.Contains(food) || obstacles.Contains(food));
                    lastFoodTime = Environment.TickCount;
                }

                Console.SetCursorPosition(food.col, food.row);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("@");

                sleepTime -= 0.01;

                Thread.Sleep((int)sleepTime);
            }
        }
    }
}