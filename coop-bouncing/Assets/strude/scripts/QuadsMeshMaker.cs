using UnityEngine;
using System.Collections;

[ExecuteInEditMode()]
public class QuadsMeshMaker : MonoBehaviour 
{
	public int maxQuads;
	public float size;
	public float inDist;
	
	protected int currQuads = 0;
	
	protected Vector3[] points = null;
	protected Mesh mesh = null;
	
	
	public Rect[] GetRects()
	{
		Rect[] rects = new Rect[currQuads];
		for(int i=0; i < rects.Length; i++)
		{
			rects[i] =  Rect.MinMaxRect (points[i].x - size*0.5f,points[i].y + size*0.5f,points[i].x + size*0.5f,points[i].y - size*0.5f);
		}
	
		return rects;
	}
	
	public Vector3[] GetPoints()
	{
		return points;
	}
	
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	protected void init()
	{
		mesh = (GetComponent(typeof(MeshFilter)) as MeshFilter).mesh;
		points = new Vector3[maxQuads];
	}
	
	protected void makeMesh()
	{
		mesh.Clear();
		Vector3[] vertices = new Vector3[currQuads* 4];
		Color[] colors = new Color[currQuads * 4];
		Vector2[] uv = new Vector2[currQuads* 4];
		
		Vector3[] protoQuadV = new Vector3[4];
		protoQuadV[0] = new Vector3( -size*0.5f, -size*0.5f,0.0f);
		protoQuadV[1] = new Vector3( size*0.5f, -size*0.5f,0.0f);
		protoQuadV[2] = new Vector3( size*0.5f, size*0.5f,0.0f);
		protoQuadV[3] = new Vector3( -size*0.5f, size*0.5f,0.0f);
		
		Color[] protoQuadC = new Color[4];
		protoQuadC[0] = Color.white;
		protoQuadC[1] = Color.white;
		protoQuadC[2] = Color.white;
		protoQuadC[3] = Color.white;
		
		
		Vector2[] protoQuadT = new Vector2[4];
		protoQuadT[0] = new Vector2( 0.0f, 0.0f);
		protoQuadT[1] = new Vector2( 1.0f, 0.0f);
		protoQuadT[2] = new Vector2( 1.0f, 1.0f);
		protoQuadT[3] = new Vector2( 0.0f, 1.0f);
		
		int[] triangles = new int[currQuads*6]; 
		for(int i=0; i < currQuads; i++)
		{
			for(int j=0; j < 4; j++)
			{
				vertices[i*4 + j] = protoQuadV[j] + points[i];
				colors[i*4 + j] = protoQuadC[j];
				uv[i*4 + j] = protoQuadT[j];
			}
			triangles[i*6 + 0] = i*4 +  0;
			triangles[i*6 + 1] = i*4 +  1;
			triangles[i*6 + 2] = i*4 +  3;
			
			triangles[i*6 + 3] = i*4 +  1;
			triangles[i*6 + 4] = i*4 +  2;
			triangles[i*6 + 5] = i*4 +  3;
		}
		
		mesh.vertices = vertices;
		mesh.colors = colors;
		mesh.uv = uv;
		mesh.triangles = triangles;
		
			
	}
	
	
	protected void makeMeshXZ()
	{
		mesh.Clear();
		Vector3[] vertices = new Vector3[currQuads* 4];
		Color[] colors = new Color[currQuads * 4];
		Vector2[] uv = new Vector2[currQuads* 4];
		
		Vector3[] protoQuadV = new Vector3[4];
		protoQuadV[0] = new Vector3( -size*0.5f, 0.0f,-size*0.5f);
		protoQuadV[1] = new Vector3( size*0.5f, 0.0f,-size*0.5f);
		protoQuadV[2] = new Vector3( size*0.5f, 0.0f,size*0.5f);
		protoQuadV[3] = new Vector3( -size*0.5f,0.0f ,size*0.5f);
		
		Color[] protoQuadC = new Color[4];
		protoQuadC[0] = Color.white;
		protoQuadC[1] = Color.white;
		protoQuadC[2] = Color.white;
		protoQuadC[3] = Color.white;
		
		
		Vector2[] protoQuadT = new Vector2[4];
		protoQuadT[0] = new Vector2( 0.0f, 0.0f);
		protoQuadT[1] = new Vector2( 1.0f, 0.0f);
		protoQuadT[2] = new Vector2( 1.0f, 1.0f);
		protoQuadT[3] = new Vector2( 0.0f, 1.0f);
		
		int[] triangles = new int[currQuads*6]; 
		for(int i=0; i < currQuads; i++)
		{
			for(int j=0; j < 4; j++)
			{
				vertices[i*4 + j] = protoQuadV[j] + points[i];
				colors[i*4 + j] = protoQuadC[j];
				uv[i*4 + j] = protoQuadT[j];
			}
			triangles[i*6 + 0] = i*4 +  0;
			triangles[i*6 + 1] = i*4 +  1;
			triangles[i*6 + 2] = i*4 +  3;
			
			triangles[i*6 + 3] = i*4 +  1;
			triangles[i*6 + 4] = i*4 +  2;
			triangles[i*6 + 5] = i*4 +  3;
		}
		
		mesh.vertices = vertices;
		mesh.colors = colors;
		mesh.uv = uv;
		mesh.triangles = triangles;
		
			
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
