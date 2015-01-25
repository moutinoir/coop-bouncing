using UnityEngine;
using System.Collections;
using FluffyUnderware.Curvy;

public class CBPathRenderer : MonoBehaviour 
{
	public CurvySpline Spline;
	private Mesh mMesh = null;

	void GenerateMesh(CurvySpline aSpline)
	{
//		mMesh = MeshHelper.CreateSplineMesh	(Spline, 2, false);
	}
}
