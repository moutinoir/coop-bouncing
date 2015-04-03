using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// TODO: Implement balls on path instead of lines
// TODO: Implement animation on balls
// TODO: Implement collisions with other surface
// TODO: Make line dissapear on bad angle
// TODO: Calculate not just with angle but also with sensitivity


public class PredicitonPath : MonoBehaviour 
{
	LineRenderer lineRenderer;
	public CBPlayer player;  // TODO: Refactor. This shouldn't live in here

	void UpdateTrajectory(Vector3 startPos, Vector3 direction, float speed, float timePerSegmentInSeconds, float maxTravelDistance)
	{
		List<Vector3> positions = new List<Vector3>();
		Vector3 currentPos = startPos;
		Vector3 lastPos = startPos;
		
		positions.Add(startPos);
		
		float traveledDistance = 0.0f;

		while(traveledDistance < maxTravelDistance)
		{
			traveledDistance += speed * timePerSegmentInSeconds;
			bool hasHitSomething = TravelTrajectorySegment(currentPos, direction, speed, timePerSegmentInSeconds, positions);

			if (traveledDistance > maxTravelDistance)
			{
				break;
			}

			lastPos = currentPos;
			currentPos = positions[positions.Count - 1];
			direction = currentPos - lastPos;
			direction.Normalize();
		}
		
		BuildTrajectoryLine(positions);
	}
	
	bool TravelTrajectorySegment(Vector3 startPos, Vector3 direction, float speed, float timePerSegmentInSeconds, List<Vector3> positions)
	{
		Vector3 newPos = startPos + direction * speed * timePerSegmentInSeconds + Physics.gravity * timePerSegmentInSeconds;
		
		RaycastHit hitInfo;

		//bool hasHitSomething = Physics.Linecast(startPos, newPos, out hitInfo);
		bool hasHitSomething = false;

		if (hasHitSomething)
		{
			newPos = hitInfo.transform.position;
		}

		positions.Add(newPos);
		
		return hasHitSomething;
	}
	
	void BuildTrajectoryLine(List<Vector3> positions)
	{
		lineRenderer.SetVertexCount(positions.Count);

		for (int i = 0; i < positions.Count; ++i)
		{
			lineRenderer.SetPosition(i, positions[i]);
		}
	}
	// Use this for initialization
	void Start () 
	{
		lineRenderer = GetComponent<LineRenderer> ();
		lineRenderer.useWorldSpace = true;
		lineRenderer.SetWidth(0.02f, 0.02f);
		lineRenderer.SetColors(Color.yellow, Color.yellow);
	}

	void Update()
	{
		// TODO: pipe in speed from controls
		if (player.mThrowVector.Equals (Vector3.zero)) 
		{
			lineRenderer.enabled = false;
		} 
		else 
		{
			lineRenderer.enabled = true;
			UpdateTrajectory(player.transform.position, player.mThrowVector, 80.0f*player.mThrowVector.magnitude, 0.01f, 10.0f);
		}
	}

}
