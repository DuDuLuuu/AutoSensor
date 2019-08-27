using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoSensor.Util
{
    class Camera
    {
        private Bitmap _screenshot = null;

        /// <summary>
        /// 截取全屏
        /// </summary>
        /// <returns>返回值</returns>
        public Bitmap CaptureScreen()
        {
            return Capture(Rectangle.Empty, false);
        }

        /// <summary>
        /// 截取全屏并保存
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <returns></returns>
        public Bitmap CaptureScreen(string fileName)
        {
            Bitmap screenshot = Capture(Rectangle.Empty, false);
            saveImage(fileName, screenshot);
            return screenshot;
        }

        /// <summary>
        /// 截屏到剪切板
        /// </summary>
        public void CopyToClipboard()
        {
            if (this._screenshot != null)
                Clipboard.SetImage(this._screenshot);
            else if (this._screenshot == null)
                MessageBox.Show("No screenshot found. Please take a screenshot first.", "Copy to Clipboard");
        }

        /// <summary>
        /// 区域截屏
        /// </summary>
        /// <param name="rect">区域范围</param>
        /// <returns>返回值</returns>
        public Bitmap CaptureRectangle(Rectangle rect)
        {
            return Capture(rect, true);
        }

        /// <summary>
        /// 区域截屏
        /// </summary>
        /// <param name="rect">区域范围</param>
        /// <param name="fileName">文件路径</param>
        /// <returns>返回值</returns>
        public Bitmap CaptureRectangle(Rectangle rect, string fileName)
        {
            Bitmap screenshot = Capture(rect, true);
            saveImage(fileName, screenshot);
            return screenshot;
        }

        private Bitmap Capture(Rectangle rect, bool isRect)
        {
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            Bitmap screenshot = null;

            if (!isRect)
                screenshot = new Bitmap(screenWidth, screenHeight);
            else if (isRect)
                screenshot = new Bitmap(rect.Width, rect.Height);

            Graphics g = Graphics.FromImage(screenshot);
            if (!isRect)
            {
                g.CopyFromScreen(Point.Empty, Point.Empty, screenshot.Size);
            }
            else if (isRect)
            {
                g.CopyFromScreen(new Point(rect.X, rect.Y), Point.Empty, rect.Size);
            }

            this._screenshot = screenshot;

            return screenshot;
        }

        private void saveImage(string fileName, Bitmap screenshot)
        {
            string ext = System.IO.Path.GetExtension(fileName); ;
            ext = ext.ToLower();

            if (ext == ".jpg" || ext == ".jpeg")
                screenshot.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            else if (ext == ".gif")
                screenshot.Save(fileName, System.Drawing.Imaging.ImageFormat.Gif);
            else if (ext == ".png")
                screenshot.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
            else if (ext == ".bmp")
                screenshot.Save(fileName, System.Drawing.Imaging.ImageFormat.Bmp);
            else if (ext == ".tiff")
                screenshot.Save(fileName, System.Drawing.Imaging.ImageFormat.Tiff);
        }
    }
}
