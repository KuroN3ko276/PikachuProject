using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GridSystem : GridAbstract
{
	[Header("Grid System")]
	public int width = 18;
	public int height = 11;
	private float offsetX = 0.19f;
	//private float offsetY = 0;
	public BlocksProfileSO blocksProfile;
	public List<Node> nodes;
	public List<int> nodeIDs;
	protected override void LoadComponents()
	{
		base.LoadComponents();
		this.InitGridSystem();
		this.LoadBlockProfile();
	}

	protected virtual void LoadBlockProfile()
	{
		if (this.blocksProfile != null) return;
		this.blocksProfile = Resources.Load<BlocksProfileSO>("Pikachu");
		Debug.LogWarning(transform.name + "LoadBlockProfile", gameObject);
	}

	protected override void Start()
	{
		this.SpawnHolders();
		this.SpawnBlocks();
		this.FindNodesNeighbors();
		this.FindBlocksNeighbors();
	}

	protected virtual void FindNodesNeighbors()
	{
		int x, y;
		foreach (Node node in this.nodes)
		{
			x = node.x;
			y = node.y;
			node.up = this.GetNodeByXY(x, y + 1);
			node.right= this.GetNodeByXY(x+1, y);
			node.down = this.GetNodeByXY(x, y - 1);
			node.left = this.GetNodeByXY(x-1,y);
		}
	}

	protected virtual Node GetNodeByXY(int x, int y)
	{
		foreach(Node node in this.nodes)
		{
			if (node.x == x && node.y == y) return node;

		}
		return null;
	}
	protected virtual void FindBlocksNeighbors()
	{
		foreach(Node node in this.nodes)
		{
			if(node.blockController == null ) continue;
			node.blockController.neighbors.Add(node.up.blockController);
			node.blockController.neighbors.Add(node.right.blockController);
			node.blockController.neighbors.Add(node.down.blockController);
			node.blockController.neighbors.Add(node.left.blockController);

		}
	}

	protected virtual void InitGridSystem()
	{
		if(this.nodes.Count>0) { return; }

		int nodeId = 0;
		for(int x = 0; x<this.width; x++)
		{
			for(int y =0; y<this.height; y++)
			{
				Node node = new Node
				{
					x = x,
					y = y,
					posX = x - (this.offsetX * x),
					nodeId = nodeId,
				};
				this.nodes.Add(node);
				this.nodeIDs.Add(nodeId);
				nodeId++;
			}
		}
	}
	protected virtual void SpawnHolders()
	{	
		Vector3 pos = new Vector3(0,0,0);
		foreach(Node node in this.nodes)
		{
			pos.x = node.posX;
			pos.y=node.y;

			Transform blockObj = this.Controller.blockSpawner.Spawn(BlockSpawner.HOLDER, pos, Quaternion.identity);
			NodeTransform blockHolder = blockObj.GetComponent<NodeTransform>();
			node.nodeTranfrom = blockHolder;
			blockObj.name = "Holder_" + node.x.ToString() + "_" + node.y.ToString();
			blockHolder.gameObject.SetActive(true);

			blockObj.gameObject.SetActive(true);

			node.occupied = true;
		}
	}

	protected virtual void SpawnBlocks()
	{
		Vector3 pos = new Vector3(0, 0, 0);
		int blockCount = 4;
		foreach(Sprite sprite in this.blocksProfile.sprites)
		{
			for(int i = 0;i<blockCount;i++)
			{
				Node node = this.GetRandomNode();
				pos.x = node.posX;
				pos.y = node.y;

				Transform block = this.Controller.blockSpawner.Spawn(BlockSpawner.BLOCK, pos, Quaternion.identity);
				BlockController blockController = block.GetComponent<BlockController>();
				blockController.blockData.SetSprite(sprite);
				
				this.LinkNodeBlock(node,blockController);
				block.name = "Block_"+node.x.ToString()+"_"+node.y.ToString();
				block.gameObject.SetActive(true);
			}
		}
	}
	protected virtual Node GetRandomNode()
	{
		Node node;
		int randId;
		int nodeCount = this.nodes.Count;
		for(int i=0;i<nodeCount;i++)
		{
			randId = Random.Range(0,this.nodeIDs.Count);	
			node = this.nodes[this.nodeIDs[randId]];
			this.nodeIDs.RemoveAt(randId);

			if (node.x == 0) continue;
			if (node.y == 0) continue;
			if (node.x == this.width - 1) continue;
			if (node.y == this.height - 1) continue;

			if (node.blockController == null) return node;
		}
		Debug.LogError("Node can't found");
		return null;
	}
	protected virtual void LinkNodeBlock(Node node, BlockController blockController)
	{
		blockController.blockData.SetNode(node);
		node.blockController = blockController;
	}	
}