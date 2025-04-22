using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace App1;

public class ProgressBar : GuiElement
{
    public float progress = 100;
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
        Vector2 nonUniformScale = new Vector2(scale * (progress/100), scale);
        Rectangle rect = new Rectangle((int)positionX,(int)positionY,(int)(texture.Width*scale * progress/100), (int)(texture.Height*scale));
        spriteBatch.Draw(texture, position,null, Color.White,0,Vector2.Zero,nonUniformScale,SpriteEffects.None,0);
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