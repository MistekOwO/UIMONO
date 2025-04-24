using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SolidSilnique;

namespace App1;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private GUI _gui;
    private ProgressBar _hProgressBar;
    private TextureCube textureCube;
    private Effect _skyBoxEffect;
    private Camera camera;
    private bool firstMouse;
// Model-View-Projection
    private Matrix _world;
    private Matrix _view;
    private Matrix _projection;
    
    private float lastX;
    private float lastY;
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _graphics.HardwareModeSwitch = false;
        Mouse.SetCursor(MouseCursor.Crosshair);
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        _graphics.GraphicsProfile = GraphicsProfile.HiDef;
        _graphics.ApplyChanges();
        Mouse.SetPosition(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
        camera = new Camera(new Vector3(0, 0, 0));
        base.Initialize();
    }
    private void processMouse(GameTime gameTime)
    {
        int w = GraphicsDevice.Viewport.Width / 2;
        int h = GraphicsDevice.Viewport.Height / 2;

        if (firstMouse)
        {
            Mouse.SetPosition(w, h);
            firstMouse = false;
            return;
        }
        //Console.WriteLine("Mouse position: " + Mouse.GetState().X + " " + Mouse.GetState().Y);
        //Console.WriteLine("Mouse position (using Mouse.GetState().Position): " + Mouse.GetState().Position.X + " " + Mouse.GetState().Position.Y);
        float mouseX = w - Mouse.GetState().X;
        float mouseY = Mouse.GetState().Y - h;

        float xOffset = (mouseX);
        float yOffset = (mouseY);

        Mouse.SetPosition(w, h);

        camera.mouseMovement(xOffset, yOffset, gameTime.ElapsedGameTime.Milliseconds);
    }
    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(_graphics.GraphicsDevice);

        
        Console.WriteLine($"Graphics Device Capabilities: {_graphics.GraphicsProfile}");
        int size = 2048;
        
        
        textureCube = new TextureCube(_graphics.GraphicsDevice, size, false, SurfaceFormat.Color);
        Texture2D posX = Content.Load<Texture2D>("left");
        Texture2D negX = Content.Load<Texture2D>("right");
        Texture2D posY = Content.Load<Texture2D>("top");
        Texture2D negY = Content.Load<Texture2D>("bottom");
        Texture2D posZ = Content.Load<Texture2D>("back");
        Texture2D negZ = Content.Load<Texture2D>("front");
        
        SetCubeFaceData(textureCube, CubeMapFace.PositiveX, posX);
        SetCubeFaceData(textureCube, CubeMapFace.NegativeX, negX);
        SetCubeFaceData(textureCube, CubeMapFace.PositiveY, posY);
        SetCubeFaceData(textureCube, CubeMapFace.NegativeY, negY);
        SetCubeFaceData(textureCube, CubeMapFace.PositiveZ, posZ);
        SetCubeFaceData(textureCube, CubeMapFace.NegativeZ, negZ);
        
        SamplerState samplerState = new SamplerState
        {
            Filter = TextureFilter.Linear,
            AddressU = TextureAddressMode.Mirror,
            AddressV = TextureAddressMode.Mirror,
            AddressW = TextureAddressMode.Mirror,
            
        };
       
        _skyBoxEffect = Content.Load<Effect>("SkyBoxShader");
        if (_skyBoxEffect == null)
            throw new Exception("SkyBoxShader effect failed to load.");
        Matrix projectionMatrix = Matrix.CreatePerspectiveFieldOfView(
            MathHelper.PiOver4,
            GraphicsDevice.Viewport.AspectRatio,
            1.0f, 300.0f);
        Matrix world = Matrix.Identity;
        _projection = projectionMatrix;
        _skyBoxEffect.Parameters["Projection"].SetValue(projectionMatrix);
        
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
        
        
        var cubeVertices = new VertexPositionTexture[36];

// Define the size and texture coordinates
Vector3 topLeftFront = new Vector3(-2, 2, -2);
Vector3 topRightFront = new Vector3(2, 2, -2);
Vector3 bottomLeftFront = new Vector3(-2, -2, -2);
Vector3 bottomRightFront = new Vector3(2, -2, -2);

Vector3 topLeftBack = new Vector3(-2, 2, 2);
Vector3 topRightBack = new Vector3(2, 2, 2);
Vector3 bottomLeftBack = new Vector3(-2, -2, 2);
Vector3 bottomRightBack = new Vector3(2, -2, 2);

Vector2 textureTopLeft = new Vector2(0, 0);
Vector2 textureTopRight = new Vector2(1, 0);
Vector2 textureBottomLeft = new Vector2(0, 1);
Vector2 textureBottomRight = new Vector2(1, 1);


