using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{

    public int gCost;
    public int hCost;
    public bool walkable;
    public Vector2 worldPosition;

    public Node(bool _walkable, Vector2 _worldPosition)
    {
        walkable = _walkable;
        worldPosition = _worldPosition;
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
}
