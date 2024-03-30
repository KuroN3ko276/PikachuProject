using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class Node
{
	public float x = 0;
	public float y = 0;
	public float posX=0;
	public int weight = 1;
	public bool occupied = false;   //có hình ảnh hay không
	public Node up;
	public Node down;
	public Node left;
	public Node right;
}

