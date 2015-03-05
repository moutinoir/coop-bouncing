using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CBMeshSquareDrawer : MonoBehaviour 
{
	public float FirstSquareSize = 0.5f;
	private MeshFilter mMeshFilter;

	private List<Vector3> mVertices;
	private List<int> mTriangles;
	private List<Vector3> mNormals;
	private List<Vector2> mUVs;

	private List<CBSquare> mSquares;

	// y mVerticalMotion x -mHorizontalMotion s mSizeIncrease2  r mChildRotation flip mFlip
	public CBSquare AddSquare(float aHorizontalOffset, float aVerticalOffset, float aSizeRatio,
	                      float aRotation, bool aIsFlipped, CBSquare aParent, CBContextModifier aContextModifier)
	{
		// get the parent
		CBSquare parent = aParent;

		// flip
		bool flip = false;
		if(parent != null)
		{
			flip = parent.Flip;
		}
		float flipFactor = 1f;
		if(flip)
		{
			flipFactor = -1f;
		}

		// create the child
		Vector2 center = Vector2.zero;
		Vector2 size = Vector2.one*FirstSquareSize;
		Vector3 orientation = Vector3.zero;
		if(parent != null)
		{
			center = parent.Rotate(aHorizontalOffset*parent.Size.x*aSizeRatio,
			                       aVerticalOffset*parent.Size.y*aSizeRatio);
			size = new Vector2(parent.Size.x, parent.Size.y) * aSizeRatio;

			// orientation
			orientation = parent.Orientation;
			float contextOrientation = 0f;
			if(aContextModifier != null)
			{
				contextOrientation += aContextModifier.Rotation;
			}
			orientation += new Vector3(0f, 0f, aRotation + contextOrientation)*flipFactor;
			if(orientation.z > Mathf.PI)
			{
				orientation.z -= 2f*Mathf.PI;
			}
			else if(orientation.z < -Mathf.PI)
			{
				orientation.z += 2f*Mathf.PI;
			}
		}

		// update the flip
		if(aIsFlipped)
		{
			flip = !flip;
		}

		CBSquare child = new CBSquare(center, size, orientation, flip);

		// add the vertices
		int vertexIndex = mVertices.Count;
		mVertices.Add(new Vector3(child.BottomLeft.x, child.BottomLeft.y, 0f));
		mVertices.Add(new Vector3(child.BottomRight.x, child.BottomRight.y, 0f));
		mVertices.Add(new Vector3(child.TopLeft.x, child.TopLeft.y, 0f));
		mVertices.Add(new Vector3(child.TopRight.x, child.TopRight.y, 0f));

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
		return child;
	}

	public void UpdateMesh ()
	{
		Mesh mesh = mMeshFilter.mesh;
		mesh.Clear ();
		
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

	public void Reset ()
	{

		mVertices.Clear ();
		mTriangles.Clear ();
		mNormals.Clear();
		mUVs.Clear ();
		mSquares.Clear ();
	}

	private void Start ()
	{
		/*AddSquare(0f, 0f, 1f, 0f, false);
		for(int i = 0; i < 10; ++i)
		{
			AddSquare(0f, 1.2f, 0.95f, Mathf.PI/24, false);
		}
		UpdateMesh ();*/
	}

}
