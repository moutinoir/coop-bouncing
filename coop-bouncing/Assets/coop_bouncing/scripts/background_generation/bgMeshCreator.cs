using UnityEngine;
using System.Collections;

public class bgMeshCreator : MonoBehaviour {

	public CurvySpline topSpline;
	public CurvySpline bottomSpline;

	// Use this for initialization
	void Start () 
	{
		MeshFilter mf = GetComponent<MeshFilter> ();
		Mesh mesh = new Mesh ();
		mf.mesh = mesh;

		// really, we should probably just force this to be the same
		int numControlPoints = Mathf.Min (bottomSpline.ControlPoints.Count, topSpline.ControlPoints.Count);

		// Vertices
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

		// Normals (1 normal per vertex)
		Vector3[] normals = new Vector3[numControlPoints * 2];
		for (int i = 0; i < numControlPoints * 2; i++) 
		{
			normals[i] = -Vector3.forward;
		}

		// UVs (1 uv per vertex)
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
