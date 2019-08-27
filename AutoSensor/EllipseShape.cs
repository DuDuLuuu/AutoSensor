/* ==================================
 * Author    :Coder.Yan
 * CreateTime:2012/8/30 22:10:12
 * Copyright :CY©2012
 * ==================================*/
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;

namespace CYControls
{
    [ToolboxBitmap(typeof(EllipseShape),"Resources.EllipseShape.png")]
    public class EllipseShape:CYBaseControl
    {
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.FillEllipse(BackgroundBrush,this.ClientRectangle);
            e.Graphics.DrawEllipse(ForegroundPen, this.DrawRect);
        }
    }
}
