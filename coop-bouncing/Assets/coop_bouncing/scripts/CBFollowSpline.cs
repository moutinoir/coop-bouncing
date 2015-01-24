using UnityEngine;
using System.Collections;
using FluffyUnderware.Curvy;

public class CBFollowSpline : MonoBehaviour 
{
	public enum EPlayerControl
	{
		Controller1,
		Controller2,
	}

	public CurvySpline Spline;
	public float Speed = 1f;
	public EPlayerControl PlayerControl;

	protected float mCurrentTF;
	protected float mTranslation;
	[SerializeField]
	protected CurvyVector mCurrent;
	
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
	}
	
	// Update is called once per frame
	void Update () 
	{
		int directionSwitch = 0;

		switch(PlayerControl)
		{
		case EPlayerControl.Controller1:

			mTranslation = Input.GetAxis("L_YAxis_1");
			directionSwitch = 1;

			if(mTranslation == 0)
			{
				mTranslation = Input.GetAxis("Vertical");
			}
			break;

		case EPlayerControl.Controller2:

			mTranslation = Input.GetAxis("L_YAxis_2");
			directionSwitch = -1;

			if(mTranslation == 0)
			{
				mTranslation = Input.GetAxis("Vertical2");
			}

			break;
		}

		mCurrent.Direction = (int) Mathf.Sign(mTranslation);

		Transform.position = Spline.MoveBy(ref mCurrentTF, ref mCurrent.m_Direction, 
		                                   Speed * Mathf.Abs(mTranslation) * Time.deltaTime, CurvyClamping.Clamp);

		//if (mTranslation == 0) 
		//{
			transform.rotation = Spline.GetOrientationFast (mCurrentTF);
			transform.rotation *= Quaternion.Euler(-90*directionSwitch, 90, 0);
		//}


		Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
		Debug.DrawRay(transform.position, forward, Color.green);
	}
}
