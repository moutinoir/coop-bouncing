using UnityEngine;
using System.Collections;

//to support editing spline and seeing generated geomtry update in realtime..
//we make script execute in edit mode.
[ExecuteInEditMode()]
public class StrudeMeshCreate : MonoBehaviour 
{

	//we need a mesh component, cause we're going to generate geometry.
	protected Mesh mesh = null;
	protected void init()
	{
		mesh = (GetComponent(typeof(MeshFilter)) as MeshFilter).sharedMesh;
	}
	
	//we use this switch to allow automatically recalcing the geometry.
	//this makes us able to adjust the spline we extrude on in realtime.
	public bool autoRecalc = false;
	bool needToRecalc = true;
	
	//resolution of generated mesh. 
	public int res;
	
	//width of generated mesh.
	public float width;
	
	public float scaleThing;

	public CurvySpline spline;
	
	//nice little function to help us get out data from the catmul-rom evaluation
	public void pathData(out Vector3 v,float t)
	{
		v = catEval(ref conPoints,conPoints.Length,t);
	}	
	
	//our controller points for the catmull-rom spline.
	public Vector3[] conPoints;
	
	//here is the catmull-rom evalution.
	float catEval(float p0,float p1,
			             float p2,float p3,
			             float t)
	{
		//evaluate a cat :)
		float t2 = t*t;
		return 0.5f * ( (2.0f * p1) + (-p0 + p2)*t + 
				      ( 2.0f*p0 - 5.0f*p1 + 4.0f*p2 - p3)*t2 
				      +(-p0 + 3.0f*p1 - 3.0f*p2 + p3)*t2*t);
	}
	
	//vector version.
		Vector3 catEval(Vector3 p0,Vector3 p1,
			             Vector3 p2,Vector3 p3,
			             float t)
	{
		//evaluate a cat :)
		float t2 = t*t;
		return 0.5f * ( (2.0f * p1) + (-p0 + p2)*t + 
				      ( 2.0f*p0 - 5.0f*p1 + 4.0f*p2 - p3)*t2 
				      +(-p0 + 3.0f*p1 - 3.0f*p2 + p3)*t2*t);
	}

	private void printCurvySpline()
	{
		Debug.Log("printing");
		Debug.Log (spline.ControlPoints.Count);

		conPoints = new Vector3 [spline.ControlPoints.Count];

		for (int i = 0; i < spline.ControlPoints.Count; i++) 
		{
			//Debug.Log (spline.ControlPoints[i]);
			conPoints[i] = spline.ControlPoints[i].Position;
		}
	}
	//takes in an array of points and the length of that array and also a t (to be to 0 to 1)
	Vector3 catEval(ref Vector3[] points,int length,float t)
	{
	
		//this function tries to calculate where in the array to get four
		//points to catmull rom with.
		int	ender =length;
		Vector3 ans = Vector3.zero;
		
		if(ender==1)
			return points[0];
		
		float eachPart = 1.0f/(float)ender;
		float tempRes = t*(float)ender;
		int index = (int)tempRes;
		
		if(index==ender)
			return points[ender-1];
		
		index--;
		int index2 = index+1;
		int index3 = index+2;
		if(index3>=ender)
			index3-=1;
		int index4 = index3+1;
		
		//make adjustments to indices.
		index = Mathf.Clamp(index,0,ender-1);
		index2 = Mathf.Clamp(index2,0,ender-1);
		index3 = Mathf.Clamp(index3,0,ender-1);
		index4 = Mathf.Clamp(index4,0,ender-1);
		
		float t1 = eachPart*(float)index2;
		float t2 = t1+eachPart;
		float localT = (t-t1)/(t2-t1);
		
		ans = catEval (points[index],
				points[index2],
				points[index3],
				points[index4],localT);
	
		//Debug.Log("" + index + " " + index2 + " " + index3 + " " + index4 + " t is " + t );
		return ans;
	}
	

	// Awake
	// Start


	// Use this for initialization
	void Awake () 
	{
		init();
	}

