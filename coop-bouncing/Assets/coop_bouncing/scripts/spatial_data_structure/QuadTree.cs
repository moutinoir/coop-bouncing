using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PopUtils
{
	public class Point
	{
		public Point(float aX, float aY)
		{
			x = aX;
			y = aY;
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

		float x;
		float y;
	}

	// Axis-aligned bounding box with half dimension and center
	public class AABB
	{
		public AABB(Point aCenter, float aHalfDimension) 
		{
			
		}
		
		public bool ContainsPoint(Point aPoint) 
		{
			return true;
		}
		
		public bool IntersectsAABB(AABB aAABB) 
		{
			return true;
		}

		public Point Center
		{
			get
			{
				return center;
			}
		}

		public float HalfDimension
		{
			get
			{
				return halfDimension;
			}
		}

		Point center;
		float halfDimension;
	}

	public class QuadTree
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

			northWest = new QuadTree (new AABB (new Point (boundary.Center.X - childHalfDimension, boundary.Center.Y + childHalfDimension), childHalfDimension));
			northEast = new QuadTree (new AABB (new Point (boundary.Center.X + childHalfDimension, boundary.Center.Y + childHalfDimension), childHalfDimension));
			southWest = new QuadTree (new AABB (new Point (boundary.Center.X - childHalfDimension, boundary.Center.Y - childHalfDimension), childHalfDimension));
			southEast = new QuadTree (new AABB (new Point (boundary.Center.X + childHalfDimension, boundary.Center.Y - childHalfDimension), childHalfDimension));

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
		const int nodeCapacity = 4;
		
		// Axis-aligned bounding box stored as a center with half-dimensions
		// to represent the boundaries of this quad tree
		AABB boundary;
		
		// Points in this quad tree node
		List<Point> points;
		
		// Children
		QuadTree northWest = null;
		QuadTree northEast = null;
		QuadTree southWest = null;
		QuadTree southEast = null;
	}
}

