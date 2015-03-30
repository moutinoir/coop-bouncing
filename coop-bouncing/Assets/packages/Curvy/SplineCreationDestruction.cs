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

	public CaveMeshGenerator caveMeshUpper;
	public CaveMeshGenerator caveMeshLower;

	private float lastAngleTop = Mathf.PI*2;
	private float lastAngleBottom = Mathf.PI*2;
	private float angleMaxVar = Mathf.PI/8;
	
	void Start()
	{
		//curvyLeft = splineLeft.GetComponent<CurvySpline>();
		//curvyRight = splineRight.GetComponent<CurvySpline>();
	}

	private Vector3 InsideArc () 
	{
		// Randomly select an angle between 45 and 135 degrees (but in radians)
		float angleRandVar = Random.Range (-angleMaxVar, angleMaxVar);
		Debug.Log ("angleRandVar: " + angleRandVar);
		lastAngleTop += angleRandVar;
		//Mathf.Clamp (lastAngleTop, Mathf.PI / 2, (3 * Mathf.PI) / 2);
		Mathf.Clamp (lastAngleTop, Mathf.PI*2, Mathf.PI*2);
		return new Vector3(Mathf.Cos(lastAngleTop)*Random.Range(0.0f,2.0f), Mathf.Sin(lastAngleTop)*Random.Range(0.0f,2.0f), 0.0f);
	}

	void GenerateNewPoint()
	{
		int numPointsLeft = FollowSplineLeft.Spline.ControlPointCount;
		int numPointsRight = FollowSplineRight.Spline.ControlPointCount;
		
		/*Debug.Log ("Left" + numPointsLeft);
			Debug.Log ("Right" + numPointsRight);

			Debug.Log ("Add! " + numPointsLeft);
			Debug.Log ("Add! " + numPointsRight);*/
		
		Vector3 leftPrevPoint = FollowSplineLeft.Spline.ControlPoints[numPointsLeft-1].Position;
		Vector3 rightPrevPoint = FollowSplineRight.Spline.ControlPoints[numPointsRight-1].Position;
		
		Random.seed = (int)Time.timeSinceLevelLoad;
		
		Vector3 leftNewPoint = new Vector3();
		Vector3 rightNewPoint = new Vector3();
		
		float distanceBetweenNewPoints = 10.0f;
		
		while(distanceBetweenNewPoints < 2.0f || distanceBetweenNewPoints > 4.0f)
		{
			/*Vector3 leftNewPoint = new Vector3(leftPrevPoint.x+caveWidthMin+Random.Range(0.0f, caveWidthVariation)-caveWidthVariation/2.0f, 
				                                   leftPrevPoint.y + 1.0f, 
				                                   leftPrevPoint.z);
				
				
				Vector3 rightNewPoint = new Vector3(rightPrevPoint.x+caveWidthMin+Random.Range(0.0f, caveWidthVariation)-caveWidthVariation/2.0f,
				                                    rightPrevPoint.y + 1.0f, 
				                                    rightPrevPoint.z);*/
			
			leftNewPoint = InsideArc() + leftPrevPoint;
			rightNewPoint = InsideArc() + rightPrevPoint;
			
			Debug.Log ("leftNewPoint:  " + leftNewPoint);
			Debug.Log ("rightNewPoint: " + rightNewPoint);
			
			distanceBetweenNewPoints = Vector3.Distance(leftNewPoint, rightNewPoint);
		}
		
		/*float randDeltaLeftX = Random.Range(-caveWidthVariation, caveWidthVariation);
			float randDeltaLeftY = Random.Range(-caveWidthVariation, caveWidthVariation);

			float randDeltaRightX = Random.Range(-caveWidthVariation, caveWidthVariation);
			float randDeltaRightY = Random.Range(-caveWidthVariation, caveWidthVariation);

			Debug.Log ("LrandX: " + randDeltaLeftX);
			Debug.Log ("LrandY: " + randDeltaLeftY);

			Debug.Log ("RrandX: " + randDeltaRightX);
			Debug.Log ("RrandY: " + randDeltaRightY);

			Vector3 leftNewPoint = new Vector3(leftPrevPoint.x+randDeltaLeftX, 
			                                   leftPrevPoint.y+randDeltaLeftY, 
			                                   leftPrevPoint.z);
			
			
			Vector3 rightNewPoint = new Vector3(rightPrevPoint.x+randDeltaRightX,
			                                    rightPrevPoint.y+randDeltaRightY, 
			                                    rightPrevPoint.z);*/
		
		
		
		// left spline
		float distanceBeforeAddingPoint = FollowSplineLeft.Spline.TFToDistance(FollowSplineLeft.CurrentTF);
		FollowSplineLeft.Spline.Add(leftNewPoint);
		
		float distanceAfterAddingPoint = FollowSplineLeft.Spline.TFToDistance(FollowSplineLeft.CurrentTF);
		float newTFAfterAddingPoint = FollowSplineLeft.Spline.DistanceToTF(distanceBeforeAddingPoint);
		/*Debug.Log("[GAMEPLAY] Left curve : TFToDistance = " + distanceBeforeAddingPoint 
			          + " oldTF = " + FollowSplineLeft.CurrentTF
			          + " after adding point TFToDistance = " + distanceAfterAddingPoint
			          + " newTF = " + newTFAfterAddingPoint);*/
		FollowSplineLeft.CurrentTF = newTFAfterAddingPoint;
		
		// right spline
		distanceBeforeAddingPoint = FollowSplineRight.Spline.TFToDistance(FollowSplineRight.CurrentTF);
		FollowSplineRight.Spline.Add(rightNewPoint);
		
		distanceAfterAddingPoint = FollowSplineRight.Spline.TFToDistance(FollowSplineRight.CurrentTF);
		newTFAfterAddingPoint = FollowSplineRight.Spline.DistanceToTF(distanceBeforeAddingPoint);
		/*Debug.Log("[GAMEPLAY] Right curve : TFToDistance = " + distanceBeforeAddingPoint 
			          + " oldTF = " + FollowSplineRight.CurrentTF
			          + " after adding point TFToDistance = " + distanceAfterAddingPoint
			          + " newTF = " + newTFAfterAddingPoint);*/
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
	void Update () 
	{
		counter++;

		// TODO: Don't implement this on a set timer, implement this based on camera/world position (generate as player moves)
		if (counter > 20)
		{
			Debug.Log("POP");
			// Generate the points out 2 at a time so UV's e.t.c. don't go screwey
			GenerateNewPoint();
			GenerateNewPoint();

			caveMeshUpper.RecalculateMeshes();
			caveMeshLower.RecalculateMeshes();

		}
	}
}