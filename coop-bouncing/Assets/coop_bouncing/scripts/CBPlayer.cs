using UnityEngine;
using System.Collections;
using FluffyUnderware.Curvy;
using InControl;

public class CBPlayer : MonoBehaviour 
{
	public CBBall theBall;
	public int playerNum;

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


	[SerializeField]
	private CurvyVector mCurrent;
	private float mCurrentTF;
	public CurvySpline Spline;

	public float Speed = 1f;
	public float SlowSpeedFactor = 0.5f;
	private float mSpeedFactor = 1f;

	public float forcePower;
	public Vector3 mThrowVector = Vector3.zero;

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

	void calculateIfAngleIsBad(float min, float max)
	{
		float angleDif = AngleSigned(Spline.GetTangent(mCurrentTF), mThrowVector);

		if(angleDif > min && angleDif < max)
			theBall.mIsAtBadAngle = false;
		else
			theBall.mIsAtBadAngle = true;
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
				if (inputDevicePlayer1.RightTrigger.WasPressed) 
				{
					Debug.Log ("Player One Tongue");

					if(Vector3.Distance(Transform.position, theBall.Transform.position) < 2.0f)
					{
						theBall.TongueBall(gameObject, playerNum);
					}
				}
				if (!theBall.mIsAtBadAngle && theBall.state == CBBall.BallState.Held) 
				{
					if (inputDevicePlayer1.RightBumper.WasPressed) 
					{
						Debug.Log ("Player 1 Throw with throw vector:" + mThrowVector);
						theBall.LobBall(mThrowVector, forcePower);
						//Physics.IgnoreCollision(mBall.GetComponent<Collider>(), player.GetComponentInChildren<Collider>());
					}

					if(theBall.holdingPlayer.name == gameObject.name)
					{	
						Debug.Log("held by player 1");
					}
				}
				
				if(theBall.holdingPlayerNumber ==  1)
					calculateIfAngleIsBad(200.0f,340.0f);

				mTranslation = inputDevicePlayer1.LeftStickY.Value;
				mThrowVector = new Vector3(inputDevicePlayer1.RightStickX.Value, inputDevicePlayer1.RightStickY.Value, 0.0f);

				if(theBall.mIsAtBadAngle)
					Debug.DrawRay(transform.position, mThrowVector, Color.red);
				else
					Debug.DrawRay(transform.position, mThrowVector, Color.green);
				break;
			}
				
			case CBPlayer.EPlayerControl.Controller2:
			{
				if (inputDevicePlayer2.RightTrigger.WasPressed) 
				{
					Debug.Log ("Player Two Tongue");

					if(Vector3.Distance(Transform.position, theBall.Transform.position) < 2.0f)
					{
						theBall.TongueBall(gameObject, playerNum);
					}
				}
				if (!theBall.mIsAtBadAngle && theBall.state == CBBall.BallState.Held) 
				{
					if (inputDevicePlayer2.RightBumper.WasPressed) 
					{
						Debug.Log ("Player 2 Throw with throw vector:" + mThrowVector);
						theBall.LobBall (mThrowVector, forcePower);
						//Physics.IgnoreCollision (mBall.GetComponent<Collider> (), player.GetComponentInChildren<Collider> ());
					}
				}
	
				if(theBall.holdingPlayerNumber ==  2)
					calculateIfAngleIsBad(20.0f,160.0f);
				mTranslation = inputDevicePlayer2.LeftStickY.Value;
				mThrowVector = new Vector3(inputDevicePlayer2.RightStickX.Value, inputDevicePlayer2.RightStickY.Value, 0.0f);
				
				if(theBall.mIsAtBadAngle)
					Debug.DrawRay(transform.position, mThrowVector, Color.red);
				else
					Debug.DrawRay(transform.position, mThrowVector, Color.green);
				
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
	void OnCollisionEnter(Collision collision)
	{
		ContactPoint first_contact = collision.contacts [0];
		//mCollisionDirection = first_contact.point - Transform.position;
		//mCollisionDirection.Normalize ();
		
		Debug.Log ("[GAMEPLAY] " + transform.parent.name + ":" + name + " collided with " 
			+ collision.transform.parent.name + ":" + collision.transform.name + " at point " + first_contact.point);
		
		//Debug.Log ("lastHeldBy: " + mBall.lastHeldBy);
		
		/*
	 * 
	 * [GAMEPLAY] Core:Ball collided with Player2:Body at point (-4.1, 0.4, 0.0)
	 */
		
		if (name == "Body" && collision.transform.name == "Ball" && theBall.state != CBBall.BallState.Held) 
		{
			theBall.GrabBall (gameObject, playerNum);
		}
	}
}
