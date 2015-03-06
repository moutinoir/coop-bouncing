using UnityEngine;
using System.Collections;

// TODO:
// Make grass clump
// Make grass orientate
// Make grass go behind curve
// Make grass not float weirdly

public class VegetationGenerator : MonoBehaviour 
{
	public CurvySpline bottomSpline;
	public GameObject grassObj;
	public GameObject treeObj;
	public int numberClumps = 20;
	public int tuftsPerClumpMin = 5;
	public int tuftsPerClumpMax = 10;

	void generateTuftAtCurvePoint(float t)
	{
		Vector3 randPointOnCurve = bottomSpline.Interpolate(t);
		
		GameObject grass = (GameObject)Instantiate(grassObj, 
		                                         randPointOnCurve, 
		                                         Quaternion.identity);

		grass.transform.position = new Vector3 (grass.transform.position.x, 
		                                        grass.transform.position.y, 
		                                        grass.transform.position.z+0.03f);
	}

	void generateClumpAtCurvePoint(float t, int tufts)
	{
		float randomAddition = 0.0f;

		for (int i = 0; i < tufts; i++) 
		{
			randomAddition += Random.Range(0.0f, 0.01f);
			generateTuftAtCurvePoint(t+randomAddition);
		}

	}

	void generateAllClumps()
	{
		for (int i = 0; i < numberClumps; i++) 
		{
			int numTufts = Random.Range(tuftsPerClumpMin, tuftsPerClumpMax);
			generateClumpAtCurvePoint(Random.Range(0.0f, 1.0f), numTufts);
		}
	}

	void spawnTree(Vector3 pos)
	{
		GameObject tree = (GameObject)Instantiate(treeObj, 
		                                         pos, 
		                                         Quaternion.identity);
	}
	void generateAllTrees()
	{
		float xMin = bottomSpline.ControlPoints[0].Position.x;
		float xMax = bottomSpline.ControlPoints[bottomSpline.ControlPointCount-1].Position.x;

		int stepFactor = ((int)xMax - (int)xMin) / 2;

		Debug.Log ("TREES IN !");
		Debug.Log ("xMin" + xMin);
		Debug.Log ("xMax" + xMax);

		for (int i = (int)xMin; i < (int)xMax; i+=4) 
		{
			Debug.Log ("TREE!");
			Vector3 pos = new Vector3(i, -3.0f, 0.5f);
			spawnTree(pos);
		}
	}
	// Use this for initialization
	void Start () 
	{
		generateAllClumps ();
		generateAllTrees ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
