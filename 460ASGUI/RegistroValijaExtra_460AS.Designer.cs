namespace _460ASGUI
{
    partial class RegistroValijaExtra_460AS
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
            textBox1 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            button1 = new Button();
            comboBox1 = new ComboBox();
            comboBox2 = new ComboBox();
            button2 = new Button();
            listBox1 = new ListBox();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            groupBox1 = new GroupBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(45, 151);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(136, 23);
            textBox1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(45, 20);
            label1.Name = "label1";
            label1.Size = new Size(55, 15);
            label1.TabIndex = 3;
            label1.Text = "Cantidad";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(45, 78);
            label2.Name = "label2";
            label2.Size = new Size(32, 15);
            label2.TabIndex = 4;
            label2.Text = "Peso";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(45, 133);
            label3.Name = "label3";
            label3.Size = new Size(40, 15);
            label3.TabIndex = 5;
            label3.Text = "Precio";
            // 
            // button1
            // 
            button1.Location = new Point(77, 207);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 6;
            button1.Text = "Agregar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "1", "2", "3", "4" });
            comboBox1.Location = new Point(45, 38);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(136, 23);
            comboBox1.TabIndex = 7;
            // 
            // comboBox2
            // 
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "10 kg", "15 kg", "23 kg", "32 kg" });
            comboBox2.Location = new Point(45, 96);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(136, 23);
            comboBox2.TabIndex = 8;
            // 
            // button2
            // 
            button2.Location = new Point(267, 207);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 9;
            button2.Text = "Registrar";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(229, 38);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(149, 154);
            listBox1.TabIndex = 10;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(229, 19);
            label4.Name = "label4";
            label4.Size = new Size(96, 15);
            label4.TabIndex = 11;
            label4.Text = "Valijas agregadas";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 23);
            label5.Name = "label5";
            label5.Size = new Size(96, 15);
            label5.TabIndex = 12;
            label5.Text = "10 kg --> 10 USD";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 38);
            label6.Name = "label6";
            label6.Size = new Size(96, 15);
            label6.TabIndex = 13;
            label6.Text = "15 kg --> 15 USD";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(6, 53);
            label7.Name = "label7";
            label7.Size = new Size(96, 15);
            label7.TabIndex = 14;
            label7.Text = "25 kg --> 25 USD";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(6, 68);
            label8.Name = "label8";
            label8.Size = new Size(96, 15);
            label8.TabIndex = 15;
            label8.Text = "32 kg --> 40 USD";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label6);
            groupBox1.Location = new Point(406, 74);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(132, 100);
            groupBox1.TabIndex = 16;
            groupBox1.TabStop = false;
            groupBox1.Text = "Precios";
            // 
            // RegistroValijaExtra_460AS
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(551, 252);
            Controls.Add(groupBox1);
            Controls.Add(label4);
            Controls.Add(listBox1);
            Controls.Add(button2);
            Controls.Add(comboBox2);
            Controls.Add(comboBox1);
            Controls.Add(button1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Name = "RegistroValijaExtra_460AS";
            Text = "RegistroValijaExtra_460AS";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button button1;
        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private Button button2;
        private ListBox listBox1;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private GroupBox groupBox1;
    }
}