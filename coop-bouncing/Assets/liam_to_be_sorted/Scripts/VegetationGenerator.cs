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

	void generateGrass()
	{
		for (int i = 0; i < 50; i++) 
		{
			Vector3 randPointOnCurve = bottomSpline.Interpolate(Random.Range(0.0f, 1.0f));

			GameObject tri = (GameObject)Instantiate(grassObj, 
			                                         randPointOnCurve, 
			                                         Quaternion.identity);
		}
	}
	// Use this for initialization
	void Start () 
	{
		generateGrass ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
