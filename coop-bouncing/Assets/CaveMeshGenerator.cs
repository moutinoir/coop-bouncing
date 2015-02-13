using UnityEngine;
using System.Collections;

public class CaveMeshGenerator : MonoBehaviour {

	public CurvySpline wallSpline;
	private Vector3[] outerPoints;
	
	public bool isUpper;   // else down
	
	// Use this for initialization
	void Start () 
	{
		MeshFilter mf = GetComponent<MeshFilter> ();
		Mesh mesh = new Mesh ();
		mf.mesh = mesh;

		int numControlPoints = wallSpline.ControlPoints.Count;
		outerPoints = new Vector3[wallSpline.ControlPoints.Count];

		// Generate out an invisible spline either above or below
		for (int i = 0; i < numControlPoints; i++) 
		{
			if(isUpper)
				outerPoints[i] = new Vector3(wallSpline.ControlPoints[i].Position.x,
				                             wallSpline.ControlPoints[i].Position.y+100,
				                             wallSpline.ControlPoints[i].Position.z);
			else
			{
				//Debug.Log ("hit right place");
				//Debug.Log ("numControlPoints");
				outerPoints[i] = new Vector3(wallSpline.ControlPoints[i].Position.x,
				                             wallSpline.ControlPoints[i].Position.y-100,
				                             wallSpline.ControlPoints[i].Position.z);
			}
		}


		// Vertices
		Vector3[] vertices = new Vector3[numControlPoints * 2];

		int arrayWalk = 0;

		for (int i = 0; i < numControlPoints; i+=2) 
		{
			if(isUpper)
			{
				vertices[arrayWalk++] = wallSpline.ControlPoints[i].Position;
				vertices[arrayWalk++] = outerPoints[i];
				vertices[arrayWalk++] = wallSpline.ControlPoints[i+1].Position;
				vertices[arrayWalk++] = outerPoints[i+1];
			}
			else
			{
				vertices[arrayWalk++] = outerPoints[i];
				vertices[arrayWalk++] = wallSpline.ControlPoints[i].Position;
				vertices[arrayWalk++] = outerPoints[i+1];
				vertices[arrayWalk++] = wallSpline.ControlPoints[i+1].Position;
			}
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
