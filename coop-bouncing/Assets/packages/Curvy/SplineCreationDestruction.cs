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

			/*float xOffset = -20.0f;
			float yOffset = 20.0f;

			while(xOffset - yOffset == 5.0f)
			{

			}*/


			curvyLeft.Add(new Vector3(leftPrevPoint.x+Random.Range(0, 4.0f)-2.0f, 
			                          leftPrevPoint.y + 1, 
			                          leftPrevPoint.z));
			curvyRight.Add(new Vector3(rightPrevPoint.x+Random.Range(0, 4.0f)-2.0f, 
			                           rightPrevPoint.y + 1, 
			                           rightPrevPoint.z));

			counter = 0;
		}
	}
}
