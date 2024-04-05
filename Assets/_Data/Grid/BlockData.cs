using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlockData : BlockAbstract
{
    [Header("BlockData")]
	public TextMeshPro text;
	public Color textColor = Color.red;
	public Node node;

	protected override void LoadComponents()
	{
		base.LoadComponents();
        this.LoadTextMeshPro();
	}

	protected virtual void LoadTextMeshPro()
	{
		if (this.text != null) return;
		this.text = GetComponentInChildren<TextMeshPro>();
		Debug.Log(transform.name + "LoadTextMeshPro", gameObject);
	}
	public virtual void SetNode(Node node)
    {
		this.node = node;
		//this.text.text = this.node.nodeId.ToString();
		this.text.color = textColor;
		this.text.text = this.node.x.ToString()+"\n"+this.node.y.ToString();
	
		
	}
	public virtual void SetColor(Color color)
	{
		this.text.color = color;
		Debug.Log("SetColor: "+this.blockController.name);
	}

    public virtual void SetSprite(Sprite sprite)
    {
        this.blockController.sprite.sprite = sprite;
    }
}
