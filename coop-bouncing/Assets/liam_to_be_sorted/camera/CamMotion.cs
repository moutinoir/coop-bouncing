using UnityEngine;
using System.Collections;

public class CamMotion : MonoBehaviour 
{
	
	public Transform lookAt;
	public Vector3 positionToBe;
	public float lookAtSpeed;
	public float posToSpeed;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void LateUpdate () 
	{
		if(lookAt==null)
			return;
	
		transform.position = Vector3.Slerp(transform.position,
		                                              positionToBe,
		                                              posToSpeed);
		
		
		Vector3 lookTo = lookAt.position - transform.position;
		Quaternion to = Quaternion.identity;
		to.SetLookRotation(lookTo);
		transform.rotation = Quaternion.Slerp(transform.rotation,to,
		                                                   lookAtSpeed);
		
	}
	
}
