namespace _460ASGUI
{
    partial class GestionFamilias_460AS
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
            listBox1 = new ListBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            treeView1 = new TreeView();
            label4 = new Label();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            button7 = new Button();
            label5 = new Label();
            label6 = new Label();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 53);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(129, 23);
            textBox1.TabIndex = 0;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(182, 53);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(129, 23);
            textBox2.TabIndex = 1;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(40, 186);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(209, 274);
            listBox1.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 35);
            label1.Name = "label1";
            label1.Size = new Size(85, 15);
            label1.TabIndex = 3;
            label1.Text = "Codigo familia";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(182, 35);
            label2.Name = "label2";
            label2.Size = new Size(90, 15);
            label2.TabIndex = 4;
            label2.Text = "Nombre familia";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(591, 118);
            label3.Name = "label3";
            label3.Size = new Size(50, 15);
            label3.TabIndex = 5;
            label3.Text = "Familias";
            // 
            // treeView1
            // 
            treeView1.Location = new Point(591, 136);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(610, 387);
            treeView1.TabIndex = 6;
            treeView1.AfterSelect += treeView1_AfterSelect;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(42, 168);
            label4.Name = "label4";
            label4.Size = new Size(55, 15);
            label4.TabIndex = 7;
            label4.Text = "Permisos";
            // 
            // button1
            // 
            button1.Location = new Point(369, 186);
            button1.Name = "button1";
            button1.Size = new Size(111, 23);
            button1.TabIndex = 8;
            button1.Text = "Crear familia";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(369, 241);
            button2.Name = "button2";
            button2.Size = new Size(111, 23);
            button2.TabIndex = 9;
            button2.Text = "Asignar permiso";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(369, 293);
            button3.Name = "button3";
            button3.Size = new Size(111, 23);
            button3.TabIndex = 10;
            button3.Text = "Asignar familia";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(369, 339);
            button4.Name = "button4";
            button4.Size = new Size(111, 23);
            button4.TabIndex = 11;
            button4.Text = "Eliminar familia";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Location = new Point(369, 388);
            button5.Name = "button5";
            button5.Size = new Size(111, 23);
            button5.TabIndex = 12;
            button5.Text = "Eliminar hijo";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button6
            // 
            button6.Location = new Point(369, 437);
            button6.Name = "button6";
            button6.Size = new Size(111, 23);
            button6.TabIndex = 13;
            button6.Text = "Seleccionar";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // button7
            // 
            button7.Location = new Point(22, 511);
            button7.Name = "button7";
            button7.Size = new Size(75, 23);
            button7.TabIndex = 14;
            button7.Text = "Salir";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(251, 493);
            label5.Name = "label5";
            label5.Size = new Size(122, 15);
            label5.TabIndex = 15;
            label5.Text = "Familia seleccionada: ";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(379, 493);
            label6.Name = "label6";
            label6.Size = new Size(0, 15);
            label6.TabIndex = 16;
            // 
            // GestionFamilias_460AS
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1261, 546);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(button7);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label4);
            Controls.Add(treeView1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(listBox1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Name = "GestionFamilias_460AS";
            Text = "GestionFamilias_460AS";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private TextBox textBox2;
        private ListBox listBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private TreeView treeView1;
        private Label label4;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Label label5;
        private Label label6;
    }
}