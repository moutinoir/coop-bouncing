using UnityEngine;
using System.Collections;

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

	public Vector2[] branches;

	public Vector2 RandomOnUnitCircle2(float radius) 
	{
		Vector2 randomPointOnCircle = new Vector2(-1,-1);

		while(randomPointOnCircle.x < 0 && randomPointOnCircle.y < 0)
			randomPointOnCircle = Random.insideUnitCircle;

		randomPointOnCircle.Normalize();
		randomPointOnCircle *= radius;
		return randomPointOnCircle;
	}

	// Use this for initialization
	void Start () 
	{
		int numBranches = Random.Range (minBranches, maxBranches);
		branches = new Vector2[numBranches*2];

		for (int i = 0; i < numBranches*2; i+=2) 
		{
			int branchLength = Random.Range(minPlantRadius, maxPlantRadius);

			branches[i]   = Vector2.zero;
			branches[i+1] = RandomOnUnitCircle2(branchLength);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		for (int i = 0; i < branches.Length; i+=2) 
		{
			Debug.DrawLine(branches[i],  branches[i+1]);
		}
	}
}
