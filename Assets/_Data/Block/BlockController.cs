using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : IT4MonoBehaviour
{
    [Header("Block Controller")]
    public SpriteRenderer sprite;
    public BlockData blockData;
    public List<BlockController> neighbors = new List<BlockController>(); //TODO: how to get this??
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadModel();
        this.LoadBlockData();
    }

    protected virtual void LoadModel()
    {
        if (this.sprite != null) return;
        Transform model = transform.Find("Model");
        this.sprite = model.GetComponent<SpriteRenderer>();
        Debug.Log(transform.name + "LoadModel", gameObject);
    }

	protected virtual void LoadBlockData()
	{
		if (this.blockData != null) return;
		this.blockData = transform.Find("BlockData").GetComponent<BlockData>();
		Debug.LogWarning(transform.name + "LoadBlockData", gameObject);
	}

}
