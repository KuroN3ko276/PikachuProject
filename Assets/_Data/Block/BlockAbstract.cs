using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAbstract : IT4MonoBehaviour
{
    [Header("Block Abstract")]
    public BlockController blockController;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadController();
    }

    protected virtual void LoadController()
    {
        if (this.blockController != null) return;
        this.blockController = transform.parent.GetComponent<BlockController>();
        Debug.Log(transform.name + "LoadController", gameObject);
    }

}
