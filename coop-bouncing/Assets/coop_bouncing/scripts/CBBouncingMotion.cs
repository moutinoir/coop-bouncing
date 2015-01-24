using UnityEngine;
using System.Collections;

public class CBBouncingMotion : MonoBehaviour 
{
	void OnTriggerEnter(Collider other) 
	{
		Debug.Log("[GAMEPLAY] " + transform.parent.name + ":" + name + " collided with " 
		          + other.transform.parent.name + ":" +  other.name);
	}
}
