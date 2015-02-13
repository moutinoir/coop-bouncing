using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class GuiMotionMover
{
	public float powX;
	public float powY;
	public float ampliX;
	public float ampliY;
	public float biasX;
	public float biasY;
	public float ampliOffX;
	public float ampliOffY;

};

[Serializable]
public class GuiMotionShaker : GuiMotionMover
{


};

public class GuiMotion : MonoBehaviour 
{
	
	public GuiMotionMover mover;
	public GuiMotionShaker shaker;
	
	public float howOftenToShakeMax;
	public float howOftenToShakeMin;
	public float shakeDuration;
	float timeToShake;
	float inShakeTimer;
	
	Vector3 posHolder;
	// Use this for initialization
	void Start () 
	{
		posHolder = transform.position;
		timeToShake = UnityEngine.Random.Range(howOftenToShakeMin,howOftenToShakeMax);
		inShakeTimer = -1.0f;
	}
	
	void shakeDoings(ref Vector3 modp)
	{
		if(inShakeTimer>0.0f)
		{
			//we are in a shake
			inShakeTimer-=Time.deltaTime;
			float shakeT = inShakeTimer/shakeDuration;
			modp.x+= (Mathf.Sin(shaker.ampliX *shakeT + shaker.ampliOffX)*shaker.powX + shaker.biasX)*shakeT;
			modp.y+= (Mathf.Sin(shaker.ampliY *shakeT + shaker.ampliOffY)*shaker.powY + shaker.biasY)*shakeT;
			
		}
		else
		{
			timeToShake-=Time.deltaTime;
			if(timeToShake<=0.0f)
			{
				inShakeTimer = shakeDuration;
				timeToShake = UnityEngine.Random.Range(howOftenToShakeMin,howOftenToShakeMax);
			}
		}
	}
	
	
	// Update is called once per frame
	void Update () 
	{
	
		
		Vector3 modPos = Vector3.zero;
		shakeDoings(ref modPos);
		modPos.x+= Mathf.Sin(mover.ampliX * Time.time + mover.ampliOffX)*mover.powX + mover.biasX;
		modPos.y+= Mathf.Sin(mover.ampliY * Time.time + mover.ampliOffY)*mover.powY + mover.biasY;
		transform.position = modPos + posHolder;
		
		
		
		
	}
	
}
