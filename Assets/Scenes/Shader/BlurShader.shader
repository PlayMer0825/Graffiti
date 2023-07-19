Shader "Unlit/BlurShader"
{
    Properties
    {
        _Radius("Radius", Range(1, 255)) = 1
    }

        Category
    {
        Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Opaque" }

        SubShader
        {
            GrabPass
            {

                Tags{ "LightMode" = "Always" }
            }

            Pass
            {
                Tags{ "LightMode" = "Always" }

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma fragmentoption ARB_precision_hint_fastest
                #include "UnityCG.cginc"

                struct appdata_t
                {
                    float4 vertex : POSITION;
                    float2 texcoord: TEXCOORD0;
                };

                struct v2f
                {
                    float4 vertex : POSITION;
                    float4 uvgrab : TEXCOORD0;
                };

                v2f vert(appdata_t v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    #if UNITY_UV_STARTS_AT_TOP
                    float scale = -1.0; // 보여지는 사이즈를 조절하는 파트
                    #else
                    float scale = 1.0; //
                    #endif
                    o.uvgrab.xy = (float2(o.vertex.x, o.vertex.y * scale) + o.vertex.w) * 0.5; // 실제로 있는 부분과 보여지는 부분이 다름
                    o.uvgrab.zw = o.vertex.zw;
                    return o;
                }

                sampler2D _GrabTexture;
                float4 _GrabTexture_TexelSize;
                float _Radius;

                half4 frag(v2f i) : COLOR
                {
                    half4 sum = half4(0,0,0,0); // 컬러감을 조절 . 전체적인 컬러가 덧씌워짐

                    #define GRABXYPIXEL(kernelx, kernely) tex2Dproj( _GrabTexture, UNITY_PROJ_COORD(float4(i.uvgrab.x + _GrabTexture_TexelSize.x * kernelx, i.uvgrab.y + _GrabTexture_TexelSize.y * kernely, i.uvgrab.z, i.uvgrab.w)))

                    sum += GRABXYPIXEL(0.0, 0.0);
                    int measurments = 1; // 값을 올리니 어두워졌음 

                    for (float range = 0.1f; range <= _Radius; range += 0.1f) // 잘 모르겠음. 값을 올렸을 때 변화 없음
                    {
                        sum += GRABXYPIXEL(range, range);
                        sum += GRABXYPIXEL(range, -range);
                        sum += GRABXYPIXEL(-range, range);
                        sum += GRABXYPIXEL(-range, -range);
                        measurments += 4;
                    }

                    return sum / measurments; // 부호 변경 시 그냥 하얀색으로
                }
                ENDCG
            }
            GrabPass
            {
                Tags{ "LightMode" = "Always" }
            }

            Pass
            {
                Tags{ "LightMode" = "Always" }

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma fragmentoption ARB_precision_hint_fastest
                #include "UnityCG.cginc"

                struct appdata_t
                {
                    float4 vertex : POSITION;
                    float2 texcoord: TEXCOORD0;
                };

                struct v2f
                {
                    float4 vertex : POSITION;
                    float4 uvgrab : TEXCOORD0;
                };

                v2f vert(appdata_t v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    #if UNITY_UV_STARTS_AT_TOP
                    float scale = -1.0; // 마찬가지로사이즈의 변경
                    #else
                    float scale = 1.0;
                    #endif
                    o.uvgrab.xy = (float2(o.vertex.x, o.vertex.y * scale) + o.vertex.w) * 0.5;
                    o.uvgrab.zw = o.vertex.zw;
                    return o;
                }

                sampler2D _GrabTexture;
                float4 _GrabTexture_TexelSize;
                float _Radius;

                half4 frag(v2f i) : COLOR
                {

                    half4 sum = half4(0,0,0,0);
                    float radius = 1.41421356237 * _Radius; //  *** Radius 의 값을 조절 시 블러 효과가 세짐

                    #define GRABXYPIXEL(kernelx, kernely) tex2Dproj( _GrabTexture, UNITY_PROJ_COORD(float4(i.uvgrab.x + _GrabTexture_TexelSize.x * kernelx, i.uvgrab.y + _GrabTexture_TexelSize.y * kernely, i.uvgrab.z, i.uvgrab.w)))

                    sum += GRABXYPIXEL(0.0, 0.0);
                    int measurments = 1;

                    for (float range = 1.41421356237f; range <= radius * 1.41; range += 1.41421356237f)
                    {
                        sum += GRABXYPIXEL(range, 0);
                        sum += GRABXYPIXEL(-range, 0);
                        sum += GRABXYPIXEL(0, range);
                        sum += GRABXYPIXEL(0, -range);
                        measurments += 4;
                    }

                    return sum / measurments;
                }
                ENDCG
            }
        }
    }
}