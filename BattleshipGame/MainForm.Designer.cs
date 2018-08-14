namespace BattleshipGame
{
    partial class MainForm
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
            this.PlayerFieldPictureBox = new System.Windows.Forms.PictureBox();
            this.AIFieldPictureBox = new System.Windows.Forms.PictureBox();
            this.ShipLabel4 = new System.Windows.Forms.Label();
            this.ShipLabel3 = new System.Windows.Forms.Label();
            this.ShipLabel2 = new System.Windows.Forms.Label();
            this.ShipLabel1 = new System.Windows.Forms.Label();
            this.RandomButton = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.StatusLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PlayerFieldPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AIFieldPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // PlayerFieldPictureBox
            // 
            this.PlayerFieldPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PlayerFieldPictureBox.Location = new System.Drawing.Point(12, 32);
            this.PlayerFieldPictureBox.Name = "PlayerFieldPictureBox";
            this.PlayerFieldPictureBox.Size = new System.Drawing.Size(400, 400);
            this.PlayerFieldPictureBox.TabIndex = 0;
            this.PlayerFieldPictureBox.TabStop = false;
            this.PlayerFieldPictureBox.Click += new System.EventHandler(this.PlayerFieldPictureBox_Click);
            // 
            // AIFieldPictureBox
            // 
            this.AIFieldPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AIFieldPictureBox.Location = new System.Drawing.Point(436, 32);
            this.AIFieldPictureBox.Name = "AIFieldPictureBox";
            this.AIFieldPictureBox.Size = new System.Drawing.Size(400, 400);
            this.AIFieldPictureBox.TabIndex = 1;
            this.AIFieldPictureBox.TabStop = false;
            this.AIFieldPictureBox.Click += new System.EventHandler(this.AIFieldPictureBox_Click);
            // 
            // ShipLabel4
            // 
            this.ShipLabel4.AutoSize = true;
            this.ShipLabel4.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ShipLabel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ShipLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ShipLabel4.Location = new System.Drawing.Point(12, 435);
            this.ShipLabel4.Name = "ShipLabel4";
            this.ShipLabel4.Size = new System.Drawing.Size(237, 26);
            this.ShipLabel4.TabIndex = 2;
            this.ShipLabel4.Text = "4 - палубный, осталось: 1";
            this.ShipLabel4.Click += new System.EventHandler(this.shipLabel4_Click);
            // 
            // ShipLabel3
            // 
            this.ShipLabel3.AutoSize = true;
            this.ShipLabel3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ShipLabel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ShipLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ShipLabel3.Location = new System.Drawing.Point(12, 468);
            this.ShipLabel3.Name = "ShipLabel3";
            this.ShipLabel3.Size = new System.Drawing.Size(237, 26);
            this.ShipLabel3.TabIndex = 3;
            this.ShipLabel3.Text = "3 - палубный, осталось: 2";
            this.ShipLabel3.Click += new System.EventHandler(this.ShipLabel3_Click);
            // 
            // ShipLabel2
            // 
            this.ShipLabel2.AutoSize = true;
            this.ShipLabel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ShipLabel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ShipLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ShipLabel2.Location = new System.Drawing.Point(12, 500);
            this.ShipLabel2.Name = "ShipLabel2";
            this.ShipLabel2.Size = new System.Drawing.Size(237, 26);
            this.ShipLabel2.TabIndex = 4;
            this.ShipLabel2.Text = "2 - палубный, осталось: 3";
            this.ShipLabel2.Click += new System.EventHandler(this.ShipLabel2_Click);
            // 
            // ShipLabel1
            // 
            this.ShipLabel1.AutoSize = true;
            this.ShipLabel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ShipLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ShipLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ShipLabel1.Location = new System.Drawing.Point(12, 536);
            this.ShipLabel1.Name = "ShipLabel1";
            this.ShipLabel1.Size = new System.Drawing.Size(237, 26);
            this.ShipLabel1.TabIndex = 5;
            this.ShipLabel1.Text = "1 - палубный, осталось: 4";
            this.ShipLabel1.Click += new System.EventHandler(this.ShipLabel1_Click);
            // 
            // RandomButton
            // 
            this.RandomButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RandomButton.Location = new System.Drawing.Point(284, 435);
            this.RandomButton.Name = "RandomButton";
            this.RandomButton.Size = new System.Drawing.Size(128, 59);
            this.RandomButton.TabIndex = 6;
            this.RandomButton.Text = "установить корабли случайно";
            this.RandomButton.UseVisualStyleBackColor = true;
            this.RandomButton.Click += new System.EventHandler(this.RandomButton_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ClearButton.Location = new System.Drawing.Point(284, 500);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(128, 62);
            this.ClearButton.TabIndex = 7;
            this.ClearButton.Text = "сброс кораблей";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.StatusLabel.ForeColor = System.Drawing.Color.Tomato;
            this.StatusLabel.Location = new System.Drawing.Point(362, 4);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(127, 25);
            this.StatusLabel.TabIndex = 8;
            this.StatusLabel.Text = "Подготовка";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::BattleshipGame.Properties.Resources.Sailing_Sea_Ships_524343_2880x1800;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(848, 620);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.RandomButton);
            this.Controls.Add(this.ShipLabel1);
            this.Controls.Add(this.ShipLabel2);
            this.Controls.Add(this.ShipLabel3);
            this.Controls.Add(this.ShipLabel4);
            this.Controls.Add(this.AIFieldPictureBox);
            this.Controls.Add(this.PlayerFieldPictureBox);
            this.Name = "MainForm";
            this.Text = "Морской Бой";
            ((System.ComponentModel.ISupportInitialize)(this.PlayerFieldPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AIFieldPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PlayerFieldPictureBox;
        private System.Windows.Forms.PictureBox AIFieldPictureBox;
        private System.Windows.Forms.Label ShipLabel4;
        private System.Windows.Forms.Label ShipLabel3;
        private System.Windows.Forms.Label ShipLabel2;
        private System.Windows.Forms.Label ShipLabel1;
        private System.Windows.Forms.Button RandomButton;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Label StatusLabel;
    }
}

