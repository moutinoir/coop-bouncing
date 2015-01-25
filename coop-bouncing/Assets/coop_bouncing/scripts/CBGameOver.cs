using UnityEngine;
using System.Collections;

public class CBGameOver : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.name == "Player1" || other.gameObject.name == "Player2" 
		   || other.gameObject.name == "Body" || other.gameObject.name == "Ball"
		   || other.gameObject.name == "ball2D")
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
