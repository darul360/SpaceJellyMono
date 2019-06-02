#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1

cbuffer MatrixBuffer
{
    matrix worldMatrix;
    matrix viewMatrix;
    matrix projectionMatrix;

    matrix viewMatrix2;
    matrix projectionMatrix2;
};

struct VertexShaderInput
{
    float4 position : POSITION;
    float2 tex : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 position : SV_POSITION;
    float2 tex : TEXCOORD0;
	
	float4 viewPosition : TEXCOORD1;
};

Texture2D projectionTexture : register(t0);

SamplerState SampleType;

VertexShaderOutput MainVS(in VertexShaderInput input)
{
	VertexShaderOutput output = (VertexShaderOutput)0;
    // Change the position vector to be 4 units for proper matrix calculations.
    input.position.w = 1.0f;

    // Calculate the position of the vertex against the world, view, and projection matrices.
    output.position = mul(input.position, worldMatrix);
    output.position = mul(output.position, viewMatrix);
    output.position = mul(output.position, projectionMatrix);

    // Store the position of the vertice as viewed by the projection view point in a separate variable.
    output.viewPosition = mul(input.position, worldMatrix);
    output.viewPosition = mul(output.viewPosition, viewMatrix2);
    output.viewPosition = mul(output.viewPosition, projectionMatrix2);

    // Store the texture coordinates for the pixel shader.
    output.tex = input.tex;

	return output;
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
	float4 color;
    float4 textureColor;
    float2 projectTexCoord;
    float4 projectionColor;

    color = float4(0.0f, 0.0f, 0.0f, 0.0f);

    // Calculate the projected texture coordinates.
    projectTexCoord.x =  input.viewPosition.x / input.viewPosition.w / 2.0f + 0.5f;
    projectTexCoord.y = -input.viewPosition.y / input.viewPosition.w / 2.0f + 0.5f;

    // Determine if the projected coordinates are in the 0 to 1 range.  If it is then this pixel is inside the projected view port.
    if((saturate(projectTexCoord.x) == projectTexCoord.x) && (saturate(projectTexCoord.y) == projectTexCoord.y))
    {

        // Sample the color value from the projection texture using the sampler at the projected texture coordinate location.
        projectionColor = projectionTexture.Sample(SampleType, projectTexCoord);

        // Set the output color of this pixel to the projection texture overriding the regular color value.
        color = projectionColor;
    }
	return color;
}

technique BasicColorDrawing
{
	pass P0
	{
		VertexShader = compile VS_SHADERMODEL MainVS();
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};