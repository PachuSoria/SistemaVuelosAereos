namespace _460ASGUI
{
    partial class GestionServiciosAdicionales_460AS
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
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            panel1 = new Panel();
            dataGridView1 = new DataGridView();
            label5 = new Label();
            comboBox1 = new ComboBox();
            label6 = new Label();
            groupBox1 = new GroupBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 59);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(120, 23);
            textBox1.TabIndex = 0;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(12, 110);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(120, 23);
            textBox2.TabIndex = 1;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(166, 110);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(120, 23);
            textBox3.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 41);
            label1.Name = "label1";
            label1.Size = new Size(27, 15);
            label1.TabIndex = 3;
            label1.Text = "DNI";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 92);
            label2.Name = "label2";
            label2.Size = new Size(51, 15);
            label2.TabIndex = 4;
            label2.Text = "Nombre";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(166, 92);
            label3.Name = "label3";
            label3.Size = new Size(51, 15);
            label3.TabIndex = 5;
            label3.Text = "Apellido";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(138, 12);
            label4.Name = "label4";
            label4.Size = new Size(44, 15);
            label4.TabIndex = 6;
            label4.Text = "Cliente";
            // 
            // panel1
            // 
            panel1.Controls.Add(button1);
            panel1.Controls.Add(textBox3);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(textBox2);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(22, 24);
            panel1.Name = "panel1";
            panel1.Size = new Size(298, 205);
            panel1.TabIndex = 7;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(439, 36);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(532, 243);
            dataGridView1.TabIndex = 8;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(439, 18);
            label5.Name = "label5";
            label5.Size = new Size(111, 15);
            label5.TabIndex = 9;
            label5.Text = "Reservas del Cliente";
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Cambio de asiento", "Comida especial", "Seguro de viaje", "Valija extra" });
            comboBox1.Location = new Point(439, 328);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(158, 23);
            comboBox1.TabIndex = 10;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(439, 310);
            label6.Name = "label6";
            label6.Size = new Size(115, 15);
            label6.TabIndex = 11;
            label6.Text = "Servicios adicionales";
            // 
            // groupBox1
            // 
            groupBox1.Location = new Point(22, 421);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(320, 169);
            groupBox1.TabIndex = 12;
            groupBox1.TabStop = false;
            groupBox1.Text = "Resumen";
            // 
            // button1
            // 
            button1.Location = new Point(107, 165);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 13;
            button1.Text = "Ingresar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(648, 328);
            button2.Name = "button2";
            button2.Size = new Size(126, 23);
            button2.TabIndex = 13;
            button2.Text = "Registrar servicio";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(372, 495);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 14;
            button3.Text = "Pagar";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // GestionServiciosAdicionales_460AS
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1009, 622);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(groupBox1);
            Controls.Add(label6);
            Controls.Add(comboBox1);
            Controls.Add(label5);
            Controls.Add(dataGridView1);
            Controls.Add(panel1);
            Name = "GestionServiciosAdicionales_460AS";
            Text = "GestionServiciosAdicionales_460AS";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Panel panel1;
        private DataGridView dataGridView1;
        private Label label5;
        private ComboBox comboBox1;
        private Label label6;
        private GroupBox groupBox1;
        private Button button1;
        private Button button2;
        private Button button3;
    }
}