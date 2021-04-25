using System;
using System.Drawing;

namespace SurePortal.Core.Kernel.Helper
{
    public static class AvatarHelper
    {

        public static System.Drawing.Image GenerateAvtarImage(String text)
        {
            Font font = new Font(FontFamily.GenericSerif, 45, FontStyle.Bold);
            Color textColor = ColorTranslator.FromHtml("#FFF");
            Color backColor = ColorTranslator.FromHtml("#83B869");

            //first, create a dummy bitmap just to get a graphics object  
            System.Drawing.Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            //measure the string to see how big the image needs to be  

            //free up the dummy image and old graphics object  
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size  
            img = new Bitmap(110, 110);

            drawing = Graphics.FromImage(img);

            //paint the background  
            drawing.Clear(backColor);

            //create a brush for the text  
            Brush textBrush = new SolidBrush(textColor);

            drawing.DrawString(text, font, textBrush, new Rectangle(-2, 20, 200, 110));

            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();


            return img;

        }
    }
}
