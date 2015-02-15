using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// TODO:
// Bendy Limits to restrict max bent
// Work on "less random" nodules
public class GrassTest : MonoBehaviour 
{
	public int minNodules = 15;
	public int maxNodules = 18;

	public float minBend = 10;
	public float maxBend = 20;

	public float minLength = 0.8f;
	public float maxLength = 1f;

	public float rootWidth = 0.15f;

	List<Vector2> vectorPoints = new List<Vector2>();
	List<Color> branchColors = new List<Color>(); 
	private int numNodules;


	Vector2 getPointOnCircle(Vector2 fromPoint, float degree, float radius)
	{
		float rad = degree * Mathf.Deg2Rad;
		float x = Mathf.Cos (rad);
		float y = Mathf.Sin (rad);
		Vector2 newOffset = new Vector2 (x, y) * radius;
		return newOffset + fromPoint;
	}

	float calculateWidthAtNodule(float nodule)
	{
		if(nodule == 1.0f)
			return 0.01f;
		else
			return rootWidth * (1.0f - nodule);
	}

	void generateNodules()
	{
		List<float> nodules = new List<float>();

		numNodules = Random.Range (minNodules, maxNodules);
		float grassLength = Random.Range (minLength, maxLength);

		nodules.Add(0.0f);

		for(int j = 0; j < numNodules; j++)
		{
			float nodPoint = Random.Range (0.0f, 1.0f);
			nodules.Add(nodPoint);
		}
		
		nodules.Add (1.0f);
		nodules.Sort();

		float desiredAngle = 90.0f;

		// add root points
		vectorPoints.Add (new Vector2 (-rootWidth,0.0f));
		vectorPoints.Add (new Vector2 (rootWidth,0.0f));

		Vector2 lastPoint = Vector2.zero;   // the last core point of the leaf

		for(int i = 1; i < nodules.Count; i++) 
		{
			float grassPartLength = grassLength*nodules[i];
			float newAngle = Random.Range(minBend, maxBend);
			desiredAngle += Random.Range (-newAngle, newAngle);
			Vector2 grassPoint = getPointOnCircle(lastPoint,desiredAngle,grassPartLength);
			lastPoint = grassPoint;

			Debug.Log ("nod *** " + nodules[i] + " ***");
			Debug.Log ("width *** " + calculateWidthAtNodule(nodules[i]) + " ***");
			Vector2 grassPointLeft  = new Vector2(grassPoint.x-calculateWidthAtNodule(nodules[i]), grassPoint.y);
			Vector2 grassPointRight = new Vector2(grassPoint.x+calculateWidthAtNodule(nodules[i]), grassPoint.y);

			vectorPoints.Add (grassPointLeft);
			vectorPoints.Add (grassPointRight);
		}

	}

	void generateBranchColors()
	{
		for(int i = 0; i < vectorPoints.Count; i++)
		{
			if(i%2 == 0)
				branchColors.Add(Color.red);
			else
				branchColors.Add(Color.white);			
		}
	}

	void generateMesh()
	{
		MeshFilter mf = GetComponent<MeshFilter> ();
		Mesh mesh = new Mesh();
		mf.mesh = mesh;

		Vector3[] vertices = new Vector3[vectorPoints.Count];
		int arrayWalk = 0;

		// Vertices
		foreach (Vector2 vector in vectorPoints)
		{
			vertices[arrayWalk++] = new Vector3(vector.x, vector.y, 0.0f);
		}

		// Tris
		arrayWalk = 0;
		int[] tri = new int[(numNodules+1)*6];

		for (int i = 0; i < (numNodules+1)*2; i+=2) 
		{
			tri[arrayWalk++] = i;
			tri[arrayWalk++] = i+2;
			tri[arrayWalk++] = i+1;
			tri[arrayWalk++] = i+2;
			tri[arrayWalk++] = i+3;
			tri[arrayWalk++] = i+1;
		}
		
		// Normals (1 normal per vertex)
		Vector3[] normals = new Vector3[vertices.Length];

		for (int i = 0; i < normals.Length; i++) 
		{
			normals[i] = -Vector3.forward;
		}
		
		// UVs (1 uv per vertex)
		Vector2[] uvs = new Vector2[(numNodules+1)*2];
		arrayWalk = 0;

		for (int i = 0; i < uvs.Length/4; i++)
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

	// Use this for initialization
	void Start () 
	{
		generateNodules ();
		generateBranchColors ();
		generateMesh();
	}
	
	// Update is called once per frame
	void Update () 
	{
		/*for (int i = 1; i < vectorPoints.Count-1; i++) 
		{
			Debug.Log ("i: " + i);
			Debug.DrawLine(vectorPoints[i-1],  vectorPoints[i], branchColors[i]);
		}*/
	}
}
