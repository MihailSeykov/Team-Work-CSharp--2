using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;

namespace _09.SnakeMultiBeta
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
    class BetaVersion
    {
        static void Main()
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
            Position food = new Position(randomNumbersGenerator.Next(5 , Console.WindowHeight - 2), randomNumbersGenerator.Next(5 ,Console.WindowWidth - 2));
           

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
                        counterOne+=100;
                        snakeTwo.Dequeue();
                        
                    }
                    else if(newHeadSnaketwo.col == food.col && newHeadSnaketwo.row == food.row)
                    {
                        counterTwo+=100;
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
              
                PrintHead(snakeNewHead.col, snakeNewHead.row, direction);
                PrintHead(newHeadSnaketwo.col, newHeadSnaketwo.row, directionTwo);
             
                Console.SetCursorPosition(food.col, food.row);
                Console.WriteLine("$");
                Thread.Sleep((int)sleepTime);
            }
            Console.Clear();

            switch (snakeOneMistake)
            {
                case 0: Console.WriteLine("Game over\nSnake Two crashed\nSnake One score:{0}\nSnake Two score:{1}\nTotal score:{2}", counterOne, counterTwo, counterOne + counterTwo); break;
                case 1: Console.WriteLine("Game over\nSnake One crashed\nSnake One score:{0}\nSnake Two score:{1}\nTotal score:{2}", counterOne, counterTwo, counterOne + counterTwo); break;
                case 2: Console.WriteLine("Game over\nBoth snakes colided\nSnake One score:{0}\nSnake Two score:{1}\nTotal score:{2}", counterOne, counterTwo, counterOne + counterTwo); break;

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
        static void PrintSnake(Queue<Position> snakeElement,int body = 0) //Prints the snake
        {
            foreach (Position position in snakeElement)
            {
                if (body == 0)
                {
                    Console.SetCursorPosition(position.col, position.row);
                    Console.WriteLine("*");
                }
                else if (body == 1)
                {
                    Console.SetCursorPosition(position.col, position.row);
                    
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
                Console.WriteLine(">");
            }
            else if (direction == 1)
            {
                Console.WriteLine("<");
            }
            else if (direction == 2)
            {
                Console.WriteLine("v");
            }
            else if (direction == 3)
            {
                Console.WriteLine("^");
            }
        }


    }
}
