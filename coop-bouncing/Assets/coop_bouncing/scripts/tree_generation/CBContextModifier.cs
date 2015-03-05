using UnityEngine;
using System.Collections;

public class CBContextModifier
{
	public float Rotation
	{
		get
		{
			return mRotation; 
		}
	}

	public CBContextModifier(float aRotation)
	{
		mRotation = aRotation;
	}

	private float mRotation;
}
