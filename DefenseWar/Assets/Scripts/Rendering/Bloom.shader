Shader "Custom/Bloom"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
    }

        CGINCLUDE
#include "UnityCG.cginc"
        sampler2D _MainTex, _SourceTex;
    float _Intensity;

    struct V2f {
        float4 pos : SV_POSITION;
        float2 uv : TEXCOORD0;
    };

    V2f VertexProgram(float4 p : POSITION, float2 uv : TEXCOORD0) {
        V2f v;
        v.pos = UnityObjectToClipPos(p);
        v.uv = uv;
        return v;
    }

    half3 Sample(float2 uv) {
        return tex2D(_MainTex, uv);
    }

    ENDCG

        SubShader
    {
        Pass
        {
            CGPROGRAM
#pragma vertex VertexProgram
#pragma fragment FragmentProgram
            half4 FragmentProgram(V2f v) :SV_Target
            {
                return half4(Sample(v.uv) , 1);
            }
        
            ENDCG
        }
        Pass
        {
            CGPROGRAM
#pragma vertex VertexProgram
#pragma fragment FragmentProgram
            half4 FragmentProgram(V2f v) :SV_Target
            {
                return half4(Sample(v.uv) , 1);
            }

            ENDCG
        }
        Pass
        {
            CGPROGRAM
#pragma vertex VertexProgram
#pragma fragment FragmentProgram
            half4 FragmentProgram(V2f v) :SV_Target
            {
                return half4(Sample(v.uv) , 1);
            }

            ENDCG
        }
        Pass
        {
            CGPROGRAM
#pragma vertex VertexProgram
#pragma fragment FragmentProgram
            half4 FragmentProgram(V2f v) :SV_Target
            {
                half4 c = tex2D(_SourceTex, v.uv);
                c.rgb += _Intensity * Sample(v.uv);
                return c;
            }

            ENDCG
        }
    }
}
