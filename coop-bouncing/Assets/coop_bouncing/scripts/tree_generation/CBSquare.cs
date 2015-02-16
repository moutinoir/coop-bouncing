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
			return Rotate(-mSize.x/2f, -mSize.y/2f/*, mCenter*/);
				/*new Vector2(mCenter.x + (-mSize.x/2f) * Mathf.Cos(mOrientation.eulerAngles.z) 
				- (-mSize.y/2f) * Mathf.Sin(mOrientation.eulerAngles.z),
			                   mCenter.y + (-mSize.x/2f) * Mathf.Sin(mOrientation.eulerAngles.z) 
			                   + (-mSize.y/2f) * Mathf.Cos(mOrientation.eulerAngles.z));*/
		}
	}

	public Vector2 BottomRight
	{
		get
		{
			return Rotate(mSize.x/2f, -mSize.y/2f/*, mCenter*/);
				/*new Vector2(mCenter.x + (mSize.x/2f) * Mathf.Cos(mOrientation.eulerAngles.z) 
			                   - (-mSize.y/2f) * Mathf.Sin(mOrientation.eulerAngles.z),
			                   mCenter.y + (mSize.x/2f) * Mathf.Sin(mOrientation.eulerAngles.z) 
			                   + (-mSize.y/2f) * Mathf.Cos(mOrientation.eulerAngles.z));*/
		}
	}

	public Vector2 TopRight
	{
		get
		{
			return Rotate(mSize.x/2f, mSize.y/2f/*, mCenter*/);
			/*new Vector2(mCenter.x + (mSize.x/2f) * Mathf.Cos(mOrientation.eulerAngles.z) 
				- (mSize.y/2f) * Mathf.Sin(mOrientation.eulerAngles.z),
			                   mCenter.y + (mSize.x/2f) * Mathf.Sin(mOrientation.eulerAngles.z) 
			                   + (mSize.y/2f) * Mathf.Cos(mOrientation.eulerAngles.z));*/
		}
	}

	public Vector2 TopLeft
	{
		get
		{
			return Rotate(-mSize.x/2f, mSize.y/2f/*, mCenter*/);
				/*new Vector2(mCenter.x + (-mSize.x/2f) * Mathf.Cos(mOrientation.eulerAngles.z) 
			                   - (mSize.y/2f) * Mathf.Sin(mOrientation.eulerAngles.z),
			                   mCenter.y + (-mSize.x/2f) * Mathf.Sin(mOrientation.eulerAngles.z) 
			                   + (mSize.y/2f) * Mathf.Cos(mOrientation.eulerAngles.z));*/
		}
	}

	public Vector2 Rotate(float aX, float aY/*, Vector2 aPivot*/)
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
			// flip the orientation
			/*float flipFactor = 1f;
			if(mFlip)
			{
				flipFactor = -1f;
			}*/

		return /*flipFactor**/mOrientation.z;
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
