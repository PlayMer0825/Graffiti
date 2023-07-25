Shader "Custom/PencilWritingShader" {
    Properties{
        _MainTex("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1, 1, 1, 1)
        _Speed("Speed", Range(0.1, 10)) = 1.0
        _Amplitude("Amplitude", Range(0.01, 2.0)) = 1.0
        _Frequency("Frequency", Range(0.1, 10)) = 1.0
        _FadeRange("Fade Range", Range(0.0, 1.0)) = 0.5
    }

        SubShader{
            Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
            Blend SrcAlpha OneMinusSrcAlpha

            Pass {
                Cull Off
                Lighting Off
                ZWrite Off
                ZTest Always

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                sampler2D _MainTex;
                fixed4 _Color;
                float _Speed;
                float _Amplitude;
                float _Frequency;
                float _FadeRange;

                float _StartTime; // 시작 시간
                bool _IsAnimating; // 애니메이션 진행 여부

                struct appdata {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                v2f vert(appdata v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target {
                    if (!_IsAnimating) {
                        return tex2D(_MainTex, i.uv);
                    }

                    float2 uv = i.uv;
                    uv.x = 1.0 - uv.x; // 좌우 반전

                    float2 diagonal = float2(1.0, -1.0); // 대각선 방향 벡터
                    float progress = dot(uv, diagonal);

                    float fadeStart = _FadeRange * 0.5;
                    float fadeEnd = 1.0 - (_FadeRange * 0.5);
                    float alpha = smoothstep(fadeStart, fadeEnd, progress);

                    fixed4 texColor = tex2D(_MainTex, i.uv);
                    fixed4 finalColor = texColor * _Color;
                    finalColor.a *= alpha;

                    return finalColor;
                }
                ENDCG
            }
        }
}
