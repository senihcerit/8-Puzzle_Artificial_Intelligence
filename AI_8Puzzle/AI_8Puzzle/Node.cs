using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_8Puzzle
{
    public class Node
    {
        public  List<Node> children = new List<Node>();
        public Node parent;
        public int costFromRoot = 0;
        public int costHeuristic = 0;
        public int costTotalEstimated = 0;

        public int[,] puzzle = new int[3, 3];
        public int i =0, j = 0;
        public static int[,] goalState = { {1, 2, 3}, {4, 5, 6}, {7, 8, 0}};

        public Node(int[,] p)
        {
            SetPuzzle(p);
        }

        public void SetPuzzle(int[,] p)
        {
            for(int i = 0; i < puzzle.GetLength(0); i++)
            {
                for(int j=0; j < puzzle.GetLength(1); j++)
                {
                    this.puzzle[i, j] = p[i, j];
                }
            }
        }

        public void ExpandNode()
        {
            for (int i = 0; i < puzzle.GetLength(0); i++)
            {
                for (int j = 0; j < puzzle.GetLength(1); j++)
                {
                    if (puzzle[i, j] == 0)
                    {
                        this.i = i;
                        this.j = j;
                    }  
                }
            }

            
            MoveToLeft(puzzle, i, j);
            MoveToUp(puzzle, i, j);
            MoveToRight(puzzle, i, j);
            MoveToDown(puzzle, i, j);
        }

        public void MoveToRight(int[,] p,int i,int j)
        {
            if(j < 2)
            {
                int[,] newPuzzle = CopyPuzzle(p);

                int temp = newPuzzle[i, j+1];
                newPuzzle[i, j + 1] = newPuzzle[i, j];
                newPuzzle[i, j] = temp;

                Node child = new Node(newPuzzle);
                children.Add(child);
                child.parent = this;
                
            }
        }

        public void MoveToLeft(int[,] p, int i, int j)
        {
            if (j > 0)
            {
                int[,] newPuzzle = CopyPuzzle(p);

                int temp = newPuzzle[i, j - 1];
                newPuzzle[i, j - 1] = newPuzzle[i, j];
                newPuzzle[i, j] = temp;

                Node child = new Node(newPuzzle);
                children.Add(child);
                child.parent = this;
                
            }
        }

        public void MoveToUp(int[,] p, int i, int j)
        {
            if (i > 0)
            {
                int[,] newPuzzle = CopyPuzzle(p);

                int temp = newPuzzle[i-1, j];
                newPuzzle[i-1, j] = newPuzzle[i, j];
                newPuzzle[i, j] = temp;

                Node child = new Node(newPuzzle);
                children.Add(child);
                child.parent = this;
                
            }
        }

        public void MoveToDown(int[,] p, int i, int j)
        {
            if (i < 2)
            {
                int[,] newPuzzle = CopyPuzzle(p);

                int temp = newPuzzle[i + 1, j];
                newPuzzle[i + 1, j] = newPuzzle[i, j];
                newPuzzle[i, j] = temp;

                Node child = new Node(newPuzzle);
                children.Add(child);
                child.parent = this;
                
            }
        }

        public void PrintPuzzle(int number)
        {
            if (number>=0 && number <= 3)
            {

            }
            else if (number == 4)
            {
                Console.WriteLine("Cost from Root is: " + costFromRoot);
            }
            else if (number == 5)
            {
                Console.WriteLine("Heuristic Cost is: " + costHeuristic);
            }
            else if (number == 6)
            {
                Console.WriteLine("Cost from Root is: " + costFromRoot);
                Console.WriteLine("Heuristic Cost is: " + costHeuristic);
                Console.WriteLine("Total Estimated Cost is: " + costTotalEstimated);
            }
            Console.WriteLine();

            for (int i = 0; i < puzzle.GetLength(0); i++)
            {
                for (int j = 0; j < puzzle.GetLength(1); j++)
                {
                    Console.Write(puzzle[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        public bool isSamePuzzle(int[,] p)
        {
            bool isSamePuzzle = true;

            for (int i = 0; i < puzzle.GetLength(0); i++)
            {
                for (int j = 0; j < puzzle.GetLength(1); j++)
                {
                    if (puzzle[i, j] != p[i, j]) isSamePuzzle = false;
                }
            }
            return isSamePuzzle;
        }

        public static int[,] CopyPuzzle(int[,] p)
        {
            int[,] copy = new int[3, 3];

            for (int i = 0; i < p.GetLength(0); i++)
            {
                for (int j = 0; j < p.GetLength(1); j++)
                {
                    copy[i, j] = p[i, j];
                }
            }
            return copy;
        }

        public static Node CopyNode(Node n)
        {
            Node copy = new Node(n.puzzle);
            return copy;
        }

        public bool GoalTest()
        {
            bool isGoal = true;

            for (int i = 0; i < puzzle.GetLength(0); i++)
            {
                for (int j = 0; j < puzzle.GetLength(1); j++)
                {
                    if (puzzle[i, j] != goalState[i, j]) isGoal = false;
                }
            }
            return isGoal;
        }


    }
}
