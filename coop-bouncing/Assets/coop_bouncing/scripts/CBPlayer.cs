using UnityEngine;
using System.Collections;

public class CBPlayer : MonoBehaviour 
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

	public enum EPlayerControl
	{
		Controller1,
		Controller2,
	}
	public EPlayerControl PlayerControl;
}
