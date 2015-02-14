using UnityEngine;
using System.Collections;

public class SimpleTriangle : MonoBehaviour 
{	
	public Vector3 a = new Vector3(0,0);
	public Vector3 b = new Vector3(0,0);
	public Vector3 c = new Vector3(0,0);

	void drawTriangle(Vector3 a, Vector3 b, Vector3 c)
	{
		//MeshFilter meshFilter = new MeshFilter ();//  (MeshFilter)gameObject.AddComponent("MeshFilter");
		MeshRenderer meshRenderer = new MeshRenderer (); //(MeshRenderer)gameObject.AddComponent("MeshRenderer");
		MeshFilter mf = (MeshFilter)transform.GetComponent<MeshFilter>();
		Mesh mesh = mf.mesh;
		mesh.Clear();
		mesh.vertices = new Vector3[] {new Vector3(a.x, a.y, a.z), new Vector3(b.x, b.y, b.z), new Vector3(c.x, c.y, c.z)};
		mesh.uv = new Vector2[] {new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1)};
		mesh.triangles = new int[] {0, 1, 2};
	}

	// Use this for initialization
	void Start () 
	{
		Debug.Log ("*********************");
		Debug.Log (transform.lossyScale);
		drawTriangle (a*transform.localScale.x, b*transform.localScale.y, c*transform.localScale.z);
	}
}
