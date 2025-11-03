namespace _460ASGUI
{
    partial class RegistrarComidaEspecial_460AS
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
            button1 = new Button();
            textBox1 = new TextBox();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            button2 = new Button();
            checkedListBox1 = new CheckedListBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(588, 370);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "Registrar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(416, 370);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(140, 23);
            textBox1.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(416, 352);
            label1.Name = "label1";
            label1.Size = new Size(66, 15);
            label1.TabIndex = 3;
            label1.Text = "Precio final";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Comida_vegetariana;
            pictureBox1.Location = new Point(49, 23);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(262, 156);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.Comida_sin_gluten;
            pictureBox2.Location = new Point(49, 237);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(262, 156);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 5;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.Comida_premium;
            pictureBox3.Location = new Point(49, 452);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(262, 156);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 6;
            pictureBox3.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(118, 199);
            label2.Name = "label2";
            label2.Size = new Size(113, 15);
            label2.TabIndex = 7;
            label2.Text = "Comida vegetariana";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(118, 413);
            label3.Name = "label3";
            label3.Size = new Size(104, 15);
            label3.TabIndex = 8;
            label3.Text = "Comida sin gluten";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(118, 627);
            label4.Name = "label4";
            label4.Size = new Size(101, 15);
            label4.TabIndex = 9;
            label4.Text = "Comida premium";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(416, 197);
            label5.Name = "label5";
            label5.Size = new Size(54, 15);
            label5.TabIndex = 10;
            label5.Text = "Comidas";
            // 
            // button2
            // 
            button2.Location = new Point(588, 619);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 11;
            button2.Text = "Salir";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // checkedListBox1
            // 
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Location = new Point(416, 215);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new Size(247, 130);
            checkedListBox1.TabIndex = 12;
            checkedListBox1.ItemCheck += checkedListBox1_ItemCheck;
            checkedListBox1.MouseDown += checkedListBox1_MouseDown;
            checkedListBox1.MouseUp += checkedListBox1_MouseUp;
            // 
            // RegistrarComidaEspecial_460AS
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 667);
            Controls.Add(checkedListBox1);
            Controls.Add(button2);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Name = "RegistrarComidaEspecial_460AS";
            Text = "RegistrarComidaEspecial_460AS";
            Load += RegistrarComidaEspecial_460AS_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button button1;
        private TextBox textBox1;
        private Label label1;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Button button2;
        private CheckedListBox checkedListBox1;
    }
}