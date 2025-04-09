namespace App1;

public class Text : GuiElement
{
    public string text;
    public float fontSize;
    public string font;

    public Text(float PositionX, float PositionY,string name, string text, float fontSize, string font)
    {
        this.positionX = PositionX;
        this.positionY = PositionY;
        this.name = name;
        this.text = text;
        this.fontSize = fontSize;
        this.font = font;
    }
}