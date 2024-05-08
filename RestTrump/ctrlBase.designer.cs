namespace RestTrump
{
    partial class ctrlBase
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.lblTitulo = new DevComponents.DotNetBar.LabelX();
            this.icoDialog = new DevComponents.DotNetBar.ButtonX();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.lblTitulo);
            this.panelEx1.Controls.Add(this.icoDialog);
            this.panelEx1.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(30, 230);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.Color = System.Drawing.Color.DarkSlateBlue;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 13;
            // 
            // lblTitulo
            // 
            this.lblTitulo.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblTitulo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblTitulo.BackgroundStyle.PaddingBottom = 10;
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.Gold;
            this.lblTitulo.Location = new System.Drawing.Point(0, 22);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(30, 208);
            this.lblTitulo.TabIndex = 8;
            this.lblTitulo.Text = "Titulo";
            this.lblTitulo.TextOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.lblTitulo.VerticalTextTopUp = false;
            // 
            // icoDialog
            // 
            this.icoDialog.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.icoDialog.BackColor = System.Drawing.Color.Transparent;
            this.icoDialog.ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
            this.icoDialog.CustomColorName = "msjTableColor";
            this.icoDialog.Dock = System.Windows.Forms.DockStyle.Top;
            this.icoDialog.Location = new System.Drawing.Point(0, 0);
            this.icoDialog.Margin = new System.Windows.Forms.Padding(0);
            this.icoDialog.MaximumSize = new System.Drawing.Size(27, 27);
            this.icoDialog.MinimumSize = new System.Drawing.Size(10, 10);
            this.icoDialog.Name = "icoDialog";
            this.icoDialog.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(2);
            this.icoDialog.Size = new System.Drawing.Size(27, 22);
            this.icoDialog.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.icoDialog.Symbol = "";
            this.icoDialog.SymbolColor = System.Drawing.Color.Red;
            this.icoDialog.SymbolSize = 12F;
            this.icoDialog.TabIndex = 9;
            this.icoDialog.Click += new System.EventHandler(this.icoMsj_Click);
            // 
            // ctrlBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.panelEx1);
            this.Name = "ctrlBase";
            this.Size = new System.Drawing.Size(185, 230);
            this.Load += new System.EventHandler(this.ctrlBase_Load);
            this.VisibleChanged += new System.EventHandler(this.ctrlBase_VisibleChanged);
            this.panelEx1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.LabelX lblTitulo;
        private DevComponents.DotNetBar.ButtonX icoDialog;
    }
}
