using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct IntVector2
{
	public IntVector2(uint x, uint y)
	{
		this.x = x;
		this.y = y;
	}

	public uint x;
	public uint y;
}

public class PierPathSolver : MonoBehaviour
{
	public enum NodeType
	{
		FREE = 1,
		END = 1001,
		OCCUPIED = 255
	}

	private IntVector2 startPosition;// = new IntVector2(0, 0); /* x,y */
	public IntVector2 StartPosition
	{
		set
		{
			startPosition = value;
		}
	}

	private NodeType[,] tiles;/*new uint[6,5] { 
		{1, 255, 1, 1, 1 },
		{1, 255, 1, 1, 1 },
		{1, 255, 1, 255, 1 },
		{1, 255, 1, 255, 1 },
		{1, 255, 1, 255, (uint)NodeType.END },
		{1, 1, 1, 1, 255 }
	};*/
	public NodeType[,] Tiles 
	{
		set 
		{
			tiles = value;
		}
	}

	//Example
	/*
	List<List<IntVector2>> solution = GetSolution ();
	foreach (List<IntVector2> list in solution) 
	{
		Debug.Log("LIST:");
		foreach (IntVector2 element in list) 
		{
				Debug.Log ("X:" + element.x + "  Y:" + element.y);
		}
		Debug.Log("END");
	}*/

	public List<List<IntVector2>> GetSolution()
	{
		return  Theodis.Algorithm.Pathfinder<IntVector2>.Dijkstra (startPosition, GetAdjacentNode, GetDistance, IsFinished, 255 /*maxnodedepth*/);
	}

	public List<IntVector2> GetAdjacentNode(IntVector2 node)
	{
		List<IntVector2> adjNodes = new List<IntVector2>();
		if (node.x < (tiles.GetLength (0) - 1))
		{
			IntVector2 downNode = new IntVector2(node.x + 1, node.y);
			if (tiles[downNode.x, downNode.y] != NodeType.OCCUPIED)
			{
				adjNodes.Add(downNode);
			}
		}
		if (node.x > 0)
		{
			IntVector2 upNode = new IntVector2(node.x - 1, node.y);
			if (tiles[upNode.x, upNode.y] != NodeType.OCCUPIED)
			{
				adjNodes.Add(upNode);
			}
		}
		if (node.y < (tiles.GetLength (1) - 1))
		{
			IntVector2 rightNode = new IntVector2(node.x, node.y + 1);
			if (tiles[rightNode.x, rightNode.y] != NodeType.OCCUPIED)
			{
				adjNodes.Add(rightNode);
			}
		}
		if (node.y > 0)
		{
			IntVector2 leftNode = new IntVector2(node.x, node.y - 1);
			if (tiles[leftNode.x, leftNode.y] != NodeType.OCCUPIED)
			{
				adjNodes.Add(leftNode);
			}
		}
		/*
		Debug.Log (" ----");
		Debug.Log ("passed node X:" + node.x + "  passed node Y:" + node.y);
		foreach (IntVector2 element in adjNodes) 
		{
			Debug.Log ("adjNodes X:" + element.x + "  adjNodes Y:" + element.y);
		}*/
		return adjNodes;
	}

	public double GetDistance(IntVector2 a, IntVector2 b)
	{
		return 1;
	}

	public Theodis.Algorithm.FinishedFlags IsFinished(IntVector2 node, double g)
	{
		if (tiles [node.x, node.y] == NodeType.END) 
		{
			return Theodis.Algorithm.FinishedFlags.FINISHED;
		}
		return Theodis.Algorithm.FinishedFlags.NOT_FOUND;
	}
}
