namespace App1;

public class TextInput : GuiElement
{
    public float fontSize;
    public string font;
    public string placeholder;
    

    public TextInput(float positionX,float positionY,string name,float fontSize, string font, string placeholder)
    {
        this.positionX = positionX;
        this.positionY = positionY;
        this.name = name;
        this.fontSize = fontSize;
        this.font = font;
        this.placeholder = placeholder;
    }
}