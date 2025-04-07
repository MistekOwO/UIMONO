using Microsoft.Xna.Framework.Graphics;

namespace App1;

public class ProgressBar : GuiElement
{
    private float progress = 0;
    private bool IsVertical = false;
    private bool Inverse = false;
    private float width = 0;
    private Texture2D texture;
    public ProgressBar(float posX, float posY,string name, bool isVertical = false, bool inverse = false, string PathToResource = "")
    {
        this.positionX = posX;
        this.positionY = posY;
        this.name = name;
        this.IsVertical = isVertical;
        this.Inverse = inverse;
        
        
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        
    }

}