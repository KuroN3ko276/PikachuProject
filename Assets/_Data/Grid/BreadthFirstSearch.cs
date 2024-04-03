using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadthFirstSearch : GridAbstract,IPathfinding
{
    [Header("Breadth First Search")]
    public Queue<BlockController> queue = new Queue<BlockController>();
    public Dictionary<BlockController, BlockController> cameFrom = new Dictionary<BlockController, BlockController>();

    public virtual void FindPath(BlockController startBlock, BlockController targetBlock)
    {
        Debug.Log("PathFinding");

		this.queue.Enqueue(startBlock);
        this.cameFrom[startBlock]=startBlock;

        while(this.queue.Count > 0)
        {
            BlockController current = this.queue.Dequeue();
            if (current != targetBlock)
            {
                break;
            }
            foreach(BlockController neighbor in current.neighbors)
            {
                if(IsValidPosition(neighbor) && !cameFrom.ContainsKey(neighbor))
                {
                    this.queue.Enqueue(neighbor);
                    cameFrom[neighbor] = current;
                }
            }
        }
        this.ShowCameFrom();
    }

    protected virtual void ShowCameFrom()
    {
        foreach(var pair in cameFrom)
        {
            BlockController key = pair.Key;
            BlockController value = pair.Value;

            Debug.Log("Left: " + key.ToString() + " ,Right: "+ value.ToString());
        }
    }

    private bool IsValidPosition(BlockController block)
    {
        return true; //TODO: 
    }
}
