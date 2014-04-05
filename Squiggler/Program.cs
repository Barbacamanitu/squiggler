using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace Squiggler
{
    class Program
    {

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please enter a filename.");
                return;
            }
            string filename = args[0];
            Bitmap mPicture;
            try
            {
                mPicture = (Bitmap)Bitmap.FromFile(filename);
            }
            catch ( Exception e)
            {
                Console.WriteLine("Couldn't open file.");
                Console.WriteLine(e.Message);
                return;
            }
    
            int squiggleWidth = 2;
            double squiggleFreq = .2;

            if (args.Length > 1)
            {
                try
                {
                    squiggleWidth = Convert.ToInt32(args[1]);
                    squiggleFreq = Convert.ToDouble(args[2]);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid Arguments");
                    Console.WriteLine(e.Message);
                    return;
                }
            }
          
            Bitmap nPicture = new Bitmap(mPicture.Width - (int)(2 * squiggleWidth), mPicture.Height);
          

           
                for (int y = 0; y < nPicture.Height; y++)
                {
                    int xOffset = Convert.ToInt32((squiggleWidth * Math.Sin(y * squiggleFreq)) + (squiggleWidth));
                    //Shift all pixels
                    for (int x = 0; x < nPicture.Width; x++)
                    {
                        int oldX = x + xOffset;
                        nPicture.SetPixel(x, y, mPicture.GetPixel(oldX, y));
                      
                    }
                    ReportProgress(y, nPicture.Height);

                 
                }

                nPicture.Save(filename + "_squiggled.png", System.Drawing.Imaging.ImageFormat.Png);
        }

        static void ReportProgress(int linesDone, int totalLines)
        {
            double progress = ((double)linesDone/(double)totalLines);
            Console.WriteLine("Progress: " + (int)(progress * 100) + "%");
        }
    }
}
