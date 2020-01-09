namespace Chesstube.Win64
{
    partial class Form1
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
            this.menuTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.scoreSheet1 = new System.Windows.Forms.RichTextBox();
            this.chessBoard1 = new Chesstube.Win64.ChessBoard();
            this.menuTabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuTabControl
            // 
            this.menuTabControl.Controls.Add(this.tabPage1);
            this.menuTabControl.Controls.Add(this.tabPage2);
            this.menuTabControl.Controls.Add(this.tabPage3);
            this.menuTabControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.menuTabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.menuTabControl.Location = new System.Drawing.Point(0, 0);
            this.menuTabControl.Name = "menuTabControl";
            this.menuTabControl.SelectedIndex = 0;
            this.menuTabControl.Size = new System.Drawing.Size(869, 132);
            this.menuTabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(861, 99);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Game";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Image = global::Chesstube.Win64.Properties.Resources.checker_02;
            this.button1.Location = new System.Drawing.Point(6, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 77);
            this.button1.TabIndex = 0;
            this.button1.Text = "New Game";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(861, 99);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Setup";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(861, 99);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Training";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // scoreSheet1
            // 
            this.scoreSheet1.Cursor = System.Windows.Forms.Cursors.Cross;
            this.scoreSheet1.Dock = System.Windows.Forms.DockStyle.Right;
            this.scoreSheet1.Enabled = false;
            this.scoreSheet1.Location = new System.Drawing.Point(342, 132);
            this.scoreSheet1.Name = "scoreSheet1";
            this.scoreSheet1.Size = new System.Drawing.Size(527, 333);
            this.scoreSheet1.TabIndex = 2;
            this.scoreSheet1.Text = "";
            this.scoreSheet1.SizeChanged += new System.EventHandler(this.scoreSheet1_SizeChanged);
            this.scoreSheet1.TextChanged += new System.EventHandler(this.scoreSheet1_TextChanged);
            // 
            // chessBoard1
            // 
            this.chessBoard1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.chessBoard1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chessBoard1.Location = new System.Drawing.Point(0, 132);
            this.chessBoard1.Name = "chessBoard1";
            this.chessBoard1.Size = new System.Drawing.Size(869, 333);
            this.chessBoard1.TabIndex = 1;
            this.chessBoard1.MoveMade += new System.EventHandler(this.chessBoard1_MoveMade);
            this.chessBoard1.Load += new System.EventHandler(this.chessBoard1_Load);
            this.chessBoard1.SizeChanged += new System.EventHandler(this.chessBoard1_SizeChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 465);
            this.Controls.Add(this.scoreSheet1);
            this.Controls.Add(this.chessBoard1);
            this.Controls.Add(this.menuTabControl);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Opacity = 0.98D;
            this.Text = "Chesstube";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuTabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl menuTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private ChessBoard chessBoard1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox scoreSheet1;
    }
}

