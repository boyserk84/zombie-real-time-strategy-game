using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZRTSModel;
using ZRTSModel.GameWorld;
using ZRTSModel.EventHandlers;

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

        }
        protected override void OnClick(EventArgs e)
        {
            controller.OnClickMapCell(cell);
            base.OnClick(e);
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
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
