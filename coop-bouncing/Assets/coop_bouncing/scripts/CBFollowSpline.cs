﻿using UnityEngine;
using System.Collections;
using FluffyUnderware.Curvy;

public class CBFollowSpline : MonoBehaviour 
{
	public enum EPlayerControl
	{
		YAxis,
		FifthAxis,
	}

	public CurvySpline Spline;
	public float Speed = 1f;
	public EPlayerControl PlayerControl;

	protected float mCurrentTF;
	protected float mTranslation;
	[SerializeField]
	protected CurvyVector mCurrent;

	/// <summary>
	/// Gets the (cached) transform
	/// </summary>
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
		switch(PlayerControl)
		{
		case EPlayerControl.YAxis:
			mTranslation = Input.GetAxis("Vertical");
			break;
		case EPlayerControl.FifthAxis:
			mTranslation = Input.GetAxis("Vertical2");
			break;
		}

		mCurrent.Direction = (int) Mathf.Sign(mTranslation);

		Transform.position = Spline.MoveBy(ref mCurrentTF, ref mCurrent.m_Direction, 
		                                   Speed * Mathf.Abs(mTranslation) * Time.deltaTime, CurvyClamping.Clamp);
	}
}