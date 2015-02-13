using UnityEngine;
using System.Collections;

public class PopController : MonoBehaviour 
{
	
	
	MoneyShower monShow;
	
	public float scaleThing = 1.0f;
	public Vector3[] conPoints;
	float currT;
	public float tSpeed;
	public bool goSwitch = false;
	
	Renderer theRenderer;
	
	
	float catEval(float p0,float p1,
			             float p2,float p3,
			             float t)
	{
		//evaluate a cat :)
		float t2 = t*t;
		return 0.5f * ( (2.0f * p1) + (-p0 + p2)*t + 
				      ( 2.0f*p0 - 5.0f*p1 + 4.0f*p2 - p3)*t2 
				      +(-p0 + 3.0f*p1 - 3.0f*p2 + p3)*t2*t);
	}
	
		Vector3 catEval(Vector3 p0,Vector3 p1,
			             Vector3 p2,Vector3 p3,
			             float t)
	{
		//evaluate a cat :)
		float t2 = t*t;
		return 0.5f * ( (2.0f * p1) + (-p0 + p2)*t + 
				      ( 2.0f*p0 - 5.0f*p1 + 4.0f*p2 - p3)*t2 
				      +(-p0 + 3.0f*p1 - 3.0f*p2 + p3)*t2*t);
	}
	
	Vector3 catEval(ref Vector3[] points,int length,float t)
	{
	
		int	ender =length;
		Vector3 ans = Vector3.zero;
		
		if(ender==1)
			return points[0];
		
		//optimiaztion possible!
		float eachPart = 1.0f/(float)ender;
		float tempRes = t*(float)ender;
		int index = (int)tempRes;
		
		if(index==ender)
			return points[ender-1];
		
		index--;
		int index2 = index+1;
		int index3 = index+2;
		if(index3>=ender)
			index3-=1;
		int index4 = index3+1;
		
		//make adjustments to indices.
		if(index<0)
			index = 0;
		if(index4>=ender)
			index4-=1;
		
		float t1 = eachPart*(float)index2;
		float t2 = t1+eachPart;
		float localT = (t-t1)/(t2-t1);
		
		ans = catEval (points[index],
				points[index2],
				points[index3],
				points[index4],localT);
	
		//Debug.Log("" + index + " " + index2 + " " + index3 + " " + index4 + " t is " + t );
		return ans;
	}

	// Use this for initialization
	void Awake () 
	{
		theRenderer = GetComponentInChildren<Renderer>();
		monShow = GetComponentInChildren<MoneyShower>();
		theRenderer .enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(goSwitch)
		{
			goSwitch= false;
			currT = 0.0f;
			theRenderer .enabled = true;
			monShow.setText("" + LittleGUI2.moneyWin + "Kr,-");
			
		}
		
		currT+=tSpeed*Time.deltaTime * 0.001f;
		
		Vector3 scale = catEval(ref conPoints,conPoints.Length,Mathf.Clamp(currT,0.0f,1.0f))*scaleThing;
		transform.localScale = new Vector3(scale.x,scale.x,scale.x);
	}
}
