using UnityEngine;
using System.Collections;

public class CBWin : MonoBehaviour 
{
	public CBAutoFollowSpline EnemyAutoFollowSpline;

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.name == "Ball"
		   || other.gameObject.name == "ball2D")
		{
			EnemyAutoFollowSpline.Speed = 0f;
			Debug.Log("Win!");
			Invoke("Reload", 5f);
		}
	}

	void Reload ()
	{
		Application.LoadLevel(0);
	}
}
