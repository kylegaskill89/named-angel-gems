﻿Shader "PSX/Vertex Lit Transparent"
{
	Properties
	{
		_Color("Color (RGBA)", Color) = (1, 1, 1, 1)
		_EmissionColor("Emissive Color(RGBA)", Color) = (0,0,0,0)
		_MainTex("Texture", 2D) = "white" {}
		_EmissiveTex("Emissive", 2D) = "black" {}
	}
		SubShader
	{
		Tags {"RenderType" = "Transparent" "Queue" = "Transparent"}
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
		LOD 100

		Pass
		{
			Tags { "LightMode" = "VertexLM" }
			CGPROGRAM
			#pragma vertex vert
			#pragma geometry geom
			#pragma fragment frag
			#pragma multi_compile_fog
			#pragma multi_compile __ PSX_ENABLE_TRIANGLE_SORTING

			#include "UnityCG.cginc"
			#include "PSX-Utils.cginc"

			#define PSX_VERTEX_LIT
			#include "PSX-ShaderSrc.cginc"

		ENDCG
		}

		Pass
		{
			Tags { "LightMode" = "Vertex" }
			CGPROGRAM
			#pragma vertex vert
			#pragma geometry geom
			#pragma fragment frag
			#pragma multi_compile_fog
			#pragma multi_compile __ PSX_ENABLE_TRIANGLE_SORTING

			#include "UnityCG.cginc"
			#include "PSX-Utils.cginc"

			#define PSX_VERTEX_LIT
			#include "PSX-ShaderSrc.cginc"

		ENDCG
		}
	}
		Fallback "PSX/Unlit Transparent"
}