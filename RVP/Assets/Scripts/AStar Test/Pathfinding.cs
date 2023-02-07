// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Pathfinding
// {
//     private const int MOVE_STRAIGHT_COST = 10;
//     private const int MOVE_DIAGONAL_COST = 14;

//     private GridScript<PathNode> grid;
//     private List<PathNode> openList;
//     private List<PathNode> closedList;

//     public Pathfinding(int width, int height)
//     {
//         grid = new GridScript<PathNode>(width, height, 1f, Vector3.zero, (GridScript<PathNode> g, int x, int y) => new PathNode(g, x, y));
//     }

//     public GridScript<PathNode> GetGrid()
//     {
//         return grid;
//     }

//     private List<PathNode> FindPath(int startX, int startY, int endX, int endY)
//     {
//         PathNode startNode = grid.GetGridObject(startX, startY);
//         PathNode endNode = grid.GetGridObject(endX, endY);

//         openList = new List<PathNode> { startNode };
//         closedList = new List<PathNode>();

//         for(int i = 0; i < grid.GetWidth(); i++)
//         {
//             for(int j = 0; j < grid.GetHeight(); j++)
//             {
//                 PathNode pathnode = grid.GetGridObject(i, j);
//                 pathnode.gCost = int.MaxValue;
//                 pathnode.CalculateFCost();
//                 pathnode.cameFromNode = null;
//             }
//         }

//         startNode.gCost = 0;
//         startNode.hCost = CalculateDistance(startNode, endNode);
//         startNode.CalculateFCost();

//         while(openList.Count > 0)
//         {
//             PathNode currentNode = GetLowestFCostNode(openList);
//             if(currentNode == endNode)
//             {
//                 // Reached final node
//                 return CalculatePath(endNode);
//             }

//             openList.Remove(currentNode);
//             closedList.Add(currentNode);

//             foreach (PathNode neighbourNode in GetNeighbourList(currentNode))
//             {
//                 if(closedList.Contains(neighbourNode)) continue;

//                 int tentativeGCost = currentNode.gCost + CalculateDistance(currentNode, neighbourNode);
//                 if(tentativeGCost < neighbourNode.gCost)
//                 {
//                     neighbourNode.cameFromNode = currentNode;
//                     neighbourNode.gCost = tentativeGCost;
//                     neighbourNode.hCost = CalculateDistance(neighbourNode, endNode);
//                     neighbourNode.CalculateFCost();

//                     if(!openList.Contains(neighbourNode))
//                     {
//                         openList.Add(neighbourNode);
//                     }
//                 }
//             }
//         }

//         // Out of nodes on the open list
//         return null;
//     }

//     private List<PathNode> GetNeighbourList(PathNode currentNode)
//     {
//         List<PathNode> neighbourList = new List<PathNode>();

//         if(currentNode.x - 1 >= 0)
//         {
//             //LEFT
//             neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y));

//             //LEFT DOWN
//             if(currentNode.y - 1 >= 0)
//             {
//                 neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y - 1));
//             }
//             //LEFT UP
//             if(currentNode.y + 1 < grid.GetHeight())
//             {
//                 neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y + 1));
//             }
//         }

//         if(currentNode.x + 1 < grid.GetWidth())
//         {
//             //RIGHT
//             neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y));

//             //RIGHT DOWN
//             if(currentNode.y - 1 >= 0)
//             {
//                 neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y - 1));
//             }
//             //RIGHT UP
//             if(currentNode.y + 1 < grid.GetHeight())
//             {
//                 neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y + 1));
//             }
//         }

//         //DOWN
//         if(currentNode.y - 1 >= 0)
//         {
//             neighbourList.Add(GetNode(currentNode.x, currentNode.y - 1));
//         }

//         //UP
//         if(currentNode.y + 1 < grid.GetHeight())
//         {
//             neighbourList.Add(GetNode(currentNode.x, currentNode.y + 1));
//         }
//     }

//     private List<PathNode> CalculatePath(PathNode endNode)
//     {
//         List<PathNode> path = new List<PathNode>();
//         path.Add(endNode);
//         PathNode currentNode = endNode;
//         while(currentNode != null)
//         {
//             path.Add(currentNode.cameFromNode);
//             currentNode = currentNode.cameFromNode;
//         }
//         path.Reverse();
//         return path;
//     }

//     private int CalculateDistance(PathNode a, PathNode b)
//     {
//         int xDistance = Mathf.Abs(a.x - b.x);
//         int yDistance = Mathf.Abs(a.y - b.y);
//         int remaining = Mathf.Abs(xDistance - yDistance);
//         return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;

//     } 

//     private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
//     {
//         PathNode lowestFCostNode = pathNodeList[0];
//         for(int i = 1; i < pathNodeList.Count; i++)
//         {
//             if(pathNodeList[i].fCost < lowestFCostNode.fCost)
//             {
//                 lowestFCostNode = pathNodeList[i];
//             }
//         }
//         return lowestFCostNode;
//     }
// }
