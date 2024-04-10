using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : Spawner
{
	[Header("Block")]
	private static BlockSpawner instance;
	public static BlockSpawner Instance => instance;

	public static string BLOCK = "Block";
	public static string HOLDER = "BlockHolder";
	public static string LINKER = "Linker";

	protected override void Awake()
	{
		base.Awake();
		if (BlockSpawner.instance != null) Debug.LogError("Only 1 BlocksSpawner allow to exist");
		BlockSpawner.instance = this;
	}

	
}
