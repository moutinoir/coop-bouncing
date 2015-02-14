using UnityEngine;
using System.Collections;

public class CBSquare 
{
	public Rect Coordinates
	{
		get 
		{
			return mCoordinates;
		}
	}
	
	public CBSquare (Rect aCoordinates)
	{
		mCoordinates = aCoordinates;
	}

	private Rect mCoordinates;
}
