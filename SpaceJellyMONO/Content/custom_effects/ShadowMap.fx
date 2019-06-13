cbuffer MatrixBuffer
{
    matrix worldMatrix;
    matrix viewMatrix;
    matrix projectionMatrix;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
	float2 Position2D    : TEXCOORD0;
};

VertexShaderOutput VertexShaderFunction(float3 InPos : SV_POSITION)
{
    VertexShaderOutput output = (VertexShaderOutput)0;
	
	float4 InPos4 = float4(InPos, 1);
    float4 worldPosition = mul(InPos4, worldMatrix);
    float4 viewPosition = mul(worldPosition, viewMatrix);
    output.Position = mul(viewPosition, projectionMatrix);
	output.Position2D = output.Position.zw;
	
    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
    return input.Position2D.x /input.Position2D.y;
}

technique Technique1
{
    pass Pass1
    {
		VertexShader = compile vs_4_0_level_9_1 VertexShaderFunction();
		PixelShader = compile ps_4_0_level_9_1 PixelShaderFunction();
    }
}