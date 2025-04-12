using System;
using Microsoft.Xna.Framework.Graphics;

namespace App1;

public class GuiElement
{
    public float positionX;
    public float positionY;
    public string name;
    public float scale;

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        Console.WriteLine("Kocham piwko");
    }
    public virtual void Load(Texture2D texture)
    {
        Console.WriteLine("Kocham piwko");
    }
    public virtual void Load(SpriteFont font)
    {
        Console.WriteLine("Kocham piwko2");
    }
    
}