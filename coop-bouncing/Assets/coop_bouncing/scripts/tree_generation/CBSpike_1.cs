using UnityEngine;
using System.Collections;

public class CBSpike_1 : MonoBehaviour 
{
	public CBMeshSquareDrawer MeshSquareDrawer;
	public float StopGrowthSize = 0.2f;
	public float VerticalOffset = 1.2f;
	//public float TimeFactor = 1f;
	//public float SinScaleFactor = 0.5f;
	//public float SubSinFrequence = 3f;
	//public float SubSinWeight = 0.1f;
	
	private int mSeed;
	//private float mTimedAngle;
	
	private void FutureUpdate ()
	{
		//mTimedAngle += Time.deltaTime * TimeFactor;
		
		MeshSquareDrawer.Reset();
		Random.seed = mSeed;
		CBSquare initialSquare = MeshSquareDrawer.AddSquare(0f, 0f, 1f, 0f, false, null, null);
		Spikes(initialSquare);
		MeshSquareDrawer.UpdateMesh();
	}
	
	private void Start ()
	{
		mSeed = Random.Range(0, 2000);
		FutureUpdate ();
	}

	private void Spikes (CBSquare aParent)
	{
		CBContextModifier contextModifier2 = new CBContextModifier (Mathf.PI / 2f);
		CBContextModifier contextModifier3 = new CBContextModifier (2f * Mathf.PI / 2f);
		CBContextModifier contextModifier4 = new CBContextModifier (3f * Mathf.PI / 2f);

		Spike (aParent, null);
		Spike (aParent, contextModifier2);
		Spike (aParent, contextModifier3);
		Spike (aParent, contextModifier4);
	}

	private void Spike (CBSquare aParent, CBContextModifier aContextModifier)
	{
		if(aParent.Size.magnitude > StopGrowthSize)
		{
			float random = Random.Range(0f, 1f);
			if(random < 0.5f)
			{
				LSpike (aParent, aContextModifier);
			}
			else
			{
				aParent.Flip = !aParent.Flip;
				LSpike (aParent, aContextModifier);
			}
		}
	}

	private void LSpike (CBSquare aParent, CBContextModifier aContextModifier)
	{
		if(aParent.Size.magnitude > StopGrowthSize)
		{
			float random = Random.Range(0f, 1f);
			if(random < 0.99f)
			{
				LSpikeGetLonger (aParent, aContextModifier);
			}
			else
			{
				LSpikeBranch (aParent, aContextModifier);
			}
		}
	}
	
	private void LSpikeGetLonger (CBSquare aParent, CBContextModifier aContextModifier)
	{
		//float moveFactor = (MeshSquareDrawer.FirstSquareSize - aParent.Size.x) / (MeshSquareDrawer.FirstSquareSize - StopGrowthSize);

		CBSquare childSquare = MeshSquareDrawer.AddSquare(0f, VerticalOffset, 0.99f, Mathf.PI/180f, false, aParent, aContextModifier);
		LSpike(childSquare, null);
	}
	
	private void LSpikeBranch (CBSquare aParent, CBContextModifier aContextModifier)
	{
		CBContextModifier contextModifier1 = new CBContextModifier (Mathf.PI / 2f);
		CBContextModifier contextModifier2 = new CBContextModifier (-Mathf.PI / 2f);
		Spike (aParent, contextModifier1);
		Spike (aParent, contextModifier2);
		LSpike (aParent, null);
	}

	/*
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
	}*/

}
