using UnityEngine;
using System.Collections;

public class SplineCreationDestruction : MonoBehaviour 
{	
	public int counter = 0;
	//public GameObject splineLeft;
	//public GameObject splineRight;
	// Use this for initialization
	// Update is called once per frame
	//public CurvySpline curvyLeft;
	//public CurvySpline curvyRight;

	public CBFollowSpline FollowSplineLeft;
	public CBFollowSpline FollowSplineRight;

	public GameObject  ball;

	public float caveWidthMin;
	public float caveWidthVariation;
	public float maxControlPoints;

	void Start()
	{
		//curvyLeft = splineLeft.GetComponent<CurvySpline>();
		//curvyRight = splineRight.GetComponent<CurvySpline>();
	}

	void Update () 
	{
		counter++;

		if (counter > 20)
		{
			Debug.Log("POP");
			int numPointsLeft = FollowSplineLeft.Spline.ControlPointCount;
			int numPointsRight = FollowSplineRight.Spline.ControlPointCount;

			/*Debug.Log ("Left" + numPointsLeft);
			Debug.Log ("Right" + numPointsRight);

			Debug.Log ("Add! " + numPointsLeft);
			Debug.Log ("Add! " + numPointsRight);*/

			Vector3 leftPrevPoint = FollowSplineLeft.Spline.ControlPoints[numPointsLeft-1].Position;
			Vector3 rightPrevPoint = FollowSplineRight.Spline.ControlPoints[numPointsRight-1].Position;

			// left spline
			float distanceBeforeAddingPoint = FollowSplineLeft.Spline.TFToDistance(FollowSplineLeft.CurrentTF);
			FollowSplineLeft.Spline.Add(new Vector3(leftPrevPoint.x+Random.Range(0, caveWidthVariation)-caveWidthVariation/2, 
			                          leftPrevPoint.y + 1.0f, 
			                          leftPrevPoint.z));

			float distanceAfterAddingPoint = FollowSplineLeft.Spline.TFToDistance(FollowSplineLeft.CurrentTF);
			float newTFAfterAddingPoint = FollowSplineLeft.Spline.DistanceToTF(distanceBeforeAddingPoint);
			Debug.Log("[GAMEPLAY] Left curve : TFToDistance = " + distanceBeforeAddingPoint 
			          + " oldTF = " + FollowSplineLeft.CurrentTF
			          + " after adding point TFToDistance = " + distanceAfterAddingPoint
			          + " newTF = " + newTFAfterAddingPoint);
			FollowSplineLeft.CurrentTF = newTFAfterAddingPoint;

			// right spline
			distanceBeforeAddingPoint = FollowSplineRight.Spline.TFToDistance(FollowSplineRight.CurrentTF);
			FollowSplineRight.Spline.Add(new Vector3(leftPrevPoint.x+caveWidthMin+Random.Range(0, caveWidthVariation)-caveWidthVariation/2, 
			                           rightPrevPoint.y + 1.0f, 
			                           rightPrevPoint.z));

			distanceAfterAddingPoint = FollowSplineRight.Spline.TFToDistance(FollowSplineRight.CurrentTF);
			newTFAfterAddingPoint = FollowSplineRight.Spline.DistanceToTF(distanceBeforeAddingPoint);
			Debug.Log("[GAMEPLAY] Right curve : TFToDistance = " + distanceBeforeAddingPoint 
			          + " oldTF = " + FollowSplineRight.CurrentTF
			          + " after adding point TFToDistance = " + distanceAfterAddingPoint
			          + " newTF = " + newTFAfterAddingPoint);
			FollowSplineRight.CurrentTF = newTFAfterAddingPoint;

			counter = 0;

			//curvyLeft.ControlPoints
			/*if(FollowSplineLeft.Spline.ControlPointCount > maxControlPoints)
			{
				FollowSplineLeft.Spline.Delete(FollowSplineLeft.Spline.ControlPoints[0]);
				FollowSplineRight.Spline.Delete(FollowSplineRight.Spline.ControlPoints[0]);
			}*/

			// game over check
			/*if(ball.transform.position.y < FollowSplineLeft.Spline.ControlPoints[0].Position.y)
			{
				Debug.Log("Game Over!");
				Application.LoadLevel(0);
			}*/

		}
	}
}
