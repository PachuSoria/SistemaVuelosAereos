namespace _460ASGUI
{
    partial class ReservaVuelos_460AS
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
            label4 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            panel2 = new Panel();
            button4 = new Button();
            groupBox1 = new GroupBox();
            label10 = new Label();
            button3 = new Button();
            dataGridView1 = new DataGridView();
            label1 = new Label();
            comboBox1 = new ComboBox();
            label3 = new Label();
            button2 = new Button();
            panel1 = new Panel();
            panel2.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(149, 16);
            label4.Name = "label4";
            label4.Size = new Size(44, 15);
            label4.TabIndex = 7;
            label4.Text = "Cliente";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(185, 86);
            label7.Name = "label7";
            label7.Size = new Size(51, 15);
            label7.TabIndex = 19;
            label7.Text = "Apellido";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(30, 86);
            label8.Name = "label8";
            label8.Size = new Size(51, 15);
            label8.TabIndex = 18;
            label8.Text = "Nombre";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(30, 32);
            label9.Name = "label9";
            label9.Size = new Size(27, 15);
            label9.TabIndex = 17;
            label9.Text = "DNI";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(185, 104);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(111, 23);
            textBox3.TabIndex = 14;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(30, 104);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(111, 23);
            textBox2.TabIndex = 13;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(30, 50);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(111, 23);
            textBox1.TabIndex = 12;
            // 
            // panel2
            // 
            panel2.Controls.Add(button4);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(label8);
            panel2.Controls.Add(label9);
            panel2.Controls.Add(textBox3);
            panel2.Controls.Add(textBox2);
            panel2.Controls.Add(textBox1);
            panel2.Controls.Add(label4);
            panel2.Location = new Point(12, 12);
            panel2.Name = "panel2";
            panel2.Size = new Size(343, 222);
            panel2.TabIndex = 23;
            // 
            // button4
            // 
            button4.Location = new Point(129, 166);
            button4.Name = "button4";
            button4.Size = new Size(75, 23);
            button4.TabIndex = 22;
            button4.Text = "Ingresar";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label10);
            groupBox1.Location = new Point(16, 283);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(310, 154);
            groupBox1.TabIndex = 24;
            groupBox1.TabStop = false;
            groupBox1.Text = "Resumen ";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(6, 33);
            label10.Name = "label10";
            label10.Size = new Size(0, 15);
            label10.TabIndex = 0;
            // 
            // button3
            // 
            button3.Location = new Point(353, 353);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 25;
            button3.Text = "Pagar";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(20, 31);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(629, 243);
            dataGridView1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 13);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 1;
            label1.Text = "Vuelos";
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(32, 304);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(142, 23);
            comboBox1.TabIndex = 3;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(32, 286);
            label3.Name = "label3";
            label3.Size = new Size(47, 15);
            label3.TabIndex = 6;
            label3.Text = "Destino";
            // 
            // button2
            // 
            button2.Location = new Point(237, 304);
            button2.Name = "button2";
            button2.Size = new Size(133, 23);
            button2.TabIndex = 24;
            button2.Text = "Seleccionar asiento";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(button2);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(comboBox1);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(dataGridView1);
            panel1.Location = new Point(540, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(678, 364);
            panel1.TabIndex = 22;
            // 
            // ReservaVuelos_460AS
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1257, 473);
            Controls.Add(button3);
            Controls.Add(groupBox1);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "ReservaVuelos_460AS";
            Text = "ReservaVuelos_460AS";
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Label label4;
        private Label label7;
        private Label label8;
        private Label label9;
        private TextBox textBox3;
        private TextBox textBox2;
        private TextBox textBox1;
        private Panel panel2;
        private GroupBox groupBox1;
        private Button button3;
        private Button button4;
        private Label label10;
        private DataGridView dataGridView1;
        private Label label1;
        private ComboBox comboBox1;
        private Label label3;
        private Button button2;
        private Panel panel1;
    }
}