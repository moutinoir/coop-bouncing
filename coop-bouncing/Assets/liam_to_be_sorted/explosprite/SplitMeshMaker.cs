using UnityEngine;
using System.Collections;

public class PartOfQuadMesh
{
	public Rect geoRect;
	public Rect txtRect;
};

public class SplitMeshMaker : MonoBehaviour 
{
	
	protected Mesh mesh = null;
	
	public int levels;
	PartOfQuadMesh[] rects;
	public GUITexture rect;
	PartOfQuadMesh source;

	// Use this for initialization
	void Start () 
	{
		mesh = (GetComponent(typeof(MeshFilter)) as MeshFilter).mesh;
		int numRects = (int)Mathf.Pow(2,levels);
		rects = new PartOfQuadMesh[numRects];
		crumbleToBits();
		makeMesh();
	}
	
	public float minT; 
	public float maxT;
	
	void crumbleToBits()
	{
		source = new PartOfQuadMesh();
		source.geoRect = rect.pixelInset;
		source.txtRect = new Rect(0.0f,0.0f,1.0f,1.0f);
		rects[0] = source;
		
		int i=0;
		while(i < (levels))
		{
			//make temp array.
			int numTempRects = (int)Mathf.Pow(2,i+1);
			PartOfQuadMesh[] tempArr= new PartOfQuadMesh[numTempRects];
			
			//loop through all our currRects..
			int numCurrRects = (int)Mathf.Pow(2,i);
			for(int j=0; j < numCurrRects; j++)
			{
				tempArr[j*2] = new PartOfQuadMesh();
				tempArr[j*2+1] = new PartOfQuadMesh();
				
				float t = Random.Range(minT,maxT);
				
				float proportionXToY = rects[j].geoRect.width / rects[j].geoRect.height;
				//float horiz = Random.Range(0.0f,proportionXToY);
				float horiz = proportionXToY + Random.Range(-0.3f,0.3f);
				
				dividePartOfMesh(ref rects[j],ref tempArr[j*2], ref tempArr[j*2 +1],t,horiz<1.0f);
			}
			
			for(int j=0; j < numTempRects; j++)
			{
				rects[j] = tempArr[j];
			}
			
			i++;
		}
	}
	
	void dividePartOfMesh(ref PartOfQuadMesh inPart, ref PartOfQuadMesh out1, ref PartOfQuadMesh out2,float t, bool horizontal)
	{
		divideRect(inPart.geoRect, out out1.geoRect,out out2.geoRect,t,horizontal);
		divideRect(inPart.txtRect, out out1.txtRect,out out2.txtRect,t,horizontal);
	}
	
	void divideRect( Rect inRect,out Rect rect1,out Rect rect2,float t, bool horizontal)
	{
		float t2 = 1.0f - t;
		if(horizontal)
		{
			//we are doing a horizontal split of the rect.
			
			rect1 = new Rect(inRect.xMin,inRect.yMin,inRect.width,inRect.height*t);
			rect2 = new Rect(inRect.xMin,inRect.yMin + inRect.height*t,inRect.width,inRect.height*t2);
			return;
		}
		
		
		// a vertical split
		rect1 = new Rect(inRect.xMin,inRect.yMin,inRect.width*t,inRect.height);
		rect2 = new Rect(inRect.xMin + inRect.width*t,inRect.yMin,inRect.width*t2,inRect.height);
		
	}
	
