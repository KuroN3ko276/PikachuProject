using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadthFirstSearch : GridAbstract,IPathfinding
{
    [Header("Breadth First Search")]
    public Queue<BlockController> queue = new Queue<BlockController>();
	public List<BlockController> path = new List<BlockController>();
	public Dictionary<BlockController, BlockController> cameFrom = new Dictionary<BlockController, BlockController>();
   

    public virtual void FindPath(BlockController startBlock, BlockController targetBlock)
    {
        Debug.Log("PathFinding");

		this.queue.Enqueue(startBlock);
        this.cameFrom[startBlock]=startBlock;

        while(this.queue.Count > 0)
        {
            BlockController current = this.queue.Dequeue();
			if (current == targetBlock)
            {
                ConstructPath(startBlock,targetBlock);
                break;
            }
            foreach(BlockController neighbor in current.neighbors)
            {
                if(neighbor == null) { continue; }
				if (IsValidPosition(neighbor) && !cameFrom.ContainsKey(neighbor))
                {
                    this.queue.Enqueue(neighbor);
                    cameFrom[neighbor] = current;
                }
            }
        }
        this.ShowPath();
    }

    protected virtual void ShowPath()
    {
        foreach(BlockController current in this.path)
        {
            current.blockData.text.color=Color.blue;
        }
    }
    protected virtual void ConstructPath(BlockController startBlock, BlockController targetBlock)
    {
        BlockController currentCell = targetBlock;

        while(currentCell != startBlock) 
        {
            path.Add(currentCell);
            currentCell = this.cameFrom[currentCell];
        }
        path.Add(startBlock);
        path.Reverse();
    }
    

    private bool IsValidPosition(BlockController block)
    {
        return true; //TODO: 
    }
}
