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
	private float offsetY = 0;
	public BlocksProfile blocksProfile;
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
		this.blocksProfile = Resources.Load<BlocksProfile>("Pikachu");
		Debug.LogWarning(transform.name + "LoadBlockProfile", gameObject);
	}

	protected override void Start()
	{
		this.SpawnBlocks();
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
	protected virtual void SpawnNodes()
	{	
		Vector3 pos = new Vector3(0,0,0);
		foreach(Node node in this.nodes)
		{
			if (node.x == 0) continue;
			if (node.x == this.width-1) continue;
			if (node.y == 0) continue;
			if(node.y==this.height-1) continue;

			pos.x = node.posX;
			pos.y=node.y;
			Transform block = this.Controller.blockSpawner.Spawn(BlockSpawner.BLOCK, pos, Quaternion.identity);
			BlockController blockController = block.GetComponent<BlockController>();
			block.gameObject.SetActive(true);
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