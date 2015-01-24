using UnityEngine;
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
		//Debug.Log (curvyLeft);
		//int numPoints = curvyLeft.ControlPoints.Capacity;


		if (counter > 60) //&& curvyLeft != null && curvyRight != null) 
		{

			int numPointsLeft = curvyLeft.ControlPointCount;
			int numPointsRight = curvyRight.ControlPointCount;

			Debug.Log ("Left" + numPointsLeft);
			Debug.Log ("Right" + numPointsRight);

			Debug.Log ("Add! " + numPointsLeft);
			Debug.Log ("Add! " + numPointsRight);

			//GetComponent
			//Debug.Log("Left"  + curvyLeft.ControlPoints[0].Position);
			//Debug.Log("Right" + curvyRight.ControlPoints[1].Position);

			Vector3 leftPrevPoint = curvyLeft.ControlPoints[numPointsLeft-1].Position;
			Vector3 rightPrevPoint = curvyRight.ControlPoints[numPointsRight-1].Position;

			curvyLeft.Add(new Vector3(leftPrevPoint.x+Random.Range(0, 4.0f)-2.0f, 
			                          leftPrevPoint.y + 1, 
			                          leftPrevPoint.z));
			curvyRight.Add(new Vector3(rightPrevPoint.x+Random.Range(0, 4.0f)-2.0f, 
			                           rightPrevPoint.y + 1, 
			                           rightPrevPoint.z));

			//curvyRight.Add(new Vector3(100, 100, 100));
			//curvyLeft.ControlPoints[numPoints+1].Position = curvyRight.ControlPoints[numPoints].Position;
			//curvyLeft.ControlPoints[numPoints+1].Position.y += 10;

			/*curvyRight.Add();
			curvyRight.ControlPoints[numPoints+1].Position = curvyRight.ControlPoints[numPoints].Position;
			curvyRight.ControlPoints[numPoints+1].Position.y += 10;
			*/counter = 0;
		}
	}
}
