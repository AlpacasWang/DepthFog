//It's a Simple Depth Fog shader.You can customize it for yourself;
Shader "AlpacasWang/DepthFog"
{
  Properties
  {
    [HideInInspector]_MainTex ("Texture", 2D) = "white" {}
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
      #pragma shader_feature GAUSSIAN_ON
      #pragma shader_feature LINEAR_ON
      #pragma shader_feature EXP_ON
      #pragma shader_feature EXP2_ON
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
      sampler2D _DepthTexture;
      fixed4 _FogColor;
      float _Focus;
      float _Rate;
      float _Scale;
      float _LinearDensity;
      float _EXPDensity;
      float _EXP2Density;
      fixed4 frag (v2f i) : SV_Target
      {
        fixed rawDepth = SAMPLE_DEPTH_TEXTURE(_DepthTexture, i.uv);
        fixed depth = Linear01Depth(rawDepth);

        float fogFactor = 1;
        #if GAUSSIAN_ON
        fogFactor =  _Scale*exp(-_Rate*abs(_Focus-depth));
        #endif 

        #if LINEAR_ON
        fogFactor =  _LinearDensity*depth;
        #endif 

        #if EXP_ON
        fogFactor =  exp(-_EXPDensity*depth);
        #endif 

        #if EXP2_ON
        fogFactor =  exp(-_EXP2Density*depth);
        #endif 

        return lerp(tex2D(_MainTex,i.uv),_FogColor,fogFactor);
      }
      ENDCG
    }
  }
  CustomEditor "DepthFogShaderGUI"
}