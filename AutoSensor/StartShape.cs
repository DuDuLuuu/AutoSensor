/* ==================================
 * Author    :Coder.Yan
 * CreateTime:2012/9/19 21:53:09
 * Copyright :CY©2012
 * ==================================*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace CYControls
{
    [ToolboxBitmap(typeof(StartShape), "Resources.StartShape.png")]
    public class StartShape : CYBaseControl
    {
        private PointF[] _pts = null;

        public StartShape()
        {
            _pts = new PointF[10];
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (DrawRect.Width > 0 && DrawRect.Height > 0)
            {
                float a1 = DrawRect.Width / 2;
                float b1 = DrawRect.Height / 2;
                float a2 = DrawRect.Width / 4;
                float b2 = DrawRect.Height / 4;
                float x0 = (DrawRect.Left + DrawRect.Right) / 2;
                float y0 = (DrawRect.Top + DrawRect.Bottom) / 2;
                double startAngle1 = -Math.PI / 2;
                double startAngle2 = Math.PI * (-1 / 2f + 1 / 5f);
                for (int i = 0; i < 5; i++)
                {
                    double angle1 = startAngle1 + 2*Math.PI * i / 5;
                    _pts[2 * i] = new PointF((float)(a1 * Math.Cos(angle1) + x0),
                        (float)(b1 * Math.Sin(angle1) + y0));
                    double angle2 = startAngle2 + 2*Math.PI * i / 5;
                    _pts[2 * i + 1] = new PointF((float)(a2 * Math.Cos(angle2) + x0),
                        (float)(b2 * Math.Sin(angle2) + y0));
                }
            }
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.FillPolygon(BackgroundBrush, _pts);
            e.Graphics.DrawPolygon(ForegroundPen, _pts);
        }
    }
}
