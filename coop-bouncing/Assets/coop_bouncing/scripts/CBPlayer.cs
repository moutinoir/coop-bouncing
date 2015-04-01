using UnityEngine;
using System.Collections;
using InControl;

public class CBPlayer : MonoBehaviour 
{
	public CBFollowSpline FollowSpline;
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

	public enum EPlayerControl
	{
		Controller1,
		Controller2,
	}

	void Update ()
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
				break;
			}
			
			case CBPlayer.EPlayerControl.Controller2:
			{
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
				break;
			}
		}
	}
}
