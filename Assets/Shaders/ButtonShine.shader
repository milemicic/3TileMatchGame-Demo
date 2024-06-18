Shader "Custom/ButtonShineShader"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _ShineColor ("Shine Color", Color) = (1,1,1,1)
        _ShineIntensity ("Shine Intensity", Range(0, 1)) = 0.5
        _ShineWidth ("Shine Width", Range(0.01, 0.2)) = 0.1
        _ShineSpeed ("Shine Speed", Range(0.1, 2.0)) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _ShineColor;
            float _ShineIntensity;
            float _ShineWidth;
            float _ShineSpeed;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float shine = smoothstep(0.5 - _ShineWidth, 0.5, frac(_Time.y * _ShineSpeed)) *
                              smoothstep(0.5 + _ShineWidth, 0.5, frac(_Time.y * _ShineSpeed));
                float4 texColor = tex2D(_MainTex, uv);
                return lerp(texColor, _ShineColor, shine * _ShineIntensity);
            }
            ENDCG
        }
    }
}