// Front face
cubeVertices[0] = new VertexPositionTexture(topLeftFront, textureTopLeft);
cubeVertices[1] = new VertexPositionTexture(bottomRightFront,  textureBottomRight);
cubeVertices[2] = new VertexPositionTexture(bottomLeftFront,  textureBottomLeft);

cubeVertices[3] = new VertexPositionTexture(topLeftFront,  textureTopLeft);
cubeVertices[4] = new VertexPositionTexture(topRightFront,  textureTopRight);
cubeVertices[5] = new VertexPositionTexture(bottomRightFront,  textureBottomRight);

// Back face
cubeVertices[6] = new VertexPositionTexture(topLeftBack,  textureTopLeft);
cubeVertices[7] = new VertexPositionTexture(bottomLeftBack,  textureBottomLeft);
cubeVertices[8] = new VertexPositionTexture(bottomRightBack,  textureBottomRight);

cubeVertices[9] = new VertexPositionTexture(topLeftBack,  textureTopLeft);
cubeVertices[10] = new VertexPositionTexture(bottomRightBack,  textureBottomRight);
cubeVertices[11] = new VertexPositionTexture(topRightBack,  textureTopRight);

// Top face
cubeVertices[12] = new VertexPositionTexture(topLeftFront,  textureBottomLeft);
cubeVertices[13] = new VertexPositionTexture(topRightBack,  textureTopRight);
cubeVertices[14] = new VertexPositionTexture(topRightFront,  textureBottomRight);

cubeVertices[15] = new VertexPositionTexture(topLeftFront,  textureBottomLeft);
cubeVertices[16] = new VertexPositionTexture(topLeftBack,  textureTopLeft);
cubeVertices[17] = new VertexPositionTexture(topRightBack,  textureTopRight);

// Bottom face
cubeVertices[18] = new VertexPositionTexture(bottomLeftFront,  textureTopLeft);
cubeVertices[19] = new VertexPositionTexture(bottomRightFront,  textureTopRight);
cubeVertices[20] = new VertexPositionTexture(bottomRightBack,  textureBottomRight);

cubeVertices[21] = new VertexPositionTexture(bottomLeftFront,  textureTopLeft);
cubeVertices[22] = new VertexPositionTexture(bottomRightBack,  textureBottomRight);
cubeVertices[23] = new VertexPositionTexture(bottomLeftBack,  textureBottomLeft);

// Left face
cubeVertices[24] = new VertexPositionTexture(topLeftFront,  textureTopRight);
cubeVertices[25] = new VertexPositionTexture(bottomLeftBack,  textureBottomLeft);
cubeVertices[26] = new VertexPositionTexture(bottomLeftFront,  textureBottomRight);

cubeVertices[27] = new VertexPositionTexture(topLeftFront,  textureTopRight);
cubeVertices[28] = new VertexPositionTexture(topLeftBack,  textureTopLeft);
cubeVertices[29] = new VertexPositionTexture(bottomLeftBack,  textureBottomLeft);

// Right face
cubeVertices[30] = new VertexPositionTexture(topRightFront,  textureTopLeft);
cubeVertices[31] = new VertexPositionTexture(bottomRightFront,  textureBottomLeft);
cubeVertices[32] = new VertexPositionTexture(bottomRightBack,  textureBottomRight);

cubeVertices[33] = new VertexPositionTexture(topRightFront,  textureTopLeft);
cubeVertices[34] = new VertexPositionTexture(bottomRightBack,  textureBottomRight);
cubeVertices[35] = new VertexPositionTexture(topRightBack, textureTopRight);

Console.WriteLine($"Cube Size: {textureCube.Size}");
Console.WriteLine($"Cube Format: {textureCube.Format}");

// Create a vertex buffer and upload the cube vertices
var vertexBuffer = new VertexBuffer(
    GraphicsDevice,
    typeof(VertexPositionTexture),
    cubeVertices.Length,
    BufferUsage.WriteOnly
);

vertexBuffer.SetData(cubeVertices);
GraphicsDevice.RasterizerState = RasterizerState.CullNone;
GraphicsDevice.DepthStencilState = DepthStencilState.Default;
GraphicsDevice.SetVertexBuffer(vertexBuffer);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        processMouse(gameTime);
        // TODO: Add your update logic here
        _view = camera.getViewMatrix();
        
        //Console.WriteLine("Yaw: " + _view + " pitch: " + _projection);
        
        _skyBoxEffect.Parameters["View"].SetValue(_view);
        _skyBoxEffect.Parameters["Projection"].SetValue(_projection);
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