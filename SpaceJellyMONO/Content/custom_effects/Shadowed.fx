float4x4 xWorldViewProjection;
float4x4 xLightsWorldViewProjection;
float4x4 xWorld;
float3 xLightPos;
float xLightPower;
float xAmbient;

Texture2D xShadowMap;
sampler2D ShadowMapSampler = sampler_state { texture = <xShadowMap> ; magfilter = LINEAR; minfilter = LINEAR; mipfilter=LINEAR; AddressU = mirror; AddressV = mirror;};

Texture2D xTexture;
sampler2D TextureSampler = sampler_state { texture = <xTexture> ; magfilter = LINEAR; minfilter = LINEAR; mipfilter=LINEAR; AddressU = clamp; AddressV = clamp;};

struct SSceneVertexToPixel
{
    float4 Position              : SV_POSITION;
	float2 Tex					 : TEXCOORD0;
    float4 Pos2DAsSeenByLight    : POSITION1;
	float4 Position3D            : POSITION2;
    float3 Normal                : NORMAL;

};

 SSceneVertexToPixel ShadowedSceneVertexShader( float3 inPos : SV_POSITION, float3 inNormal : NORMAL, float2 inTex : TEXCOORD)
 {
     SSceneVertexToPixel Output = (SSceneVertexToPixel)0;
 
	 float4 inPos4 = float4(inPos, 1);
     Output.Position = mul(inPos4, xWorldViewProjection);    
     Output.Pos2DAsSeenByLight = mul(inPos4, xLightsWorldViewProjection);    
     Output.Normal = normalize(mul(inNormal, (float3x3)xWorld));    
     Output.Position3D = mul(inPos4, xWorld);
	 Output.Tex = inTex;
 
     return Output;
 }
 
float4 ShadowedScenePixelShader(SSceneVertexToPixel PSIn) : COLOR0
 {
     float4 color = 0;
	 
     float2 ProjectedTexCoords;
     ProjectedTexCoords.x = PSIn.Pos2DAsSeenByLight.x/PSIn.Pos2DAsSeenByLight.w/2.0f +0.5f;
     ProjectedTexCoords.y = -PSIn.Pos2DAsSeenByLight.y/PSIn.Pos2DAsSeenByLight.w/2.0f +0.5f;
     
	 float diffuseLightingFactor = 0;
	 
     if ((saturate(ProjectedTexCoords).x == ProjectedTexCoords.x) && (saturate(ProjectedTexCoords).y == ProjectedTexCoords.y))
     {
        float depthStoredInShadowMap = tex2D(ShadowMapSampler, ProjectedTexCoords).r;
        float realDistance = PSIn.Pos2DAsSeenByLight.z/PSIn.Pos2DAsSeenByLight.w;
		
		float3 lightDir = normalize(PSIn.Position3D.xyz - xLightPos);
		diffuseLightingFactor = dot(-lightDir, PSIn.Normal);
		diffuseLightingFactor = saturate(diffuseLightingFactor);
		diffuseLightingFactor *= xLightPower;
		 
         if ((realDistance - 0.01f) > depthStoredInShadowMap)
         {
			diffuseLightingFactor = 0;
         }
     }
	              
     color = tex2D(TextureSampler, PSIn.Tex) * (diffuseLightingFactor + xAmbient);
 
     return color;
 }

technique ShadowedScene
{
    pass Pass0
    {
        VertexShader = compile vs_4_0_level_9_1 ShadowedSceneVertexShader();
        PixelShader = compile ps_4_0_level_9_1 ShadowedScenePixelShader();
    }
}