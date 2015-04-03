using UnityEngine;
using System.Collections;
using FluffyUnderware.Curvy;
using InControl;

public class CBPlayer : MonoBehaviour 
{
	public CBBall theBall;

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

	public bool isAtBadAngle;

	public EPlayerControl playerControl;

	private float mTranslation;
	public Vector3 mThrowAngle;

	[SerializeField]
	private CurvyVector mCurrent;
	private float mCurrentTF;
	public CurvySpline Spline;

	public float Speed = 1f;
	public float SlowSpeedFactor = 0.5f;
	private float mSpeedFactor = 1f;


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

	public enum EPlayerControl
	{
		Controller1,
		Controller2,
	}

	// Use this for initialization
	void Start () 
	{
		mCurrent = new CurvyVector(0, 1);
		mCurrentTF = Spline.DistanceToTF(Spline.ControlPoints[1].Distance);
	}

	void UpdatePlayerControls()
	{
		if(InputManager.Devices.Count < 2)
		{
			return;
		}
		
		InputDevice inputDevicePlayer1 = InputManager.Devices[0];
		InputDevice inputDevicePlayer2 = InputManager.Devices[1];
		
		switch (playerControl) 
		{
			case CBPlayer.EPlayerControl.Controller1:
			{
				Debug.Log ("Hitting update loop for Controller 1:" + transform.name);
				if (inputDevicePlayer1.RightTrigger.WasPressed) 
				{
					Debug.Log ("Player One Tongue");
				}
				if (!theBall.mIsAtBadAngle) 
				{
					if (inputDevicePlayer1.RightBumper.WasPressed) 
					{
						Debug.Log ("Player 1 Throw");
						//mBall.LobBall(mCollisionDirection, forcePower);
						//Physics.IgnoreCollision(mBall.GetComponent<Collider>(), player.GetComponentInChildren<Collider>());
					}
				}
				
				mTranslation = inputDevicePlayer1.LeftStickX.Value;
				mThrowAngle = new Vector3(inputDevicePlayer1.RightStickX.Value, inputDevicePlayer1.RightStickY.Value, 0.0f);
				Debug.DrawRay(transform.position, mThrowAngle, Color.red);
				break;
			}
				
			case CBPlayer.EPlayerControl.Controller2:
			{
				Debug.Log ("Hitting update loop for Controller 2:" + transform.name);
				if (inputDevicePlayer2.RightTrigger.WasPressed) 
				{
					Debug.Log ("Player Two Tongue");
					//if(Vector3.Distance(otherPlayer.Transform.position, mBall.Transform.position) < 5.0f)
					//{
					//	CatchBall(mBall);
					//}
				}
				if (!theBall.mIsAtBadAngle) 
				{
					if (inputDevicePlayer2.RightBumper.WasPressed) 
					{
						Debug.Log ("Player Two Throw");
						//mBall.LobBall (mCollisionDirection, forcePower);
						//Physics.IgnoreCollision (mBall.GetComponent<Collider> (), player.GetComponentInChildren<Collider> ());
					}
				}
				
				mTranslation = inputDevicePlayer2.LeftStickX.Value;
				mThrowAngle = new Vector3(inputDevicePlayer2.RightStickX.Value, inputDevicePlayer2.RightStickY.Value, 0.0f);
				Debug.DrawRay(transform.position, mThrowAngle, Color.green);
				
				break;
			}
		}
	}
	void UpdatePlayerPosition()
	{
		mCurrent.Direction = (int) Mathf.Sign(mTranslation);
		
		Transform.position = Spline.MoveBy(ref mCurrentTF, ref mCurrent.m_Direction, 
		                                   Speed * Mathf.Abs(mTranslation)* mSpeedFactor * Time.deltaTime, CurvyClamping.Clamp);
		Transform.rotation = Spline.GetOrientationFast(mCurrentTF, false);
		
		// give a little z offset to position on extrude properly
		Transform.position = new Vector3 (Transform.position.x, Transform.position.y, Transform.position.z-0.2f);
	}

	void Update ()
	{
		UpdatePlayerControls ();
		UpdatePlayerPosition ();


	}
}
