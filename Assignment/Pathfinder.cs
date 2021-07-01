using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Assignment
{
    class Pathfinder
    {
        private static bool ContainsNode(Node node, List<Node> list)
        {
            foreach (Node n in list)
            {
                if(node.Position == n.Position)
                { return true; }
            }
            return false;
        }

        private static int GetSqrDistance(Tile a, Tile b)
        {
            int deltaX = a.MyRow = b.MyRow;
            int deltaY = a.MyCol = b.MyCol;

            return (deltaX * deltaY) + (deltaY * deltaX);
        }

        public static List<Tile> GetPath(Tile startTile, Tile endTile, Tile[,] map)
        {
            List<Node> openNodes = new List<Node>();               //Create two lists
            List<Node> closedNodes = new List<Node>();

            Node start = new Node(null, startTile);                //Create a start node and add it to OpenList

            openNodes.Add(start);

            while (openNodes.Count > 0)                            //While there are items in Open...
            {
                Node currentNode = openNodes[0];                   //currentNode = first item in list
                
                int currentIndex = 0;                              
                
                for(int i = 0; i < openNodes.Count; i++)    
                {
                    Node openNode = openNodes[i];                  //open first item in the OpenList
                    if (openNode.F_Cost < currentNode.F_Cost)      //if the F Cost is lower than the current node...
                    {           
                        currentNode = openNode;                    //make the open node the current node
                        currentIndex = i;                          //update the index and keep going through the list
                    }
                }
                openNodes.RemoveAt(currentIndex);                  // Remove the current node from open
                closedNodes.Add(currentNode);                      // Add it to closed


                //IF GOAL IS FOUND 

                if (currentNode.Position == endTile)               //If the currentNode is the end node
                {
                    List<Tile> path = new List<Tile>();            //Create a list called Path

                    Node pathNode = currentNode;                   //Add the current Node as a path node
                    while (pathNode != null)
                    {
                        path.Add(pathNode.Position);
                        pathNode = pathNode.Parent;                //Change the pathnode to the Parent and repeat
                    }
                    return path;                                   //Return the reverse engineered path.
                }

                List<Node> childNodes = new List<Node>();          //Create a list of Child Nodes

                var directionList = Enum.GetValues(typeof(Directions));       //Using the direction list, find the children in all directions
                foreach (Directions dir in directionList)
                {
                    int row;
                    int col;
                    Game.GetRowAndColForDirection(dir, out row, out col);

                    int childRow = currentNode.Position.MyRow + row;            
                    int childCol = currentNode.Position.MyCol + col;

                    if (Game.IsPositionEmptyAndValid(childRow, childCol, map))
                    {
                        Tile childPosition = map[childRow, childCol];               
                        Node child = new Node(currentNode, childPosition);
                        childNodes.Add(child);                                //If the Tile is valid, ass the position and the child to the node list
                    }
                }


                //ADD CHILDREN TO OPEN NODES
                foreach (Node child in childNodes)
                {
                    //If child is closed, skip it
                    if(ContainsNode(child, closedNodes))
                    {
                        continue;
                    }

                    child.G_Cost = currentNode.G_Cost + 1;                      //if not, calculate it's G and H cost. 
                    child.H_Cost = GetSqrDistance(child.Position, endTile);

                    //Check if the child node is in the Open Node list. If so, skip it
                    foreach (Node open in openNodes)
                    {
                        if((open.Position == child.Position) && (child.G_Cost >= open.G_Cost))
                        {
                            continue;
                        }
                    }

                    //Otherwise add it to the Open Node list
                    openNodes.Add(child);
                }
            }

            return new List<Tile>(); //Return an empty list.            
        }

    }
}
