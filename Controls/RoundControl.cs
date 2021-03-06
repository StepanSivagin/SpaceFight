﻿using System;
using System.Drawing;
using System.Windows.Forms;
using MathUtils = SF.Space.MathUtils;
using MouseEventType = SF.Space.MouseEventType;

namespace SF.Controls
{

    public class RoundControl : UserControl
    {
	    public bool ReadOnly { get; set; }
        public StringFormat CenteredLayout;
        public StringFormat VerticalLayout;

        public RoundControl()
        {
            DoubleBuffered = true;
            Size = new Size(200, 200);
            CenteredLayout = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
                Trimming = StringTrimming.None,
            };
            VerticalLayout = new StringFormat
            {
                FormatFlags = StringFormatFlags.DirectionVertical,
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
                Trimming = StringTrimming.None,
            };
        }

        protected int m_size
        {
            get { return Math.Min(ClientRectangle.Width, ClientRectangle.Height) - 1; }
        }

        protected Point m_center
        {
            get
            {
                return new Point
                {
                    X = ClientRectangle.Top + ClientRectangle.Width / 2,
                    Y = ClientRectangle.Y + ClientRectangle.Height / 2
                };
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (ReadOnly || e.Button != System.Windows.Forms.MouseButtons.Left)
                return;
            MouseHit(e.Location, MouseEventType.MouseDown);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (ReadOnly || e.Button != System.Windows.Forms.MouseButtons.Left)
                return;
            MouseHit(e.Location, MouseEventType.MouseUp);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (ReadOnly || e.Button != System.Windows.Forms.MouseButtons.Left)
                return;
            MouseHit(e.Location, MouseEventType.MouseMove);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            DrawBackgroound(e.Graphics);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawContents(e.Graphics);
        }

        protected static Point GetXY(Point center, int radius, double angle)
        {
            return new Point
            {
                X = (int)(center.X + Math.Sin(angle) * radius),
                Y = (int)(center.Y - Math.Cos(angle) * radius)
            };
        }

        private void MouseHit(Point point, MouseEventType t)
        {
            int x = point.X - m_center.X;
            int y = point.Y - m_center.Y;
            if (x == 0 && y == 0)
                return;
            var alpha = Math.Atan2(x, -y);
            MouseHit(point, alpha, t);
        }

        protected virtual void MouseHit(Point point, double alpha, MouseEventType t)
        {
        }

        protected virtual void DrawContents(Graphics g)
        {
        }

        protected virtual void DrawBackgroound(Graphics g)
        {
        }
    }
}
