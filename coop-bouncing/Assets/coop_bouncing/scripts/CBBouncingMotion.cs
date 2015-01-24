using UnityEngine;
using System.Collections;

public class CBBouncingMotion : MonoBehaviour 
{
	public CBBall Ball;
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

	public void StopMotion ()
	{
		rigidbody.Sleep();
	}

	public void AddForce (Vector3 aForceDirection, float aForcePower)
	{
		rigidbody.AddForce(aForceDirection * aForcePower);
	}

	/*private Vector3 mCollisionDirection;

	void OnTriggerEnter(Collider other) 
	{
		Debug.Log("[GAMEPLAY] " + Transform.parent.name + ":" + name + " triggered contact with " 
		          + other.transform.parent.name + ":" +  other.name);
	}

	void OnCollisionEnter(Collision collision)
	{
		ContactPoint first_contact = collision.contacts[0];
		mCollisionDirection = first_contact.point - Transform.position;
		mCollisionDirection.Normalize();

		Debug.Log("[GAMEPLAY] " + transform.parent.name + ":" + name + " collided with " 
		          + collision.transform.parent.name + ":" +  collision.transform.name + " at point " + first_contact.point);

	}*/
}
