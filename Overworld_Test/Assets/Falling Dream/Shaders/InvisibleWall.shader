
Shader "Unlit/InvisibleWall"
{
    Properties
    {
        _Color ("color", Color) = (1, 0, 0, 1)
        _PlayerPos ("player position", Vector) = (0, 0, 0, 1)
        _MaxDist ("maximum distance", float) = 1
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        ZWrite Off
        
        Blend One OneMinusSrcAlpha // Premultiplied transparency

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct vertexInput
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct vertexOutput
            {
                float4 vertex : SV_POSITION;
                float2 texcoord : TEXCOORD0;
                float4 fragPos : TEXCOORD1;
                float4 playerPos : TEXCOORD2;
            };

            float4 _Color;
            float4 _PlayerPos;
            float _MaxDist;


            vertexOutput vert (vertexInput input)
            {
                vertexOutput output;

                output.vertex = UnityObjectToClipPos(input.vertex);
                output.texcoord = input.texcoord;

                output.fragPos = mul (unity_ObjectToWorld, input.vertex);
                output.playerPos = _PlayerPos; // mul (unity_ObjectToWorld, _PlayerPos);

                return output;
            }

            fixed4 frag (vertexOutput input) : Color
            {
                float dist = sqrt((input.fragPos.x - input.playerPos.x) * (input.fragPos.x - input.playerPos.x)
                    + (input.fragPos.y - input.playerPos.y) * (input.fragPos.y - input.playerPos.y)
                    + (input.fragPos.z - input.playerPos.z) * (input.fragPos.z - input.playerPos.z));
                // sample the texture
                fixed4 col;

                if (dist < _MaxDist) {
                    col = _Color;
                } else {
                    col = float4(0, 0, 0, 0);
                }

                return col;
            }
            ENDCG
        }
    }
}
