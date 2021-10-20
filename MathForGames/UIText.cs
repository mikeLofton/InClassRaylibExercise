using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class UIText : Actor
    {
        public string Text;
        public int Width;
        public int Height;
        public int FontSize;
        public Font Font;

        /// <summary>
        /// Sets the starting value for the text box
        /// </summary>
        /// <param name="x">The x position of the text box</param>
        /// <param name="y">The y position of the text box</param>
        /// <param name="name"></param>
        /// <param name="color"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="text"></param>
        public UIText(float x, float y, Color color, string name, int width, int height, int fontSize, string text = "")
            : base('\0', x, y, color, name)
        {
            Text = text;
            Width = width;
            Height = height;
            Font = Raylib.LoadFont("resources/fonts/alagard.png");
            FontSize = fontSize;
        }

        public override void Draw()
        {
            //Create a new rectangle that will act as the borders of the text box
            Rectangle textBox = new Rectangle(Position.X, Position.Y, Width, Height);
            //Draw text box
            Raylib.DrawTextRec(Font, Text, textBox, FontSize, 1, true, Icon.Color);         
        }
    }
}
