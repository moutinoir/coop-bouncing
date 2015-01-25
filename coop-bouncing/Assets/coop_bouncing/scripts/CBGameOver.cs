using UnityEngine;
using System.Collections;

public class CBGameOver : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.name == "Player1" || collision.gameObject.name == "Player2" 
		   || collision.gameObject.name == "Body" || collision.gameObject.name == "Ball"
		   || collision.gameObject.name == "ball2D")
		{
			Debug.Log("Game Over!");
			Application.LoadLevel(0);
		}
	}

	// Update is called once per frame
	void Update () 
	{
	
	}
}
