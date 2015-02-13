using UnityEngine;
using System.Collections;

public class GUIShake : MonoBehaviour 
{

	public bool autoShake = true;
	public bool initiateShake = false;

	public float shakeDuration;
	
	public GuiMotionShaker shaker;
	public Vector3 posHolder;
	protected float inShakeTimer;
	
	protected void shakeInit()
	{
			posHolder = transform.localPosition;
			inShakeTimer = -1.0f;
	}
	
	// Use this for initialization
	void Start () 
	{
		shakeInit();
			
	}
	
	void shakeDoings(ref Vector3 modp)
	{
		if(inShakeTimer>=-0.1f)
		{
			
			//we are in a shake
			inShakeTimer-=Time.deltaTime;
			if(inShakeTimer<0.0f)
				inShakeTimer=0.0f;
			float shakeT = inShakeTimer/shakeDuration;
			modp.x+= (Mathf.Sin(shaker.ampliX *shakeT + shaker.ampliOffX)*shaker.powX + shaker.biasX)*shakeT;
			modp.y+= (Mathf.Sin(shaker.ampliY *shakeT + shaker.ampliOffY)*shaker.powY + shaker.biasY)*shakeT;
			
		}
	}
	
	
	public void doShakeStuff()
	{
		if(initiateShake)
		{
			initiateShake = false;
			inShakeTimer = shakeDuration;
		}
		
		Vector3 modPos = Vector3.zero;
		shakeDoings(ref modPos);
		transform.localPosition = modPos + posHolder;
	}
	
	public void doShakeStuff(ref Vector3 pos)
	{
		if(initiateShake)
		{
			initiateShake = false;
			inShakeTimer = shakeDuration;
		}
		
		Vector3 modPos = Vector3.zero;
		shakeDoings(ref modPos);
		pos += modPos;
	}
	
	public bool shakeDone()
	{
		return inShakeTimer<=0.0f;
	}

	
	// Update is called once per frame
	void Update () 
	{
		if(autoShake )
			doShakeStuff();
		
	}
}
