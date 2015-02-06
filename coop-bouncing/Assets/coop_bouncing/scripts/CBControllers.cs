using UnityEngine;
using System.Collections;

public class CBControllers : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		/*for(int i = 0; i < Input.GetJoystickNames().Length ; ++i)
		{
			Debug.Log("[GAMEPLAY] joystick " + Input.GetJoystickNames()[i]);
		}*/
		Debug.Log("[GAMEPLAY] " + Input.GetJoystickNames().Length + " controller(s) connected");
		
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
