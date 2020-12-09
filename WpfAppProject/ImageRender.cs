using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfAppProject
{
    public static class ImageRender
    {
        private static Dictionary<string, Bitmap> Cache = new Dictionary<string, Bitmap>();

        public static Bitmap GetBitmapUrl(string url)
        {
            if (Cache.ContainsKey(url))
            {
                return Cache[url];
            }
            else
            {
                Cache.Add(url, new Bitmap(url));
                return (Bitmap)Cache[url].Clone();
            }
            
        }

       
        

        public static void ClearCache()
        {
            Cache.Clear();
        }

        public static Bitmap GetBitmap(int x, int y)
        {
            if (!Cache.ContainsKey("empty"))
            {
                Bitmap bitmap = new Bitmap(x, y);
                Graphics graphics = Graphics.FromImage(bitmap);
                graphics.FillRectangle(new SolidBrush(System.Drawing.Color.Green), 0, 0, x, y);
                Cache.Add("empty", bitmap);
                return (Bitmap)bitmap.Clone();
            }
            return (Bitmap)Cache["empty"].Clone();
        }
        public static BitmapSource CreateBitmapSourceFromGdiBitmap(Bitmap bitmap)
        {
            if (bitmap == null)
                throw new ArgumentNullException("bitmap");

            var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            var bitmapData = bitmap.LockBits(
                rect,
                ImageLockMode.ReadWrite,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            try
            {
                var size = (rect.Width * rect.Height) * 4;

                return BitmapSource.Create(
                    bitmap.Width,
                    bitmap.Height,
                    bitmap.HorizontalResolution,
                    bitmap.VerticalResolution,
                    PixelFormats.Bgra32,
                    null,
                    bitmapData.Scan0,
                    size,
                    bitmapData.Stride);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
        }
    }
}
