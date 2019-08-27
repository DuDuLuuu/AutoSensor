/* ==================================
 * Author    :Coder.Yan
 * CreateTime:2012/8/30 21:54:45
 * Copyright :CY©2012
 * ==================================*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace CYControls
{
    /// <summary>
    /// 提供透明和旋转功能的基础控件
    /// </summary>
    public class CYBaseControl : Control
    {
        private float _iBorderThickness = 1f;
        private float _iOpacity = 1f;
        private Brush _brushBg = null;
        private Pen _penFg = null;

        public CYBaseControl()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor |
                ControlStyles.Opaque, true);
            this.BackColor = Color.Transparent;
            BackgroundBrush = Brushes.Transparent;
            ForegroundPen = Pens.Black;
        }

        #region Propertys
        #region HideParent
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override Image BackgroundImage
        {
            get { return base.BackgroundImage; }
            set { base.BackgroundImage = value; }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override ImageLayout BackgroundImageLayout
        {
            get
            {
                return base.BackgroundImageLayout;
            }
            set
            {
                base.BackgroundImageLayout = value;
            }
        }
        #endregion

        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
                ResetBgBrush();
            }
        }

        //[EditorAttribute(typeof(BrushTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]  
        //public double Background
        //{
        //    get;
        //    set;
        //}

        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
                ResetFgPen();
            }
        }

        public float BorderThickness
        {
            get { return _iBorderThickness; }
            set
            {
                if (value < 0)
                {
                    throw new Exception("Out off range");
                }
                _iBorderThickness = value;
                ResetFgPen();
                ResetDrawRect();
            }
        }

        public virtual float RotateAngle
        {
            get;
            set;
        }


        public float Opacity
        {
            get { return _iOpacity; }
            set
            {
                if (value > 1 || value < 0)
                {
                    throw new Exception("Out of range,the Value be in [0,1]");
                }
                else
                {
                    _iOpacity = value;
                    ResetBrushes();
                }
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = 0x20;
                return cp;
            }
        }

        protected virtual Brush BackgroundBrush
        {
            get { return _brushBg; }
            set { _brushBg = value; }
        }

        protected virtual Pen ForegroundPen
        {
            get { return _penFg; }
            set { _penFg = value; }
        }

        protected virtual RectangleF DrawRect
        {
            get;
            set;
        }
        #endregion

        #region Methods
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            ResetDrawRect();
        }
        protected override void OnPaddingChanged(EventArgs e)
        {
            base.OnPaddingChanged(e);
            ResetDrawRect();
        }

        protected void ResetBrushes()
        {
            ResetBgBrush();
            ResetFgPen();
        }

        protected void ResetBgBrush()
        {
            BackgroundBrush = new SolidBrush(GetOpacityColor(BackColor, Opacity));
        }

        protected void ResetFgPen()
        {
            ForegroundPen = new Pen(GetOpacityColor(ForeColor, Opacity), BorderThickness);
        }

        protected Color GetOpacityColor(Color baseColor, float op)
        {
            return Color.FromArgb(Convert.ToInt32(op * baseColor.A), baseColor);
        }

        private void ResetDrawRect()
        {
            float dbwidth = 2 * BorderThickness;
            float halfwidth = BorderThickness / 2;
            int paddingWhith = Padding.Left + Padding.Right;
            int paddingHeight = Padding.Top + Padding.Bottom;
            if (dbwidth > Width - paddingWhith || dbwidth > Height - paddingHeight)
            {
                DrawRect = this.Bounds;
            }
            else
            {
                DrawRect = new RectangleF(Padding.Left + halfwidth,
                    Padding.Top + halfwidth,
                    Width - BorderThickness - paddingWhith,
                    Height - BorderThickness - paddingHeight);
            }
        }
        #endregion
    }
}
