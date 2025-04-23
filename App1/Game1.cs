using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace App1;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private GUI _gui;
    private ProgressBar _hProgressBar;
    private TextureCube textureCube;
    private Effect _skyBoxEffect;
    
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        _graphics.GraphicsProfile = GraphicsProfile.HiDef;
        _graphics.ApplyChanges();
    
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(_graphics.GraphicsDevice);

        _gui = new GUI("App1/resources/UI.xml", Content);
        _hProgressBar = _gui.progressBars[0];
        Console.WriteLine($"Graphics Device Capabilities: {_graphics.GraphicsProfile}");
        int size = 2048;
        
        
        textureCube = new TextureCube(_graphics.GraphicsDevice, size, false, SurfaceFormat.Color);
        Texture2D posX = Content.Load<Texture2D>("right");
        Texture2D negX = Content.Load<Texture2D>("left");
        Texture2D posY = Content.Load<Texture2D>("top");
        Texture2D negY = Content.Load<Texture2D>("bottom");
        Texture2D posZ = Content.Load<Texture2D>("front");
        Texture2D negZ = Content.Load<Texture2D>("back");
        
        SetCubeFaceData(textureCube, CubeMapFace.PositiveX, posX);
        SetCubeFaceData(textureCube, CubeMapFace.NegativeX, negX);
        SetCubeFaceData(textureCube, CubeMapFace.PositiveY, posY);
        SetCubeFaceData(textureCube, CubeMapFace.NegativeY, negY);
        SetCubeFaceData(textureCube, CubeMapFace.PositiveZ, posZ);
        SetCubeFaceData(textureCube, CubeMapFace.NegativeZ, negZ);
        
        SamplerState samplerState = new SamplerState
        {
            Filter = TextureFilter.Linear,
            AddressU = TextureAddressMode.Clamp,
            AddressV = TextureAddressMode.Clamp,
            AddressW = TextureAddressMode.Clamp
        };
       
        _skyBoxEffect = Content.Load<Effect>("SkyBoxShader");
        if (_skyBoxEffect == null)
            throw new Exception("SkyBoxShader effect failed to load.");
        Matrix view = Matrix.CreateLookAt(new Vector3(0, 0, -5), Vector3.Zero, Vector3.Up);
        Matrix projectionMatrix = Matrix.CreatePerspectiveFieldOfView(
            MathHelper.PiOver4,
            GraphicsDevice.Viewport.AspectRatio,
            1.0f, 300.0f);
        Matrix world = Matrix.Identity;
        
        _skyBoxEffect.Parameters["WorldViewProjection"].SetValue(world*view*projectionMatrix);
        
        foreach (var parameter in _skyBoxEffect.Parameters)
        {
            Console.WriteLine($"Parameter: {parameter.Name}, Type: {parameter.ParameterType}");
        }

        if (textureCube == null)
        {
            Console.WriteLine("piwo");
        }
        _skyBoxEffect.Parameters["CubeSampler+CubeTexture"].SetValue(textureCube);
        _graphics.GraphicsDevice.SamplerStates[0] = samplerState;
        
        
        var cubeVertices = new VertexPositionNormalTexture[36];

// Define the size and texture coordinates
Vector3 topLeftFront = new Vector3(-1, 1, -1);
Vector3 topRightFront = new Vector3(1, 1, -1);
Vector3 bottomLeftFront = new Vector3(-1, -1, -1);
Vector3 bottomRightFront = new Vector3(1, -1, -1);

Vector3 topLeftBack = new Vector3(-1, 1, 1);
Vector3 topRightBack = new Vector3(1, 1, 1);
Vector3 bottomLeftBack = new Vector3(-1, -1, 1);
Vector3 bottomRightBack = new Vector3(1, -1, 1);

Vector2 textureTopLeft = new Vector2(0, 0);
Vector2 textureTopRight = new Vector2(1, 0);
Vector2 textureBottomLeft = new Vector2(0, 1);
Vector2 textureBottomRight = new Vector2(1, 1);

// Normals for each face
Vector3 frontNormal = new Vector3(0, 0, -1);
Vector3 backNormal = new Vector3(0, 0, 1);
Vector3 topNormal = new Vector3(0, 1, 0);
Vector3 bottomNormal = new Vector3(0, -1, 0);
Vector3 leftNormal = new Vector3(-1, 0, 0);
Vector3 rightNormal = new Vector3(1, 0, 0);

// Front face
cubeVertices[0] = new VertexPositionNormalTexture(topLeftFront, frontNormal, textureTopLeft);
cubeVertices[1] = new VertexPositionNormalTexture(bottomRightFront, frontNormal, textureBottomRight);
cubeVertices[2] = new VertexPositionNormalTexture(bottomLeftFront, frontNormal, textureBottomLeft);

cubeVertices[3] = new VertexPositionNormalTexture(topLeftFront, frontNormal, textureTopLeft);
cubeVertices[4] = new VertexPositionNormalTexture(topRightFront, frontNormal, textureTopRight);
cubeVertices[5] = new VertexPositionNormalTexture(bottomRightFront, frontNormal, textureBottomRight);

