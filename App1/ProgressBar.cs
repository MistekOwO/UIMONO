using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace App1;

public class ProgressBar : GuiElement
{
    public float progress = 40;
    private bool IsVertical = false;
    private bool Inverse = false;
    private float width = 0;
    private Texture2D texture;

    public ProgressBar(float posX, float posY, string name, bool isVertical, bool inverse, float width, float scale)
    {
        this.positionX = posX;
        this.positionY = posY;
        this.name = name;
        this.IsVertical = isVertical;
        this.Inverse = inverse;
        this.width = width;
        this.scale = scale;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        var position = new Vector2(positionX, positionY);
        Rectangle rect = new Rectangle((int)positionX,(int)positionY,(int)(texture.Width * progress/100), texture.Height);
        spriteBatch.Draw(texture, position,null, Color.White,0,Vector2.Zero,scale,SpriteEffects.None,0);
    }

    public override void Load(Texture2D texture)
    {
        this.texture = texture;
    }

    public void setProgress(float progress)
    {
        this.progress = progress;
    }
}