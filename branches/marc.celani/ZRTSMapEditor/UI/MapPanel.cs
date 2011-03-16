using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace ZRTSMapEditor
{
    public partial class MapPanel : Panel
    {
        int pixelsPerMapCoordinate;
        List<Control> extraControls = new List<Control>();

        public MapPanel()
        {
            InitializeComponent();
            SetRatioOfGameCoordToPixels(20);
        }

        public void SetRatioOfGameCoordToPixels(int pixels)
        {
            pixelsPerMapCoordinate = pixels;
        }

        public void AddControlAtMapCoordinate(Control control, float x, float y)
        {
            if (x <= 40 && y <= 40)
            {
                this.Controls.Add(control);
            }
            else
                extraControls.Add(control);
            this.Controls.SetChildIndex(control, 0);
            control.Location = new Point((int)(x * (float)pixelsPerMapCoordinate), (int)(y * (float)pixelsPerMapCoordinate));
        }

        public void SetControlLocationAtMapCoordinate(Control control, float x, float y)
        {
            if (Controls.Contains(control))
            {
                this.Controls.SetChildIndex(control, 0);
                control.Location = new Point((int)(x * (float)pixelsPerMapCoordinate), (int)(y * (float)pixelsPerMapCoordinate));
            }
        }

        public void Empty()
        {
            extraControls.Clear();
        }

    }
}
