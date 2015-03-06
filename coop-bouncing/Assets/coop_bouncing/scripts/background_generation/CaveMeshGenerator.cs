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


		int numControlPoints = wallSpline.ControlPointCount;
		Debug.Log ("numControlPoints: " + numControlPoints);
		//Debug.Log ("the control points: " + wallSpline.ControlPoints[wallSpline.ControlPointCount-1].Position);
		outerPoints = new Vector3[numControlPoints];

		/*
		// Generate out an invisible spline either above or below
		for (int i = 0; i < numControlPoints; i++) 
		{
			if(isUpper)
			{
				outerPoints[i] = new Vector3(wallSpline.ControlPoints[i].Position.x,
				                             wallSpline.ControlPoints[i].Position.y+5,
				                             wallSpline.ControlPoints[i].Position.z);
			}
			else
			{
				//Debug.Log ("hit right place");
				//Debug.Log ("numControlPoints");
				outerPoints[i] = new Vector3(wallSpline.ControlPoints[i].Position.x,
				                             wallSpline.ControlPoints[i].Position.y-5,
				                             wallSpline.ControlPoints[i].Position.z);
			}
		}
		*/


		// Vertices
		Vector3[] vertices = new Vector3[numControlPoints * 4];

		int arrayWalk = 0;

		for (int i = 0; i < numControlPoints-1; i++) 
		{
			Vector3 off = new Vector3(0.0f,isUpper ? 5.0f : -5.0f,0.0f);

			Vector3 p0 = wallSpline.ControlPoints[i].Position;
			Vector3 p1 = wallSpline.ControlPoints[i].Position+off;
			Vector3 p2 = wallSpline.ControlPoints[i+1].Position;
			Vector3 p3 = wallSpline.ControlPoints[i+1].Position+off;


			vertices[arrayWalk++] = p0;
			vertices[arrayWalk++] = p1;
			vertices[arrayWalk++] = p2;
			vertices[arrayWalk++] = p3;


		}
		
		// Tris
		arrayWalk = 0;
		int[] tri = new int[numControlPoints*6];
		for (int i = 0; i < numControlPoints-1; i++)
		{
			int index = i*4;
			if (isUpper)
			{
				tri[arrayWalk++] = index;
				tri[arrayWalk++] = index+1;
				tri[arrayWalk++] = index+2;

				tri[arrayWalk++] = index+2; 
				tri[arrayWalk++] = index+1;
				tri[arrayWalk++] = index+3;
			}else{

				tri[arrayWalk++] = index+2;
				tri[arrayWalk++] = index+1;
				tri[arrayWalk++] = index;
				
				tri[arrayWalk++] = index+3;
				tri[arrayWalk++] = index+1;
				tri[arrayWalk++] = index+2;

			}
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
		for (int i = 0; i < numControlPoints; i+=2) 
		{
			uvs[arrayWalk++] = new Vector2 (0,0);
			uvs[arrayWalk++] = new Vector2 (1,0);
			uvs[arrayWalk++] = new Vector2 (0,1);
			uvs[arrayWalk++] = new Vector2 (1,1);
		}
		
		// Assign
		mesh.vertices = vertices;
		mesh.triangles = tri;
		//mesh.normals = normals;
		//mesh.uv = uvs;
	}
}
