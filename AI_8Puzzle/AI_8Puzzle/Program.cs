using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_8Puzzle
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;
            var watch = new System.Diagnostics.Stopwatch();

            SearchAlgorithms search = new SearchAlgorithms();
            List<Node> solution = new List<Node>();
            Node root;
            int input=-1;

            //int[,] puzzle = RandomFill();
            int[,] puzzle = {  { 8, 7, 3}, { 0, 2, 6 }, { 5, 4, 1 } };
            root = new Node(puzzle);

            Console.WriteLine("\nSearch Algorithms");

            while (true)
            {

                Console.WriteLine("\n\n-----------------------------------------------------------------------------------------------------------------------\n\n");

                Console.WriteLine("This is puzzle that filled with random numbers:");

                root.PrintPuzzle(0);

                Console.WriteLine("\nPlease enter the number of Search Algortihm: ");

                Console.WriteLine(" 1. Breadth First Search \n 2. Depth First Search \n 3. Iterative Deepining Search \n 4. Uniform Cost Search \n 5. Greedy Best First Search \n 6. A* Search \n");

                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                    

                watch.Start();
                switch (input)
                {
                    case 1:
                        solution = search.BreadthFirstSearch(root);
                        break;
                    case 2:
                        solution = search.DepthFirstSearch(root);
                        break;
                    case 3:
                        solution = search.IterativeDeepeningSearch(root);
                        break;
                    case 4:
                        solution = search.UniformCostSearch(root);
                        break;
                    case 5:
                        solution = search.GreedyBestFirstSearch(root);
                        break;
                    case 6:
                        solution = search.AStarSearch(root);
                        break;
                    default:
                        Console.WriteLine("Wrong Number");
                        break;
                }
                watch.Stop();
               
                Console.WriteLine($"Execution time is {watch.ElapsedMilliseconds} ms");
                watch.Reset();
                
                if (solution.Count > 0)
                {
                    /*
                    solution.Reverse();
                    for (int i = 0; i < solution.Count; i++)
                    {
                        solution[i].PrintPuzzle(input);
                    }*/
                }
                else
                {
                    Console.WriteLine("No path solution is found");
                }
               
            }
        }

        static int[,] RandomFill()
        {
            List<int> numbers = new List<int>();
            Random rnd = new Random();
            int[,] puzzle = new int[3,3];
            int indexOfList = 0;
            for (int i = 0; i <= 8; i++)
            {
                numbers.Add(i);
            }

            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    indexOfList = rnd.Next(0,numbers.Count);

                    puzzle[i, j] = numbers[indexOfList];

                    numbers.Remove(numbers[indexOfList]);
                }
            }

            return puzzle;
        }
    }
}
