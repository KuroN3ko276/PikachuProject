using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPathfinding
{
	public abstract void FindPath(BlockController startBlock, BlockController targetBlock);
}
