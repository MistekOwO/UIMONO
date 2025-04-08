using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace App1;

public static class XMLLoader
{
    

    public static List<GuiElement> readUIFile(string filename)
    {
        List<GuiElement> elements = new List<GuiElement>();
        FileStream XMLFile = new FileStream(filename, FileMode.Open);
        XmlDocument UIFile = new XmlDocument();
        UIFile.Load(XMLFile);
        XmlElement root = UIFile.DocumentElement;
        XmlNodeList nodes = root.GetElementsByTagName("GuiElement");

        foreach (XmlNode node in nodes)
        {
           
            switch (amog)
            {
               
            }
        }
        return elements;
    }

    private static GuiElement resolveElement(XmlNode node)
    {
        string? amog = node.FirstChild.Name;
        float PositionX = float.Parse(node.Attributes["positionX"].Value);
        float PositionY = float.Parse(node.Attributes["positionY"].Value);
        string name = node.Attributes["name"].Value;
        switch (amog)
        {
            case "Bar":
                bool orientation = node.Attributes["orientation"].Value.Equals("vertical");
                bool inverted = node.Attributes["inverse"].Value.Equals("true");
                float width = float.Parse(node.Attributes["width"].Value);
                ProgressBar newProgressBar = new ProgressBar(PositionX, PositionY,name,orientation,inverted,width);
                break;
            case "Button":
                float width2 = float.Parse(node.Attributes["width"].Value);
                float height = float.Parse(node.Attributes["height"].Value);
                Button newButton = new Button(PositionX,PositionY,name,width2,height);
                break;
            case "Image":
                Image newImage = new Image(PositionX, PositionY, name);
                break;
            case "TextInput":
                
                break;
            case "Text":
                break;
            default:
                Console.WriteLine("Unknown element");
                break;
        }
    }
    
}