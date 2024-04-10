using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadthFirstSearch : GridAbstract,IPathfinding
{
    [Header("Breadth First Search")]
    public Queue<Node> queue = new Queue<Node>();
	public List<Node> path = new List<Node>();
	public Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>();
   

    public virtual void FindPath(BlockController startBlock, BlockController targetBlock)
    {
        Debug.Log("PathFinding");
        Node startNode = startBlock.blockData.node;
        Node targetNode = targetBlock.blockData.node;

		this.queue.Enqueue(startNode);
        this.cameFrom[startNode]=startNode;

        while(this.queue.Count > 0)
        {
            Node current = this.queue.Dequeue();
			if (current == targetNode)
            {
                ConstructPath(startNode,targetNode);
                break;
            }
            foreach(Node neighbor in current.Neighbors)
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
        Vector3 pos;
        foreach(Node node in this.path)
        {
            pos = node.nodeTranfrom.transform.position;
            Transform linker = this.Controller.blockSpawner.Spawn(BlockSpawner.LINKER, pos, Quaternion.identity);
            linker .gameObject.SetActive(true);
        }
    }
    protected virtual void ConstructPath(Node startNode, Node targetNode)
    {
        Node currentCell = targetNode;

        while(currentCell != startNode) 
        {
            path.Add(currentCell);
            currentCell = this.cameFrom[currentCell];
        }
        path.Add(startNode);
        path.Reverse();
    }
    

    private bool IsValidPosition(Node node)
    {
        return !node.occupied;
	}
         
}
