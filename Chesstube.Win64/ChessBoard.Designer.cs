namespace Chesstube.Win64
{
    partial class ChessBoard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChessBoard));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyPositionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pastePositionToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.copyGameToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteGameToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savePositionToFENToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadPGNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFENToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chessImg = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyPositionToolStripMenuItem,
            this.pasteGameToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(211, 80);
            // 
            // copyPositionToolStripMenuItem
            // 
            this.copyPositionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.pastePositionToolStripMenuItem1,
            this.copyGameToolStripMenuItem1,
            this.pasteGameToolStripMenuItem1});
            this.copyPositionToolStripMenuItem.Name = "copyPositionToolStripMenuItem";
            this.copyPositionToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.copyPositionToolStripMenuItem.Text = "Clipboard";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.copyToolStripMenuItem.Text = "Copy Position";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pastePositionToolStripMenuItem1
            // 
            this.pastePositionToolStripMenuItem1.Name = "pastePositionToolStripMenuItem1";
            this.pastePositionToolStripMenuItem1.Size = new System.Drawing.Size(216, 26);
            this.pastePositionToolStripMenuItem1.Text = "Paste Position";
            this.pastePositionToolStripMenuItem1.Click += new System.EventHandler(this.pastePositionToolStripMenuItem1_Click);
            // 
            // copyGameToolStripMenuItem1
            // 
            this.copyGameToolStripMenuItem1.Name = "copyGameToolStripMenuItem1";
            this.copyGameToolStripMenuItem1.Size = new System.Drawing.Size(216, 26);
            this.copyGameToolStripMenuItem1.Text = "Copy Game";
            // 
            // pasteGameToolStripMenuItem1
            // 
            this.pasteGameToolStripMenuItem1.Name = "pasteGameToolStripMenuItem1";
            this.pasteGameToolStripMenuItem1.Size = new System.Drawing.Size(216, 26);
            this.pasteGameToolStripMenuItem1.Text = "Paste Game";
            // 
            // pasteGameToolStripMenuItem
            // 
            this.pasteGameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.savePositionToFENToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.loadPGNToolStripMenuItem,
            this.loadFENToolStripMenuItem});
            this.pasteGameToolStripMenuItem.Name = "pasteGameToolStripMenuItem";
            this.pasteGameToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.pasteGameToolStripMenuItem.Text = "File";
            // 
            // savePositionToFENToolStripMenuItem
            // 
            this.savePositionToFENToolStripMenuItem.Name = "savePositionToFENToolStripMenuItem";
            this.savePositionToFENToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.savePositionToFENToolStripMenuItem.Text = "Save Position as FEN";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.saveToolStripMenuItem.Text = "Save  Game as PGN";
            // 
            // loadPGNToolStripMenuItem
            // 
            this.loadPGNToolStripMenuItem.Name = "loadPGNToolStripMenuItem";
            this.loadPGNToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.loadPGNToolStripMenuItem.Text = "Load PGN";
            // 
            // loadFENToolStripMenuItem
            // 
            this.loadFENToolStripMenuItem.Name = "loadFENToolStripMenuItem";
            this.loadFENToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.loadFENToolStripMenuItem.Text = "Load FEN";
            // 
            // chessImg
            // 
            this.chessImg.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("chessImg.ImageStream")));
            this.chessImg.TransparentColor = System.Drawing.Color.Transparent;
            this.chessImg.Images.SetKeyName(0, "Chess_bdt60.png");
            this.chessImg.Images.SetKeyName(1, "Chess_blt60.png");
            this.chessImg.Images.SetKeyName(2, "Chess_kdt60.png");
            this.chessImg.Images.SetKeyName(3, "Chess_klt60.png");
            this.chessImg.Images.SetKeyName(4, "Chess_ndt60.png");
            this.chessImg.Images.SetKeyName(5, "Chess_nlt60.png");
            this.chessImg.Images.SetKeyName(6, "Chess_pdt60.png");
            this.chessImg.Images.SetKeyName(7, "Chess_plt60.png");
            this.chessImg.Images.SetKeyName(8, "Chess_qdt60.png");
            this.chessImg.Images.SetKeyName(9, "Chess_qlt60.png");
            this.chessImg.Images.SetKeyName(10, "Chess_rdt60.png");
            this.chessImg.Images.SetKeyName(11, "Chess_rlt60.png");
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tableLayoutPanel1.ColumnCount = 8;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Chess Kingdom", 48F);
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(500, 500);
            this.tableLayoutPanel1.TabIndex = 1;
            this.tableLayoutPanel1.CellPaint += new System.Windows.Forms.TableLayoutCellPaintEventHandler(this.tableLayoutPanel1_CellPaint);
            this.tableLayoutPanel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tableLayoutPanel1_MouseClick);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ChessBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ChessBoard";
            this.Size = new System.Drawing.Size(500, 500);
            this.Load += new System.EventHandler(this.ChessBoard_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ImageList chessImg;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem copyPositionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pastePositionToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem copyGameToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem pasteGameToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem savePositionToFENToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadPGNToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadFENToolStripMenuItem;
    }
}
