using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : Spawner
{
	[Header("Block")]
	private static BlockSpawner instance;
	public static BlockSpawner Instance => instance;

	public static string BLOCK = "Block";

	public BlocksProfile blocksProfile;

	protected override void Awake()
	{
		base.Awake();
		if (BlockSpawner.instance != null) Debug.LogError("Only 1 BlocksSpawner allow to exist");
		BlockSpawner.instance = this;
	}

	protected override void LoadComponents()
	{
		base.LoadComponents();
		this.LoadBlockProfile();
	}

	protected virtual void LoadBlockProfile()
	{
		if (this.blocksProfile != null) return;
		this.blocksProfile = Resources.Load<BlocksProfile>("Pikachu");
		Debug.Log(transform.name + "LoadBlockProfile", gameObject);
	}
}
