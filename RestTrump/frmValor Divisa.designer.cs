namespace RestTrump
{
    partial class frmValor_Divisa
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
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.btnSalir = new DevComponents.DotNetBar.ButtonX();
            this.txtDivisa = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.LightGreen;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.ForeColor = System.Drawing.Color.Black;
            this.labelX1.Location = new System.Drawing.Point(7, 2);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(122, 58);
            this.labelX1.TabIndex = 7;
            this.labelX1.Text = "Divisa Ref. Bs.:";
            // 
            // btnSalir
            // 
            this.btnSalir.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSalir.BackColor = System.Drawing.Color.Cyan;
            this.btnSalir.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange;
            this.btnSalir.Location = new System.Drawing.Point(416, 3);
            this.btnSalir.Margin = new System.Windows.Forms.Padding(5);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(101, 55);
            this.btnSalir.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSalir.TabIndex = 8;
            this.btnSalir.Text = "APLICAR";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // txtDivisa
            // 
            // 
            // 
            // 
            this.txtDivisa.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtDivisa.FocusHighlightEnabled = true;
            this.txtDivisa.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDivisa.Location = new System.Drawing.Point(135, 4);
            this.txtDivisa.Name = "txtDivisa";
            this.txtDivisa.Size = new System.Drawing.Size(273, 53);
            this.txtDivisa.TabIndex = 9;
            this.txtDivisa.Text = "1";
            this.txtDivisa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSrchPrecio_KeyPress);
            this.txtDivisa.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSrchPrecio_KeyUp);
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.BackColor = System.Drawing.Color.Coral;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange;
            this.buttonX1.Location = new System.Drawing.Point(524, 4);
            this.buttonX1.Margin = new System.Windows.Forms.Padding(5);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(104, 55);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 8;
            this.buttonX1.Text = "CANCELAR";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSalir);
            this.panel1.Controls.Add(this.txtDivisa);
            this.panel1.Controls.Add(this.labelX1);
            this.panel1.Controls.Add(this.buttonX1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 57);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(635, 70);
            this.panel1.TabIndex = 10;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.labelX2);
            this.panel2.Controls.Add(this.buttonX2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(635, 55);
            this.panel2.TabIndex = 13;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel2_Paint);
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelX2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX2.ForeColor = System.Drawing.Color.Black;
            this.labelX2.Location = new System.Drawing.Point(0, 0);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(544, 55);
            this.labelX2.TabIndex = 7;
            this.labelX2.Text = "<div align=\"center\">Por favor indique el valor <b>Actual </b>de la divisa de cálc" +
    "ulo para los nuevos productos.</div>";
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.BackColor = System.Drawing.Color.White;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange;
            this.buttonX2.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonX2.Location = new System.Drawing.Point(544, 0);
            this.buttonX2.Margin = new System.Windows.Forms.Padding(5);
            this.buttonX2.MaximumSize = new System.Drawing.Size(91, 52);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(91, 52);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.Symbol = "";
            this.buttonX2.TabIndex = 11;
            this.buttonX2.Text = "Consultar";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click_1);
            // 
            // frmValor_Divisa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 127);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmValor_Divisa";
            this.Text = "frmValor_Divisa";
            this.Load += new System.EventHandler(this.frmValor_Divisa_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX btnSalir;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDivisa;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.ButtonX buttonX2;
    }
}