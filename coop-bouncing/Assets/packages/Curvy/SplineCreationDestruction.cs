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
	public GameObject  ball;

	public float caveWidthMin;
	public float caveWidthVariation;
	public float maxControlPoints;

	public CBFollowSpline splineLeft;
	public CBFollowSpline splineRight;

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

			Vector3 leftPrevPoint = curvyLeft.ControlPoints[numPointsLeft-1].Position;
			Vector3 rightPrevPoint = curvyRight.ControlPoints[numPointsRight-1].Position;

			curvyLeft.Add(new Vector3(leftPrevPoint.x+Random.Range(0, caveWidthVariation)-caveWidthVariation/2, 
			                          leftPrevPoint.y + 1.0f, 
			                          leftPrevPoint.z));

			curvyRight.Add(new Vector3(leftPrevPoint.x+caveWidthMin+Random.Range(0, caveWidthVariation)-caveWidthVariation/2, 
			                           rightPrevPoint.y + 1.0f, 
			                           rightPrevPoint.z));

			counter = 0;

			//curvyLeft.ControlPoints
			if(curvyLeft.ControlPointCount > maxControlPoints)
			{

				curvyLeft.Delete(curvyLeft.ControlPoints[0]);
				curvyRight.Delete(curvyRight.ControlPoints[0]);
			}

			// game over check
			if(ball.transform.position.y < curvyLeft.ControlPoints[0].Position.y)
			{
				Debug.Log("Game Over!");
				Application.LoadLevel(0);
			}

		}
	}
}
