using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManagerController : IT4MonoBehaviour
{
	[Header("GridSystemController")]
	private static GridManagerController instance;
	public static GridManagerController Instance => instance;

	public BlockSpawner blockSpawner;
	public IPathfinding pathFinding;
	public BlockController firstBlock;
	public BlockController lastBlock;

	protected override void Awake()
	{
		base.Awake();
		if (GridManagerController.instance != null) Debug.LogError("Only 1 GridManagerController allow to exist");
		GridManagerController.instance = this;
	}

	protected override void LoadComponents()
	{
		base.LoadComponents();
		this.LoadSpawner();
		this.LoadPathfinding();
	}

	protected virtual void LoadPathfinding()
	{
		if (this.pathFinding != null) return;
		this.pathFinding = transform.GetComponentInChildren<IPathfinding>();
		Debug.Log(transform.name + "pathFinding", gameObject);
	}
	protected virtual void LoadSpawner()
	{
		if (this.blockSpawner != null) return;
		this.blockSpawner = transform.Find("BlockSpawner").GetComponent<BlockSpawner>();
		Debug.Log(transform.name + "LoadSpawner", gameObject);
	}
	
	public virtual void SetNode(BlockController blockCtrl)
	{
		if(this.firstBlock != null && this.lastBlock != null)
		{
			this.pathFinding.FindPath(this.firstBlock, this.lastBlock);
			this.firstBlock = null;
			this.lastBlock = null;
			Debug.Log("Reset Blocks");
			return;
		}
		if(this.firstBlock==null)
		{
			this.firstBlock = blockCtrl;
			return;
		}

		this.lastBlock = blockCtrl;

	}
}
