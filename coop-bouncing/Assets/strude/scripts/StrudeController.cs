using UnityEngine;
using System.Collections;

public class StrudeController :  MonoBehaviour
{
	
	CamFollow camMotion;
	
	
	public PopController popFab;
	public PopController popFab2;
	//public int minPops;
	
	
	public StrudeMeshCreate strudeMesh;
	
	//public bool restart;
	public float from;
	public float to;
	
	
	public float startspeed;
	public float minSpeed;
	public float deAcc;
	
	public bool startTrigger = true;
	float currSpeed = 0.0f;
	float currT = 0.0f;
	public float flowLength;
	
	
	public bool fireChildrenMode = true;
	public float fireTrigger = 1.0f;
	
	bool inAStrude = false;
	
	StrudeController[] strudeChilds;


	


void Awake()
{
	strudeMesh = GetComponent<StrudeMeshCreate>();
	camMotion = Camera.main.GetComponent<CamFollow>();
	
}

	// Use this for initialization
	void Start () 
	{
		
		strudeChilds = GetComponentsInChildren<StrudeController>();
	}
	
	public void updateStrudeChilds()
	{
		strudeChilds = GetComponentsInChildren<StrudeController>();
	}
	
	void triggerChilds()
	{
		if(strudeChilds.Length == 1)
		{
			if(currT>=1.0f)
			{
				if(LittleGUI2.looseCount !=3)
					LittleGUI2.currMoney+=LittleGUI2.moneyWin;
				LittleGUI2.looseCount = 3;
				
			}
		}
		
		foreach(StrudeController strudeCon in strudeChilds)
		{
			if(strudeCon.transform.parent == transform)
			if(currT >= strudeCon.fireTrigger && !strudeCon.inAStrude)
			{
				//we should fire it!
				strudeCon.startTrigger = true;
			}
		}
	}
	
	void passTriggerStartToChildren()
	{
		if(strudeChilds!=null)
		if(fireChildrenMode)
		foreach(StrudeController strudeCon in strudeChilds)
		{
			if(strudeCon!=this)
			{
			
				//we should fire it!
				strudeCon.inAStrude = false;
				strudeCon.passTriggerStartToChildren();
			}
			
		}
	}
	
	
	bool popGenerated = false;
	
	// Update is called once per frame
	void Update () 
	{
		if(startTrigger)
		{
			camMotion.strudeCon = this;
			currSpeed = startspeed;
			currT= 0.0f;
			startTrigger = false;
			inAStrude  = true;
			passTriggerStartToChildren();
		}
		if(inAStrude)
		{
			currSpeed-=Time.deltaTime*deAcc;
			if(currSpeed<=minSpeed)
				currSpeed = minSpeed;
		
			currT +=Time.deltaTime * currSpeed;
			renderer.material.SetFloat("_Seed",Mathf.Lerp(from,to,currT));
			renderer.material.SetFloat("_FlowLength",flowLength);
		
			if(camMotion.strudeCon==this)
			{
				
				float camDist = -8.0f;
				float camlRDist = 2.0f;
				Vector3 theLookPos = Vector3.zero;
				strudeMesh.pathData(out theLookPos,currT*0.8f);
				camMotion.lookAt = transform.rotation*theLookPos + transform.position;
			
				Vector3 theBePos = Vector3.zero;
				strudeMesh.pathData(out theBePos,currT*0.6f);
				camMotion.positionToBe= transform.rotation*theBePos + Vector3.forward*camDist + transform.position + Vector3.right*camlRDist ;
			}
			
			
			if(currT>=0.7f && !popGenerated)
			{
				
				LittleGUI2.moneyWin++;
				
				Vector3 thePos = Vector3.zero;
				strudeMesh.pathData(out thePos,currT);
				
				PopController popCon;
				if(Random.Range(0.0f,1.0f)>0.5f)
					popCon = Instantiate(popFab,transform.position + transform.rotation*thePos,Quaternion.identity) as PopController;
				else
					popCon = Instantiate(popFab2,transform.position + transform.rotation*thePos,Quaternion.identity) as PopController;
					
				popCon.transform.parent = transform;
				popCon.transform.Translate(Vector3.forward*0.5f,Space.Self);
				popCon.transform.Rotate(Vector3.right,90.0f);
				popCon.transform.Rotate(Vector3.up,Random.Range(0.0f,360.0f));
				popGenerated = true;
				
			}
		
			if(fireChildrenMode)
				triggerChilds();
		}
		else
		{
			renderer.material.SetFloat("_Seed",from);
		}
	}
}

