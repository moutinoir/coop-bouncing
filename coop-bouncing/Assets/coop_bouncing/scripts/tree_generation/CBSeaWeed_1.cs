using UnityEngine;
using System.Collections;

public class CBSeaWeed_1 : MonoBehaviour 
{
	public CBMeshSquareDrawer MeshSquareDrawer;
	public float StopGrowthSize = 0.2f;
	public float VerticalOffset = 1.2f;
	public float TimeFactor = 1f;
	public float SinScaleFactor = 0.5f;
	public float SubSinFrequence = 3f;
	public float SubSinWeight = 0.1f;

	private int mSeed;
	private float mTimedAngle;

	private void Update ()
	{
		mTimedAngle += Time.deltaTime * TimeFactor;

		MeshSquareDrawer.Reset();
		Random.seed = mSeed;
		CBSquare initialSquare = MeshSquareDrawer.AddSquare(0f, 0f, 1f, 0f, false, null, null);
		Grow(initialSquare);
		MeshSquareDrawer.UpdateMesh();
	}

	private void Start ()
	{
		mSeed = Random.Range(0, 2000);
		/*Random.seed = mSeed;
		CBSquare initialSquare = MeshSquareDrawer.AddSquare(0f, 0f, 1f, 0f, false, null);
		Grow(initialSquare);
		MeshSquareDrawer.UpdateMesh();*/
	}

	private void Grow (CBSquare aParent)
	{
		if(aParent.Size.magnitude > StopGrowthSize)
		{
			float random = Random.Range(0f, 1f);
			if(random < 0.88f)
			{
				GetLonger (aParent);
			}
			else if(random < 0.95f)
			{
				Branch (aParent);
			}
			else
			{
				Flip (aParent);
			}
		}
	}

	private void GetLonger (CBSquare aParent)
	{
		float moveFactor = (MeshSquareDrawer.FirstSquareSize - aParent.Size.x) / (MeshSquareDrawer.FirstSquareSize - StopGrowthSize);
		CBSquare childSquare = MeshSquareDrawer.AddSquare(0f, VerticalOffset, 0.97f, Mathf.PI/96 * GetMotionParameter(moveFactor), false, aParent, null);
		Grow(childSquare);
	}

	private void Branch (CBSquare aParent)
	{
		CBSquare childSquare1 = MeshSquareDrawer.AddSquare(0f, VerticalOffset, 0.97f, Mathf.PI/96 /** GetMotionParameter()*/, true, aParent, null);
		CBSquare childSquare2 = MeshSquareDrawer.AddSquare(0f, VerticalOffset, 0.6f, -Mathf.PI/3 /** GetMotionParameter()*/, true, aParent, null);
		CBSquare childSquare3 = MeshSquareDrawer.AddSquare(0f, VerticalOffset, 0.4f, Mathf.PI/3 /** GetMotionParameter()*/, false, aParent, null);
		Grow(childSquare1);
		Grow(childSquare2);
		Grow(childSquare3);
	}

	private float GetMotionParameter (float aMoveFactor)
	{
		float t = Random.Range(0f, 2*Mathf.PI) + mTimedAngle;

		float s1 = Mathf.Sin(t);
		float s2 = Mathf.Sin(t * SubSinFrequence) * SubSinWeight;

		return (s1 + s2) * SinScaleFactor * aMoveFactor + 1f;
			
	}

	private void Flip (CBSquare aParent)
	{
		aParent.Flip = !aParent.Flip;
		Grow(aParent);
	}


}
