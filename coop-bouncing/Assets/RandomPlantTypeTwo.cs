using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomPlantTypeTwo : MonoBehaviour 
{
	public Material mat;
	public GameObject triangle;

	public float minLength = 1.2f;
	public float maxLength = 1.5f;

	// determines the "spikyness" of the bush
	public float minRotateAround = 10.0f;
	public float maxRotateAround = 25.0f;

	// determines the width of the leaf and variation
	public float minLeafWidth = 0.01f;
	public float maxLeafWidth = 0.05f;

	Vector2 getPointOnCircle(float degree, float radius)
	{
		float rad = degree * Mathf.Deg2Rad;
		float x = Mathf.Cos (rad);
		float y = Mathf.Sin (rad);
		return new Vector2 (x, y) * radius;
	}

	void spawnBranches()
	{
		float rotateCount = 0.0f;
		
		while (rotateCount < 360.0f) 
		{
			Debug.Log (rotateCount);
			rotateCount += Random.Range(minRotateAround, maxRotateAround);

			float leafWidth = Random.Range(minLeafWidth, maxLeafWidth);

			Vector3 endVec       = new Vector3(0.0f, Random.Range(minLength, maxLength), 0.0f);
			Vector3 baseVecLeft  = new Vector3(-leafWidth, 0.0f,0.0f);
			Vector3 baseVecRight = new Vector3(leafWidth, 0.0f, 0.0f);

			Debug.Log (endVec);
			Debug.Log (baseVecLeft);
			Debug.Log (baseVecRight);
		
			GameObject tri = (GameObject)Instantiate(triangle, 
			                                         transform.position, 
			                                         Quaternion.identity);

			SimpleTriangle triScript = tri.GetComponent<SimpleTriangle>();

			triScript.a = baseVecLeft;
			triScript.b = endVec;
			triScript.c = baseVecRight;

			tri.transform.RotateAround(transform.position,Vector3.forward,rotateCount);
		}
	}
	void generateTriangles()
	{

	}

	// Use this for initialization
	void Start () 
	{
		spawnBranches ();
		generateTriangles ();
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	void OnPostRender() 
	{

	}
}
