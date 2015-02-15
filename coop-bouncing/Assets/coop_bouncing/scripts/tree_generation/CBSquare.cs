using UnityEngine;
using System.Collections;

public class CBSquare 
{
	public Quaternion Orientation
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
			return new Vector2(mCenter.x + (-mSize.x/2f) * Mathf.Cos(mOrientation.eulerAngles.z) 
				- (-mSize.y/2f) * Mathf.Sin(mOrientation.eulerAngles.z),
			                   mCenter.y + (-mSize.x/2f) * Mathf.Sin(mOrientation.eulerAngles.z) 
			                   + (-mSize.y/2f) * Mathf.Cos(mOrientation.eulerAngles.z));
		}
	}

	public Vector2 BottomRight
	{
		get
		{
			return new Vector2(mCenter.x + (mSize.x/2f) * Mathf.Cos(mOrientation.eulerAngles.z) 
			                   - (-mSize.y/2f) * Mathf.Sin(mOrientation.eulerAngles.z),
			                   mCenter.y + (mSize.x/2f) * Mathf.Sin(mOrientation.eulerAngles.z) 
			                   + (-mSize.y/2f) * Mathf.Cos(mOrientation.eulerAngles.z));
		}
	}

	public Vector2 TopRight
	{
		get
		{
			return new Vector2(mCenter.x + (mSize.x/2f) * Mathf.Cos(mOrientation.eulerAngles.z) 
				- (mSize.y/2f) * Mathf.Sin(mOrientation.eulerAngles.z),
			                   mCenter.y + (mSize.x/2f) * Mathf.Sin(mOrientation.eulerAngles.z) 
			                   + (mSize.y/2f) * Mathf.Cos(mOrientation.eulerAngles.z));
		}
	}

	public Vector2 TopLeft
	{
		get
		{
			return new Vector2(mCenter.x + (-mSize.x/2f) * Mathf.Cos(mOrientation.eulerAngles.z) 
			                   - (mSize.y/2f) * Mathf.Sin(mOrientation.eulerAngles.z),
			                   mCenter.y + (-mSize.x/2f) * Mathf.Sin(mOrientation.eulerAngles.z) 
			                   + (mSize.y/2f) * Mathf.Cos(mOrientation.eulerAngles.z));
		}
	}

	/*public Rect Coordinates
	{
		get 
		{
			return mCoordinates;
		}
	}

	public Quaternion Orientation
	{
		get
		{
			return mOrientation;
		}
	}

	public Transform Transform
	{
		get
		{
			return mTransform;
		}
	}
	
	public CBSquare (Rect aCoordinates)//, Quaternion aOrientation)
	{
		mCoordinates = aCoordinates;
		//mOrientation = aOrientation;
	}

	public CBSquare (Transform aTransform)
	{
		mTransform = aTransform;
	}

	private Rect mCoordinates;
	private Quaternion mOrientation;
	private Transform mTransform;*/

	public CBSquare (Vector2 aCenter, Vector2 aSize, Quaternion aOrientation)
	{
		mCenter = aCenter;
		mSize = aSize;
		mOrientation = aOrientation;
	}

	private Vector2 mCenter;
	private Vector2 mSize;
	private Quaternion mOrientation;
}
