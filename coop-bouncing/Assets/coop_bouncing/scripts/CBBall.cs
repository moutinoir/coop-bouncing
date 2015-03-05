using UnityEngine;
using System.Collections;

public class CBBall : MonoBehaviour 
{
	public CBBouncingMotion mBouncingMotion;

	public CBBall()
	{
		mIsAtBadAngle = false;
	}

	public bool Free
	{
		get
		{
			return mIsFree;
		}
	}

	public Transform Transform
	{
		get
		{
			if (!mTransform)
				mTransform = transform;
			return mTransform;
		}
	}
	Transform mTransform;
	private Transform mInitialParent;
	private bool mIsFree;
	
	public bool mIsAtBadAngle;

	void Awake ()
	{
		mIsFree = true;
		mIsAtBadAngle = false;
		mInitialParent = Transform.parent;
	}

	public void RemoveFreedom ()
	{
		mIsFree = false;
		mBouncingMotion.StopMotion();
	}

	public void RegainFreedom (Vector3 aForceDirection, float aForcePower)
	{
		Transform.parent = mInitialParent;
		mBouncingMotion.AddForce (aForceDirection, aForcePower);
		mIsFree = true;
	}

	public void Push (Vector3 aDirection)
	{
		if(Free)
		{
			mBouncingMotion.Push(aDirection);
		}
	}

	void Update ()
	{
		if(mIsAtBadAngle)
			GetComponentInChildren<SpriteRenderer>().color = Color.red;
		else
			GetComponentInChildren<SpriteRenderer>().color = Color.green;
	}
}
