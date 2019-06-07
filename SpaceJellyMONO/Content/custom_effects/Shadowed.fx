float4x4 xWorldViewProjection;
float4x4 xLightsWorldViewProjection;
float4x4 xWorld;
float3 xLightPos;
float xLightPower;
float xAmbient;

Texture xShadowMap;
sampler ShadowMapSampler = sampler_state { texture = <xShadowMap> ; magfilter = LINEAR; minfilter = LINEAR; mipfilter=LINEAR; AddressU = clamp; AddressV = clamp;};
 
 float DotProduct(float3 lightPos, float3 pos3D, float3 normal)
{
    float3 lightDir = normalize(pos3D - lightPos);
    return dot(-lightDir, normal);    
}

struct SSceneVertexToPixel
{
    float4 Position             : POSITION;
    float4 Pos2DAsSeenByLight    : TEXCOORD0;

     float3 Normal                : TEXCOORD2;
     float4 Position3D            : TEXCOORD3;

};

 SSceneVertexToPixel ShadowedSceneVertexShader( float4 inPos : POSITION, float3 inNormal : NORMAL)
 {
     SSceneVertexToPixel Output = (SSceneVertexToPixel)0;
 
     Output.Position = mul(inPos, xWorldViewProjection);    
     Output.Pos2DAsSeenByLight = mul(inPos, xLightsWorldViewProjection);    
     Output.Normal = normalize(mul(inNormal, (float3x3)xWorld));    
     Output.Position3D = mul(inPos, xWorld);  
 
     return Output;
 }
 
float4 ShadowedScenePixelShader(SSceneVertexToPixel PSIn) : COLOR0
 {
     float4 color = 0; 
 
     float2 ProjectedTexCoords;
     ProjectedTexCoords[0] = PSIn.Pos2DAsSeenByLight.x/PSIn.Pos2DAsSeenByLight.w/2.0f +0.5f;
     ProjectedTexCoords[1] = -PSIn.Pos2DAsSeenByLight.y/PSIn.Pos2DAsSeenByLight.w/2.0f +0.5f;
     
     float diffuseLightingFactor = 0;
     if ((saturate(ProjectedTexCoords).x == ProjectedTexCoords.x) && (saturate(ProjectedTexCoords).y == ProjectedTexCoords.y))
     {
         float depthStoredInShadowMap = tex2D(ShadowMapSampler, ProjectedTexCoords).r;
         float realDistance = PSIn.Pos2DAsSeenByLight.z/PSIn.Pos2DAsSeenByLight.w;
         if ((realDistance - 1.0f/100.0f) <= depthStoredInShadowMap)
         {
             diffuseLightingFactor = DotProduct(xLightPos, PSIn.Position3D, PSIn.Normal);
             diffuseLightingFactor = saturate(diffuseLightingFactor);
             diffuseLightingFactor *= xLightPower;            
         }
     }
     float4 baseColor = float4(20,20,20,1);                
     color = baseColor*(diffuseLightingFactor + xAmbient);
 
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