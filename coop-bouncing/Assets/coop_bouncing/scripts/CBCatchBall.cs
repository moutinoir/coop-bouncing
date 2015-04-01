using UnityEngine;
using System.Collections;
using InControl;

public class CBCatchBall : MonoBehaviour 
{
	public CBPlayer player;
	public float forcePower;
	public float intervalBetweenCatches = 0.1f;
	public CBBall mBall;
	private Vector3 mBallLocalPosition;
	
	public Vector3 mCollisionDirection;
	
	private float mTimeSinceReleasedBall;

	public CBCatchBall()
	{
		mCollisionDirection = new Vector3(0.0f, 1.0f, 0.0f);
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
		if(mBall.Free == true && mTimeSinceReleasedBall > intervalBetweenCatches)
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
		mBall.RegainFreedom(mCollisionDirection, forcePower);
		mTimeSinceReleasedBall = 0.0f;
	}

	void Update ()
	{
		if(InputManager.Devices.Count < 2)
		{
			return;
		}
		
		InputDevice inputDevicePlayer1 = InputManager.Devices[0];
		InputDevice inputDevicePlayer2 = InputManager.Devices[1];

		mTimeSinceReleasedBall += Time.deltaTime;

		switch(player.playerControl)
		{
			case CBPlayer.EPlayerControl.Controller1:
			{
				if(inputDevicePlayer1.RightTrigger.WasPressed)
				{
					Debug.Log ("Splat1");
					
					if(Vector3.Distance(player.Transform.position, mBall.Transform.position) < 5.0f)
					{
						CatchBall(mBall);
					}
				}
				if(!mBall.mIsAtBadAngle)
				{
					if(inputDevicePlayer1.RightBumper.WasPressed)
					{
						Debug.Log ("Splosh1");
						mBall.LobBall(mCollisionDirection, forcePower);
						Physics.IgnoreCollision(mBall.GetComponent<Collider>(), player.GetComponentInChildren<Collider>());
					}
				}
				break;
			}
				
			case CBPlayer.EPlayerControl.Controller2:
			{
				if(inputDevicePlayer2.RightTrigger.WasPressed)
				{
					Debug.Log("Splat2");
					
					//if(Vector3.Distance(otherPlayer.Transform.position, mBall.Transform.position) < 5.0f)
					//{
					//	CatchBall(mBall);
					//}
				}
				if(!mBall.mIsAtBadAngle)
				{
					if(inputDevicePlayer2.RightBumper.WasPressed)
					{
						Debug.Log ("Splosh2");
						mBall.LobBall(mCollisionDirection, forcePower);
						Physics.IgnoreCollision(mBall.GetComponent<Collider>(), player.GetComponentInChildren<Collider>());
					}
				}
				break;
			}
		}


		if(mBall.Free != true)
		{
			KeepBallClose();

//			float release_ball = 0f;
			bool release_ball = false;	

			switch(player.playerControl)
			{
			case CBPlayer.EPlayerControl.Controller1:
				
				//if(!mBall.mIsAtBadAngle)
				//{
//					release_ball = Input.GetAxis("L_Fire_1");
				//	release_ball = inputDevicePlayer1.RightBumper.IsPressed;
				//	mBall.lastHeldBy = player.name;
				//	Physics.IgnoreCollision(mBall.GetComponent<Collider>(), player.GetComponentInChildren<Collider>());
					//Physics.IgnoreCollision(mBall.GetComponent<Collider>(), otherPlayer.GetComponentInChildren<Collider>(), false);
				//}

				if(!mBall.mIsAtBadAngle)
				{
					if(inputDevicePlayer1.RightBumper.WasPressed)
					{
						mBall.LobBall(mCollisionDirection, forcePower);
						Physics.IgnoreCollision(mBall.GetComponent<Collider>(), player.GetComponentInChildren<Collider>());
					}
				}

				break;

			case CBPlayer.EPlayerControl.Controller2:

				// only allow this to happen if the player is pointing the throw away from the wall
				if(!mBall.mIsAtBadAngle)
				{
					if(inputDevicePlayer2.RightBumper.WasPressed)
					{
						mBall.LobBall(mCollisionDirection, forcePower);
						Physics.IgnoreCollision(mBall.GetComponent<Collider>(), player.GetComponentInChildren<Collider>());
					}
				}

				break;
				
				//if(!mBall.mIsAtBadAngle)
				//{
//					release_ball = Input.GetAxis("L_Fire_2");
				//	release_ball = inputDevicePlayer2.RightBumper.IsPressed;
				//	mBall.lastHeldBy = player.name;
				//	Physics.IgnoreCollision(mBall.GetComponent<Collider>(), player.GetComponentInChildren<Collider>());
					//Physics.IgnoreCollision(mBall.GetComponent<Collider>(), otherPlayer.GetComponentInChildren<Collider>(), false);
				//}
				//break;
			//}

//			if(release_ball > 0.5f)
			//if(release_ball)
			//{

			//}
		}

		
	}
}
}