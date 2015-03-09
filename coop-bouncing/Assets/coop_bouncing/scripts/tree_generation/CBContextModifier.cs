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
		set
		{
			mRotation = value; 
		}
	}

	public bool Flip
	{
		get
		{
			return mFlip; 
		}
		set
		{
			mFlip = value; 
		}
	}

	public CBContextModifier(float aRotation = 0f, bool aFlip = false)
	{
		mRotation = aRotation;
		mFlip = aFlip;
	}

	private float mRotation;
	private bool mFlip;
}
