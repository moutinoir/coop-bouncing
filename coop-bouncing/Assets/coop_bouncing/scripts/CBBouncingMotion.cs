using UnityEngine;
using System.Collections;

public class CBBouncingMotion : MonoBehaviour 
{
	public enum EMotionType
	{
		UnityPhysics,
		Manual
	}

	public EMotionType MotionType = EMotionType.UnityPhysics;
	public CBBall mBall;
	public float maxSpeed = 2f;
	public float minSpeed = 0.5f;

	private float mVelocityMagnitude = 0f;

	// this is horrible... Lets find a better way to do it
	//private string toSpline = "Player1Path";

	public Rigidbody Rigidbody
	{
		get
		{
			if (!mRigidbody)
				mRigidbody = GetComponent<Rigidbody>();
			return mRigidbody;
		}
	}
	Rigidbody mRigidbody;

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
		switch(MotionType)
		{
		case EMotionType.UnityPhysics:
			Rigidbody.Sleep();
			break;

		case EMotionType.Manual:
			Rigidbody.isKinematic = true;
			break;
		}
	}

	public void StopMotion ()
	{
		switch(MotionType)
		{
		case EMotionType.UnityPhysics:
			Rigidbody.Sleep();
			Rigidbody.velocity = Vector3.zero;
			break;
			
		case EMotionType.Manual:
			break;
		}
	}

	public void AddForce (Vector3 aForceDirection, float aForcePower)
	{
		switch(MotionType)
		{
		case EMotionType.UnityPhysics:
			Rigidbody.velocity = Vector3.zero;
			Rigidbody.AddForce(aForceDirection * aForcePower);
			break;
			
		case EMotionType.Manual:
			break;
		}
	}

	public void Push (Vector2 aDirection)
	{
		switch(MotionType)
		{
		case EMotionType.UnityPhysics:
			mVelocityMagnitude = Rigidbody.velocity.magnitude;
			Rigidbody.velocity = aDirection * mVelocityMagnitude;

			if(mVelocityMagnitude < minSpeed)
			{
				Rigidbody.velocity = Rigidbody.velocity.normalized * minSpeed;
			}
			break;
			
		case EMotionType.Manual:
			break;
		}
	}

	void FixedUpdate()
	{
		switch(MotionType)
		{
		case EMotionType.UnityPhysics:
			mVelocityMagnitude = Rigidbody.velocity.magnitude;
			if(Rigidbody.velocity.magnitude > maxSpeed)
			{
				Rigidbody.velocity = Rigidbody.velocity.normalized * maxSpeed;
			}
			/*else if(Rigidbody.velocity.magnitude < minSpeed && !Rigidbody.IsSleeping())
			{
				Rigidbody.velocity = Rigidbody.velocity.normalized * minSpeed;
			}*/
			break;
			
		case EMotionType.Manual:
			break;
		}
	}

	private Vector3 mCollisionDirection;

	void OnTriggerEnter(Collider other) 
	{
		Debug.Log("[GAMEPLAY] " + Transform.parent.name + ":" + name + " triggered contact with " 
		          + other.transform.parent.name + ":" +  other.name);

	}

	void OnCollisionEnter(Collision collision)
	{
		ContactPoint first_contact = collision.contacts [0];
		mCollisionDirection = first_contact.point - Transform.position;
		mCollisionDirection.Normalize ();

		Debug.Log ("[GAMEPLAY] " + transform.parent.name + ":" + name + " collided with " 
				+ collision.transform.parent.name + ":" + collision.transform.name + " at point " + first_contact.point);

		Debug.Log ("lastHeldBy: " + mBall.lastHeldBy);
	
	/*
	 * 
	 * [GAMEPLAY] Core:Ball collided with Player2:Body at point (-4.1, 0.4, 0.0)
	 */

		if(name == "Ball" && collision.transform.name == "Body")
		{
			mBall.GrabBall(collision.gameObject);
		}

		//[GAMEPLAY] Core:Ball collided with Player1Path:Collider at point (-3.6, 2.8, -0.2)
		if (name == "Ball" && collision.transform.name == "Collider") 
		{
			//mBall.RegainFreedom();
		}

		// TODO: Fix path collision

		//if (mBall.lastHeldBy == collision.transform.parent.name)
		//	Debug.Log ("We want to ignore this");


		// tidy this up it's a mess
		//if (collision != null) 
		//{
		//	if(collision.transform.parent != null)
		//	{
		//		if(collision.transform.parent.name == "Player1Path" ||
		//		   collision.transform.parent.name == "Player2Path")
		//		{
		//			mBall.RemoveFreedom ();
		//		}
					//if (collision.transform.parent.name == toSpline) 
					//{
					// add some logic for going between splines (save last touched path)
					//Debug.Log ("1: ****************" + toSpline + "**************");
					
					/*	if(toSpline == "Player1Path")
					{
						toSpline = "Player2Path";
					}
					else
					{
						toSpline = "Player1Path";
					}*/
					
					//Debug.Log ("2: ****************" + toSpline + "************");
					
					
					//}
		//	}
		//}
	}
}
