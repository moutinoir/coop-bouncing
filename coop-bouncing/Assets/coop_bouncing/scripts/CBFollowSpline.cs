﻿using UnityEngine;
using System.Collections;
using FluffyUnderware.Curvy;

public class CBFollowSpline : MonoBehaviour 
{
	public CBPlayer Player;

	public CurvySpline Spline;
	public float Speed = 1f;

	protected float mCurrentTF;
	protected float mTranslation;
	[SerializeField]
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
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch(Player.PlayerControl)
		{
		case CBPlayer.EPlayerControl.Controller1:
			mTranslation = Input.GetAxis("L_YAxis_1");
			if(mTranslation == 0)
			{
				mTranslation = Input.GetAxis("Vertical");
			}
			break;
		case CBPlayer.EPlayerControl.Controller2:
			mTranslation = Input.GetAxis("L_YAxis_2");
			if(mTranslation == 0)
			{
				mTranslation = Input.GetAxis("Vertical2");
			}
			break;
		}

		mCurrent.Direction = (int) Mathf.Sign(mTranslation);

		Transform.position = Spline.MoveBy(ref mCurrentTF, ref mCurrent.m_Direction, 
		                                   Speed * Mathf.Abs(mTranslation) * Time.deltaTime, CurvyClamping.Clamp);
	}
}
