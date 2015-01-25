using UnityEngine;
using System.Collections;

public class loadLevel : MonoBehaviour 
{

	void Play()
	{
		Application.LoadLevel (1);
	}

	void Start()
	{
		Invoke("Play",3f);
	}


}
