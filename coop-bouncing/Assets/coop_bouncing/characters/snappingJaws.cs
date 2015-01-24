using UnityEngine;
using System.Collections;

public class snappingJaws : MonoBehaviour 
{
	public float StartZAngleDegree;
	public float EndZAngleDegree;
	private float mAngularSpeed = 2f;
	private float mAnglePercentage = 0f;

	void Update()
	{
		mAnglePercentage += Time.deltaTime * mAngularSpeed;
		if(mAnglePercentage > 1f)
		{
			mAnglePercentage = 1f;
			mAngularSpeed *= -1f;
		}
		else if(mAnglePercentage < 0f)
		{
			mAnglePercentage = 0f;
			mAngularSpeed *= -1f;
		}
		
		transform.rotation = Quaternion.Slerp (Quaternion.Euler (0f, 0f, StartZAngleDegree), Quaternion.Euler (0f, 0f, EndZAngleDegree), mAnglePercentage);
	}
}

