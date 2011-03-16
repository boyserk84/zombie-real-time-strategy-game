using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZRTSModel;
using ZRTSModel.GameWorld;
using ZRTSModel.EventHandlers;
using System.Diagnostics;

namespace ZRTSMapEditor
{
    public class TileUI : PictureBox
    {
        private MapEditorController controller;
        private CellComponent cell;

        public CellComponent Cell
        {
            get { return cell; }
            set { cell = value; }
        }

        public TileUI(MapEditorController controller, CellComponent observable)
        {
            InitializeComponent();

            this.controller = controller;
            this.cell = observable;

            // Initialize Image
            if (observable != null)
            {
                // Register for TileChange event.
                observable.TileChangedEvent += this.ChangeTile;

                TileFactory tf = TileFactory.Instance;
                this.Image = tf.getBitmapImproved(observable.GetTile());
            }
            ((Control)this).AllowDrop = true;

        }

        private void ChangeTile(Object sender, TileChangedEventArgs args)
        {
            TileFactory tf = TileFactory.Instance;
            this.Image = tf.getBitmapImproved(args.Tile);
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // TileUI
            // 
            this.Margin = new System.Windows.Forms.Padding(0);
            this.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.TileUI_DragEnter);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TileUI_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        private void TileUI_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.DoDragDrop("paint", DragDropEffects.None);
                controller.OnClickMapCell(cell);
                base.OnClick(e);
            }
        }

        private void TileUI_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(String)))
                return;

            if (e.Data.GetData(typeof(String)).Equals("paint"))
            {
                e.Effect = DragDropEffects.None;
                controller.OnClickMapCell(cell);
                base.OnClick(e);
            }
        }


    }
}