	protected void makeMesh()
	{
		mesh.Clear();
		Vector3[] vertices = new Vector3[rects.Length* 4];
		Color[] colors = new Color[rects.Length * 4];
		Vector3[] normals= new Vector3[rects.Length * 4];
		Vector2[] uv = new Vector2[rects.Length* 4];
		Vector2[] uv2 = new Vector2[rects.Length* 4];
		
		Color[] protoQuadC = new Color[4];
		protoQuadC[0] = Color.white;
		protoQuadC[1] = Color.white;
		protoQuadC[2] = Color.white;
		protoQuadC[3] = Color.white;
		
		Vector2[] protoQuadT = new Vector2[4];
		protoQuadT[0] = new Vector2( 0.0f, 0.0f);
		protoQuadT[1] = new Vector2( 1.0f, 0.0f);
		protoQuadT[2] = new Vector2( 1.0f, 1.0f);
		protoQuadT[3] = new Vector2( 0.0f, 1.0f);
		
		
		
		int[] triangles = new int[rects.Length*6]; 
		for(int i=0; i < rects.Length; i++)
		{
			
			
			Rect geoRect = rects[i].geoRect;
			Rect txtRect = rects[i].txtRect;
			
			//Debug.Log("xmin:" + geoRect .xMin + " xMax:" + geoRect.xMax);
			//Debug.Log("ymin:" + geoRect .yMin + " yMax:" + geoRect.yMax);
			
			Vector3 midPoint = new Vector3(rect.pixelInset.xMin + (rect.pixelInset.xMax - rect.pixelInset.xMin)*0.5f,
														 rect.pixelInset.yMin + (rect.pixelInset.yMax - rect.pixelInset.yMin)*0.5f,0.0f);
			
			Vector3 locMidPoint = new Vector3(geoRect.xMin + (geoRect.xMax - geoRect.xMin)*0.5f,
														 geoRect.yMin + (geoRect.yMax - geoRect.yMin)*0.5f,0.0f);
			
			Vector3 dirNormal = midPoint - locMidPoint;
			dirNormal.Normalize();
			dirNormal.x = (geoRect.xMin / rect.pixelInset.width)*200.0f + (geoRect.yMin / rect.pixelInset.height)*100.0f ;
			dirNormal.x = dirNormal.x * dirNormal.x;
			//dirNormal*=0.01f;
			//dirNormal=dirNormal / dirNormal;
			normals[i*4 + 0]= dirNormal;
			normals[i*4 + 1]= dirNormal;
			normals[i*4 + 2]= dirNormal;
			normals[i*4 + 3]= dirNormal;
			
			vertices[i*4 + 0] = new Vector3(geoRect.xMin,geoRect.yMin,0.0f);
			vertices[i*4 + 1] = new Vector3(geoRect.xMax,geoRect.yMin,0.0f);
			vertices[i*4 + 2] = new Vector3(geoRect.xMax,geoRect.yMax,0.0f);
			vertices[i*4 + 3] = new Vector3(geoRect.xMin,geoRect.yMax,0.0f);
			
			
			uv[i*4 + 0] = new Vector2( txtRect.xMin  ,txtRect.yMin);
			uv[i*4 + 1] = new Vector2( txtRect.xMax,txtRect.yMin);
			uv[i*4 + 2] = new Vector2( txtRect.xMax ,txtRect.yMax);
			uv[i*4 + 3] = new Vector2( txtRect.xMin ,txtRect.yMax);
			
			for(int j=0; j < 4; j++)
			{
				//vertices[i*4 + j] = protoQuadV[j] + points[i];
				
				float modulator = Mathf.Abs(geoRect.xMin + (geoRect.xMax - geoRect.xMin)*0.5f);
				
				
				Color col = new Color(modulator/512.0f,1.0f,1.0f,1.0f);
				//Debug.Log("CAOOOL" + col.r);
				colors[i*4 + j] = col;
				uv2[i*4 + j] = protoQuadT[j];
			}
			triangles[i*6 + 0] = i*4 +  0;
			triangles[i*6 + 1] = i*4 +  1;
			triangles[i*6 + 2] = i*4 +  3;
			
			triangles[i*6 + 3] = i*4 +  1;
			triangles[i*6 + 4] = i*4 +  2;
			triangles[i*6 + 5] = i*4 +  3;
		}
		
		mesh.vertices = vertices;
		mesh.colors = colors;
		mesh.uv = uv;
		mesh.uv2 = uv2;
		mesh.normals = normals;
		mesh.triangles = triangles;
		
			
	}
	
	
	// Update is called once per frame
	void Update () 
	{
		//crumbleToBits();
		//makeMesh();
	}
}
