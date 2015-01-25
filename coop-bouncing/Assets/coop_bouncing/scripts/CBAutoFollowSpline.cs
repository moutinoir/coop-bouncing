using UnityEngine;
using System.Collections;
using FluffyUnderware.Curvy;

public class CBAutoFollowSpline : MonoBehaviour 
{
	public CurvySpline Spline;
	public float Speed = 1f;
	
	protected float mCurrentTF;
	protected CurvyVector mCurrent;
	
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
		mCurrentTF = 0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Transform.position = Spline.MoveBy(ref mCurrentTF, ref mCurrent.m_Direction, 
		                                   Speed * Time.deltaTime, CurvyClamping.Clamp);
		Transform.rotation = Spline.GetOrientationFast(mCurrentTF, false);
	}
}
