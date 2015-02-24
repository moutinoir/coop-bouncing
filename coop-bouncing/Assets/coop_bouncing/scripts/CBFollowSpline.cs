using UnityEngine;
using System.Collections;
using FluffyUnderware.Curvy;

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

	protected float mCurrentTF;
	protected float mTranslation;
	[SerializeField]
	protected CurvyVector mCurrent;
	
	private Vector3 throwAngle;

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
	// Update is called once per frame
	void Update () 
	{
		switch(player.PlayerControl)
		{
			case CBPlayer.EPlayerControl.Controller1:
			{
				mTranslation = Input.GetAxis("p1_L_XAxis");
				
				if(mTranslation == 0)
				{
					mTranslation = Input.GetAxis("Horizontal1");
				}

				throwAngle = new Vector3(Input.GetAxis("p1_R_XAxis"), Input.GetAxis("p1_R_YAxis"), 0.0f);
				Debug.DrawRay( player.transform.position, throwAngle, Color.red);
	
				break;
			}
			case CBPlayer.EPlayerControl.Controller2:
			{
				mTranslation = Input.GetAxis("p2_L_XAxis");
				
				if(mTranslation == 0) 
				{
					mTranslation = Input.GetAxis("Horizontal2");
				}

				throwAngle = new Vector3(Input.GetAxis("p2_R_XAxis"), Input.GetAxis("p2_R_YAxis"), 0.0f);
				Debug.DrawRay( player.transform.position, throwAngle, Color.green);
				break;
			}
		}

		mCurrent.Direction = (int) Mathf.Sign(mTranslation);
		
		Transform.position = Spline.MoveBy(ref mCurrentTF, ref mCurrent.m_Direction, 
		                                   Speed * Mathf.Abs(mTranslation)* mSpeedFactor * Time.deltaTime, CurvyClamping.Clamp);
		Transform.rotation = Spline.GetOrientationFast(mCurrentTF, false);

		if(player.CatchBall.Ball != null)
		{
			mSpeedFactor = SlowSpeedFactor;
			player.CatchBall.mCollisionDirection = throwAngle;
			//float angleDif = calculateAngleBetweenTwoVectors(Transform.rotation.eulerAngles, throwAngle);
			float angleDif = Vector3.Angle(Spline.GetTangent(mCurrentTF), throwAngle);
			Debug.DrawRay(player.transform.position, Spline.GetTangent(mCurrentTF), Color.yellow);
			Debug.Log(angleDif);
			if(angleDif > 20.0f && angleDif < 160.0f)
			{
				player.CatchBall.Ball.mIsAtBadAngle = false;
			}
			else
				player.CatchBall.Ball.mIsAtBadAngle = true;
		}
		else
		{
			mSpeedFactor = 1f;
		}
	}
}