// Back face
cubeVertices[6] = new VertexPositionNormalTexture(topLeftBack, backNormal, textureTopLeft);
cubeVertices[7] = new VertexPositionNormalTexture(bottomLeftBack, backNormal, textureBottomLeft);
cubeVertices[8] = new VertexPositionNormalTexture(bottomRightBack, backNormal, textureBottomRight);

cubeVertices[9] = new VertexPositionNormalTexture(topLeftBack, backNormal, textureTopLeft);
cubeVertices[10] = new VertexPositionNormalTexture(bottomRightBack, backNormal, textureBottomRight);
cubeVertices[11] = new VertexPositionNormalTexture(topRightBack, backNormal, textureTopRight);

// Top face
cubeVertices[12] = new VertexPositionNormalTexture(topLeftFront, topNormal, textureBottomLeft);
cubeVertices[13] = new VertexPositionNormalTexture(topRightBack, topNormal, textureTopRight);
cubeVertices[14] = new VertexPositionNormalTexture(topRightFront, topNormal, textureBottomRight);

cubeVertices[15] = new VertexPositionNormalTexture(topLeftFront, topNormal, textureBottomLeft);
cubeVertices[16] = new VertexPositionNormalTexture(topLeftBack, topNormal, textureTopLeft);
cubeVertices[17] = new VertexPositionNormalTexture(topRightBack, topNormal, textureTopRight);

// Bottom face
cubeVertices[18] = new VertexPositionNormalTexture(bottomLeftFront, bottomNormal, textureTopLeft);
cubeVertices[19] = new VertexPositionNormalTexture(bottomRightFront, bottomNormal, textureTopRight);
cubeVertices[20] = new VertexPositionNormalTexture(bottomRightBack, bottomNormal, textureBottomRight);

cubeVertices[21] = new VertexPositionNormalTexture(bottomLeftFront, bottomNormal, textureTopLeft);
cubeVertices[22] = new VertexPositionNormalTexture(bottomRightBack, bottomNormal, textureBottomRight);
cubeVertices[23] = new VertexPositionNormalTexture(bottomLeftBack, bottomNormal, textureBottomLeft);

// Left face
cubeVertices[24] = new VertexPositionNormalTexture(topLeftFront, leftNormal, textureTopRight);
cubeVertices[25] = new VertexPositionNormalTexture(bottomLeftBack, leftNormal, textureBottomLeft);
cubeVertices[26] = new VertexPositionNormalTexture(bottomLeftFront, leftNormal, textureBottomRight);

cubeVertices[27] = new VertexPositionNormalTexture(topLeftFront, leftNormal, textureTopRight);
cubeVertices[28] = new VertexPositionNormalTexture(topLeftBack, leftNormal, textureTopLeft);
cubeVertices[29] = new VertexPositionNormalTexture(bottomLeftBack, leftNormal, textureBottomLeft);

// Right face
cubeVertices[30] = new VertexPositionNormalTexture(topRightFront, rightNormal, textureTopLeft);
cubeVertices[31] = new VertexPositionNormalTexture(bottomRightFront, rightNormal, textureBottomLeft);
cubeVertices[32] = new VertexPositionNormalTexture(bottomRightBack, rightNormal, textureBottomRight);

cubeVertices[33] = new VertexPositionNormalTexture(topRightFront, rightNormal, textureTopLeft);
cubeVertices[34] = new VertexPositionNormalTexture(bottomRightBack, rightNormal, textureBottomRight);
cubeVertices[35] = new VertexPositionNormalTexture(topRightBack, rightNormal, textureTopRight);

Console.WriteLine($"Cube Size: {textureCube.Size}");
Console.WriteLine($"Cube Format: {textureCube.Format}");

// Create a vertex buffer and upload the cube vertices
var vertexBuffer = new VertexBuffer(
    GraphicsDevice,
    typeof(VertexPositionNormalTexture),
    cubeVertices.Length,
    BufferUsage.WriteOnly
);

vertexBuffer.SetData(cubeVertices);
GraphicsDevice.RasterizerState = RasterizerState.CullClockwise;
GraphicsDevice.DepthStencilState = DepthStencilState.Default;
GraphicsDevice.SetVertexBuffer(vertexBuffer);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        
        foreach (var pass in _skyBoxEffect.CurrentTechnique.Passes)
        {
            pass.Apply();
            _graphics.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList,0,12);
        }
        
        base.Draw(gameTime);
    }

    void SetCubeFaceData(TextureCube cube, CubeMapFace face, Texture2D texture)
    {
        Color[] colorData = new Color[texture.Width * texture.Height];
        texture.GetData(colorData);
        Console.WriteLine($"Setting data for {face}: First Pixel = {colorData[0]}");

        try
        {
            cube.SetData(face, 0, null, colorData, 0, colorData.Length);
            Console.WriteLine($"Successfully set data for face {face}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to set data for face {face}: {ex.Message}");
        }
    }
}