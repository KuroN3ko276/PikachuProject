using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GridAbstract : IT4MonoBehaviour
{
	[Header("Grid Abstract")]
	public GridManagerController Controller;
	protected override void LoadComponents()
	{
		base.LoadComponents();
		this.LoadController();
	}

	protected virtual void LoadController()
	{
		if (this.Controller != null) return;
		this.Controller = transform.parent.GetComponent<GridManagerController>();
		Debug.LogWarning(transform.name + "LoadController", gameObject);
	}

}
