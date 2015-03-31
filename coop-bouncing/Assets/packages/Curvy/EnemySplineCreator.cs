using UnityEngine;
using System.Collections;

public class EnemySplineCreator : MonoBehaviour 
{
	public CBAutoFollowSpline FollowSplineEnemy;
	private Vector3 prevPoint;
	private Vector3 newPoint;
	
	private float lastAngle = Mathf.PI*2;
	private float angleMaxVar = Mathf.PI/8;

	// calculate tangent points and store here to use to generate cave walls
	public Vector3 tangentTop;
	public Vector3 tangentBottom;

	private void GenerateProspectivePoint()
	{
		// Randomly select an angle between 45 and 135 degrees (but in radians)
		float angleRandVar = Random.Range (-angleMaxVar, angleMaxVar);
		Debug.Log (angleRandVar);
		lastAngle += angleRandVar;
		lastAngle = Mathf.Clamp (lastAngle, (Mathf.PI/2)*3.5f, (Mathf.PI/2)*4.5f);

		newPoint  = (new Vector3(Mathf.Cos(lastAngle), Mathf.Sin(lastAngle), 0.0f)*2) + prevPoint;
	}

	private void CalculateTangents()
	{
		Vector3 newTestPoint = FollowSplineEnemy.Spline.Interpolate(1.0f);

		tangentTop    = Quaternion.Euler(0,0,90)*(-FollowSplineEnemy.Spline.GetTangent (1.0f));
		tangentTop = newTestPoint + (tangentTop*1.5f);
		tangentBottom = Quaternion.Euler(0,0,90)*(FollowSplineEnemy.Spline.GetTangent (1.0f));
		tangentBottom = newTestPoint + (tangentBottom*1.5f);
	}
	public void GenerateNewPoint()
	{
		int numPoints = FollowSplineEnemy.Spline.ControlPointCount;
		
		prevPoint = FollowSplineEnemy.Spline.ControlPoints[numPoints-1].Position;
		
		Random.seed = (int)Time.timeSinceLevelLoad;
		
		GenerateProspectivePoint();
		
		// left spline
		float distanceBeforeAddingPoint = FollowSplineEnemy.Spline.TFToDistance(FollowSplineEnemy.CurrentTF);
		FollowSplineEnemy.Spline.Add(newPoint);
		
		float distanceAfterAddingPoint = FollowSplineEnemy.Spline.TFToDistance(FollowSplineEnemy.CurrentTF);
		float newTFAfterAddingPoint = FollowSplineEnemy.Spline.DistanceToTF(distanceBeforeAddingPoint);
		FollowSplineEnemy.CurrentTF = newTFAfterAddingPoint;

		CalculateTangents ();
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}