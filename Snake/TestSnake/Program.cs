using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TestSnake
{
    struct Position
    {
        public int Row;
        public int Coll;

        public Position(int row, int coll)
        {
            this.Row = row;
            this.Coll = coll;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Position[] directions = new Position[]
            {
                new Position (0,1),         //ritgh
                new Position (0,-1),        //left
                new Position (1,0),         //down
                new Position(-1,0)          //up
            };

            int right = 0;
            int left = 1;
            int down = 2;
            int up = 3;

            int direction = 0;
            int sleepTime = 100;
            Queue<Position> snakeElements = new Queue<Position>();

            for (int i = 0; i <= 5; i++)
            {
                snakeElements.Enqueue(new Position(0, i));
            }

            Random randomNumGenertor = new Random();
            Position food = new Position(randomNumGenertor.Next(0, Console.WindowHeight), randomNumGenertor.Next(0, Console.WindowHeight));

            Console.SetCursorPosition(food.Coll, food.Row);
            Console.Write("@");

            Console.BufferHeight = Console.WindowHeight;  // fix bug with scrolling down

            foreach (Position position in snakeElements)
            {
                Console.SetCursorPosition(position.Coll, position.Row);
                Console.Write("*");
            }

            while (true)
            {
                if (Console.KeyAvailable) // snake movement and positions
                {

                    var userInput = Console.ReadKey();

                    if (userInput.Key == ConsoleKey.RightArrow)
                    {
                       if (direction!=left) direction = 0;
                    }
                    if (userInput.Key == ConsoleKey.LeftArrow)
                    {
                       if(direction!=right) direction = 1;
                    }
                    if (userInput.Key == ConsoleKey.DownArrow)
                    {
                        if(direction!=up)direction = 2;
                    }
                    if (userInput.Key == ConsoleKey.UpArrow)
                    {
                       if(direction!=down) direction = 3;
                    }
                }

                var snakeHead = snakeElements.Last();
                var nextDirection = directions[direction];
                var newHead = new Position(nextDirection.Row + snakeHead.Row, nextDirection.Coll + snakeHead.Coll);

                if (newHead.Row < 0 ||
                    newHead.Coll < 0 ||
                    newHead.Coll >= Console.WindowWidth ||
                    newHead.Row >= Console.WindowHeight||
                    snakeElements.Contains(newHead))
                {

                    //draw score board
                    Console.SetCursorPosition(45, 8);
                    Console.WriteLine(new string('=', 40));
                    int row = 9;
                    for (int i = 0; i < 6; i++)
                    {
                        Console.SetCursorPosition(45, row);
                        Console.WriteLine('$' + new string(' ', 38) + '$');
                        row++;
                    }
                    Console.SetCursorPosition(45, 15);
                    Console.WriteLine(new string('=', 40));
                     
                    // print score

                    Console.SetCursorPosition(50, 11);
                    Console.Write("GAME OVER BROOOO!!!!!!!!!!");
                    Console.SetCursorPosition(50, 12);
                    Console.WriteLine($"Your score is : {(snakeElements.Count-6) * 100}!!!");

                    if (snakeElements.Count<=10)
                    {
                        Console.SetCursorPosition(50, 13);
                        Console.WriteLine("YOU ARE NOOB!!!");
                    }
                    else
                    {
                        Console.SetCursorPosition(50, 13);
                        Console.WriteLine("YOU ARE GOD!!!");
                    }

                    break;
                }
                snakeElements.Enqueue(newHead);
                Console.SetCursorPosition(newHead.Coll, newHead.Row);
                Console.Write('*');

                if (newHead.Coll == food.Coll && newHead.Row == food.Row)
                {
                    //feding the snake
                    food = new Position(randomNumGenertor.Next(0, Console.WindowHeight), randomNumGenertor.Next(0, Console.WindowHeight));

                    Console.SetCursorPosition(food.Coll, food.Row);
                    Console.Write("@");
                    sleepTime-=2;
                }
                else
                {
                    //moving
                   var last = snakeElements.Dequeue();
                    Console.SetCursorPosition(last.Coll, last.Row);
                    Console.WriteLine(" ");
                }


                Console.Clear(); // refresh console

                foreach (Position position in snakeElements)  //print snake 
                {
                    Console.SetCursorPosition(position.Coll, position.Row);
                    Console.Write("*");
                }



                Console.SetCursorPosition(food.Coll, food.Row);
                Console.Write("@");

                Thread.Sleep(sleepTime);
            }
        }
    }
}
