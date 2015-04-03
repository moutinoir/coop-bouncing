using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FluffyUnderware.Curvy;
using InControl;

// make shell stick properly to sides, currently sliding about on collision
// make shell follow angle and stick out from player
// calculate bad throwing angle and paint shell red and block player from throwing
// calculate parabola for throw. Create parabola line


public class CBFollowSpline : MonoBehaviour 
{
	public CBPlayer player;
	public bool isFirstPlayer; 
	
	private float mCurrentTF;

	[SerializeField]
	private CurvyVector mCurrent;



	public float CurrentTF
	{
		get
		{
			return mCurrentTF;
		}
		set
		{
			mCurrentTF = value;
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



	float calculateAngleBetweenTwoVectors(Vector3 source, Vector3 target)
	{
		float angle = Mathf.DeltaAngle(Mathf.Atan2(source.y, source.x) * Mathf.Rad2Deg,
		                               Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg);
		Debug.Log("angle difference: " + angle);
		return angle;
	}

	float AngleSigned(Vector3 v1, Vector3 v2)
	{
		Vector2 fromVector2 = v1;
		Vector2 toVector2 = v2;
		
		float ang = Vector2.Angle(fromVector2, toVector2);
		Vector3 cross = Vector3.Cross(fromVector2, toVector2);
		
		if (cross.z < 0)
			ang = 360 - ang;

		return ang;
	}

	// Update is called once per frame
	void Update () 
	{
		/*if(player.theBall != null)
		{
			mSpeedFactor = SlowSpeedFactor;
			float angleDif = AngleSigned(Spline.GetTangent(mCurrentTF), mThrowAngle);
			Debug.DrawRay(player.transform.position, Spline.GetTangent(mCurrentTF), Color.yellow);

			if(!isFirstPlayer)
			{
				if(angleDif > 20.0f && angleDif < 160.0f)
				{
					//player.CatchBall.Ball.mIsAtBadAngle = false;
				}
				else
				{
					//player.CatchBall.Ball.mIsAtBadAngle = true;
				}
			}
			else
			{
				if(angleDif < 340.0f && angleDif > 200.0f)
				{
					//player.CatchBall.Ball.mIsAtBadAngle = false;
				}
				else
				{
					//player.CatchBall.Ball.mIsAtBadAngle = true;
				}
			}

		}
		else
		{
			mSpeedFactor = 1f;
		}*/
	}
}
