// Upgrade NOTE: replaced 'glstate.matrix.mvp' with 'UNITY_MATRIX_MVP'

Shader "strudeShade" {
Properties {
	_MainTex ("Base (RGBA)", 2D) = "white" {}
	_EdgeTex ("Base (RGBA)", 2D) = "white" {}
	_Color ("Main Color", COLOR) = (1,1,1,1)
	_Seed("Seed", Range(-0.1, 2.1)) = 0
	_FlowLength("FlowLength", Range(-0.1, 1.1)) = 0
	_Opas("Opas", Range(0.0, 1.0)) = 1
	

}

Category {
	Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
	Blend SrcAlpha OneMinusSrcAlpha
	AlphaTest Greater .01
	ColorMask RGB
	Cull Off Lighting Off ZWrite Off Fog { Color (0,0,0,0) }


SubShader {
	Pass {
	Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		ZTest LEqual Cull Off ZWrite On
		Fog { Mode off }
		Blend SrcAlpha OneMinusSrcAlpha
				
CGPROGRAM
// Upgrade NOTE: excluded shader from DX11 and Xbox360; has structs without semantics (struct appdata members vertex,texcoord,color)
#pragma exclude_renderers d3d11 xbox360
#pragma vertex vert
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest 
#include "UnityCG.cginc" 


// vertex input: position, UV
struct appdata {
    float4 vertex;
    float4 texcoord;
	float4 color;
};

struct v2f { 
	float4 pos : POSITION;
	float2	uv;
	float4 color : COLOR;
};

v2f vert (appdata v)
{
	v2f o;
	
	o.pos = mul( UNITY_MATRIX_MVP, v.vertex);
	//PositionFog( modVert, o.pos, o.fog );
	//TRANSFER_VERTEX_TO_FRAGMENT(o);
	o.uv = v.texcoord;//clamp(v.texcoord,0.0f,1.0f);
	o.color = v.color;
	return o;
}

uniform sampler2D _MainTex;
uniform float _Seed;
uniform float _FlowLength;
uniform float _Opas;
uniform float4 _Color;
float4 frag (v2f i) : COLOR
{	
	
	float2 uvMods2 = i.uv;
	float4 original = tex2D(_MainTex, uvMods2);
	
	float lightIntensityR = step(i.color.r,_Seed) * (1.0f -step(i.color.r, _Seed - _FlowLength));
	
	//original.rgb = half3(1.0f,1.0f,1.0f);
	//original.rgb = i.color.rgb * original.rgb;
	original.a*=lightIntensityR*_Opas;
	original.rgb*=_Color.rgb;
	
	return original;
}
ENDCG

	}
}
}
Fallback off

}