namespace _460ASGUI
{
    partial class GestionPerfiles_460AS
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
            treeView1 = new TreeView();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            listBox1 = new ListBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            SuspendLayout();
            // 
            // treeView1
            // 
            treeView1.CheckBoxes = true;
            treeView1.Location = new Point(420, 143);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(292, 397);
            treeView1.TabIndex = 0;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(31, 61);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(144, 23);
            textBox1.TabIndex = 2;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(237, 61);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(144, 23);
            textBox2.TabIndex = 3;
            // 
            // button1
            // 
            button1.Location = new Point(225, 156);
            button1.Name = "button1";
            button1.Size = new Size(104, 23);
            button1.TabIndex = 4;
            button1.Text = "Nuevo perfil";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(225, 203);
            button2.Name = "button2";
            button2.Size = new Size(104, 23);
            button2.TabIndex = 5;
            button2.Text = "Guardar perfil";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(225, 248);
            button3.Name = "button3";
            button3.Size = new Size(104, 23);
            button3.TabIndex = 6;
            button3.Text = "Modificar perfil";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(225, 291);
            button4.Name = "button4";
            button4.Size = new Size(104, 23);
            button4.TabIndex = 7;
            button4.Text = "Eliminar perfil";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(31, 143);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(188, 199);
            listBox1.TabIndex = 8;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(31, 43);
            label1.Name = "label1";
            label1.Size = new Size(76, 15);
            label1.TabIndex = 9;
            label1.Text = "Codigo perfil";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(237, 43);
            label2.Name = "label2";
            label2.Size = new Size(81, 15);
            label2.TabIndex = 10;
            label2.Text = "Nombre perfil";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(31, 125);
            label3.Name = "label3";
            label3.Size = new Size(45, 15);
            label3.TabIndex = 11;
            label3.Text = "Perfiles";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(420, 125);
            label4.Name = "label4";
            label4.Size = new Size(55, 15);
            label4.TabIndex = 12;
            label4.Text = "Permisos";
            // 
            // GestionPerfiles_460AS
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(845, 620);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(listBox1);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(treeView1);
            Name = "GestionPerfiles_460AS";
            Text = "GestionPerfiles_460AS";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TreeView treeView1;
        private TextBox textBox1;
        private TextBox textBox2;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private ListBox listBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}