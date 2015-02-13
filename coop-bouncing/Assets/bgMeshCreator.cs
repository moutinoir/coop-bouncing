using UnityEngine;
using System.Collections;

public class bgMeshCreator : MonoBehaviour {

	public CurvySpline topSpline;
	public CurvySpline bottomSpline;
	
	public float width = 50;
	public float height = 50;

	// Use this for initialization
	void Start () 
	{
		MeshFilter mf = GetComponent<MeshFilter> ();
		Mesh mesh = new Mesh ();
		mf.mesh = mesh;

		Debug.Log (topSpline.ControlPoints.Count);
		Debug.Log (bottomSpline.ControlPoints.Count);


		int numControlPoints = Mathf.Min (bottomSpline.ControlPoints.Count, topSpline.ControlPoints.Count);
		// Vertices

		/*{
			bottomSpline.ControlPoints[0].Position,
			bottomSpline.ControlPoints[1].Position,
			topSpline.ControlPoints[0].Position,
			topSpline.ControlPoints[1].Position,
			bottomSpline.ControlPoints[2].Position,
			bottomSpline.ControlPoints[3].Position,
			topSpline.ControlPoints[2].Position,
			topSpline.ControlPoints[3].Position,
			bottomSpline.ControlPoints[0].Position,
			bottomSpline.ControlPoints[1].Position,
			topSpline.ControlPoints[0].Position,
			topSpline.ControlPoints[1].Position,
			bottomSpline.ControlPoints[2].Position,
			bottomSpline.ControlPoints[3].Position,
			topSpline.ControlPoints[2].Position,
			topSpline.ControlPoints[3].Position
		};*/

		Vector3[] vertices = new Vector3[numControlPoints * 2];

		int arrayWalk = 0;

		for (int i = 0; i < numControlPoints; i+=2) 
		{
			vertices[arrayWalk++] = bottomSpline.ControlPoints[i].Position;
			vertices[arrayWalk++] = topSpline.ControlPoints[i].Position;
			vertices[arrayWalk++] = bottomSpline.ControlPoints[i+1].Position;
			vertices[arrayWalk++] = topSpline.ControlPoints[i+1].Position;
		}

		// Tris
		arrayWalk = 0;
		int[] tri = new int[numControlPoints*6];
		for (int i = 0; i < numControlPoints; i+=2) 
		{
			//Debug.Log("i: " + i);
			//Debug.Log("arraywalk:" + arrayWalk);
			tri[arrayWalk++] = i;
			tri[arrayWalk++] = i+1;
			tri[arrayWalk++] = i+2;
			tri[arrayWalk++] = i+1;
			tri[arrayWalk++] = i+3;
			tri[arrayWalk++] = i+2;
		}

		// Triangles
		/*
		tri [0] = 0;
		tri [1] = 2;
		tri [2] = 1; //
		tri [3] = 2;
		tri [4] = 3;
		tri [5] = 1; //
		tri [6] = 4;
		tri [7] = 1;
		tri [8] = 3; //
		tri [9] = 3;
		tri [10] = 6;
		tri [11] = 4; //
		tri [12] = 4;
		tri [13] = 6;
		tri [14] = 5; //
		tri [15] = 6;
		tri [16] = 7;
		tri [17] = 5;*/

		// Normals
		Vector3[] normals = new Vector3[numControlPoints * 2];
		for (int i = 0; i < numControlPoints * 2; i++) 
		{
			normals[i] = -Vector3.forward;
		}

		// UVs
		Vector2[] uvs = new Vector2[numControlPoints * 2];
		arrayWalk = 0;
		for (int i = 0; i < numControlPoints/2; i++) 
		{
			uvs[arrayWalk++] = new Vector2 (0,0);
			uvs[arrayWalk++] = new Vector2 (1,0);
			uvs[arrayWalk++] = new Vector2 (0,1);
			uvs[arrayWalk++] = new Vector2 (1,1);
		}

		// Assign
		mesh.vertices = vertices;
		mesh.triangles = tri;
		mesh.normals = normals;
		mesh.uv = uvs;
	}

}
