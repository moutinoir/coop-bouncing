using UnityEngine;
using System.Collections;

public class CBBall : MonoBehaviour 
{
	public GameObject holdingPlayer;

	//public CBBouncingMotion mBouncingMotion;
	public string lastHeldBy;

	public enum BallState { Held, OnSpline, InAir }  

	public BallState state = BallState.OnSpline;
	private Vector3 forceThrown;

	public CBBall()
	{
		mIsAtBadAngle = false;
		//mIsFree = true;
	}

	public void GrabBall(GameObject player)
	{
		SphereCollider sphere = gameObject.GetComponent<SphereCollider>();
		sphere.enabled = false;
		holdingPlayer = player;
		state = BallState.Held;
	}

	public void LobBall(Vector3 force, float power)
	{
		state = BallState.InAir;
		//mBouncingMotion.AddForce (force, power);
		//mIsFree = true;
	}

	/*
public void RegainFreedom (Vector3 aForceDirection, float aForcePower)
	{
		Transform.parent = mInitialParent;
		mBouncingMotion.AddForce (aForceDirection, aForcePower);
		Debug.Log ("force direction = " + aForceDirection + " , force power = " + aForcePower );
		mIsFree = true;
	}
	*/

	/*public bool Free
	{
		get
		{
			return mIsFree;
		}
	}*/

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
	//private bool mIsFree;
	
	public bool mIsAtBadAngle;

	void Awake ()
	{
		//mIsFree = true;
		mIsAtBadAngle = false;
		mInitialParent = Transform.parent;
	}

	public void RemoveFreedom ()
	{
		//mIsFree = false;
		//mBouncingMotion.StopMotion();
	}

	public void RegainFreedom (Vector3 aForceDirection, float aForcePower)
	{
		Transform.parent = mInitialParent;
		//mBouncingMotion.AddForce (aForceDirection, aForcePower);
		Debug.Log ("force direction = " + aForceDirection + " , force power = " + aForcePower );
		//mIsFree = true;
	}

	public void Push (Vector3 aDirection)
	{
		//if(Free)
		//{
			//mBouncingMotion.Push(aDirection);
		//}
	}

	void Update ()
	{
		if (state == BallState.Held) 
		{
			transform.position = holdingPlayer.transform.position;
		}

		if (state == BallState.InAir) 
		{

		}

		if(mIsAtBadAngle)
			GetComponentInChildren<SpriteRenderer>().color = Color.red;
		else
			GetComponentInChildren<SpriteRenderer>().color = Color.green;
	}
}
