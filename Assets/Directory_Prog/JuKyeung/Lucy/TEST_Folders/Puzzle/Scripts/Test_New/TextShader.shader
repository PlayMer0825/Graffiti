Shader "Custom/TextShader" {
    Properties{
        _Color("Color", Color) = (1, 1, 1, 1)
        _Speed("Speed", Range(0, 1)) = 0.5
    }

        SubShader{
            Tags {
                "Queue" = "Transparent"
                "RenderType" = "Transparent"
            }
            Blend SrcAlpha OneMinusSrcAlpha

            Pass {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                float4 _Color;
                float _Speed;

                v2f vert(appdata v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target {
                    fixed4 col = _Color;

                    float yOffset = _Time.y * _Speed;
                    float alpha = smoothstep(0.0, 0.05, i.uv.y - yOffset);

                    col.a *= alpha;
                    return col;
                }
                ENDCG
            }
    }
}
