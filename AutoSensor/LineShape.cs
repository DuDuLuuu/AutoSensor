/* ==================================
 * Author    :Coder.Yan
 * CreateTime:2012/9/5 22:59:05
 * Copyright :CY©2012
 * ==================================*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace CYControls
{
    [ToolboxBitmap(typeof(EllipseShape), "Resources.LineShape.png")]
    public class LineShape : CYBaseControl
    {
        private LineDirection _direct = LineDirection.Horizontial;

        /// <summary>
        /// 直线方向
        /// </summary>
        public LineDirection Direction
        {
            get { return _direct; }
            set
            {
                _direct = value;
                ResetPoint();
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
                ResetPoint();
            }
        }

        private PointF StartPoint
        {
            get;
            set;
        }

        private PointF EndPoint
        {
            get;
            set;
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawLine(ForegroundPen, StartPoint, EndPoint);
        }

        private void ResetPoint()
        {
            switch (Direction)
            {
                case LineDirection.Horizontial:
                    float y =  (DrawRect.Top + DrawRect.Bottom) / 2;
                    StartPoint = new PointF(DrawRect.Left,y);
                    EndPoint = new PointF(DrawRect.Right, y);
                    break;
                case LineDirection.Vertical:
                    float x = (DrawRect.Left + DrawRect.Right) / 2;
                    StartPoint = new PointF(x, DrawRect.Top);
                    EndPoint = new PointF(x, DrawRect.Bottom);
                    break;
                case LineDirection.Up:
                    StartPoint = new PointF(DrawRect.Left, DrawRect.Bottom);
                    EndPoint = new PointF(DrawRect.Right, DrawRect.Top);
                    break;
                case LineDirection.Down:
                    StartPoint = DrawRect.Location;
                    EndPoint = new PointF(DrawRect.Right, DrawRect.Bottom);
                    break;
            }
        }
    }

    public enum LineDirection
    {
        /// <summary>
        /// 水平
        /// </summary>
        Horizontial,
        /// <summary>
        /// 垂直
        /// </summary>
        Vertical,
        /// <summary>
        /// 向上
        /// </summary>
        Up,
        /// <summary>
        /// 向下
        /// </summary>
        Down
    }
}
