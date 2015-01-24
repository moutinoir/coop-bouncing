using UnityEngine;
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

	private int directionSwitch;
	protected float mCurrentTF;
	protected float mTranslation;
	[SerializeField]
	protected CurvyVector mCurrent;

	public Vector3 pLast;

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

		/*Transform.position = Spline.MoveBy(ref mCurrentTF, ref mCurrent.m_Direction, 
		                                   Speed * Mathf.Abs(mTranslation) * Time.deltaTime, CurvyClamping.Clamp);
*/
		Transform.position = Spline.Move (ref mCurrentTF, ref mCurrent.m_Direction, 0.5f, CurvyClamping.Clamp);
	}

	public void calculatePosition()
	{
		mCurrent.Direction = (int) Mathf.Sign(mTranslation);
		
		if (pLast != Vector3.zero)
		{
			Debug.Log("And here I am restoring it: " + pLast);
			Transform.position = pLast;
			pLast = Vector3.zero;
		}
		else if (mTranslation != 0)
		{
			Transform.position = Spline.MoveBy(ref mCurrentTF, ref mCurrent.m_Direction, 
			                                   Speed * Mathf.Abs(mTranslation) * Time.deltaTime, CurvyClamping.Clamp);
			
			Transform.rotation = Spline.GetOrientationFast (mCurrentTF);
			//Transform.rotation *= Quaternion.Euler(-90*directionSwitch, 90, 0);
		}
	}

	void drawDebugRay()
	{
		Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
		Debug.DrawRay(transform.position, forward, Color.green);
	}

	// Update is called once per frame
	void Update () 
	{
		directionSwitch = 0;

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

		calculatePosition ();
		drawDebugRay ();
	}
}
