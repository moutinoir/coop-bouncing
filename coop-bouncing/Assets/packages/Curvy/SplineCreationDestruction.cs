﻿using UnityEngine;
using System.Collections;

public class SplineCreationDestruction : MonoBehaviour 
{	
	public int counter = 0;
	//public GameObject splineLeft;
	//public GameObject splineRight;
	// Use this for initialization
	// Update is called once per frame
	public CurvySpline curvyLeft;
	public CurvySpline curvyRight;

	void Start()
	{
		//curvyLeft = splineLeft.GetComponent<CurvySpline>();
		//curvyRight = splineRight.GetComponent<CurvySpline>();
	}
	void Update () 
	{
		counter++;

		if (counter > 60)
		{
			int numPointsLeft = curvyLeft.ControlPointCount;
			int numPointsRight = curvyRight.ControlPointCount;

			Debug.Log ("Left" + numPointsLeft);
			Debug.Log ("Right" + numPointsRight);

			Debug.Log ("Add! " + numPointsLeft);
			Debug.Log ("Add! " + numPointsRight);

			Vector3 leftPrevPoint = curvyLeft.ControlPoints[numPointsLeft-1].Position;
			Vector3 rightPrevPoint = curvyRight.ControlPoints[numPointsRight-1].Position;

			curvyLeft.Add(new Vector3(leftPrevPoint.x+Random.Range(0, 2.0f)-1.0f, 
			                          leftPrevPoint.y + 1, 
			                          leftPrevPoint.z));



			curvyRight.Add(new Vector3(leftPrevPoint.x+3.0f+Random.Range(0, 2.0f)-1.0f, 
			                           rightPrevPoint.y + 1, 
			                           rightPrevPoint.z));

			counter = 0;

			//curvyLeft.ControlPoints
			//curvyLeft.Delete(curvyLeft.ControlPoints[0]);
			//curvyRight.Delete(curvyRight.ControlPoints[0]);
		}
	}
}
