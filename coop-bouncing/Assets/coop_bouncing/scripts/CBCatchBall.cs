using UnityEngine;
using System.Collections;
using InControl;

public class CBCatchBall : MonoBehaviour 
{
	public CBPlayer player;

	public float intervalBetweenCatches = 0.1f;
	public CBBall mBall;
	private Vector3 mBallLocalPosition;
	
	private float mTimeSinceReleasedBall;

	public CBCatchBall()
	{

	}

	public CBBall Ball
	{
		get
		{
			return mBall;
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

	void Start()
	{
		mTimeSinceReleasedBall = intervalBetweenCatches;
	}

	void OnCollisionStay(Collision collision)
	{
		//Debug.Log ("collision enter");
		/*if(mBall.Free == true && mTimeSinceReleasedBall > intervalBetweenCatches)
		{
			ContactPoint first_contact = collision.contacts[0];
			//mCollisionDirection = first_contact.point - Transform.position;
			//mCollisionDirection.Normalize();

			CBBouncingMotion bouncingMotion = collision.gameObject.GetComponent<CBBouncingMotion>();

			if(bouncingMotion != null)
			{
				CBBall ball = bouncingMotion.mBall;

				//if(ball.Free)
				//{
				//	Debug.Log("INNER"  + ball.Free);
					//CatchBall(ball);
				//}
			}

			Debug.Log("[GAMEPLAY] " + Transform.parent.name + ":" + name + " collided with " 
			          + collision.transform.parent.name + ":" +  collision.transform.name + " at point " + first_contact.point);
		}	
		else if(mBall.Free == true)
		{
			CBBouncingMotion BouncingMotion = collision.gameObject.GetComponent<CBBouncingMotion>();
			if(BouncingMotion != null)
			{
				CBBall ball = BouncingMotion.mBall;
				//if(ball != null)
				//{
					//PushBall(ball);
				//}
			}
		}*/
	}

	/*void OnTriggerEnter(Collider other)
	{ 
		//Debug.Log ("trigger enter");
		if(mBall == null && mTimeSinceReleasedBall > IntervalBetweenCatches)
		{
			Transform other_transform = other.transform;
			Vector3 other_position = other_transform.position;
			mCollisionDirection = other_position - Transform.position;
			mCollisionDirection.Normalize();
			
			CBBouncingMotion BouncingMotion = other.gameObject.GetComponent<CBBouncingMotion>();
			if(BouncingMotion != null)
			{
				CBBall ball = BouncingMotion.Ball;
				if(ball.Free)
				{
					CatchBall(ball);
				}
			}
			
			//Debug.Log("[GAMEPLAY] " + Transform.parent.name + ":" + name + " triggered with " 
			//          + other_transform.parent.name + ":" +  other_transform.name + " at direction " + mCollisionDirection);
		}		
	}*/

	void PushBall(CBBall aBall)
	{
		//mCollisionDirection = aBall.Transform.position - Transform.position;
		//mCollisionDirection.Normalize();
		//aBall.Push (mCollisionDirection);
	}

	void CatchBall(CBBall aBall)
	{
		mBall = aBall;
		mBall.RemoveFreedom();
		mBall.Transform.parent = player.Transform;
		mBallLocalPosition = mBall.Transform.localPosition;
	}

	void KeepBallClose()
	{
		//mBall.Transform.localPosition = mBallLocalPosition;
		mBall.Transform.position = player.Transform.position;
	}

	void ReleaseBall()
	{ 
		//mCollisionDirection = mBall.Transform.position - Transform.position;
		//mCollisionDirection.Normalize();
		//RegainFreedom(mCollisionDirection, forcePower);
		mTimeSinceReleasedBall = 0.0f;
	}

	void Update ()
	{

	}
}