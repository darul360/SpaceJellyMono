float4 projectionColorMultiplier;

cbuffer MatrixBuffer
{
    matrix worldMatrix;
    matrix viewMatrix;
    matrix projectionMatrix;

    matrix viewMatrix2;
    matrix projectionMatrix2;
};

struct VertexShaderOutput
{
    float4 position : SV_POSITION;	
	float4 projectionPosition : POSITION1;
};

Texture2D projectedTexture;
sampler2D textureSampler = sampler_state{ texture = <projectedTexture> ; magfilter = LINEAR; minfilter = LINEAR; mipfilter=LINEAR; AddressU = clamp; AddressV = clamp;};

VertexShaderOutput ProjectionVertexShader(float3 inPos : SV_POSITION)
{

	VertexShaderOutput output = (VertexShaderOutput)0;

	float4 inPos4 = float4(inPos, 1);

    output.position = mul(inPos4, worldMatrix);
    output.position = mul(output.position, viewMatrix);
    output.position = mul(output.position, projectionMatrix);

    output.projectionPosition = mul(inPos4, worldMatrix);
    output.projectionPosition = mul(output.projectionPosition, viewMatrix2);
    output.projectionPosition = mul(output.projectionPosition, projectionMatrix2);

	return output;
}

float4 ProjectionPixelShader(VertexShaderOutput PSIn) : COLOR0
{
    float4 color = float4(0.0f, 0.0f, 0.0f, 0.0f);

	float2 projectTexCoord;
    projectTexCoord.x =  PSIn.projectionPosition.x / PSIn.projectionPosition.w / 2.0f + 0.5f;
    projectTexCoord.y = -PSIn.projectionPosition.y / PSIn.projectionPosition.w / 2.0f + 0.5f;

    if((saturate(projectTexCoord.x) == projectTexCoord.x) && (saturate(projectTexCoord.y) == projectTexCoord.y))
    {
        float4 projectionColor = tex2D(textureSampler, projectTexCoord);

        projectionColor *= projectionColorMultiplier;
        color = projectionColor;
    }
	return color;
}

technique BasicColorDrawing
{
	pass P0
	{
		VertexShader = compile vs_4_0_level_9_1 ProjectionVertexShader();
		PixelShader = compile ps_4_0_level_9_1 ProjectionPixelShader();
	}
};