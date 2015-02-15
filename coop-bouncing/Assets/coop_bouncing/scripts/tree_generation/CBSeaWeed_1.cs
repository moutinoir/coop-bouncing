using UnityEngine;
using System.Collections;

public class CBSeaWeed_1 : MonoBehaviour 
{
	public CBMeshSquareDrawer MeshSquareDrawer;
	public float StopGrowthSize = 0.2f;
	public float VerticalOffset = 1.2f;

	private void Start ()
	{
		CBSquare initialSquare = MeshSquareDrawer.AddSquare(0f, 0f, 1f, 0f, false, null);
		Grow(initialSquare);
		MeshSquareDrawer.UpdateMesh();
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
		CBSquare childSquare = MeshSquareDrawer.AddSquare(0f, VerticalOffset, 0.97f, Mathf.PI/96, false, aParent);
		Grow(childSquare);
	}

	private void Branch (CBSquare aParent)
	{
		CBSquare childSquare1 = MeshSquareDrawer.AddSquare(0f, VerticalOffset, 0.97f, Mathf.PI/96, false, aParent);
		CBSquare childSquare2 = MeshSquareDrawer.AddSquare(0f, VerticalOffset, 0.6f, -Mathf.PI/3, true, aParent);
		CBSquare childSquare3 = MeshSquareDrawer.AddSquare(0f, VerticalOffset, 0.4f, Mathf.PI/3, false, aParent);
		Grow(childSquare1);
		Grow(childSquare2);
		Grow(childSquare3);
	}

	private void Flip (CBSquare aParent)
	{
		aParent.Flip = !aParent.Flip;
		Grow(aParent);
	}


}
