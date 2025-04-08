namespace App1;

public class Button : GuiElement
{
    public float width;
    public float height;

    public Button(float posX, float posY, string name, float width, float height)
    {
        this.positionX = posX;
        this.positionY = posY;
        this.name = name;
        this.width = width;
        this.height = height;
    }
}