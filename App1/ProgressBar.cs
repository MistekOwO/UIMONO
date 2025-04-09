using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace App1;

public class ProgressBar : GuiElement
{
    private float progress = 120;
    private bool IsVertical = false;
    private bool Inverse = false;
    private float width = 0;
    private Texture2D texture;

    public ProgressBar(float posX, float posY, string name, bool isVertical, bool inverse, float width)
    {
        this.positionX = posX;
        this.positionY = posY;
        this.name = name;
        this.IsVertical = isVertical;
        this.Inverse = inverse;
        this.width = width;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        var position = new Vector2(positionX, positionY);
        Rectangle rect = new Rectangle((int)positionX, (int)positionY, (int)((progress/100)*width * texture.Width), texture.Height);
        spriteBatch.Draw(texture, position,rect, Color.White);
        
    }

    public override void Load(Texture2D texture)
    {
        this.texture = texture;
    }
    
}