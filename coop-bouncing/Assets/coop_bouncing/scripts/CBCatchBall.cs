using UnityEngine;
using System.Collections;

public class CBCatchBall : MonoBehaviour 
{
	public CBPlayer Player;
	private CBBall mBall;
	private Vector3 mBallLocalPosition;
	private Vector3 mCollisionDirection;

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

	void OnCollisionEnter(Collision collision)
	{
		ContactPoint first_contact = collision.contacts[0];
		mCollisionDirection = first_contact.point - Transform.position;
		mCollisionDirection.Normalize();

		CBBall ball = collision.gameObject.GetComponent<CBBouncingMotion>().Ball;
		CatchBall(ball);
		Debug.Log("[GAMEPLAY] " + Transform.parent.name + ":" + name + " collided with " 
		          + collision.transform.parent.name + ":" +  collision.transform.name + " at point " + first_contact.point);
		
	}

	void CatchBall(CBBall aBall)
	{
		mBall = aBall;
		mBall.Transform.parent = Player.Transform;
		mBallLocalPosition = mBall.Transform.localPosition;
	}

	void KeepBallClose()
	{
		mBall.Transform.localPosition = mBallLocalPosition;
	}

	void ReleaseBall()
	{
		mBall.RegainFreedom();
	}

	void Update ()
	{
		if(mBall != null)
		{
			KeepBallClose();
		}
	}
}
