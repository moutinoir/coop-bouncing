using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TreeStrudeGen : MonoBehaviour 
{
	
	public int depth;
	int currDepth = 0;
	public StrudeController strudeFab;
	
	public float minDistJoint;
	public float maxDistJoint;
	public int joints;
	
	public float minTSpawn;
	public float maxTSpawn;
	
	public int minChildSpawn;
	public int maxChildSpawn;
	
	public float gravity;
	public float minAngleMod;
	public float maxAngleMod;

	
	public Vector3 startVector;
	public Vector3 currPos = Vector3.zero;
	
	public float jointNoiseMinX;
	public float jointNoiseMaxX;
	public float jointNoiseMinY;
	public float jointNoiseMaxY;
	
	// Use this for initialization
	void Awake() 
	{
		generate(null,0.0f);
	}
	
	
	void generateControllerData(ref StrudeMeshCreate strudeMesh)
	{
		strudeMesh.conPoints = new List<Vector3>(joints);
		startVector = Quaternion.AngleAxis(Random.Range(minAngleMod,minAngleMod),Vector3.forward) * startVector;
		startVector.Normalize();
		
		strudeMesh.conPoints.Add(currPos);

		for(int i=1; i < /*strudeMesh.conPoints.Count*/joints; i++)
		{
			float distance = Random.Range(minDistJoint,maxDistJoint);
			Vector3 newPoint = new Vector3();
			newPoint = currPos + startVector*distance;
			newPoint.x += Random.Range(jointNoiseMinX,jointNoiseMaxX);
			newPoint.y += Random.Range(jointNoiseMinY,jointNoiseMaxY);
			newPoint.z = currDepth*2.1f;
			newPoint.x += 0.5f;
			strudeMesh.conPoints.Add(newPoint);
			currPos = newPoint;
		}
		strudeMesh.makeMeshFromSpline();
	}
	
	void generate(StrudeMeshCreate par, float t)
	{
		currDepth++;
		if(currDepth>=depth)
			return;
		
			StrudeController strudCon = Instantiate(strudeFab,transform.position,transform.rotation) as StrudeController;
			StrudeMeshCreate strudMesh = strudCon.strudeMesh;
		
		if(par!=null)
		{
			strudCon.fireTrigger = t;
			strudCon.transform.parent = par.transform;
			par.pathData(out currPos,t);
			startVector = Vector3.Cross(startVector,Vector3.forward);
			
			
		}
		else
		{
			strudCon.transform.parent = transform;
		}
		
		generateControllerData(ref strudMesh);
		int numChilds =Random.Range(minChildSpawn,maxChildSpawn);
		for(int i=0; i < numChilds; i++)
		{
			generate(strudMesh,Random.Range(minTSpawn,maxTSpawn));
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
		
		
	}
}


