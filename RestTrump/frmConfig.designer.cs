namespace RestTrump
{
    partial class frmConfig
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
            this.btnSalir = new DevComponents.DotNetBar.ButtonX();
            this.label1 = new System.Windows.Forms.Label();
            this.usuario = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.startwith = new System.Windows.Forms.TextBox();
            this.codigomoneda = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbredondeo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.conexionString = new System.Windows.Forms.TextBox();
            this.btnTestConexion = new DevComponents.DotNetBar.ButtonX();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.txtdirdatos = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSalir
            // 
            this.btnSalir.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSalir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalir.BackColor = System.Drawing.Color.SteelBlue;
            this.btnSalir.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange;
            this.btnSalir.Location = new System.Drawing.Point(391, 331);
            this.btnSalir.Margin = new System.Windows.Forms.Padding(5);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(100, 36);
            this.btnSalir.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSalir.TabIndex = 9;
            this.btnSalir.Text = "Guardar";
            this.btnSalir.TextColor = System.Drawing.Color.White;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 25);
            this.label1.TabIndex = 11;
            this.label1.Text = "Página:";
            // 
            // usuario
            // 
            this.usuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usuario.Location = new System.Drawing.Point(177, 14);
            this.usuario.Name = "usuario";
            this.usuario.Size = new System.Drawing.Size(313, 30);
            this.usuario.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 25);
            this.label2.TabIndex = 11;
            this.label2.Text = "Start With:";
            // 
            // startwith
            // 
            this.startwith.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startwith.Location = new System.Drawing.Point(177, 53);
            this.startwith.Name = "startwith";
            this.startwith.Size = new System.Drawing.Size(314, 30);
            this.startwith.TabIndex = 12;
            // 
            // codigomoneda
            // 
            this.codigomoneda.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codigomoneda.Location = new System.Drawing.Point(177, 92);
            this.codigomoneda.Name = "codigomoneda";
            this.codigomoneda.Size = new System.Drawing.Size(43, 30);
            this.codigomoneda.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(13, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 25);
            this.label4.TabIndex = 11;
            this.label4.Text = "Codigo Divisa:";
            // 
            // cmbredondeo
            // 
            this.cmbredondeo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbredondeo.DropDownWidth = 350;
            this.cmbredondeo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbredondeo.FormattingEnabled = true;
            this.cmbredondeo.Items.AddRange(new object[] {
            "Sin Redondeo",
            "Unidades a Cero(< 10 = 0)",
            "5 Unidades a Decimas(> 5 = 10)",
            "Unidades a Decimas(> 1 = 10)",
            "Unidades a Centesimas(> 1 = 100)",
            "Decimas a Cero(< 100 = 0)",
            "5 Decimas a Centesimas(> 50 = 100)",
            "Decimas a Centesimas(> 10 = 100)",
            "Centesimas a Cero(< 1000 = 0)",
            "5 Centesimas a Milesimas(< 500 = 1000)",
            "Centesimas a Milesimas(> 100 = 1000)"});
            this.cmbredondeo.Location = new System.Drawing.Point(177, 130);
            this.cmbredondeo.Name = "cmbredondeo";
            this.cmbredondeo.Size = new System.Drawing.Size(313, 33);
            this.cmbredondeo.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 25);
            this.label3.TabIndex = 11;
            this.label3.Text = "Redondeo:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(10, 171);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 25);
            this.label5.TabIndex = 11;
            this.label5.Text = "Conexión:";
            // 
            // conexionString
            // 
            this.conexionString.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.conexionString.Location = new System.Drawing.Point(18, 252);
            this.conexionString.Multiline = true;
            this.conexionString.Name = "conexionString";
            this.conexionString.Size = new System.Drawing.Size(472, 71);
            this.conexionString.TabIndex = 12;
            // 
            // btnTestConexion
            // 
            this.btnTestConexion.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnTestConexion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTestConexion.BackColor = System.Drawing.Color.LightGray;
            this.btnTestConexion.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange;
            this.btnTestConexion.Location = new System.Drawing.Point(177, 171);
            this.btnTestConexion.Margin = new System.Windows.Forms.Padding(5);
            this.btnTestConexion.Name = "btnTestConexion";
            this.btnTestConexion.Size = new System.Drawing.Size(145, 29);
            this.btnTestConexion.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnTestConexion.TabIndex = 9;
            this.btnTestConexion.Text = "Configurar Base Datos";
            this.btnTestConexion.Click += new System.EventHandler(this.btnTestConexion_Click);
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonX2.BackColor = System.Drawing.Color.LightGray;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange;
            this.buttonX2.Location = new System.Drawing.Point(118, 130);
            this.buttonX2.Margin = new System.Windows.Forms.Padding(5);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(53, 33);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 9;
            this.buttonX2.Text = "Test";
            this.buttonX2.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // txtdirdatos
            // 
            this.txtdirdatos.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtdirdatos.Location = new System.Drawing.Point(177, 209);
            this.txtdirdatos.Name = "txtdirdatos";
            this.txtdirdatos.Size = new System.Drawing.Size(313, 30);
            this.txtdirdatos.TabIndex = 17;
            this.txtdirdatos.Text = "c:\\retail_shop\\data";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(13, 212);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 25);
            this.label6.TabIndex = 16;
            this.label6.Text = "Dir Datos:";
            // 
            // frmConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 372);
            this.Controls.Add(this.txtdirdatos);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbredondeo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.codigomoneda);
            this.Controls.Add(this.startwith);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.conexionString);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.usuario);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonX2);
            this.Controls.Add(this.btnTestConexion);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmConfig";
            this.Text = "Configuraciónn";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnSalir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox usuario;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox startwith;
        private System.Windows.Forms.TextBox codigomoneda;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbredondeo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox conexionString;
        private DevComponents.DotNetBar.ButtonX btnTestConexion;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private System.Windows.Forms.TextBox txtdirdatos;
        private System.Windows.Forms.Label label6;
    }
}