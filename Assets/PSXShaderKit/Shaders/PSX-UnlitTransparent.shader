﻿Shader "PSX/Unlit Transparent"
{
    Properties
    {
        _Color("Color (RGBA)", Color) = (1, 1, 1, 1)
        _MainTex("Texture", 2D) = "white" {}
    }
        SubShader
    {
        Tags {"RenderType" = "Transparent" "Queue" = "Transparent"}
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma geometry geom
            #pragma fragment frag
            #pragma multi_compile_fog

            #include "UnityCG.cginc"
            #include "PSX-Utils.cginc"

            #include "PSX-ShaderSrc.cginc"

            ENDCG
        }
    }
        Fallback "Unlit/Color"
}