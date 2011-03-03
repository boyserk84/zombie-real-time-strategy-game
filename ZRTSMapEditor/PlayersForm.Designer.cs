namespace ZRTSMapEditor
{
    partial class PlayersForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.uiPlayerList = new System.Windows.Forms.DataGridView();
            this.addPlayerButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.race = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.gold = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wood = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.metal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.uiPlayerList)).BeginInit();
            this.SuspendLayout();
            // 
            // uiPlayerList
            // 
            this.uiPlayerList.AllowUserToAddRows = false;
            this.uiPlayerList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.uiPlayerList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name,
            this.race,
            this.gold,
            this.wood,
            this.metal});
            this.uiPlayerList.Location = new System.Drawing.Point(12, 27);
            this.uiPlayerList.Name = "uiPlayerList";
            this.uiPlayerList.Size = new System.Drawing.Size(514, 150);
            this.uiPlayerList.TabIndex = 0;
            // 
            // addPlayerButton
            // 
            this.addPlayerButton.Location = new System.Drawing.Point(35, 183);
            this.addPlayerButton.Name = "addPlayerButton";
            this.addPlayerButton.Size = new System.Drawing.Size(118, 46);
            this.addPlayerButton.TabIndex = 1;
            this.addPlayerButton.Text = "Add Player";
            this.addPlayerButton.UseVisualStyleBackColor = true;
            this.addPlayerButton.Click += new System.EventHandler(this.addPlayerButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(211, 183);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(141, 46);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(417, 183);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(121, 46);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // name
            // 
            this.name.HeaderText = "Player Name";
            this.name.Name = "name";
            this.name.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // race
            // 
            this.race.HeaderText = "Race";
            this.race.Items.AddRange(new object[] {
            "Human",
            "Zombie"});
            this.race.Name = "race";
            this.race.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.race.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // gold
            // 
            this.gold.HeaderText = "Starting Gold";
            this.gold.Name = "gold";
            this.gold.Width = 90;
            // 
            // wood
            // 
            this.wood.HeaderText = "StartingWood";
            this.wood.Name = "wood";
            this.wood.Width = 90;
            // 
            // metal
            // 
            this.metal.HeaderText = "Starting Metal";
            this.metal.Name = "metal";
            this.metal.Width = 90;
            // 
            // PlayersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 262);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.addPlayerButton);
            this.Controls.Add(this.uiPlayerList);
            this.Name = "PlayersForm";
            this.Text = "Players";
            ((System.ComponentModel.ISupportInitialize)(this.uiPlayerList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView uiPlayerList;
        private System.Windows.Forms.Button addPlayerButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewComboBoxColumn race;
        private System.Windows.Forms.DataGridViewTextBoxColumn gold;
        private System.Windows.Forms.DataGridViewTextBoxColumn wood;
        private System.Windows.Forms.DataGridViewTextBoxColumn metal;
    }
}