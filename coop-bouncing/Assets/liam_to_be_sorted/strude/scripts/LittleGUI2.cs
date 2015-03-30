using UnityEngine;
using System.Collections;

public class LittleGUI2 : MonoBehaviour 
{

	CamFollow camMotion;
	SplodeBob sploder;
	public static int moneyWin = 0;
	public static int looseCount = 0;
	
	public static int currMoney = 100;
	
	public TreeStrudeGen genFab;
	TreeStrudeGen currFab;
	
	GUIText monText;
	GUIText lastWinText;
	
	// Use this for initialization
	void Start () 
	{
		  Time.timeScale = 0.02f;
			camMotion = Camera.main.GetComponent<CamFollow>();
		camMotion.posToSpeed = 0.02f;

		Vector3 theLookPos = Vector3.zero;
		camMotion.lookAt = GameObject.Find("CAMERALOOK").transform.position;
			
		Vector3 theBePos = Vector3.zero;
		camMotion.positionToBe= GameObject.Find("CAMERAPOS").transform.position ;
		
		sploder = GetComponent<SplodeBob>();
		
		looseCount = 0;
		
		
		
		
		monText = GameObject.Find("MONEYTEXT").GetComponent<GUIText>();
		lastWinText = GameObject.Find("LASTWINTEXT").GetComponent<GUIText>();

	}
	
	
	float restartTimer = 0.0f;
	
	// Update is called once per frame
	void Update () 
	{
		
			monText = GameObject.Find("MONEYTEXT").GetComponent<GUIText>();
		lastWinText = GameObject.Find("LASTWINTEXT").GetComponent<GUIText>();
		
		monText.text = currMoney + ",- Kr";
		lastWinText.text = "sist gevinst, " + moneyWin + ",- Kr";
		
		
		if(Time.timeScale<5.0f)
		{
			//Time.timeScale +=Time.deltaTime*  0.00002f;
			
		}
		
		
		if(Input.GetKeyDown(KeyCode.Y))
		{
			sploder.onSwitch = true;
			looseCount=3;
		}
		
		if(looseCount==3)
		{
			
			if(sploder.holdMe)
				sploder.onSwitch = true;
			
			restartTimer+=Time.deltaTime;
			if(restartTimer>=8.0f)
				Application.LoadLevel(0);
			
			return;
		}
		
		if(Input.GetKeyDown(KeyCode.G))
			Application.LoadLevel(0);
	
		if(Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (GetComponent<Collider>().Raycast (ray, out hit, 2000.0f)) 
			{
				Time.timeScale = 7.0f;
				camMotion.posToSpeed = 0.09f;
				startGamblingPOWER();
			}
		}
		
		
		
	}
	
	void startGamblingPOWER()
	{
		
		currMoney-=1;
			//Vector3 theLookPos = Vector3.zero;
		//camMotion.lookAt = transform.position;
			
		//Vector3 theBePos = Vector3.zero;
	//	camMotion.positionToBe= Vector3.forward*camDist + transform.position + Vector3.right*camlRDist ;
			   
			   sploder.onSwitch = true;
			   moneyWin = 0;
			   if(currFab)
				   Destroy(currFab.gameObject);
			   //TreeStrudeGen
			   int winType = Random.Range(0,6);
			   if(winType<=0)
			   {
				 if(winType==0)  
					genFab.depth = Random.Range(1,21);
				 if(winType==1)  
					genFab.depth = Random.Range(10,21);
				 if(winType==2)  
					genFab.depth = Random.Range(10,21);
			   }
			   else
			   {
				   genFab.depth = 0;
				   looseCount++;
				   
			   }
				   
			   currFab = Instantiate(genFab,transform.position,transform.rotation) as TreeStrudeGen;
			   
			   
			   StrudeController strudeCon = currFab.GetComponentInChildren(typeof(StrudeController)) as StrudeController;
			   if(strudeCon!=null)
				   {
					   //Debug.Log("HERERERE MAOOHTHA FUCKER");
						strudeCon.updateStrudeChilds();
						   strudeCon.startTrigger = true;
					   
				   }
	}
	
	
	
	void OnGUI()
	{
		   /*if (GUI.Button ( new  Rect (10,10,150,100), "PRESS ME!")) 
		   {
			   
			   //startGamblingPOWER();
			   Application.LoadLevel(0);
			   
		   }*/
		   
		   //Time.timeScale = GUI.VerticalSlider (new  Rect (10,110,150,200), Time.timeScale, 20.0f, 0.0f);
	}
}
