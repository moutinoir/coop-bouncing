using UnityEngine;
using System.Collections;

public class CamFollow : MonoBehaviour 
{

	
	public StrudeController strudeCon = null;
	
	public Vector3 lookAt;
	public Vector3 positionToBe;
	public float lookAtSpeed;
	public float posToSpeed;
	
	
	
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		if(lookAt==null)
			return;
	
		transform.position = Vector3.Slerp(transform.position,
		                                              positionToBe,
		                                              posToSpeed);
		
		
		Vector3 lookTo = lookAt - transform.position;
		Quaternion to = Quaternion.identity;
		to.SetLookRotation(lookTo);
		transform.rotation = Quaternion.Slerp(transform.rotation,to,
		                                                   lookAtSpeed);
		
	}
}
