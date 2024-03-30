using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : IT4MonoBehaviour
{
    public SpriteRenderer sprite;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadModel();
    }

    protected virtual void LoadModel()
    {
        if (this.sprite != null) return;
        Transform model = transform.Find("Model");
        this.sprite = model.GetComponent<SpriteRenderer>();
        Debug.Log(transform.name + "LoadModel", gameObject);
    }

}
