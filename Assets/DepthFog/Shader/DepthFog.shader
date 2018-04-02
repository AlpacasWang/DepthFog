//It's a Simple Depth Fog shader.You can customize it for yourself;
Shader "AlpacasWang/DepthFog"
{
  Properties
  {
    [HideInInspector]_MainTex ("Texture", 2D) = "white" {}

    
    [KeywordEnum(GAUSSIAN, LINEAR, EXP,EXP2,DEPTHMAP)] _TYPE("Type", Float) = 0

    [HideInInspector]_FogColor ("Fog Color", Color) = (1,1,1,1)
    [HideInInspector]_Focus("Focus",float) = 0.1
    [HideInInspector]_Rate("Rate",float) = 0.1
    [HideInInspector]_Scale("Scale",float) = 1

    [HideInInspector]_LinearDensity("Linear Density",float) = 1
    [HideInInspector]_EXPDensity("EXP Density",float) = 0.1
    [HideInInspector]_EXP2Density("EXP2 Density",float) = 0.1

  }
  SubShader
  {
    Cull Off ZWrite Off ZTest Always

    Pass
    {
      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag
      #pragma multi_compile _TYPE_GAUSSIAN _TYPE_LINEAR _TYPE_EXP _TYPE_EXP2 _TYPE_DEPTHMAP
      #include "UnityCG.cginc"
      struct appdata
      {
        float4 vertex : POSITION;
        float2 uv : TEXCOORD0;
      };

      struct v2f
      {
        float2 uv : TEXCOORD0;
        float4 vertex : SV_POSITION;
      };

      v2f vert (appdata v)
      {
        v2f o;
        o.vertex = UnityObjectToClipPos(v.vertex);
        o.uv = v.uv;
        return o;
      }
      
      sampler2D _MainTex;
      sampler2D _CameraDepthNormalsTexture;
      fixed4 _FogColor;
      float _Focus;
      float _Rate;
      float _Scale;
      float _LinearDensity;
      float _EXPDensity;
      float _EXP2Density;
      fixed4 frag (v2f i) : SV_Target
      {
        half4 depthNormal = tex2D(_CameraDepthNormalsTexture, i.uv);
        half depth;
        half3 normal;
        DecodeDepthNormal(depthNormal, depth, normal);
        //fixed depth = Linear01Depth(rawDepth);

        float fogFactor = 1;
        #ifdef _TYPE_GAUSSIAN
        fogFactor =  _Scale*exp(-_Rate*abs(_Focus-depth));
        #elif _TYPE_LINEAR
        fogFactor =  _LinearDensity*depth;
        #elif _TYPE_EXP
        fogFactor =  1-exp(-_EXPDensity*depth);
        #elif _TYPE_EXP2
        fogFactor =  1-exp(-pow(_EXP2Density*depth,2));
        #elif _TYPE_DEPTHMAP
        return depth;
        #endif 

        return lerp(tex2D(_MainTex,i.uv),_FogColor,fogFactor);
      }
      ENDCG
    }
  }
  CustomEditor "DepthFogShaderGUI"
}