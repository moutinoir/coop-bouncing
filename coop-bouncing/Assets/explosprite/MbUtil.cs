using UnityEngine;
using System.Collections;

public class MbUtil 
{
	public static bool pickedTexture(GUITexture gt)
	{
		
		if (Input.GetMouseButtonDown(0))
		{
			Rect r = gt.GetScreenRect(); 
			if(r.Contains(Input.mousePosition))
			{
				return true;
			}
		}
		return false;
	}
	
	public static bool mouseOnTexture(GUITexture gt)
	{
		
		Rect r = gt.GetScreenRect(); 
		if(r.Contains(Input.mousePosition))
		{
			return true;
		}
		
		return false;
	}
}
