using UnityEngine;
using System.Collections;

public class LittleGUI : MonoBehaviour 
{

	
	// Use this for initialization
	void Start () 
	{
		  Time.timeScale = 5.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void OnGUI()
	{
		   if (GUI.Button ( new  Rect (10,10,150,100), "PRESS ME!")) 
		   {
			   StrudeController[] allObjs = FindObjectsOfType(typeof(StrudeController)) as StrudeController[];
			   foreach(StrudeController obj in allObjs)
			   {
				   StrudeController strudeCon = obj;
				   if(strudeCon)
				   {
					   if(strudeCon.transform.parent == null)
						   strudeCon.startTrigger = true;
						   
				   }
			   }
		   }
		   
		   Time.timeScale = GUI.VerticalSlider (new  Rect (10,110,150,200), Time.timeScale, 20.0f, 0.0f);
	}
}
