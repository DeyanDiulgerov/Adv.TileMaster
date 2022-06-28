using System;
using System.Collections.Generic;
using System.Linq;

namespace Adv_TilesMaster
{
    class Program
    {
        static void Main(string[] args)
        {
            var tileResult = new Dictionary<string, int>
            {
                {"Floor", 0},
                {"Countertop", 0},
                {"Oven", 0},
                {"Sink", 0},
                {"Wall", 0}
            };

            var white = Console.ReadLine().Split(" ").Select(double.Parse).ToArray();
            var gray = Console.ReadLine().Split(" ").Select(double.Parse).ToArray();

            var grayQueue = new Queue<double>(gray);
            var whiteStack = new Stack<double>(white);

            while (grayQueue.Any() && whiteStack.Any())
            {
                var currentGray = grayQueue.Peek();
                var currentWhite = whiteStack.Peek();

                var sum = currentGray + currentWhite;

                if (currentGray == currentWhite)
                {
                    if (sum == 40)
                    {
                        tileResult["Sink"]++;
                        grayQueue.Dequeue();
                        whiteStack.Pop();
                    }
                    else if (sum == 50)
                    {
                        tileResult["Oven"]++;
                        grayQueue.Dequeue();
                        whiteStack.Pop();
                    }
                    else if (sum == 60)
                    {
                        tileResult["Countertop"]++;
                        grayQueue.Dequeue();
                        whiteStack.Pop();
                    }
                    else if (sum == 70)
                    {
                        tileResult["Wall"]++;
                        grayQueue.Dequeue();
                        whiteStack.Pop();
                    }
                    else
                    {
                        tileResult["Floor"]++;
                        grayQueue.Dequeue();
                        whiteStack.Pop();
                    }
                }
                else
                {
                    var newWhite = whiteStack.Peek() / 2;
                    whiteStack.Pop();
                    whiteStack.Push(newWhite);

                    var newGray = grayQueue.Peek();
                    grayQueue.Dequeue();
                    grayQueue.Enqueue(newGray);
                }
            }

            var whiteLeft = whiteStack.Count == 0 ? "none" : string.Join(", ", whiteStack);
            var grayLeft = grayQueue.Count == 0 ? "none" : string.Join(", ", grayQueue);

            Console.WriteLine($"White tiles left: {whiteLeft}");
            Console.WriteLine($"Grey tiles left: {grayLeft}");

            foreach (var item in tileResult.OrderByDescending(x => x.Value).ThenBy(x => x.Key))
            {
                if (item.Value > 0)
                {
                    Console.WriteLine($"{item.Key}: {item.Value}");
                }
            }
        }
    }
}
