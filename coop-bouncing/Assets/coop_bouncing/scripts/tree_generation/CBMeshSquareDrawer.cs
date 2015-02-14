using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CBMeshSquareDrawer : MonoBehaviour 
{
	private MeshFilter mMeshFilter;

	private List<Vector3> mVertices;
	private List<int> mTriangles;
	private List<Vector3> mNormals;
	private List<Vector2> mUVs;

	private List<CBSquare> mSquares;

	// y mVerticalMotion x -mHorizontalMotion s mSizeIncrease2  r mChildRotation flip mFlip
	public void AddSquare(float aHorizontalOffset, float aVerticalOffset, float aSizeRatio,
	                      float aRotation, bool aIsFlipped)
	{
		// get the parent
		CBSquare parent = null;
		if(mSquares.Count > 0)
		{
			parent = mSquares[mSquares.Count - 1];
		}

		// create the child
		Rect childrenCoordinates = new Rect(0f, 1f, 1f, 1f);
		if(parent != null)
		{
			childrenCoordinates = new Rect(parent.Coordinates);
		}
		childrenCoordinates.center += new Vector2(aHorizontalOffset, aVerticalOffset);
		childrenCoordinates.size *= aSizeRatio;
		CBSquare child = new CBSquare(childrenCoordinates);

		// add the vertices
		int vertexIndex = mVertices.Count;
		mVertices.Add(new Vector3(childrenCoordinates.xMin, childrenCoordinates.yMin, 0f));
		mVertices.Add(new Vector3(childrenCoordinates.xMax, childrenCoordinates.yMin, 0f));
		mVertices.Add(new Vector3(childrenCoordinates.xMin, childrenCoordinates.yMax, 0f));
		mVertices.Add(new Vector3(childrenCoordinates.xMax, childrenCoordinates.yMax, 0f));

		// add the triangles
		mTriangles.Add(vertexIndex);
		mTriangles.Add(vertexIndex + 2);
		mTriangles.Add(vertexIndex + 1);

		mTriangles.Add(vertexIndex + 1);
		mTriangles.Add(vertexIndex + 2);
		mTriangles.Add(vertexIndex + 3);

		// create normals facing the screen
		mNormals.Add(Vector3.forward);
		mNormals.Add(Vector3.forward);
		mNormals.Add(Vector3.forward);
		mNormals.Add(Vector3.forward);

		// uvs
		mUVs.Add(new Vector2 (0,0));
		mUVs.Add(new Vector2 (1,0));
		mUVs.Add(new Vector2 (0,1));
		mUVs.Add(new Vector2 (1,1));

		// push the child
		mSquares.Add(child);
	}

	public void UpdateMesh ()
	{
		Mesh mesh = mMeshFilter.mesh;
		
		mesh.vertices = mVertices.ToArray();
		mesh.triangles = mTriangles.ToArray();
		mesh.normals = mNormals.ToArray();
		mesh.uv = mUVs.ToArray();
	}

	private void Awake () 
	{
		mMeshFilter = GetComponent<MeshFilter> ();
		Mesh mesh = new Mesh ();
		mMeshFilter.mesh = mesh;

		mVertices = new List<Vector3>();
		mTriangles = new List<int>();
		mNormals = new List<Vector3>();
		mUVs = new List<Vector2>();
		mSquares = new List<CBSquare>();
	}

	private void Start ()
	{
		AddSquare(0f, 0f, 1f, 0f, false);
		for(int i = 0; i < 10; ++i)
		{
			AddSquare(0f, 1f, 0.95f, 0f, false);
		}
		UpdateMesh ();
	}

}
