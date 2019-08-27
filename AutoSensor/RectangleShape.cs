/* ==================================
 * Author    :Coder.Yan
 * CreateTime:2012/9/5 22:40:12
 * Copyright :CY©2012
 * ==================================*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace CYControls
{
    [ToolboxBitmap(typeof(RectangleShape), "Resources.RectangleShape.png")]
    public class RectangleShape : CYBaseControl
    {
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.FillRectangle(BackgroundBrush, this.ClientRectangle);
            e.Graphics.DrawRectangle(ForegroundPen, Rectangle.Round(this.DrawRect));
        }
    }
}
