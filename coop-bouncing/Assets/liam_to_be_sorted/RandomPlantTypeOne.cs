using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// http://corexii.com/ocean_insight.png as inspiration
// This plant will represent the small clump of grass without any branching
// Other types should be:

// "Spiky Ball" plant
// Branching Seaweed plant

// From http://corexii.com/jungle.jpg
//
// Draping vines
// Standing Grass
// Branching Tree


public class RandomPlantTypeOne : MonoBehaviour {

	public int minBranches = 6;
	public int maxBranches = 10;

	public int maxPlantRadius = 20;
	public int minPlantRadius = 10;

	public int minNodules = 3;
	public int maxNodules = 5;

	public Vector2[] branches;
	public Color[] branchColors; // for debugging

	public Vector2 RandomOnUnitCircle2(float radius) 
	{
		Vector2 randomPointOnCircle = new Vector2(-1,-1);

		while (randomPointOnCircle.y < 0) 
		{
			randomPointOnCircle = Random.insideUnitCircle;
		}

		randomPointOnCircle.Normalize();
		randomPointOnCircle *= radius;
		return randomPointOnCircle;
	}

	void initFirstVersion()
	{
		int numBranches = Random.Range (minBranches, maxBranches);
		int numNodules = Random.Range (minNodules, maxNodules);
		
		// +2 for beginning and end point
		branches     = new Vector2[numBranches*(numNodules + 2)];
		branchColors = new Color[numBranches * (numNodules + 2)];
		
		int arrayWalk = 0;
		
		for (int i = 0; i < numBranches; i+=2) 
		{
			int branchLength = Random.Range(minPlantRadius, maxPlantRadius);
			
			//branches[i]   = Vector2.zero;
			//branches[i+1] = RandomOnUnitCircle2(branchLength);
			
			Vector2 initVec = Vector2.zero;
			branches[arrayWalk++] = initVec;
			Vector2 endVec  = RandomOnUnitCircle2(branchLength);
			
			List<float> nodules = new List<float>();
			
			nodules.Add(0.0f);
			for(int j = 0; j < numNodules; j++)
			{
				float nodPoint = Random.Range (0.0f, 1.0f);
				Debug.Log(nodPoint);
				nodules.Add(nodPoint);
			}
			
			nodules.Add (1.0f);
			nodules.Sort();
			
			foreach(float nod in nodules)
			{
				Vector2 partVec = new Vector2(endVec.x*nod, endVec.y*nod);
				Debug.Log (arrayWalk);
				branches[arrayWalk++] = partVec;
			}
		}
		
		for(int i = 0; i < numBranches*(numNodules+2); i++)
		{
			if(i%2 == 0)
				branchColors[i] = Color.red;
			else
				branchColors[i] = Color.white;			
			
			//branchColors[i] = new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f));
		}
		
		Debug.Log ("Generation Results");
		Debug.Log (branches);
	}

	void initSecondVersion()
	{
		int numBranches = Random.Range (minBranches, maxBranches);
		int branchLength = Random.Range(minPlantRadius, maxPlantRadius);

		float minBranchLength = 5.0f;
		float maxBranchLength = 10.0f;

		Vector2 initVec = Vector2.zero;
		Vector2 endVec  = RandomOnUnitCircle2(branchLength);
	}

	// Use this for initialization
	void Start () 
	{
		initSecondVersion ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		for (int i = 0; i < branches.Length-1; i++) 
		{
			Debug.DrawLine(branches[i],  branches[i+1], branchColors[i]);
		}
	}
}
