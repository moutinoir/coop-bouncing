using UnityEngine;
using System.Collections;

public class CBBall : MonoBehaviour 
{
	public CBBouncingMotion BouncingMotion;

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

	void Awake ()
	{
		mIsFree = true;
		mInitialParent = Transform.parent;
	}

	public void RemoveFreedom ()
	{
		mIsFree = false;
		BouncingMotion.StopMotion();
	}

	public void RegainFreedom (Vector3 aForceDirection, float aForcePower)
	{
		Transform.parent = mInitialParent;
		BouncingMotion.AddForce (aForceDirection, aForcePower);
		mIsFree = true;
	}
}
