Shader "Custom/ShipShader"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _FlashColor ("Flash Color", Color) = (1, 1, 1, 1)
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
                fixed4 color : COLOR;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float4 _FlashColor;
            float _WhitePercent;

            float2 _BaseSpriteCoord;
            float2 _AShadowSpriteCoord;
            float2 _BShadowSpriteCoord;
            float _WeightA;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 color = tex2D(_MainTex, i.uv);
                fixed4 shadowA = tex2D(_MainTex, i.uv - _BaseSpriteCoord.xy + _AShadowSpriteCoord.xy);
                fixed4 shadowB = tex2D(_MainTex, i.uv - _BaseSpriteCoord.xy + _BShadowSpriteCoord.xy);

                if (shadowA.a > 0 && shadowB.a > 0)
                    color.rgb = shadowA.rgb;
                else if (shadowA.a > 0)
                    color.rgb = lerp(color.rgb, shadowA.rgb, _WeightA);
                else if (shadowB.a > 0)
                    color.rgb = lerp(color.rgb, shadowB.rgb, 1 - _WeightA);

                color *= i.color; // apply sprite renderer color

                color.rgb = lerp(color.rgb, _FlashColor.rgb, _WhitePercent);

                return color;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}