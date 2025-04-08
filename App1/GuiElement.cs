using System;
using Microsoft.Xna.Framework.Graphics;

namespace App1;

public class GuiElement
{
    public float positionX;
    public float positionY;
    public string name;

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        Console.WriteLine("Kocham piwko");
    }
}