using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PopUtils
{
	[System.Serializable]
	public class Point : System.Object
	{
		public Point(float aX, float aY)
		{
			x = aX;
			y = aY;
		}

		public Point(Vector2 aPosition)
		{
			x = aPosition.x;
			y = aPosition.y;
		}

		public float X
		{
			get
			{
				return x;
			}
		}

		public float Y
		{
			get
			{
				return y;
			}
		}

		[SerializeField]
		float x;
		[SerializeField]
		float y;
	}

	public abstract class Area : System.Object
	{
		public abstract bool ContainsPoint(float aX, float aY);
		
		public bool ContainsPoint(Point aPoint) 
		{
			return ContainsPoint(aPoint.X, aPoint.Y);
		}
		
		public bool ContainsPoint(Vector2 aPoint) 
		{
			return ContainsPoint(aPoint.x, aPoint.y);
		}
	}

	[System.Serializable]
	public class Circle : Area
	{
		public Circle (Vector2 aCenter, float aRadius)
		{
			center = aCenter;
			radius = aRadius;
		}
		
		public override bool ContainsPoint(float aX, float aY) 
		{
			if((center.x - aX)*(center.x - aX) + (center.y - aY)*(center.y - aY) < radius*radius)
			{
				return true;
			}
			return false;
		}
		
		public Vector2 Center
		{
			get
			{
				return center;
			}
		}
		
		public float Radius
		{
			get
			{
				return radius;
			}
		}

		[SerializeField]
		Vector2 center;
		[SerializeField]
		float radius;
	}

	// Axis-aligned bounding box with half dimension and center
	[System.Serializable]
	public class AABB : Area
	{
		public AABB(Vector2 aCenter, float aHalfDimension) 
		{
			center = aCenter;
			halfDimension = aHalfDimension;
		}
		
		public override bool ContainsPoint(float aX, float aY) 
		{
			if(aX >= center.x - halfDimension && aX < center.x + halfDimension
				&& aY >= center.y - halfDimension && aY < center.y + halfDimension)
				return true;
				
			return false;
		}
		
		public bool IntersectsAABB(AABB aAABB) 
		{
			return ContainsPoint(aAABB.NorthEast) || ContainsPoint(aAABB.NorthWest)
				|| ContainsPoint(aAABB.SouthEast) || ContainsPoint(aAABB.SouthWest);
		}

		public Vector2 Center
		{
			get
			{
				return center;
			}
		}
		
		public Vector2 NorthWest
		{
			get
			{
				return center + new Vector2(- halfDimension, halfDimension);
			}
		}
		
		public Vector2 NorthEast
		{
			get
			{
				return center + new Vector2(halfDimension, halfDimension);
			}
		}
		
		public Vector2 SouthEast
		{
			get
			{
				return center + new Vector2(halfDimension, -halfDimension);
			}
		}
		
		public Vector2 SouthWest
		{
			get
			{
				return center + new Vector2(-halfDimension, -halfDimension);
			}
		}
		
		public float HalfDimension
		{
			get
			{
				return halfDimension;
			}
		}

		[SerializeField]
		Vector2 center;
		[SerializeField]
		float halfDimension;
	}

	[System.Serializable]
	public class QuadTree : System.Object
	{
		public QuadTree(AABB aBoundary) 
		{
			boundary = aBoundary;
			points = new List<Point>();
		}

		public bool Insert(Point aPoint) 
		{
			// Ignore objects that do not belong in this quad tree
			if (!boundary.ContainsPoint(aPoint))
				return false; // object cannot be added
			
			// If there is space in this quad tree, and the tree has no children, add the object here
			if (northWest == null && points.Count < nodeCapacity)
			{
				points.Add(aPoint);
				return true;
			}
			
			// Otherwise, subdivide and then add the point to whichever node will accept it
			if (northWest == null)
				Subdivide();
			
			if (northWest.Insert(aPoint)) return true;
			if (northEast.Insert(aPoint)) return true;
			if (southWest.Insert(aPoint)) return true;
			if (southEast.Insert(aPoint)) return true;
			
			// Otherwise, the point cannot be inserted for some unknown reason (this should never happen)
			return false;
		}

		// create four children that fully divide this quad into four quads of equal area
		void Subdivide() 
		{
			float childHalfDimension = boundary.HalfDimension / 2f;

			northWest = new QuadTree (new AABB (new Vector2(boundary.Center.x - childHalfDimension, boundary.Center.y + childHalfDimension), childHalfDimension));
			northEast = new QuadTree (new AABB (new Vector2(boundary.Center.x + childHalfDimension, boundary.Center.y + childHalfDimension), childHalfDimension));
			southWest = new QuadTree (new AABB (new Vector2(boundary.Center.x - childHalfDimension, boundary.Center.y - childHalfDimension), childHalfDimension));
			southEast = new QuadTree (new AABB (new Vector2(boundary.Center.x + childHalfDimension, boundary.Center.y - childHalfDimension), childHalfDimension));

			foreach (Point point in points) 
			{
				Insert(point);
			}
			points.Clear ();
		} 

		List<Point> QueryRange(AABB aRange) 
		{
			// Prepare an array of results
			List<Point> pointsInRange = new List<Point>();
			
			// Automatically abort if the range does not intersect this quad
			if (!boundary.IntersectsAABB(aRange))
				return pointsInRange; // empty list
			
			// Check objects at this quad level
			for (int p = 0; p < points.Count; p++)
			{
				if (aRange.ContainsPoint(points[p]))
					pointsInRange.Add(points[p]);
			}
			
			// Terminate here, if there are no children
			if (northWest == null)
				return pointsInRange;
			
			// Otherwise, add the points from the children
			pointsInRange.AddRange(northWest.QueryRange(aRange));
			pointsInRange.AddRange(northEast.QueryRange(aRange));
			pointsInRange.AddRange(southWest.QueryRange(aRange));
			pointsInRange.AddRange(southEast.QueryRange(aRange));
			
			return pointsInRange;
		}

		// how many elements can be stored in this quad tree node
		const int nodeCapacity = 2;
		
		// Axis-aligned bounding box stored as a center with half-dimensions
		// to represent the boundaries of this quad tree
		[SerializeField]
		AABB boundary;
		
		// Points in this quad tree node
		[SerializeField]
		List<Point> points;
		
		// Children
		[SerializeField]
		QuadTree northWest = null;
		[SerializeField]
		QuadTree northEast = null;
		[SerializeField]
		QuadTree southWest = null;
		[SerializeField]
		QuadTree southEast = null;
	}
}