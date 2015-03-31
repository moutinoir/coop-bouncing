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

	public EnemySplineCreator enemySplineCreator;

	private float lastAngleTop = Mathf.PI*2;
	private float lastAngleBottom = Mathf.PI*2;
	private float angleMaxVar = Mathf.PI/8;

	private Vector3 leftPrevPoint;
	private Vector3 rightPrevPoint;

	private Vector3 leftNewPoint;
	private Vector3 rightNewPoint;
	
	void Start()
	{
		//curvyLeft = splineLeft.GetComponent<CurvySpline>();
		//curvyRight = splineRight.GetComponent<CurvySpline>();
	}

	private void GenerateProspectivePoints () 
	{
		// Randomly select an angle between 45 and 135 degrees (but in radians)
		float angleRandVar = Random.Range (-angleMaxVar, angleMaxVar);
		lastAngleTop += angleRandVar;
		lastAngleTop = Mathf.Clamp (lastAngleTop, (Mathf.PI/2)*3.5f, (Mathf.PI/2)*4.5f);

		angleRandVar = Random.Range (-angleMaxVar, angleMaxVar);
		lastAngleBottom += angleRandVar;
		lastAngleBottom = Mathf.Clamp (lastAngleBottom, (Mathf.PI/2)*3.5f, (Mathf.PI/2)*4.5f);

		leftNewPoint  = new Vector3(Mathf.Cos(lastAngleTop), Mathf.Sin(lastAngleTop), 0.0f) + leftPrevPoint;
		rightNewPoint = new Vector3(Mathf.Cos(lastAngleBottom), Mathf.Sin(lastAngleBottom), 0.0f) + rightPrevPoint;
	}
	
	private bool IsIntersecting(Vector2 a, Vector2 b, Vector2 c, Vector2 d)
	{
		float denominator = ((b.x - a.x) * (d.y - c.y)) - ((b.y - a.y) * (d.x - c.x));
		float numerator1 = ((a.y - c.y) * (d.x - c.x)) - ((a.x - c.x) * (d.y - c.y));
		float numerator2 = ((a.y - c.y) * (b.x - a.x)) - ((a.x - c.x) * (b.y - a.y));
		
		// Detect coincident lines (has a problem, read below)
		if (denominator == 0) return numerator1 == 0 && numerator2 == 0;
		
		float r = numerator1 / denominator;
		float s = numerator2 / denominator;
		
		return (r >= 0 && r <= 1) && (s >= 0 && s <= 1);
	}

	static bool FasterLineSegmentIntersection(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4) {
		
		Vector2 a = p2 - p1;
		Vector2 b = p3 - p4;
		Vector2 c = p1 - p3;
		
		float alphaNumerator = b.y*c.x - b.x*c.y;
		float alphaDenominator = a.y*b.x - a.x*b.y;
		float betaNumerator  = a.x*c.y - a.y*c.x;
		float betaDenominator  = alphaDenominator; /*2013/07/05, fix by Deniz*/
		
		bool doIntersect = true;
		
		if (alphaDenominator == 0 || betaDenominator == 0) {
			doIntersect = false;
		} else {
			
			if (alphaDenominator > 0) {
				if (alphaNumerator < 0 || alphaNumerator > alphaDenominator) {
					doIntersect = false;
				}
			} else if (alphaNumerator > 0 || alphaNumerator < alphaDenominator) {
				doIntersect = false;
			}
			
			if (doIntersect && betaDenominator > 0) {
				if (betaNumerator < 0 || betaNumerator > betaDenominator) {
					doIntersect = false;
				}
			} else if (betaNumerator > 0 || betaNumerator < betaDenominator) {
				doIntersect = false;
			}
		}
		
		return doIntersect;
	}

	void GenerateNewPoint()
	{
		int numPointsLeft = FollowSplineLeft.Spline.ControlPointCount;
		int numPointsRight = FollowSplineRight.Spline.ControlPointCount;
		
		leftPrevPoint = FollowSplineLeft.Spline.ControlPoints[numPointsLeft-1].Position;
		rightPrevPoint = FollowSplineRight.Spline.ControlPoints[numPointsRight-1].Position;
		
		Random.seed = (int)Time.timeSinceLevelLoad;

		float distanceBetweenNewPoints = 10.0f;
		bool newPointsInvalid = true;

		while (newPointsInvalid) 
		{
			newPointsInvalid = false;

			GenerateProspectivePoints();

			distanceBetweenNewPoints = Vector3.Distance(leftNewPoint, rightNewPoint);

			if(distanceBetweenNewPoints < 2.0f || distanceBetweenNewPoints > 4.0f)
			{
				Debug.Log ("Too much distance happened");
				newPointsInvalid = true;
			}
			if(FasterLineSegmentIntersection(new Vector2(leftPrevPoint.x, leftPrevPoint.y), 
			                  new Vector2(leftNewPoint.x, leftNewPoint.y), 
			                  new Vector2(rightPrevPoint.x, rightPrevPoint.y), 
			                  new Vector2(rightNewPoint.x, rightNewPoint.y)))
			{
				Debug.Log ("Intersection happened");
				newPointsInvalid = true;
			}
		}
	}

	private void AddPointsAndRecalculateTFs()
	{
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
	}
	private void GenerateNewPoints()
	{
		enemySplineCreator.GenerateNewPoint();
		leftNewPoint  = enemySplineCreator.tangentBottom;
		rightNewPoint = enemySplineCreator.tangentTop;
		
		AddPointsAndRecalculateTFs();
	}
	void Update () 
	{
		counter++;

		// TODO: Don't implement this on a set timer, implement this based on camera/world position (generate as player moves)
		if (counter > 40)
		{
			Random.seed = (int)Time.timeSinceLevelLoad;

			GenerateNewPoints();
			GenerateNewPoints();

			caveMeshUpper.RecalculateMeshes();
			caveMeshLower.RecalculateMeshes();

			counter = 0;
		}
	}
}