/* ==================================
 * Author    :Coder.Yan
 * CreateTime:2012/9/5 23:54:52
 * Copyright :CY©2012
 * ==================================*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace CYControls
{
    [ToolboxBitmap(typeof(EllipseShape), "Resources.PictureObject.png")]
    public class OpacityImage : CYBaseControl
    {
        private Image _image = null;
        private ImageFillMode _fillMode = ImageFillMode.Zoom;

        public Image Image
        {
            get { return _image; }
            set
            {
                _image = value;
                ResetPaintImage();
            }
        }

        public ImageFillMode FillMode
        {
            get { return _fillMode; }
            set
            {
                _fillMode = value;
                ResetPaintImage();
            }
        }

        protected override RectangleF DrawRect
        {
            get
            {
                return base.DrawRect;
            }
            set
            {
                base.DrawRect = value;
                ResetPaintImage();
            }
        }

        protected Image PaintImage
        {
            get;
            private set;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (PaintImage != null)
            {
                e.Graphics.DrawImage(PaintImage, Point.Empty);
            }
        }

        private void ResetPaintImage()
        {
            if (PaintImage != null)
            {
                PaintImage.Dispose();
                PaintImage = null;
            }
            if (Image != null && Image.Width > 0 && Image.Height > 0 && DrawRect.Width > 0 && DrawRect.Height > 0)
            {
                PaintImage = new Bitmap((int)DrawRect.Width, (int)DrawRect.Height);
                using (Graphics g = Graphics.FromImage(PaintImage))
                {
                    System.Drawing.Imaging.ImageAttributes ima = new System.Drawing.Imaging.ImageAttributes();
                    ColorMatrix cm = new ColorMatrix();
                    cm.Matrix33 = Opacity;
                    ima.SetColorMatrix(cm);
                    Point pt = Point.Empty;
                    switch (FillMode)
                    {
                        case ImageFillMode.Center:
                            pt = new Point((int)(DrawRect.Width - Image.Width) / 2, (int)(DrawRect.Height - Image.Height) / 2);
                            g.DrawImage(Image, new Rectangle(pt, Image.Size), 0, 0, Image.Width, Image.Height, GraphicsUnit.Pixel, ima);
                            break;
                        case ImageFillMode.Strength:
                            g.DrawImage(Image, Rectangle.Round(DrawRect), 0, 0, Image.Width, Image.Height, GraphicsUnit.Pixel, ima);
                            break;
                        case ImageFillMode.Title:
                            ima.SetWrapMode(System.Drawing.Drawing2D.WrapMode.Tile);
                            TextureBrush brush = new TextureBrush(Image,new Rectangle(0,0,Image.Width,Image.Height), ima);
                            g.FillRectangle(brush, DrawRect);
                            break;
                        case ImageFillMode.Zoom:
                            float scale = 1;
                            if (Image.Width > 0 && Image.Height > 0 && DrawRect.Width > 0 && DrawRect.Height > 0)
                            {
                                float f1 = DrawRect.Width / Image.Width;
                                float f2 = DrawRect.Height / Image.Height;
                                scale = f1 > f2 ? f2 : f1;
                                float zWidth = scale * Image.Width;
                                float zHeight = scale * Image.Height;
                                //RectangleF zoomRect = new RectangleF(
                            }
                            break;
                    }
                }
            }
            else
            {
                PaintImage = null;
            }
        }
    }

    public enum ImageFillMode
    {
        Zoom,
        Title,
        Strength,
        Center
    }
}
