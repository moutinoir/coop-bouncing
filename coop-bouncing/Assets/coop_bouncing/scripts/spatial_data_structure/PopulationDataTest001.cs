using UnityEngine;
using System.Collections;

public class PopulationDataTest001 : MonoBehaviour 
{
	public PopUtils.QuadTree QuadTree;
	
	void Awake () 
	{
		Vector2[] initData = new Vector2[]
		{
			new Vector2(2f, 2f),
			new Vector2(-2f, -2f),
			new Vector2(-2f, 2f),
			new Vector2(2f, -2f),
		};

		PopUtils.AABB boundaries = new PopUtils.AABB(new Vector2(0f, 0f), 10f);
		QuadTree = new PopUtils.QuadTree(boundaries);

		foreach(Vector2 data in initData)
		{
			QuadTree.Insert(new PopUtils.Point(data));
		}
	}
}
