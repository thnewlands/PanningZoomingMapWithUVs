Shader "Unlit/PanAndZoom"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Pan ("Pan", vector) = (0,0,0,0)
        _Pivot ("Pivot", vector) = (0,0,0,0)
        _Zoom ("Zoom", vector) = (1,1,0,0)

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float2 _Pivot;
            float2 _Pan;
            float2 _Zoom;
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                //o.uv = ((v.uv.xy - _MainTex_ST.zw) * _MainTex_ST.xy) + _MainTex_ST.zw;
                //o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv = v.uv;
                o.uv = o.uv.xy - _Pivot;
                o.uv = (o.uv.xy * _Zoom);
                o.uv = o.uv.xy + _Pivot;
                
                o.uv = o.uv + _Pan;

                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                //col *= step(1-saturate(distance(i.uv, _Pivot) * 5), .5);
                //col = 0;
                //col.rg = i.uv.xy;

                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
