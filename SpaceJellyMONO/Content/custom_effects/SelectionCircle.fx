#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

cbuffer WVP
{
	matrix worldMatrix;
	matrix viewMatrix;
	matrix projectionMatrix;
}

Texture2D cicrcleTexture;
sampler2D textureSampler = sampler_state{ texture = <circleTexture> ; magfilter = LINEAR; minfilter = LINEAR; mipfilter=LINEAR; AddressU = clamp; AddressV = clamp;};

float4 tintColor;

struct VertexShaderInput
{
	float3 Position : SV_POSITION;
	float2 Tex : TEXCOORD0;
};

struct VertexShaderOutput
{
	float4 Position : SV:POSITION;
	float2 Tex : TEXCOORD0;
};

VertexShaderOutput MainVS(in VertexShaderInput input)
{
	VertexShaderOutput output = (VertexShaderOutput)0;
	float4 Position4 = float4(input.Position, 1);
	
	output.Position = mul(Position4, worldMatrix);
	output.Position = mul(output.Position, viewMatrix);
	output.Position = mul(output.Position, projectionMatrix);
	
	output.Tex = input.Tex;

	return output;
}

float4 MainPS(VertexShaderOutput input) : COLOR0
{
	float4 color = tex2D(textureSampler, input.Tex);
	return color * tintColor;
}

technique BasicColorDrawing
{
	pass P0
	{
		VertexShader = compile VS_SHADERMODEL MainVS();
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};