namespace RestTrump
{
    partial class frmMonitorvlaView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMonitorvlaView));
            this.panelSearch = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelMoveSearch = new System.Windows.Forms.Panel();
            this.lblSrchEncontrados = new DevComponents.DotNetBar.LabelX();
            this.btnSrchPrev = new DevComponents.DotNetBar.Controls.SymbolBox();
            this.btnSrchNext = new DevComponents.DotNetBar.Controls.SymbolBox();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.lblFeed = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.panelSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelMoveSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSearch
            // 
            this.panelSearch.BackColor = System.Drawing.Color.LightGray;
            this.panelSearch.Controls.Add(this.pictureBox1);
            this.panelSearch.Controls.Add(this.panelMoveSearch);
            this.panelSearch.Controls.Add(this.buttonX2);
            this.panelSearch.Controls.Add(this.lblFeed);
            this.panelSearch.Controls.Add(this.labelX3);
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSearch.Location = new System.Drawing.Point(0, 0);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Padding = new System.Windows.Forms.Padding(7);
            this.panelSearch.Size = new System.Drawing.Size(318, 406);
            this.panelSearch.TabIndex = 4;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(7, 60);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(304, 301);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 23;
            this.pictureBox1.TabStop = false;
            // 
            // panelMoveSearch
            // 
            this.panelMoveSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMoveSearch.BackColor = System.Drawing.Color.Coral;
            this.panelMoveSearch.Controls.Add(this.lblSrchEncontrados);
            this.panelMoveSearch.Controls.Add(this.btnSrchPrev);
            this.panelMoveSearch.Controls.Add(this.btnSrchNext);
            this.panelMoveSearch.Location = new System.Drawing.Point(10, 365);
            this.panelMoveSearch.Name = "panelMoveSearch";
            this.panelMoveSearch.Padding = new System.Windows.Forms.Padding(5);
            this.panelMoveSearch.Size = new System.Drawing.Size(134, 31);
            this.panelMoveSearch.TabIndex = 15;
            // 
            // lblSrchEncontrados
            // 
            this.lblSrchEncontrados.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblSrchEncontrados.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblSrchEncontrados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSrchEncontrados.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSrchEncontrados.ForeColor = System.Drawing.Color.Black;
            this.lblSrchEncontrados.Location = new System.Drawing.Point(29, 5);
            this.lblSrchEncontrados.Name = "lblSrchEncontrados";
            this.lblSrchEncontrados.Size = new System.Drawing.Size(76, 21);
            this.lblSrchEncontrados.TabIndex = 8;
            this.lblSrchEncontrados.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // btnSrchPrev
            // 
            this.btnSrchPrev.BackColor = System.Drawing.Color.Coral;
            // 
            // 
            // 
            this.btnSrchPrev.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.btnSrchPrev.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSrchPrev.Location = new System.Drawing.Point(5, 5);
            this.btnSrchPrev.Name = "btnSrchPrev";
            this.btnSrchPrev.Size = new System.Drawing.Size(24, 21);
            this.btnSrchPrev.Symbol = "";
            this.btnSrchPrev.TabIndex = 14;
            this.btnSrchPrev.Tag = "1";
            this.btnSrchPrev.Text = "symbolBox2";
            this.btnSrchPrev.Click += new System.EventHandler(this.btnSrchPrev_Click);
            // 
            // btnSrchNext
            // 
            this.btnSrchNext.BackColor = System.Drawing.Color.Coral;
            // 
            // 
            // 
            this.btnSrchNext.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.btnSrchNext.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSrchNext.Location = new System.Drawing.Point(105, 5);
            this.btnSrchNext.Name = "btnSrchNext";
            this.btnSrchNext.Size = new System.Drawing.Size(24, 21);
            this.btnSrchNext.Symbol = "";
            this.btnSrchNext.TabIndex = 14;
            this.btnSrchNext.Tag = "2";
            this.btnSrchNext.Text = "symbolBox2";
            this.btnSrchNext.Click += new System.EventHandler(this.btnSrchNext_Click);
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonX2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange;
            this.buttonX2.Location = new System.Drawing.Point(234, 366);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(78, 32);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 10;
            this.buttonX2.Text = "Cerrar";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // lblFeed
            // 
            this.lblFeed.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblFeed.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblFeed.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFeed.ForeColor = System.Drawing.Color.Black;
            this.lblFeed.Location = new System.Drawing.Point(7, 33);
            this.lblFeed.Name = "lblFeed";
            this.lblFeed.Size = new System.Drawing.Size(304, 27);
            this.lblFeed.TabIndex = 8;
            this.lblFeed.Text = "Descripción:";
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.WhiteSmoke;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelX3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX3.Location = new System.Drawing.Point(7, 7);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(304, 26);
            this.labelX3.TabIndex = 1;
            this.labelX3.Text = "Monitor Dolar Ve";
            this.labelX3.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // frmMonitorvlaView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 406);
            this.Controls.Add(this.panelSearch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmMonitorvlaView";
            this.Text = "frmMonitorvlaView";
            this.Load += new System.EventHandler(this.frmMonitorvlaView_Load);
            this.panelSearch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelMoveSearch.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panelMoveSearch;
        private DevComponents.DotNetBar.LabelX lblSrchEncontrados;
        private DevComponents.DotNetBar.Controls.SymbolBox btnSrchPrev;
        private DevComponents.DotNetBar.Controls.SymbolBox btnSrchNext;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private DevComponents.DotNetBar.LabelX lblFeed;
        private DevComponents.DotNetBar.LabelX labelX3;
    }
}