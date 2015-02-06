using UnityEngine;
using System.Collections;
using FluffyUnderware.Curvy;

public class CBFollowSpline : MonoBehaviour 
{
	public CBPlayer Player;

	public CurvySpline Spline;
	public float Speed = 1f;
	public float SlowSpeedFactor = 0.5f;
	private float mSpeedFactor = 1f;

	protected float mCurrentTF;
	protected float mTranslation;
	[SerializeField]
	protected CurvyVector mCurrent;

	public float InputStick1;
	public float InputStick2;
	public float InputVertical1;
	public float InputVertical2;
	public float InputHorizontal1;
	public float InputHorizontal2;

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
	
	// Update is called once per frame
	void Update () 
	{
		switch(Player.PlayerControl)
		{
		case CBPlayer.EPlayerControl.Controller1:
		{
			mTranslation = Input.GetAxis("L_XAxis_1");
			if(mTranslation == 0)
			{
				mTranslation = Input.GetAxis("Horizontal1");
			}
			break;
		}
		case CBPlayer.EPlayerControl.Controller2:
		{
			mTranslation = Input.GetAxis("L_XAxis_2");
			if(mTranslation == 0)
			{
				mTranslation = Input.GetAxis("Horizontal2");
			}
			break;
		}
		}

		InputStick1 = Input.GetAxis("L_XAxis_1");
		InputStick2 = Input.GetAxis("L_XAxis_2");
		InputVertical2 = Input.GetAxis("Vertical2");
		InputVertical1 = Input.GetAxis("Vertical1");
		InputHorizontal2 = Input.GetAxis("Horizontal2");
		InputHorizontal1 = Input.GetAxis("Horizontal1");

		if(Player.CatchBall.Ball != null)
		{
			mSpeedFactor = SlowSpeedFactor;
		}
		else
		{
			mSpeedFactor = 1f;
		}

		mCurrent.Direction = (int) Mathf.Sign(mTranslation);

		Transform.position = Spline.MoveBy(ref mCurrentTF, ref mCurrent.m_Direction, 
		                                   Speed * Mathf.Abs(mTranslation)* mSpeedFactor * Time.deltaTime, CurvyClamping.Clamp);
		Transform.rotation = Spline.GetOrientationFast(mCurrentTF, false);
	}
}
