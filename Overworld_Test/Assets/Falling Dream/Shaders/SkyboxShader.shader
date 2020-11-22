Shader "SkyboxShader" {
   Properties {
      _ColorTop ("top color", Color) = (.5, .5, .5, 1)
      _ColorBot ("bottom color", Color) = (.5, .5, .5, 1)
      _TopEnhancer ("top color booster", Range(1, 10)) = 0
   }

   SubShader {
      Tags { "Queue"="Background"  }

      Pass {
         ZWrite Off 
         Cull Off

         CGPROGRAM
         #pragma vertex vert // method: "vert" is our vertex shader
         #pragma fragment frag // method: "frag" is our fragment shader

         // User-specified uniforms
         float4 _ColorTop;
         float4 _ColorBot;
         float _TopEnhancer;


         struct vertexInput {
            float4 vertex : POSITION;
            float3 texcoord : TEXCOORD0;
         };

         struct vertexOutput {
            float4 vertex : SV_POSITION;
            float3 texcoord : TEXCOORD0;
         };

         vertexOutput vert(vertexInput input)
         {
            vertexOutput output;
            output.vertex = UnityObjectToClipPos(input.vertex);
            output.texcoord = input.texcoord;
            return output;
         }

         fixed4 frag (vertexOutput input) : COLOR
         {         
            /* float pi = 3.14159265358979323846264338327950288;

            int numSlices = 100, sliceRate = 10;
            float thetaN = atan2(input.texcoord.z, input.texcoord.x) / (2 * pi);
            int slice = ((int) (thetaN * numSlices)) % sliceRate; */

            float p = pow((1 - (input.texcoord.y + 1.0) / 2.0), _TopEnhancer);

            /* if (thetaN > 0) {
               return float4(thetaN, 0, 0, 1);
            } else {
               return float4(1 + thetaN, 0, 0, 1);
            } */

            /* if (slice == 0) {
               return float4(0, 0, 0, 1);

            } */

            return lerp(_ColorTop, _ColorBot, p);
         }
         ENDCG 
      }
   } 	
}