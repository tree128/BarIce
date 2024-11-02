Shader "Hidden/Wave_shader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Ratio ("ratio", Range(0, 1)) = 0.5
        _Color1 ("Color1", Color) = (0.5, 0.5, 0.5, 1)
        _Color2 ("Color2", Color) = (0, 0, 0, 1)
    }
    
    SubShader
    {
    Blend SrcAlpha OneMinusSrcAlpha
        Tags { "RenderType"="Opaque" }
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
 
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
 
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Ratio;
            fixed4 _Color1;
            fixed4 _Color2;
 
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
 
            fixed4 frag (v2f i) : SV_Target
            {
                float t1 = _Ratio + sin(_Time.w + i.uv.x * 3) / 30;                
                float t2 = _Ratio + 0.05 + cos(_Time.z + i.uv.x) / 40;
                
                fixed4 gaugeColor = lerp(tex2D(_MainTex, i.uv), _Color2, saturate(i.uv.y < t2));
                return lerp(gaugeColor, _Color1, saturate(i.uv.y < t1));
            }
            ENDCG
        }
    }
}