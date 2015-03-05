using UnityEngine;
using System.Collections;

public class CBCatchBall : MonoBehaviour 
{
	public CBPlayer Player;
	public float ForcePower;
	public float IntervalBetweenCatches = 0.1f;
	private CBBall mBall = null;
	private Vector3 mBallLocalPosition;
	
	public Vector3 mCollisionDirection;
	
	private float mTimeSinceReleasedBall;

	public CBCatchBall()
	{
		mCollisionDirection = new Vector3(0.0f, 2.0f, 0.0f);
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
		mTimeSinceReleasedBall = IntervalBetweenCatches;
	}

	void OnCollisionStay(Collision collision)
	{
		//Debug.Log ("collision enter");
		if(mBall == null && mTimeSinceReleasedBall > IntervalBetweenCatches)
		{
			ContactPoint first_contact = collision.contacts[0];
			//mCollisionDirection = first_contact.point - Transform.position;
			//mCollisionDirection.Normalize();

			CBBouncingMotion bouncingMotion = collision.gameObject.GetComponent<CBBouncingMotion>();

			if(bouncingMotion != null)
			{
				CBBall ball = bouncingMotion.mBall;

				if(ball.Free)
					CatchBall(ball);
			}

			//Debug.Log("[GAMEPLAY] " + Transform.parent.name + ":" + name + " collided with " 
			//          + collision.transform.parent.name + ":" +  collision.transform.name + " at point " + first_contact.point);
		}	
		else if(mBall == null)
		{
			CBBouncingMotion BouncingMotion = collision.gameObject.GetComponent<CBBouncingMotion>();
			if(BouncingMotion != null)
			{
				CBBall ball = BouncingMotion.mBall;
				if(ball != null)
				{
					PushBall(ball);
				}
			}
		}
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
		aBall.Push (mCollisionDirection);
	}

	void CatchBall(CBBall aBall)
	{
		mBall = aBall;
		mBall.RemoveFreedom();
		mBall.Transform.parent = Player.Transform;
		mBallLocalPosition = mBall.Transform.localPosition;
	}

	void KeepBallClose()
	{
		mBall.Transform.localPosition = mBallLocalPosition;
	}

	void ReleaseBall()
	{ 
		//mCollisionDirection = mBall.Transform.position - Transform.position;
		//mCollisionDirection.Normalize();
		mBall.RegainFreedom(mCollisionDirection, ForcePower);
		mBall = null;
		mTimeSinceReleasedBall = 0.0f;
	}

	void Update ()
	{
		mTimeSinceReleasedBall += Time.deltaTime;
		if(mBall != null)
		{
			KeepBallClose();

			float release_ball = 0f;

			switch(Player.PlayerControl)
			{
				case CBPlayer.EPlayerControl.Controller1:
					if(!mBall.mIsAtBadAngle)
						release_ball = Input.GetAxis("L_Fire_1");
					break;
				case CBPlayer.EPlayerControl.Controller2:
					if(!mBall.mIsAtBadAngle)
						release_ball = Input.GetAxis("L_Fire_2");
					break;
			}

			if(release_ball > 0.5f)
			{
				ReleaseBall();
			}
		}
	}
}
