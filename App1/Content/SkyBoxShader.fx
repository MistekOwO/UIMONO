#if OPENGL
    #define SV_POSITION POSITION
    #define VS_SHADERMODEL vs_3_0
    #define PS_SHADERMODEL ps_3_0
#else
    #define VS_SHADERMODEL vs_4_0
    #define PS_SHADERMODEL ps_4_0
#endif

TextureCube CubeTexture : register(t0); // The cube map bound to texture register t0
SamplerState CubeSampler : register(s0); // The sampler bound to sampler register s0

matrix WorldViewProjection; // Combined World-View-Projection matrix

struct VertexShaderInput
{
    float4 Position : POSITION0; // Vertex position
    float3 Normal : NORMAL0;     // Vertex normal
};

struct VertexShaderOutput
{
    float4 Position : SV_POSITION; // Transformed position
    float3 Normal : TEXCOORD0;     // Pass normal to pixel shader
};

VertexShaderOutput MainVS(in VertexShaderInput input)
{
    VertexShaderOutput output = (VertexShaderOutput)0;

    // Transform position using World-View-Projection matrix
    output.Position = mul(input.Position, WorldViewProjection);

    // Pass the normal vector (normalize if needed)
    output.Normal = normalize(input.Normal);

    return output;
}

float4 MainPS(VertexShaderOutput input) : SV_TARGET
{
    // Normalize the direction vector
    float3 direction = normalize(input.Normal);

    // Sample the cube map using the direction vector
    return CubeTexture.Sample(CubeSampler, direction);
}

technique BasicColorDrawing
{
    pass P0
    {
        VertexShader = compile VS_SHADERMODEL MainVS();
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
};