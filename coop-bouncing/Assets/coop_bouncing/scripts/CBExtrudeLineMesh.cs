using UnityEngine;
using System.Collections;

public class CBExtrudeLineMesh : MonoBehaviour 
{
	public Mesh LineMesh;
	public MeshFilter MeshFilter;
	private Mesh mMesh = null;

	void Awake()
	{
		ExtrudeLineMesh();
	}

	void ExtrudeLineMesh () 
	{
		mMesh = new Mesh();
		mMesh.vertices = new Vector3[LineMesh.vertexCount + 2];
		mMesh.uv = new Vector2[LineMesh.vertexCount + 2];

		// create vertices
		int vertexIndex;
		for(vertexIndex = 0; vertexIndex < LineMesh.vertexCount; ++vertexIndex)
		{
			mMesh.vertices[vertexIndex] = LineMesh.vertices[vertexIndex];
		}
		Vector3 firstVertex = mMesh.vertices[0];
		Vector3 lastVertex = mMesh.vertices[vertexIndex - 1];
		firstVertex.x += 20f;
		mMesh.vertices[vertexIndex++] = firstVertex;
		lastVertex.x += 20f;
		mMesh.vertices[vertexIndex++] = lastVertex;

		// create uv's
		int uvIndex;
		for(uvIndex = 0; uvIndex < LineMesh.vertexCount; ++uvIndex)
		{
			mMesh.uv[uvIndex] = LineMesh.uv[uvIndex];
		}
		mMesh.uv[uvIndex++] = mMesh.uv[0];
		mMesh.uv[uvIndex++] = mMesh.uv[0];

		// create triangles
		mMesh.triangles = new int[mMesh.vertexCount * 3];
		int triangleTripleIndex = 0;
		vertexIndex = 1;
		for(; vertexIndex < mMesh.vertexCount - 1 && triangleTripleIndex < mMesh.vertexCount * 3 - 2; 
		    triangleTripleIndex += 3, vertexIndex += 1)
		{
			mMesh.triangles[triangleTripleIndex] = vertexIndex;
			mMesh.triangles[triangleTripleIndex + 1] = vertexIndex + 1;
			mMesh.triangles[triangleTripleIndex + 2] = 0;
		}

		MeshFilter.sharedMesh = mMesh;
	}
}
