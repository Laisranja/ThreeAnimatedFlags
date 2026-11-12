
Shader "Custom/Flag_03"
{
    Properties
    {
        _MainColor ("Cor da Bandeira", Color) = (0.2, 0.65, 0.35, 1)
        _Amplitude ("Amplitude", Range(0, 1)) = 0.20
        _Frequency ("Frequencia", Range(0, 10)) = 1.5
        _Speed ("Velocidade", Range(0, 10)) = 1.4
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            Cull Off

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
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            fixed4 _MainColor;
            float _Amplitude;
            float _Frequency;
            float _Speed;

            v2f vert (appdata v)
            {
                v2f o;

                float x = v.vertex.x + 5.0;

                float wave1 = sin(x * _Frequency - _Time.y * _Speed) * _Amplitude;
                float wave2 = cos(x * _Frequency * 2.0 + _Time.y * _Speed * 1.5) * (_Amplitude * 0.35);

                float pin = saturate((10.0 - x) / 10.0);

                v.vertex.y += (wave1 + wave2) * pin;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return _MainColor;
            }

            ENDCG
        }
    }
}
