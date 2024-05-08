namespace RestTrump
{
    partial class frmSerial
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPosiciones = new System.Windows.Forms.Label();
            this.lblSerialHDD = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblSerialAnterior = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(558, 7);
            this.button1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(42, 42);
            this.button1.TabIndex = 0;
            this.button1.Text = "x";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(463, 111);
            this.button2.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(137, 42);
            this.button2.TabIndex = 1;
            this.button2.Text = "unlock";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 67);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(593, 38);
            this.label1.TabIndex = 2;
            this.label1.Text = "000-000-000-000";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(84, 111);
            this.textBox1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.textBox1.MaxLength = 20;
            this.textBox1.MinimumSize = new System.Drawing.Size(0, 42);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(369, 37);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = "000";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 120);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "Codigo";
            // 
            // lblPosiciones
            // 
            this.lblPosiciones.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPosiciones.AutoSize = true;
            this.lblPosiciones.ForeColor = System.Drawing.Color.LightGray;
            this.lblPosiciones.Location = new System.Drawing.Point(550, 159);
            this.lblPosiciones.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblPosiciones.Name = "lblPosiciones";
            this.lblPosiciones.Size = new System.Drawing.Size(50, 24);
            this.lblPosiciones.TabIndex = 4;
            this.lblPosiciones.Text = "1913";
            // 
            // lblSerialHDD
            // 
            this.lblSerialHDD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSerialHDD.AutoSize = true;
            this.lblSerialHDD.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerialHDD.ForeColor = System.Drawing.Color.Blue;
            this.lblSerialHDD.Location = new System.Drawing.Point(7, 159);
            this.lblSerialHDD.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblSerialHDD.Name = "lblSerialHDD";
            this.lblSerialHDD.Size = new System.Drawing.Size(90, 24);
            this.lblSerialHDD.TabIndex = 4;
            this.lblSerialHDD.Text = "00000000";
            this.lblSerialHDD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.SlateBlue;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(7, 7);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(541, 38);
            this.label3.TabIndex = 2;
            this.label3.Text = "Licenciamiento Kalixto Systems Solutions";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSerialAnterior
            // 
            this.lblSerialAnterior.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSerialAnterior.AutoSize = true;
            this.lblSerialAnterior.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerialAnterior.ForeColor = System.Drawing.Color.Red;
            this.lblSerialAnterior.Location = new System.Drawing.Point(311, 159);
            this.lblSerialAnterior.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblSerialAnterior.Name = "lblSerialAnterior";
            this.lblSerialAnterior.Size = new System.Drawing.Size(90, 24);
            this.lblSerialAnterior.TabIndex = 4;
            this.lblSerialAnterior.Text = "00000000";
            this.lblSerialAnterior.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmSerial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 192);
            this.Controls.Add(this.lblSerialAnterior);
            this.Controls.Add(this.lblSerialHDD);
            this.Controls.Add(this.lblPosiciones);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "frmSerial";
            this.Text = "frmSerial";
            this.Load += new System.EventHandler(this.frmSerial_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPosiciones;
        private System.Windows.Forms.Label lblSerialHDD;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblSerialAnterior;
    }
}