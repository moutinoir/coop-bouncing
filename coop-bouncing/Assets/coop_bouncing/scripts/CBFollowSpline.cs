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

	public CurvySpline Spline;
	public float Speed = 1f;
	public float SlowSpeedFactor = 0.5f;
	private float mSpeedFactor = 1f;

	private float mCurrentTF;
	private float mTranslation;
	[SerializeField]
	private CurvyVector mCurrent;
	
	private Vector3 mThrowAngle;

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

	// Use this for initialization
	void Start () 
	{
		mCurrent = new CurvyVector(0, 1);
		mCurrentTF = Spline.DistanceToTF(Spline.ControlPoints[1].Distance);
	}

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
		if(InputManager.Devices.Count < 2)
		{
			return;
		}

		InputDevice inputDevicePlayer1 = InputManager.Devices[0];
		InputDevice inputDevicePlayer2 = InputManager.Devices[1];

		switch(player.PlayerControl)
		{
		case CBPlayer.EPlayerControl.Controller1:
		{
//			mTranslation = Input.GetAxis("p1_L_XAxis");
			mTranslation = inputDevicePlayer1.LeftStickX.Value;
				
//			if(mTranslation == 0)
//			{
//				mTranslation = Input.GetAxis("Horizontal1");
//			}

//			mThrowAngle = new Vector3(Input.GetAxis("p1_R_XAxis"), Input.GetAxis("p1_R_YAxis"), 0.0f);
			mThrowAngle = new Vector3(inputDevicePlayer1.RightStickX.Value, inputDevicePlayer1.RightStickY.Value, 0.0f);

			Debug.DrawRay(player.transform.position, mThrowAngle, Color.red);

			break;
		}
		case CBPlayer.EPlayerControl.Controller2:
		{
//			mTranslation = Input.GetAxis("p2_L_XAxis");
			mTranslation = inputDevicePlayer2.LeftStickX.Value;
			
//			if(mTranslation == 0) 
//			{
//				mTranslation = Input.GetAxis("Horizontal2");
//			}

//			mThrowAngle = new Vector3(Input.GetAxis("p2_R_XAxis"), Input.GetAxis("p2_R_YAxis"), 0.0f);
			mThrowAngle = new Vector3(inputDevicePlayer2.RightStickX.Value, inputDevicePlayer2.RightStickY.Value, 0.0f);

			Debug.DrawRay(player.transform.position, mThrowAngle, Color.green);
			break;
		}
		}

		mCurrent.Direction = (int) Mathf.Sign(mTranslation);
		
		Transform.position = Spline.MoveBy(ref mCurrentTF, ref mCurrent.m_Direction, 
		                                   Speed * Mathf.Abs(mTranslation)* mSpeedFactor * Time.deltaTime, CurvyClamping.Clamp);
		Transform.rotation = Spline.GetOrientationFast(mCurrentTF, false);

		// give a little z offset to position on extrude properly
		Transform.position = new Vector3 (Transform.position.x, Transform.position.y, Transform.position.z-0.2f);

		if(player.CatchBall.Ball != null)
		{
			mSpeedFactor = SlowSpeedFactor;
			player.CatchBall.mCollisionDirection = mThrowAngle;
			//float angleDif = calculateAngleBetweenTwoVectors(Transform.rotation.eulerAngles, throwAngle);
			//float angleDif = Vector3.Angle(Spline.GetTangent(mCurrentTF), throwAngle);
			float angleDif = AngleSigned(Spline.GetTangent(mCurrentTF), mThrowAngle);
			Debug.DrawRay(player.transform.position, Spline.GetTangent(mCurrentTF), Color.yellow);

			if(!isFirstPlayer)
			{
				if(angleDif > 20.0f && angleDif < 160.0f)
				{
					player.CatchBall.Ball.mIsAtBadAngle = false;
				}
				else
					player.CatchBall.Ball.mIsAtBadAngle = true;
			}
			else
			{
				if(angleDif < 340.0f && angleDif > 200.0f)
				{
					player.CatchBall.Ball.mIsAtBadAngle = false;
				}
				else
					player.CatchBall.Ball.mIsAtBadAngle = true;
			}

		}
		else
		{
			mSpeedFactor = 1f;
		}
	}
}
