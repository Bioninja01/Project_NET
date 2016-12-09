using UnityEngine;
using System.Collections;
using System;

public class Node : IHeapItem<Node> {
    public bool walkable;
    public Vector3 worldPosition;
    public int gridX, gridY;
    public Node parent;

    public int gCost;
    public int hCost;
    int heapIndex; 

    public int fCost {
        get {
            return gCost + hCost;
        }
    }

    public Node(bool walkable, Vector3 worldPosition, int x, int y) {
        this.walkable = walkable;
        this.worldPosition = worldPosition;
        this.gridX = x;
        this.gridY = y;
    }


    public int HeapIndex {
        get {
            return heapIndex;
        }
        set {
            heapIndex = value;
        }
    }

    public int CompareTo(Node other) {
        int compare = fCost.CompareTo(other.fCost);
        if(compare == 0) {
            compare = hCost.CompareTo(other.hCost);
        }
        return -compare;
    }
}
