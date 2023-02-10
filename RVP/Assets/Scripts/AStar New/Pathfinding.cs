using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{

    Grid grid;
    void Awake()
    {
        grid = GetComponent<Grid>();
    }

    void FindPath(Vector2 startPos, Vector2 targetPos)
    {
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();

        openSet.Add(startNode);

        while(openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for(int i =0; i < openSet.Count; i++)
            {
                if(openSet[i].fCost < currentNode.fCost)
                {
                    currentNode = openSet[i];
                }
            }
        }
    }
}
