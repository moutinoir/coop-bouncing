using UnityEngine;
using System.Collections;

public class MoneyShower : GUIShake 
{
	
	TextMesh tMesh;
	TextMesh[] tMeshes;
	
	void Awake()
	{
		tMesh  = GetComponent(typeof(TextMesh)) as TextMesh;
		tMeshes = GetComponentsInChildren<TextMesh>();
	}

	// Use this for initialization
	void Start () 
	{
		shakeInit();
	}
	
	// Update is called once per frame
	void Update () 
	{
		doShakeStuff();
		if(!initiateShake && inShakeTimer<0.0f)
		{
			Destroy(gameObject);
		}
	}
	
	
	public void setText(string txt)
	{
		foreach(TextMesh tm in tMeshes)
		{
			tm.text = txt;
		}
		tMesh.text = txt;
		
	}
}
