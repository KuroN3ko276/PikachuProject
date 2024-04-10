using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class Node
{
	public int x = 0;
	public int y = 0;
	public float posX=0;
	public float posY=0;
	public int weight = 1;
	public bool occupied = false;   //có hình ảnh hay không
	public int nodeId;
	public Node up;
	public Node down;
	public Node left;
	public Node right;
	public BlockController blockController;
	public NodeTransform nodeTranfrom;

	public virtual List<Node> Neighbors
	{
		get
		{
			List<Node> nodes = new List<Node>();
			nodes.Add(up);
			nodes.Add(right);
			nodes.Add(down);
			nodes.Add(left);
			return nodes;
		}
	}
}

