namespace Management_of_a_veterinary_clinic
{
    partial class startM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(startM));
            this.welcome = new System.Windows.Forms.Label();
            this.enterbt = new System.Windows.Forms.Button();
            this.Manegerbt = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.כניסהלמערכתToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.יציאהToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.מנהלמערכתToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.יציאהToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.closebt = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // welcome
            // 
            this.welcome.AutoSize = true;
            this.welcome.BackColor = System.Drawing.Color.Transparent;
            this.welcome.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.welcome.Location = new System.Drawing.Point(338, 31);
            this.welcome.Name = "welcome";
            this.welcome.Size = new System.Drawing.Size(118, 22);
            this.welcome.TabIndex = 0;
            this.welcome.Text = "ברוכים הבאים";
            // 
            // enterbt
            // 
            this.enterbt.BackColor = System.Drawing.Color.LightGreen;
            this.enterbt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.enterbt.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.enterbt.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.enterbt.Location = new System.Drawing.Point(253, 341);
            this.enterbt.Name = "enterbt";
            this.enterbt.Size = new System.Drawing.Size(284, 98);
            this.enterbt.TabIndex = 1;
            this.enterbt.Text = "כניסת למערכת";
            this.enterbt.UseVisualStyleBackColor = false;
            this.enterbt.Click += new System.EventHandler(this.enterbt_Click);
            // 
            // Manegerbt
            // 
            this.Manegerbt.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Manegerbt.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.Manegerbt.Location = new System.Drawing.Point(6, 390);
            this.Manegerbt.Name = "Manegerbt";
            this.Manegerbt.Size = new System.Drawing.Size(81, 50);
            this.Manegerbt.TabIndex = 2;
            this.Manegerbt.Text = "מנהל מערכת";
            this.Manegerbt.UseVisualStyleBackColor = true;
            this.Manegerbt.Click += new System.EventHandler(this.Manegerbt_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.DarkKhaki;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.כניסהלמערכתToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(791, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // כניסהלמערכתToolStripMenuItem
            // 
            this.כניסהלמערכתToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.יציאהToolStripMenuItem,
            this.מנהלמערכתToolStripMenuItem,
            this.יציאהToolStripMenuItem1});
            this.כניסהלמערכתToolStripMenuItem.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.כניסהלמערכתToolStripMenuItem.Name = "כניסהלמערכתToolStripMenuItem";
            this.כניסהלמערכתToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.כניסהלמערכתToolStripMenuItem.Text = "אפשרויות";
            // 
            // יציאהToolStripMenuItem
            // 
            this.יציאהToolStripMenuItem.Name = "יציאהToolStripMenuItem";
            this.יציאהToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.יציאהToolStripMenuItem.Text = "כניסה למערכת";
            this.יציאהToolStripMenuItem.Click += new System.EventHandler(this.יציאהToolStripMenuItem_Click);
            // 
            // מנהלמערכתToolStripMenuItem
            // 
            this.מנהלמערכתToolStripMenuItem.Name = "מנהלמערכתToolStripMenuItem";
            this.מנהלמערכתToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.מנהלמערכתToolStripMenuItem.Text = "מנהל מערכת";
            this.מנהלמערכתToolStripMenuItem.Click += new System.EventHandler(this.מנהלמערכתToolStripMenuItem_Click);
            // 
            // יציאהToolStripMenuItem1
            // 
            this.יציאהToolStripMenuItem1.Name = "יציאהToolStripMenuItem1";
            this.יציאהToolStripMenuItem1.Size = new System.Drawing.Size(143, 22);
            this.יציאהToolStripMenuItem1.Text = "יציאה";
            this.יציאהToolStripMenuItem1.Click += new System.EventHandler(this.יציאהToolStripMenuItem1_Click);
            // 
            // closebt
            // 
            this.closebt.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closebt.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.closebt.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.closebt.Location = new System.Drawing.Point(97, 390);
            this.closebt.Name = "closebt";
            this.closebt.Size = new System.Drawing.Size(87, 50);
            this.closebt.TabIndex = 4;
            this.closebt.Text = "יציאה";
            this.closebt.UseVisualStyleBackColor = true;
            this.closebt.Click += new System.EventHandler(this.closebt_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(57, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(358, 22);
            this.label1.TabIndex = 6;
            this.label1.Text = "תוכנת בארני  (מערכת ניהול מרפאה וטרינרית)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.Location = new System.Drawing.Point(691, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "גרסת תוכנה 4.0";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::Management_of_a_veterinary_clinic.Properties.Resources.Animals_Icon;
            this.pictureBox1.Location = new System.Drawing.Point(664, 41);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(117, 99);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // startM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.Color.Ivory;
            this.BackgroundImage = global::Management_of_a_veterinary_clinic.Properties.Resources.e04w_ywe8_180426;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(791, 444);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.closebt);
            this.Controls.Add(this.Manegerbt);
            this.Controls.Add(this.enterbt);
            this.Controls.Add(this.welcome);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "startM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ברוכים הבאים לבארני";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label welcome;
        private System.Windows.Forms.Button enterbt;
        private System.Windows.Forms.Button Manegerbt;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem כניסהלמערכתToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem יציאהToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem מנהלמערכתToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem יציאהToolStripMenuItem1;
        private System.Windows.Forms.Button closebt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

