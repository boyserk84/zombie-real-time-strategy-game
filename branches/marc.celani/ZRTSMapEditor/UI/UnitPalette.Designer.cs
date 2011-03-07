using System.Windows.Forms;
namespace ZRTSMapEditor
{
    partial class UnitPalette : UserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnitPalette));
            this.previewBox = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.unitName1 = new System.Windows.Forms.PictureBox();
            this.unitName2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.unitImageList = new System.Windows.Forms.ImageList(this.components);
            this.uiPlayerList = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.unitName1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unitName2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // previewBox
            // 
            this.previewBox.Location = new System.Drawing.Point(40, 3);
            this.previewBox.Name = "previewBox";
            this.previewBox.Size = new System.Drawing.Size(50, 50);
            this.previewBox.TabIndex = 0;
            this.previewBox.TabStop = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.unitName1);
            this.flowLayoutPanel1.Controls.Add(this.unitName2);
            this.flowLayoutPanel1.Controls.Add(this.pictureBox1);
            this.flowLayoutPanel1.Controls.Add(this.pictureBox2);
            this.flowLayoutPanel1.Controls.Add(this.pictureBox3);
            this.flowLayoutPanel1.Controls.Add(this.pictureBox4);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(5, 90);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(120, 200);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // unitName1
            // 
            this.unitName1.Location = new System.Drawing.Point(5, 3);
            this.unitName1.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.unitName1.Name = "unitName1";
            this.unitName1.Size = new System.Drawing.Size(50, 50);
            this.unitName1.TabIndex = 0;
            this.unitName1.TabStop = false;
            // 
            // unitName2
            // 
            this.unitName2.Location = new System.Drawing.Point(65, 3);
            this.unitName2.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.unitName2.Name = "unitName2";
            this.unitName2.Size = new System.Drawing.Size(50, 50);
            this.unitName2.TabIndex = 1;
            this.unitName2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(5, 59);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 50);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(65, 59);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(50, 50);
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(5, 115);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(50, 50);
            this.pictureBox3.TabIndex = 4;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Location = new System.Drawing.Point(65, 115);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(50, 50);
            this.pictureBox4.TabIndex = 5;
            this.pictureBox4.TabStop = false;
            // 
            // unitImageList
            // 
            this.unitImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("unitImageList.ImageStream")));
            this.unitImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.unitImageList.Images.SetKeyName(0, "tile1.png");
            this.unitImageList.Images.SetKeyName(1, "tile2.png");
            this.unitImageList.Images.SetKeyName(2, "tile3.png");
            // 
            // uiPlayerList
            // 
            this.uiPlayerList.FormattingEnabled = true;
            this.uiPlayerList.Location = new System.Drawing.Point(3, 60);
            this.uiPlayerList.Name = "uiPlayerList";
            this.uiPlayerList.Size = new System.Drawing.Size(124, 21);
            this.uiPlayerList.TabIndex = 2;
            this.uiPlayerList.SelectedIndexChanged += new System.EventHandler(this.uiPlayerList_SelectedIndexChanged);
            // 
            // UnitPalette
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.uiPlayerList);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.previewBox);
            this.Name = "UnitPalette";
            this.Size = new System.Drawing.Size(130, 300);
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.unitName1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unitName2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox previewBox;
        private FlowLayoutPanel flowLayoutPanel1;
        private PictureBox unitName1;
        private PictureBox unitName2;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private PictureBox pictureBox4;
        private ImageList unitImageList;
        private ComboBox uiPlayerList;
    }
}
