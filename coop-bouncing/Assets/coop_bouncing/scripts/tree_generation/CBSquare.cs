using UnityEngine;
using System.Collections;

public class CBSquare 
{
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

	public Vector3 Orientation
	{
		get
		{
			return mOrientation;
		}
	}

	public Vector2 Center
	{
		get
		{
			return mCenter;
		}
	}

	public Vector2 Size
	{
		get
		{
			return mSize;
		}
	}

	public Vector2 BottomLeft
	{
		get
		{
			return Rotate(-mSize.x/2f, -mSize.y/2f);
		}
	}

	public Vector2 BottomRight
	{
		get
		{
			return Rotate(mSize.x/2f, -mSize.y/2f);
		}
	}

	public Vector2 TopRight
	{
		get
		{
			return Rotate(mSize.x/2f, mSize.y/2f);
		}
	}

	public Vector2 TopLeft
	{
		get
		{
			return Rotate(-mSize.x/2f, mSize.y/2f);
		}
	}

	public Vector2 Rotate(float aX, float aY)
	{
		return new Vector2(mCenter.x + aX * Mathf.Cos(ZOrientation) 
		                   - aY * Mathf.Sin(ZOrientation),
		                   mCenter.y + aX * Mathf.Sin(ZOrientation) 
		                   + aY * Mathf.Cos(ZOrientation));
	}

	public float ZOrientation
	{
		get
		{
			return mOrientation.z;
		}
	}

	public CBSquare (Vector2 aCenter, Vector2 aSize, Vector3 aOrientation, bool aFlip)
	{
		mCenter = aCenter;
		mSize = aSize;
		mOrientation = aOrientation;
		mFlip = aFlip;
	}

	private Vector2 mCenter;
	private Vector2 mSize;
	private Vector3 mOrientation;
	private bool mFlip;
}
