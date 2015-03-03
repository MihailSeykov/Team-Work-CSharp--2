﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;
using System.Media;

namespace snakeHellHoundTeam
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
        public static int origRow;
        public static int origCol;
    }
    class AlphaVersion
    {
        protected static int origRow;
        protected static int origCol;

        protected static void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(origCol + x, origRow + y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        static void Main()
        {

            (new SoundPlayer(@"..\..\sound.wav")).Play();

            // Clear the screen, then save the top and left coordinates.
            Console.Clear();
            origRow = Console.CursorTop;
            origCol = Console.CursorLeft;

            Console.ForegroundColor = System.ConsoleColor.Red;

            // Draw the left side of a 15x60 rectangle, from top to bottom.

            for (int i = 2; i < 17; i++)
            {
                WriteAt("|", 10, i);
            }

            // Draw the bottom side, from left to right.

            for (int i = 10; i < 70; i++)
            {
                WriteAt("-", i, 17);
            }

            // Draw the right side, from bottom to top.

            for (int i = 2; i < 17; i++)
            {
                WriteAt("|", 69, i);
            }

            // Draw the top side, from right to left.
            for (int i = 10; i < 70; i++)
            {
                WriteAt("-", i, 1);
            }

            Console.ForegroundColor = System.ConsoleColor.Yellow;

            WriteAt(@" # # #  #       #      #      #     #  # # # # ", 17, 7);
            Console.ForegroundColor = System.ConsoleColor.Yellow;
            WriteAt(@"#       # #     #     # #     #   #    #", 17, 8);
            Console.ForegroundColor = System.ConsoleColor.Yellow;
            WriteAt(@"   #    #   #   #    #   #    # #      # # #", 17, 9);
            Console.ForegroundColor = System.ConsoleColor.Green;
            WriteAt(@"     #  #     # #   # # # #   #   #    #", 17, 10);
            Console.ForegroundColor = System.ConsoleColor.Red;
            WriteAt(@"# # #   #       #  #       #  #     #  # # # #", 17, 11);

            Console.WriteLine();
            Console.ForegroundColor = System.ConsoleColor.White;

            Console.ForegroundColor = System.ConsoleColor.Red;
            WriteAt(@"#   #  ####  #    #      #   #  ####  #  #  #   #  ###", 13, 19);
            WriteAt(@"#####  ###   #    #      #####  #  #  #  #  # # #  #  #", 13, 20);
            WriteAt(@"#   #  ####  ###  ###    #   #  ####  ####  #   #  ###", 13, 21);

            WriteAt("Studio", 62, 23);

            Console.ForegroundColor = System.ConsoleColor.White;
            Console.WriteLine();
            Console.Write("Press ENTER to continiune");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Choose your game mode:");
            Console.WriteLine();
            Console.WriteLine("1: Single player");
            Console.WriteLine("2: Multiplayer");
            int option = 2;
            try
            {
                option = int.Parse(Console.ReadLine());
            }
            catch (System.FormatException)
            {
                Console.WriteLine("Invalid input");
                Console.ReadLine();
            }
            if (option == 2)
            {
                Multiplayer();
            }
            else if (option == 1)
            {
                Singlelayer();
            }



        }
        public static void Singlelayer()
        {
            Console.Clear();
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
                new Position(3, 10), new Position(3, 11), new Position(3, 12), new Position(3, 13), new Position(3, 14), new Position(4, 10), new Position(5, 10), 
                new Position(6, 10), new Position(7, 10), new Position(8, 10), new Position(4, 11), new Position(5, 11), new Position(6, 11), new Position(7, 11),
                new Position(8, 11), new Position(4, 12), new Position(5, 12), new Position(6, 12), new Position(7, 12), new Position(8, 12), new Position(4, 13),
                new Position(5, 13), new Position(6, 13), new Position(7, 13), new Position(8, 13), new Position(4, 14), new Position(5, 14), new Position(6, 14),
                new Position(7, 14), new Position(8, 14), new Position(3, 63), new Position(3, 64), new Position(3, 65), new Position(3, 66), new Position(3, 67),
                new Position(4, 63), new Position(5, 63), new Position(6, 63), new Position(7, 63), new Position(8, 63), new Position(4, 64), new Position(5, 64),
                new Position(6, 64), new Position(7, 64), new Position(8, 64), new Position(4, 65), new Position(5, 65), new Position(6, 65), new Position(7, 65),
                new Position(8, 65), new Position(4, 66), new Position(5, 66), new Position(6, 66), new Position(7, 66), new Position(8, 66), new Position(4, 67),
                new Position(5, 67), new Position(6, 67), new Position(7, 67), new Position(8, 67), new Position(16, 10), new Position(16, 11), new Position(16, 12),
                new Position(16, 13), new Position(16, 14), new Position(17, 10), new Position(18, 10), new Position(19, 10), new Position(20, 10), new Position(21, 10),
                new Position(17, 11), new Position(18, 11), new Position(19, 11), new Position(20, 11), new Position(21, 11), new Position(17, 12), new Position(18, 12),
                new Position(19, 12), new Position(20, 12), new Position(21, 12), new Position(17, 13), new Position(18, 13), new Position(19, 13), new Position(20, 13),
                new Position(21, 13), new Position(17, 14), new Position(18, 14), new Position(19, 14), new Position(20, 14), new Position(21, 14), new Position(16, 63),
                new Position(16, 64), new Position(16, 65), new Position(16, 66), new Position(16, 67), new Position(17, 63), new Position(18, 63), new Position(19, 63),
                new Position(20, 63), new Position(21, 63),
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
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("$");

            foreach (Position position in snakeElements)
            {
                Console.SetCursorPosition(position.col, position.row);
                Console.ForegroundColor = ConsoleColor.Cyan;
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


                    Console.Clear();
                    int userPoints = (snakeElements.Count - 6) * 100 - negativePoints;
                    if (userPoints < 0) userPoints = 0;
                    userPoints = Math.Max(userPoints, 0);
                    Console.WriteLine("Top 10 all time results:");
                    Console.WriteLine();
                    int count = 1;
                    bool isAdded = false;
                    try
                    {
                        string[] lines = System.IO.File.ReadAllLines(@"..\..\Snake.txt");
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
                        System.IO.File.WriteAllLines(@"..\..\Snake.txt", write);
                    }
                    catch (Exception)
                    {

                        Console.WriteLine("File is missing");
                    }

                    char[,] logo = new char[100, 5];
                    int counter = 1;

                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("These are all the $ you collected");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                    if (counter != 0)
                    {
                        for (int row = 0; row < 100; row++)
                        {
                            if (counter == snakeElements.Count - 5)
                            {
                                break;
                            }
                            for (int col = 0; col < 5; col++)
                            {
                                if (counter == snakeElements.Count - 5)
                                {
                                    break;
                                }
                                logo[row, col] = '$';
                                counter++;
                                Console.Write(logo[row, col]);

                            }

                        }
                    }
                    else
                    {
                        Console.WriteLine();
                    }

                    Console.WriteLine();

                    return;

                }

                Console.SetCursorPosition(snakeHead.col, snakeHead.row);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("*");

                snakeElements.Enqueue(snakeNewHead);
                Console.SetCursorPosition(snakeNewHead.col, snakeNewHead.row);
                Console.ForegroundColor = ConsoleColor.Cyan;
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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("$");
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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("$");

                sleepTime -= 0.01;

                Thread.Sleep((int)sleepTime);
            }
        }
        public static void Multiplayer()
        {
            byte right = 0;
            byte left = 1;
            byte down = 2;
            byte up = 3;
            int counterOne = 0;
            int counterTwo = 0;
            int snakeOneMistake = 0;
            int snakeTwoMistake = 0;




            double sleepTime = 100;
            int direction = right;
            int directionTwo = right;

            Random randomNumbersGenerator = new Random();

            Console.BufferHeight = Console.WindowHeight;
            Position food = new Position(randomNumbersGenerator.Next(5, Console.WindowHeight - 2), randomNumbersGenerator.Next(5, Console.WindowWidth - 2));


            Queue<Position> snake = GenerateSnake(1);
            Queue<Position> snakeTwo = GenerateSnake(20);


            Position[] outOfMethodDirections = Directions();

            while (true)
            {



                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo userInput = Console.ReadKey();
                    direction = nextDirection(ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.UpArrow, ConsoleKey.DownArrow, direction, userInput);
                    directionTwo = nextDirection(ConsoleKey.A, ConsoleKey.D, ConsoleKey.W, ConsoleKey.S, directionTwo, userInput);

                }


                Position snakeNewHead = SnakeNewHead(snake, Directions(), direction);
                Position newHeadSnaketwo = SnakeNewHead(snakeTwo, Directions(), directionTwo);
                if (snake.Contains(snakeNewHead) || snake.Contains(newHeadSnaketwo) || snakeTwo.Contains(snakeNewHead) || snakeTwo.Contains(newHeadSnaketwo))
                {
                    if (snake.Contains(newHeadSnaketwo) && snakeTwo.Contains(snakeNewHead))
                    {
                        snakeOneMistake += 2;
                        snakeTwoMistake += 2;
                    }
                    else if (snake.Contains(snakeNewHead) || snakeTwo.Contains(snakeNewHead))
                    {
                        snakeOneMistake++;
                    }
                    else if (snake.Contains(newHeadSnaketwo) || snakeTwo.Contains(newHeadSnaketwo))
                    {
                        snakeTwoMistake++;
                    }
                    break;
                }
                snakeNewHead = goingThroughWalls(snakeNewHead);
                newHeadSnaketwo = goingThroughWalls(newHeadSnaketwo);
                if ((snakeNewHead.col == food.col && snakeNewHead.row == food.row) || (newHeadSnaketwo.col == food.col && newHeadSnaketwo.row == food.row))
                {


                    if (snakeNewHead.col == food.col && snakeNewHead.row == food.row)
                    {
                        counterOne += 100;
                        snakeTwo.Dequeue();

                    }
                    else if (newHeadSnaketwo.col == food.col && newHeadSnaketwo.row == food.row)
                    {
                        counterTwo += 100;
                        snake.Dequeue();
                    }
                    food = new Position(randomNumbersGenerator.Next(0, Console.WindowHeight), randomNumbersGenerator.Next(0, Console.WindowWidth));

                }
                else
                {
                    snake.Dequeue();
                    snakeTwo.Dequeue();
                }


                snake.Enqueue(snakeNewHead);
                snakeTwo.Enqueue(newHeadSnaketwo);
                Console.Clear();




                PrintSnake(snake);
                PrintSnake(snakeTwo, 1);

                Console.ForegroundColor = ConsoleColor.Cyan;
                PrintHead(snakeNewHead.col, snakeNewHead.row, direction);
                Console.ForegroundColor = ConsoleColor.Green;
                PrintHead(newHeadSnaketwo.col, newHeadSnaketwo.row, directionTwo);

                Console.SetCursorPosition(food.col, food.row);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("$");
                Thread.Sleep((int)sleepTime);
            }
            Console.Clear();

            switch (snakeOneMistake)
            {
                case 0: Console.WriteLine("Game over!\nSnake Two crashed\nSnake One score:{0}\nSnake Two score:{1}\nTotal score:{2}", counterOne, counterTwo, counterOne + counterTwo); Console.ForegroundColor = ConsoleColor.White; break;
                case 1: Console.WriteLine("Game over!\nSnake One crashed\nSnake One score:{0}\nSnake Two score:{1}\nTotal score:{2}", counterOne, counterTwo, counterOne + counterTwo); Console.ForegroundColor = ConsoleColor.White; break;
                case 2: Console.WriteLine("Game over!\nBoth snakes colided\nSnake One score:{0}\nSnake Two score:{1}\nTotal score:{2}", counterOne, counterTwo, counterOne + counterTwo); Console.ForegroundColor = ConsoleColor.White; break;

            }
        }

        static Queue<Position> GenerateSnake(int snakeRowStar)
        {
            //Snake lenght is set to 6. It can be changed with the for cycle
            Queue<Position> snakeElement = new Queue<Position>();
            for (int i = 0; i < 6; i++)
            {
                snakeElement.Enqueue(new Position(snakeRowStar, i));
            }
            return snakeElement;
        }
        static Position[] Directions()
        {
            Position[] direction = new Position[]
            { 
                new Position(0, 1), // right
                new Position(0, -1), // left
                new Position(1, 0), // down
                new Position(-1, 0), // up
            };
            return direction;
        }
        static Position SnakeNewHead(Queue<Position> snakeElement, Position[] directions, int direction) //Snake head
        {
            Position snakeHead = snakeElement.Last();

            Position nextDirection = directions[direction];
            ;
            Position snakeNewHead = new Position(snakeHead.row + nextDirection.row, snakeHead.col + nextDirection.col);




            return snakeNewHead;

        }
        static void PrintSnake(Queue<Position> snakeElement, int body = 0) //Prints the snake
        {
            foreach (Position position in snakeElement)
            {
                if (body == 0)
                {
                    Console.SetCursorPosition(position.col, position.row);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("*");
                }
                else if (body == 1)
                {
                    Console.SetCursorPosition(position.col, position.row);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("o");
                }

            }
        }
        static Position goingThroughWalls(Position snakeNewHead)  //Makes the snakes able to go through walls
        {
            if (snakeNewHead.col < 0) snakeNewHead.col = Console.WindowWidth - 1;
            if (snakeNewHead.row < 0) snakeNewHead.row = Console.WindowHeight - 1;
            if (snakeNewHead.row >= Console.WindowHeight) snakeNewHead.row = 0;
            if (snakeNewHead.col >= Console.WindowWidth) snakeNewHead.col = 0;

            return snakeNewHead;
        }

        static int nextDirection(ConsoleKey Left, ConsoleKey Right, ConsoleKey Up, ConsoleKey Down, int currentDirection, ConsoleKeyInfo userInputKey)
        {
            int direction = currentDirection;
            byte right = 0;
            byte left = 1;
            byte down = 2;
            byte up = 3;
            ConsoleKeyInfo userInput = userInputKey;
            if (userInput.Key == Left)
            {
                if (direction != right) direction = left;
            }
            if (userInput.Key == Right)
            {
                if (direction != left) direction = right;
            }
            if (userInput.Key == Up)
            {
                if (direction != down) direction = up;
            }
            if (userInput.Key == Down)
            {
                if (direction != up) direction = down;
            }

            return direction;


        }
        static void PrintHead(int col, int row, int direction)
        {
            Console.SetCursorPosition(col, row);
            if (direction == 0)
            {
                //Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(">");
            }
            else if (direction == 1)
            {
                //Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("<");
            }
            else if (direction == 2)
            {
                //Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("v");
            }
            else if (direction == 3)
            {
                //Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("^");
            }
        }


    }
}
