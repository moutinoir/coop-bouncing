using UnityEngine;
using System.Collections;

public class CBBall : MonoBehaviour 
{
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

	void Awake ()
	{
		mInitialParent = Transform.parent;
	}

	public void RegainFreedom ()
	{
		Transform.parent = mInitialParent;
	}
}
