using System.Drawing;
using System.Drawing.Imaging;
//TBF Tool by TheGameExplorer

namespace tbftool
{
    public static class BitmapHelper
    {
        public static Bitmap Create(Color[] palette, byte[] indices, int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    bitmap.SetPixel(x, y, palette[indices[x + y * width]]);
                }
            }
            return bitmap;
        }

        public static Bitmap Create(Color[] colors, int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    bitmap.SetPixel(x, y, colors[x + y * width]);
                }
            }
            return bitmap;
        }

        public static Bitmap Rescale(Bitmap bitmap, int width, int height)
        {
            int factorX = bitmap.Width / width;
            int factorY = bitmap.Height / height;
            int sum = (factorX + factorY);
            sum = (sum + (sum % 2)) / 2;
            if (sum < 1)
                return bitmap;
            else
                return new Bitmap(bitmap, new Size(bitmap.Width / sum, bitmap.Height / sum));
        }

        /*
        public unsafe static byte[] GetIndices(Bitmap bitmap)
        {
            BitmapData rawData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, bitmap.PixelFormat);

            byte[] indices = new byte[rawData.Height * rawData.Width];
            byte* p = (byte*)rawData.Scan0.ToPointer();
            for (int y = 0; y < rawData.Height; y++)
            {
                for (int x = 0; x < rawData.Width; x++)
                {
                    int offset = y * rawData.Stride + x;
                    indices[x + y * rawData.Width] = (p[offset]);
                }
            }
            bitmap.UnlockBits(rawData);

            return indices;
        }
        */
    }
}
