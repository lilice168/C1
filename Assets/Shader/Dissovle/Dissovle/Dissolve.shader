Shader "Lilice/Dissolve"
{
	Properties
   {
      [PerRendererData]_MainTex("Sprite Texture", 2D) = "white" {}
      _NoiseTex("Noise Texture", 2D) = "white" {}

      _Tile("Tile", Range (0, 1)) = 1 // 雜訊大小 
      _DissolveValue ("Dissolve Value", Range (0, 1)) = 0.5 // 溶解度 
      _DissSize("Dissolve Size", Range (0, 1)) = 0.1 // 溶解大小 
      _DissColor ("Dissolve Color", Color) = (1,1,1,1) // 溶解颜色 
      _AddColor("Dissolve Add Color", Color) = (1,1,1,1)
   }
   SubShader
   {
      Tags
      {
         "Queue"="Transparent"
         "IgnoreProjector"="True"
         "RenderType"="Transparent"
         "PreviewType"="Plane"
         "CanUseSpriteAtlas"="True"
      }
      Cull Off ZWrite Off ZTest Always

      Pass
      {
      CGPROGRAM
         #pragma vertex vert
         #pragma fragment frag
         #pragma target 2.0

         #include "UnityCG.cginc"
         #include "UnitySprites.cginc"


         v2f vert(appdata_t IN)
         {
           v2f OUT;
           OUT.vertex = UnityObjectToClipPos(IN.vertex);
           OUT.texcoord = IN.texcoord;
           OUT.color = IN.color;
 
           return OUT;
        }

        sampler2D _NoiseTex;
        half _Tile;
        half _DissolveValue;
        half _DissSize;
        half4 _DissColor;  
        fixed4 _AddColor;

        fixed4 frag(v2f IN) : SV_Target
        {
           fixed4 color = tex2D(_MainTex, IN.texcoord);

           // 用Ｒ通道與溶解值做判斷是否要畫
           fixed4 noiseColor = tex2D (_NoiseTex, IN.texcoord/_Tile); 
           if (noiseColor.r < _DissolveValue)  
                discard;  
             
           // 溶解度百分比
           float percentage = _DissolveValue / noiseColor.r;  
           // 溶解區塊上色
           float lerpEdge = sign(percentage - _DissSize - 0.05);  
           fixed3 edgeColor = lerp(_DissColor.rgb, _AddColor.rgb, saturate(lerpEdge));  
           // 最後顏色
           float lerpOut = sign(percentage - 0.05);  
           fixed3 colorOut = lerp(color, edgeColor, saturate(lerpOut));  
           return fixed4(colorOut, 1); 
        }
      ENDCG
      }
   }
}