	void Start()
	{
		printCurvySpline ();
	}
	//some variables to use for the spline extrude
	Vector3 theForward = Vector3.zero;
	Vector3 theSide = Vector3.zero;
	Vector3 midPoint1 = Vector3.zero;
	Vector3 midPoint2 = Vector3.zero;
	
	//if we've got a valid inited midpoint.
	bool initedMidPoint = false;
	
	//NOW THIS, IS A CORE FUNCTION FOR SPLINE EXTRUDE GEOMETRY
	void calculateData(float currT,float tStep)
	{
		//midpoint is a point directly on the spline.
		
		//store last midPoint
		Vector3 lastMid = midPoint1;
		
		//generate new midPoint
		midPoint1 = catEval(ref conPoints,conPoints.Length,currT )*scaleThing;
		
		if(initedMidPoint)
		{//if we have an inited midPoint , use lastMid to generate forward vector 
			theForward =lastMid - midPoint1;
		}
		else
		{
			//our midPoint is not inited yet, let's calculate forward vector by calculating
			//the next midPoint
			midPoint2 = catEval(ref conPoints,conPoints.Length,currT + tStep)*scaleThing;
			theForward = midPoint1  - midPoint2;
		}
		//we don't want to support z extruding at this time.
		//we work with a splinemesh in the xy plane.
		theForward.z =0.0f;
		theForward.Normalize();
		
		//now theForward is a vector that points along the current point of the spline..
		//i.e the tangent of the spline at t
		
		//now, we finally get what we're after.. theSide vector, which allows us 
		//to generate a quad.
		//to get it, we just get the cross product of our spline tangent and  0,0,1 
		theSide = Vector3.Cross(theForward,Vector3.forward);
		theSide.Normalize();		
		initedMidPoint = true;
	}
	
	public void makeMeshFromSpline()
	{
		//this function generates the geometry
		
		int numConPoints = conPoints.Length;

		if(numConPoints>=4)
		{
			initedMidPoint = false;
			mesh.Clear();
			Vector3[] vertices = new Vector3[res * 2];
			Color[] colors = new Color[res * 2];
			Vector2[] uv = new Vector2[res * 2];
			
			int howManySquares = res-1;
			int[] triangles = new int[howManySquares*2 *3]; //two triangles in a square, three indices in a triangle.
		
			//loop through spline and evaluate
			float tStep = 1.0f/res;			
			for(int i=0; i < res; i++)
			{
				float t = (float)i/ (float)(res+10);
				float textureT = (float)i/ (float)(res);
				
				//calculate the data at this t.
				Debug.Log ("t:" + t);
				calculateData(t,tStep);
				
				//calculate vertices
				vertices[i*2] = midPoint1 - theSide*width;
				vertices[i*2 + 1] = midPoint1 + theSide*width;
				
				uv[i*2] = new Vector2(0.0f,textureT);
				uv[i*2 + 1] = new Vector2(1.0f,textureT);
				
				//IMPORTANT! In this we store textureT , which we use for extrude effect
				//in the shader! 
				colors[i*2] = new Color(textureT,1.0f,1.0f,1.0f); 
				colors[i*2 + 1] = new Color(textureT,1.0f,1.0f,1.0f);
				
				//generate the indices for our mesh.
				
				//all this index magic stuff.. it's stuff you just gotta work down
				//on paper to see that it actually is correct and makes up a mesh.
				if(i!=0)
				{
					int triInd = i-1;
					triangles[triInd *6 + 0] = triInd*2 + 0;
					triangles[triInd *6 + 1] = triInd*2 + 1;
					triangles[triInd *6 + 2] = triInd*2 + 2;
					
					triangles[triInd *6 + 3] = triInd*2 + 1;
					triangles[triInd *6 + 4] = triInd*2 + 3;
					triangles[triInd *6 + 5] = triInd*2 + 2;
				}
				
				
				
			}
			
			//transfer to mesh!
			mesh.vertices = vertices;
			mesh.colors = colors;
			mesh.uv = uv;
			mesh.triangles = triangles;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		//just some stuff to decide if we want to recalc things.
		if(needToRecalc==true || autoRecalc==true)
		{
			
			makeMeshFromSpline();
			needToRecalc = false;
		}
		
	}
}
