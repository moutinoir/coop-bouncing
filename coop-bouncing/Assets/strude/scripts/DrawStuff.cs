using UnityEngine;
using System.Collections;

public class DrawStuff : QuadsMeshMaker 
{
	
	
	public bool hasPath()
	{
		return numConPoints >=4;
	}
	
	public void pathData(out Vector3 v,float t)
	{
		v = catEval(ref conPoints,numConPoints,t);
	}	
	

	public int mButton;
	public float distancePerQuadSqr;

	
	Vector3 mouseRef;
	
	Vector3[] conPoints = null;
	int numConPoints = 0;
	public int howManyConPoints;
	
	
	
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
	
		return ans;
	}
	
	
	// Use this for initialization
	void Start () 
	{
		init();
		conPoints = new Vector3[howManyConPoints];
	}
	
	
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 pos = transform.position;
		pos = Camera.main.transform.rotation * Vector3.forward*inDist;
		transform.position = pos;
		
		Vector3 mouseNow = Input.mousePosition;
		Vector3 point = Camera.main.ScreenToWorldPoint (mouseNow);
		//Debug.Log("blowing your point:" + point );
		
		if(Input.GetMouseButtonDown(mButton))
		{
				
			numConPoints = 0;
			currQuads = 0;
			mouseRef = mouseNow;
		}
		
		if(Input.GetMouseButton(mButton))
		{
			float disSqr = (mouseRef - mouseNow).sqrMagnitude;
			if(numConPoints==0 || disSqr >= distancePerQuadSqr)
			{
				numConPoints++;
				mouseRef = mouseNow;
				if(numConPoints >= conPoints.Length)
				{
					numConPoints--;
					Debug.Log("oops overblew the point array");
					return;
				}
				
				conPoints[numConPoints-1] = point;
			}
		}
		
		
		if(Input.GetKeyDown(KeyCode.A))
		{
			renderer.enabled = !renderer.enabled;
		}
		
	
		if(Input.GetMouseButtonUp(mButton))
		{
			
		}
	
		if(numConPoints>=4)
		{
			for(int i=0; i < maxQuads; i++)
			{
				float t = (float)i/ (float)(maxQuads-1);
				points[i] = catEval(ref conPoints,numConPoints,t);
				currQuads = i;
			}
		}
		makeMesh();
	}
}
