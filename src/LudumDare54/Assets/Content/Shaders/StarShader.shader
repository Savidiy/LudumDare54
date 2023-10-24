Shader "Custom/StarShader"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _RedColor ("Red Color", Color) = (1, 1, 1, 1)
        _BlueColor ("Blue Color", Color) = (1, 1, 1, 1)
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent" "RenderType"="Transparent"
        }

        Pass
        {
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            Lighting Off

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

            float4 _RedColor;
            float4 _BlueColor;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 color = tex2D(_MainTex, i.uv);

                if (color.r == 1)
                    color.rgb = _RedColor.rgb;
                else if (color.b == 1)
                    color.rgb = _BlueColor.rgb;

                return color;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}