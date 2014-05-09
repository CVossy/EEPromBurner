namespace EEPROM_burner_vossy
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.cbxCom = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rtbCodeView = new System.Windows.Forms.RichTextBox();
            this.btnHexRead = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btn_burn = new System.Windows.Forms.Button();
            this.tbxHexFile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.btnRead = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 238);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(456, 160);
            this.listBox1.TabIndex = 0;
            // 
            // cbxCom
            // 
            this.cbxCom.FormattingEnabled = true;
            this.cbxCom.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10",
            "COM11",
            "COM12",
            "COM13",
            "COM14",
            "COM15"});
            this.cbxCom.Location = new System.Drawing.Point(12, 28);
            this.cbxCom.Name = "cbxCom";
            this.cbxCom.Size = new System.Drawing.Size(121, 21);
            this.cbxCom.TabIndex = 1;
            this.cbxCom.SelectedIndexChanged += new System.EventHandler(this.cbxCom_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Comport:";
            // 
            // rtbCodeView
            // 
            this.rtbCodeView.Location = new System.Drawing.Point(12, 87);
            this.rtbCodeView.Name = "rtbCodeView";
            this.rtbCodeView.Size = new System.Drawing.Size(454, 130);
            this.rtbCodeView.TabIndex = 3;
            this.rtbCodeView.Text = "";
            // 
            // btnHexRead
            // 
            this.btnHexRead.Location = new System.Drawing.Point(355, 26);
            this.btnHexRead.Name = "btnHexRead";
            this.btnHexRead.Size = new System.Drawing.Size(111, 23);
            this.btnHexRead.TabIndex = 4;
            this.btnHexRead.Text = "Code einlesen";
            this.btnHexRead.UseVisualStyleBackColor = true;
            this.btnHexRead.Click += new System.EventHandler(this.btnHexRead_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btn_burn
            // 
            this.btn_burn.Location = new System.Drawing.Point(12, 415);
            this.btn_burn.Name = "btn_burn";
            this.btn_burn.Size = new System.Drawing.Size(75, 23);
            this.btn_burn.TabIndex = 5;
            this.btn_burn.Text = "Burn!!!";
            this.btn_burn.UseVisualStyleBackColor = true;
            this.btn_burn.Click += new System.EventHandler(this.btn_burn_Click);
            // 
            // tbxHexFile
            // 
            this.tbxHexFile.Location = new System.Drawing.Point(139, 28);
            this.tbxHexFile.Name = "tbxHexFile";
            this.tbxHexFile.Size = new System.Drawing.Size(210, 20);
            this.tbxHexFile.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(136, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Hex File:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Hex Data:";
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(391, 415);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(75, 23);
            this.btnRead.TabIndex = 11;
            this.btnRead.Text = "Read!!!";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 445);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbxHexFile);
            this.Controls.Add(this.btn_burn);
            this.Controls.Add(this.btnHexRead);
            this.Controls.Add(this.rtbCodeView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxCom);
            this.Controls.Add(this.listBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ComboBox cbxCom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtbCodeView;
        private System.Windows.Forms.Button btnHexRead;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btn_burn;
        private System.Windows.Forms.TextBox tbxHexFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button btnRead;
    }
}

