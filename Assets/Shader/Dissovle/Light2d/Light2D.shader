Shader "Lilice/Light2D"
{
	Properties
   {
      [PerRendererData]_MainTex("Sprite Texture", 2D) = "white" {}
      _NormalTex("Normal Texture", 2D) = "white" {}

      _LightColor ("Light Color", Color) = (1,1,0,1)
      _DarkColor ("Dark Color", Color) = (0,0,1,1)
      _LightPosition ("Light Position", Vector) = (0,0,0,0)
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

         Tags { "LightMode"="ForwardBase" }
         Blend SrcAlpha OneMinusSrcAlpha

      CGPROGRAM
         #pragma vertex vert
         #pragma fragment frag
         #pragma target 2.0

         #include "UnityCG.cginc"
         #include "Lighting.cginc"

         sampler2D _MainTex;
         float4 _MainTex_ST;
         sampler2D _NormalTex;
         float4 _NormalTex_ST;
         float4 _LightColor;
         float4 _DarkColor;
         float4 _LightPosition;

         struct a2v {
             float4 vertex : POSITION;
             float4 texcoord : TEXCOORD0;
         };

         struct v2f {
             float4 pos : SV_POSITION;
             float4 uv : TEXCOORD0;
             float2 ao : TEXCOORD1;
             float3 worldpos : TEXCOORD2;
         };


         v2f vert(a2v v)
         {
           v2f o;
           o.pos = UnityObjectToClipPos(v.vertex);
           o.uv = v.texcoord;
           o.uv.xy = v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
           o.uv.zw = v.texcoord.xy * _NormalTex_ST.xy + _NormalTex_ST.zw;
           o.worldpos = mul(unity_ObjectToWorld, v.vertex).xyz;
           return o;
        }



        fixed4 frag(v2f i) : SV_Target
        {
           //fixed4 color = tex2D(_MainTex, IN.texcoord);

           float4 diffuse = tex2D(_MainTex, i.uv.xy).rgba;
           float3 normal = tex2D(_NormalTex, i.uv.zw).rgb;

           normal = normalize(2*normal-1);
           float3 light = float3(0,0,0);

           float3 light_dir = _LightPosition.xyz - i.worldpos;
           float3 view_dir = normalize(UnityWorldSpaceViewDir(i.worldpos));

           float dist = light_dir.z;
           float atten = smoothstep(90, 30 ,dist);
           light_dir = normalize(light_dir);
           float3 current_light = atten * lerp(0,1,dot(normal, light_dir))*1.5;

           light = max(0,current_light-0.38);

           diffuse += tex2D(_MainTex, i.uv.xy).rgba/2 ;

           float3 gooch_light = ( _DarkColor * (1 - light)  + _LightColor * light * 2) * 0.4;


           float3 cel_light = smoothstep(0.1, 0.19, (current_light)/2)  + diffuse.rgb;

           return float4(float3 (( cel_light * diffuse *0.7 + gooch_light) * 1), diffuse.a); 
        }
      ENDCG
      }
   }
}
