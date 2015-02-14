using UnityEngine;
using System.Collections;

public class SplodeBob : MonoBehaviour 
{
	GUIShake gShake;
	public float speed;
	public float start;
	public float end;
	float t =0.0f;
	float tSpeed = 0.0f;

	// Use this for initialization
	void Start () 
	{
		gShake = GetComponent(typeof (GUIShake)) as GUIShake;
	}
	
	
	public bool onSwitch = false;
	public bool holdMe = true;
	
	// Update is called once per frame
	void Update () 
	{
	
		if(Input.GetKeyDown(KeyCode.R) || onSwitch==true)
		{
			onSwitch = false;
			holdMe = false;
			t = -0.0f;
			tSpeed = 0.0f;
		}
		if(holdMe)
			return;
		
		t+=tSpeed*Time.deltaTime;
		tSpeed+=Time.deltaTime/speed;
		
		
		
		float val = Mathf.Lerp(start,end,t);
		if(Input.GetKey(KeyCode.T))
		{
			val = 0.0f;
		}
		if(Input.GetKeyUp(KeyCode.T))
		{
			gShake.initiateShake = true;
			t = -0.0f;
			tSpeed = 0.0f;
		}
		renderer.material.SetFloat("_Power",val);
		
	}
}
