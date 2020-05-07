using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_8Puzzle
{
    class SearchAlgorithms
    {
        public SearchAlgorithms()
        {

        }

        #region Search Algorithms 

        public List<Node> BreadthFirstSearch(Node root)
        {
            Console.WriteLine("Breadth First Search\n");
            List<Node> PathToSolution = new List<Node>();
            List<Node> FrontierList = new List<Node>();
            List<Node> ExploredList = new List<Node>();
            int maxNumberOfFrontierList = 0;

            FrontierList.Add(root);
            bool isGoalFound = false;

            while(FrontierList.Count >0 && !isGoalFound)
            {
                Node currentNode = FrontierList[0];

                if (currentNode.GoalTest())
                {
                    Console.WriteLine("Goal Found");
                    isGoalFound = true;
                    Console.WriteLine("Expanded List's Count is " + ExploredList.Count());
                    Console.WriteLine("Maximum number stored in Frontier List is " + maxNumberOfFrontierList);
                    PathTrace(PathToSolution, currentNode);
                }

                ExploredList.Add(currentNode);
                if (maxNumberOfFrontierList < FrontierList.Count()) maxNumberOfFrontierList = FrontierList.Count();
                FrontierList.RemoveAt(0);

                currentNode.ExpandNode();
                //currentNode.PrintPuzzle();

                for (int i=0; i < currentNode.children.Count; i++)
                {
                    currentNode.children[i].costFromRoot = getCostFromRoot(currentNode.children[i]);

                    if (!Contains(FrontierList,currentNode.children[i])&& !Contains(ExploredList, currentNode.children[i]))
                        FrontierList.Add(currentNode.children[i]);
                }
            }

            return PathToSolution;
        }

        public List<Node> DepthFirstSearch(Node root)
        {
            Console.WriteLine("Depth First Search\n");

            List<Node> PathToSolution = new List<Node>();
            Stack<Node> FrontierList = new Stack<Node>();
            List<Node> ExploredList = new List<Node>();
            int maxNumberOfFrontierList = 0;

            FrontierList.Push(root);
            bool isGoalFound = false;

            while (FrontierList.Count > 0)
            {
                Node currentNode = FrontierList.Peek();

                if (currentNode.GoalTest())
                {
                    Console.WriteLine("Goal Found");
                    isGoalFound = true;
                    Console.WriteLine("Expanded List's Count is " + ExploredList.Count());
                    Console.WriteLine("Maximum number stored in Frontier List is " + maxNumberOfFrontierList);
                    PathTrace(PathToSolution, currentNode);
                    break;
                }
                
                currentNode.ExpandNode();
                //currentNode.PrintPuzzle();

                ExploredList.Add(currentNode);
                if (maxNumberOfFrontierList < FrontierList.Count()) maxNumberOfFrontierList = FrontierList.Count();
                FrontierList.Pop();

                for (int i = 0; i < currentNode.children.Count; i++)
                {
                    currentNode.children[i].costFromRoot = getCostFromRoot(currentNode.children[i]);
                    if (!Contains(FrontierList, currentNode.children[i]) && !Contains(ExploredList,currentNode.children[i]))
                        FrontierList.Push(currentNode.children[i]);
                }
                
            }

            return PathToSolution;
        }

        public List<Node> IterativeDeepeningSearch(Node root)
        {
            Console.WriteLine("Iterative Deepening Search\n");
            int iterateTimes = 0;
            List<Node> PathToSolution = new List<Node>();
            for (int i = 0; i < 10000; i++)
            {
                Node rootCopy = Node.CopyNode(root);
                PathToSolution = DepthUtils(rootCopy, i);
                if (PathToSolution.Count != 0)
                {
                    iterateTimes = i + 1;
                    i = 10000;
                }
            }
            Console.WriteLine("Iterate times: " + iterateTimes);

            return PathToSolution;
        }

        public List<Node> DepthUtils(Node n,int maxDepth)
        {
            List<Node> PathToSolution = new List<Node>();
            Stack<Node> FrontierList = new Stack<Node>();
            List<Node> ExploredList = new List<Node>();
            int maxNumberOfFrontierList = 0;

            FrontierList.Push(n);
            bool isGoalFound = false;
            
            while(FrontierList.Count > 0)
            {
                Node currentNode = FrontierList.Peek();

                if (currentNode.GoalTest())
                {
                    Console.WriteLine("Goal Found");
                    isGoalFound = true;
                    Console.WriteLine("Expanded List's Count is " + ExploredList.Count());
                    Console.WriteLine("Maximum number stored in Frontier List is " + maxNumberOfFrontierList);
                    PathTrace(PathToSolution, currentNode);
                    break;
                }
                
                if(currentNode.costFromRoot < maxDepth)
                {
                    currentNode.ExpandNode();
                }

                ExploredList.Add(currentNode);
                if (maxNumberOfFrontierList < FrontierList.Count()) maxNumberOfFrontierList = FrontierList.Count();
                FrontierList.Pop();

                for (int i = 0; i < currentNode.children.Count; i++)
                {
                    currentNode.children[i].costFromRoot = getCostFromRoot(currentNode.children[i]);
                    if (!FrontierList.Contains(currentNode.children[i]) && !ExploredList.Contains(currentNode.children[i]))
                        FrontierList.Push(currentNode.children[i]);
                }
                
            }
            
            return PathToSolution;
        }

        public List<Node> UniformCostSearch(Node root)
        {
            Console.WriteLine("Uniform Cost Search\n");

            List<Node> PathToSolution = new List<Node>();
            PriorityQueue<int, Node> FrontierList = new PriorityQueue<int, Node>();
            List<Node> ExploredList = new List<Node>();
            int maxNumberOfFrontierList = 0;

            FrontierList.Enqueue(0,root);
            bool isGoalFound = false;

            while (FrontierList.Count() > 0 && !isGoalFound)
            {
                Node currentNode = FrontierList.Peek().Value;

                if (currentNode.GoalTest())
                {
                    Console.WriteLine("Goal Found");
                    isGoalFound = true;
                    Console.WriteLine("Expanded List's Count is " + ExploredList.Count());
                    Console.WriteLine("Maximum number stored in Frontier List is " + maxNumberOfFrontierList);
                    PathTrace(PathToSolution, currentNode);
                    break;
                }

                ExploredList.Add(currentNode);
                if (maxNumberOfFrontierList < FrontierList.Count()) maxNumberOfFrontierList = FrontierList.Count();
                FrontierList.Dequeue();

                currentNode.ExpandNode();
                //currentNode.PrintPuzzle();

                for (int i = 0; i < currentNode.children.Count; i++)
                {
                    currentNode.children[i].costFromRoot = getCostFromRoot(currentNode.children[i]);
                    if (!Contains(FrontierList,currentNode.children[i]) && !Contains(ExploredList, currentNode.children[i]))
                    {
                        FrontierList.Enqueue(currentNode.children[i].costFromRoot, currentNode.children[i]);
                    }                        
                    else if (Contains(FrontierList, currentNode.children[i]))
                    {
                        checkWhichHasHighCost(ref FrontierList, currentNode.children[i], "Uniform");
                    }
                }
            }
            return PathToSolution;
        }

        public List<Node> GreedyBestFirstSearch(Node root)
        {
            Console.WriteLine("Greedy Best First Search\n");

            List<Node> PathToSolution = new List<Node>();
            PriorityQueue<int, Node> FrontierList = new PriorityQueue<int, Node>();
            List<Node> ExploredList = new List<Node>();
            int maxNumberOfFrontierList = 0;

            root.costHeuristic = getHeuristicCost(root);
            FrontierList.Enqueue(root.costHeuristic, root);
            bool isGoalFound = false;

            while (FrontierList.Count() > 0 && !isGoalFound)
            {
                Node currentNode = FrontierList.Peek().Value;

                if (currentNode.GoalTest())
                {
                    Console.WriteLine("Goal Found");
                    isGoalFound = true;
                    Console.WriteLine("Expanded List's Count is " + ExploredList.Count());
                    Console.WriteLine("Maximum number stored in Frontier List is " + maxNumberOfFrontierList);
                    PathTrace(PathToSolution, currentNode);
                    break;
                }

                ExploredList.Add(currentNode);
                if (maxNumberOfFrontierList < FrontierList.Count()) maxNumberOfFrontierList = FrontierList.Count();
                FrontierList.Dequeue();

                currentNode.ExpandNode();
                //currentNode.PrintPuzzle();

                for (int i = 0; i < currentNode.children.Count; i++)
                {
                    //currentNode.children[i].costFromRoot = getCostFromRoot(currentNode.children[i]);
                    currentNode.children[i].costHeuristic = getHeuristicCost(currentNode.children[i]);
                    //currentNode.children[i].costTotalEstimated = currentNode.children[i].costFromRoot + currentNode.children[i].costHeuristic;

                    if (!Contains(FrontierList, currentNode.children[i]) && !Contains(ExploredList, currentNode.children[i]))
                    {
                        FrontierList.Enqueue(currentNode.children[i].costHeuristic, currentNode.children[i]);
                    }
                    else if (Contains(FrontierList, currentNode.children[i]))
                    {
                        checkWhichHasHighCost(ref FrontierList, currentNode.children[i],"Heuristic");
                    }
                }

            }
            return PathToSolution;
        }

        public List<Node> AStarSearch(Node root)
        {
            Console.WriteLine("A* Search\n");

            List<Node> PathToSolution = new List<Node>();
            PriorityQueue<int, Node> FrontierList = new PriorityQueue<int, Node>();
            List<Node> ExploredList = new List<Node>();
            int maxNumberOfFrontierList = 0;

            root.costHeuristic = getHeuristicCost(root);
            root.costTotalEstimated = root.costFromRoot+getHeuristicCost(root);
            FrontierList.Enqueue(getCostFromRoot(root) + root.costHeuristic, root);
            bool isGoalFound = false;

            while (FrontierList.Count() > 0 && !isGoalFound)
            {
                Node currentNode = FrontierList.Peek().Value;
                
                if (currentNode.GoalTest())
                {
                    Console.WriteLine("Goal Found");
                    isGoalFound = true;
                    PathTrace(PathToSolution, currentNode);
                    Console.WriteLine("Expanded List's Count is " + ExploredList.Count());
                    Console.WriteLine("Maximum number stored in Frontier List is " + maxNumberOfFrontierList);
                    break;
                }

                ExploredList.Add(currentNode);
                if (maxNumberOfFrontierList < FrontierList.Count()) maxNumberOfFrontierList = FrontierList.Count();
                FrontierList.Dequeue();

                currentNode.ExpandNode();
                //currentNode.PrintPuzzle();

                for (int i = 0; i < currentNode.children.Count; i++)
                {
                    currentNode.children[i].costFromRoot = getCostFromRoot(currentNode.children[i]);
                    currentNode.children[i].costHeuristic = getHeuristicCost(currentNode.children[i]);
                    currentNode.children[i].costTotalEstimated = currentNode.children[i].costFromRoot
                                                                + currentNode.children[i].costHeuristic;

                    if (!Contains(FrontierList, currentNode.children[i]) && !Contains(ExploredList, currentNode.children[i]))
                    {
                        FrontierList.Enqueue(currentNode.children[i].costTotalEstimated, currentNode.children[i]);
                    }
                    else if (Contains(FrontierList, currentNode.children[i]))
                    {
                        checkWhichHasHighCost(ref FrontierList, currentNode.children[i], "Heuristic");
                    }
                }
            }
            return PathToSolution;
        }

        #endregion

        #region Help Methods

        public void PathTrace(List<Node> path, Node n)
        {
            Console.WriteLine("Tracing path..");
            Node current = n;
            path.Add(current);

            while(current.parent != null)
            {
                current = current.parent;
                path.Add(current);
            }
        }

        public void checkWhichHasHighCost(ref PriorityQueue<int, Node> FrontierList, Node n, string nameOfCost)
        {
            int value = 0;

            if (nameOfCost.Equals("Uniform")) value = n.costFromRoot;
            else if (nameOfCost.Equals("Heuristic")) value = n.costHeuristic;
                 
            PriorityQueue<int, Node> templateList = new PriorityQueue<int, Node>();
            List<Node> nodeList = FrontierList.getHeapVariables();
            List<int> costList = FrontierList.getHeapCosts();
            int index=0;
            bool willChange = false;

            for (int i = 0; i < nodeList.Count(); i++)
            {
                if (nodeList[i].isSamePuzzle(n.puzzle))
                {
                    if (value < costList[i])
                    {
                        willChange = true;
                        index = i;
                        break;
                    }
                }
            }

            if (willChange)
            {
                for (int i = 0; i < nodeList.Count(); i++)
                {
                    if (i != index)  templateList.Enqueue(costList[i], nodeList[i]);
                }
                templateList.Enqueue(value, nodeList[nodeList.Count-1]);

                FrontierList = templateList;
            }
            
        }

        public static bool Contains(List<Node> list, Node c)
        {
            bool contains = false;

            for(int i = 0; i < list.Count; i++)
            {
                if (list[i].isSamePuzzle(c.puzzle))
                    contains = true;
            }
            return contains;
        }

        public static bool Contains(Stack<Node> stack, Node c)
        {
            bool contains = false;
            Node[] template = new Node[stack.Count];

            stack.CopyTo(template, 0);
            
            for (int i = 0; i < stack.Count; i++)
            {
                if (template[i].isSamePuzzle(c.puzzle))
                    contains = true;
            }
            return contains;
        }

        public static bool Contains(PriorityQueue<int, Node> list, Node c)
        {
            bool contains = false;
            
            List<Node> nodeList = list.getHeapVariables();

            for (int i = 0; i < list.Count(); i++)
            {
                if (nodeList[i].isSamePuzzle(c.puzzle))
                    contains = true;
            }
            return contains;
        }

        public static int getCostFromRoot(Node n)
        {
            Node current = n;
            List<Node> listUC = new List<Node>();
            listUC.Add(current);

            while (current.parent != null)
            {
                current = current.parent;
                listUC.Add(current);
            }
            return listUC.Count - 1;
        }

        public static int getHeuristicCost(Node n)
        {
            //Using Manhattan distance
            int heuristicValue = 0;

            int[] rowIndexes    = { 2, 0, 0, 0, 1, 1, 1, 2, 2 };
            int[] columnIndexes = { 2, 0, 1, 2, 0, 1, 2, 0, 1 };

            for (int number = 1; number <= 8; number++)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (n.puzzle[i, j] == number)
                        {
                            heuristicValue += ManhattanDistance(i, rowIndexes[number], j, columnIndexes[number]);
                            i = j = 3;
                            break;
                        }
                              
                    }
                }
            }

            return heuristicValue;
        }

        public static int ManhattanDistance(int x1, int x2, int y1, int y2)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }

        #endregion

    }
}
