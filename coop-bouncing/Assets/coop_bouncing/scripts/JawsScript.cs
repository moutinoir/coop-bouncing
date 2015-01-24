using UnityEngine;
using System.Collections;

public class JawsScript : MonoBehaviour 
{
	public float jawsSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate(0, jawsSpeed, 0);
		//transform.Translate(0, Time.deltaTime, 0, Space.World);
	}
}
