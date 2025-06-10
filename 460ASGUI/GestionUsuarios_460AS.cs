using _460ASBLL;
using _460ASServicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _460ASGUI
{
    public partial class GestionUsuarios_460AS : Form
    {
        BLL460AS_Usuario bllUsuario_460AS;
        public GestionUsuarios_460AS()
        {
            InitializeComponent();
            bllUsuario_460AS = new BLL460AS_Usuario();
            label6.Text = "Modo Consulta";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            ActualizarGrillas_460AS();
            this.MinimumSize = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1200, 800);
            this.MinimumSize = new Size(panel1.Width + 40, panel1.Height + 40);
            this.Size = new Size(panel1.Width + 80, panel1.Height + 80);
            this.BackColor = Color.LightSteelBlue;
        }

        private enum FormEstado_460AS
        {
            Consulta,
            Añadir,
            Modificar,
            Desbloquear,
            Activado
        }

        private void ActualizarGrillas_460AS()
        {
            List<Usuario_460AS> ListaUsuarios = bllUsuario_460AS.ObtenerUsuarios460AS();
            dataGridView1.DataSource = null;
            if (radioButton1.Checked)
            {
                var linq = from x in ListaUsuarios
                           where x.Activo_460AS == true && x.Rol_460AS != "Admin"
                           select new
                           {
                               DNI = x.DNI_460AS,
                               Nombre = x.Nombre_460AS,
                               Apellido = x.Apellido_460AS,
                               Username = x.Login_460AS,
                               Rol = x.Rol_460AS,
                               Telefono = x.Telefono_460AS
                           };
                dataGridView1.DataSource = linq.ToList();
            }
            else
            {
                var linq = from x in ListaUsuarios
                           where x.Rol_460AS != "Admin"
                           select new
                           {
                               DNI = x.DNI_460AS,
                               Nombre = x.Nombre_460AS,
                               Apellido = x.Apellido_460AS,
                               Username = x.Login_460AS,
                               Rol = x.Rol_460AS,
                               Telefono = x.Telefono_460AS
                           };
                dataGridView1.DataSource = linq.ToList();
            }
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string dni = row.Cells[0].Value.ToString();
                var usuario = ListaUsuarios.FirstOrDefault(x => x.DNI_460AS == dni);
                if (usuario != null && usuario.Bloqueado_460AS) row.DefaultCellStyle.BackColor = Color.Salmon;
                else if (usuario.Activo_460AS == false) row.DefaultCellStyle.BackColor = Color.Gray;
                if (dataGridView1.Rows.Count > 0) dataGridView1.ClearSelection();
            }
        }

        private FormEstado_460AS estadoActual = FormEstado_460AS.Consulta;

        private void HabilitarCampos_460AS()
        {
            label2.Enabled = true;
            label3.Enabled = true;
            label4.Enabled = true;
            label5.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            comboBox1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            estadoActual = FormEstado_460AS.Añadir;
            label1.Enabled = true;
            textBox1.Enabled = true;
            HabilitarCampos_460AS();
            label6.Text = "Modo Añadir";
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            estadoActual = FormEstado_460AS.Modificar;
            HabilitarCampos_460AS();
            label6.Text = "Modo Modificar";
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (estadoActual == FormEstado_460AS.Añadir)
                {
                    if (textBox1.Text.Length == 0) throw new Exception("Debe ingresar el DNI");
                    string dni = textBox1.Text;
                    if (!Regex.IsMatch(dni, @"^[0-9]{8}$")) throw new Exception("El DNI no es válido");
                    if (bllUsuario_460AS.ObtenerUsuarios460AS().Any(x => x.DNI_460AS == dni)) throw new Exception("El DNI está repetido");
                    if (textBox2.Text.Length == 0) throw new Exception("Debe ingresar el nombre");
                    string nombre = textBox2.Text;
                    if (textBox3.Text.Length == 0) throw new Exception("Debe ingresar el apellido");
                    string apellido = textBox3.Text;
                    if (textBox4.Text.Length == 0) throw new Exception("Debe ingresar el telefono");
                    string tel = Regex.Replace(textBox4.Text, @"\D", "");
                    if (tel.Length > 8 || tel.Length < 8 || !int.TryParse(tel, out int telefono)) throw new Exception("El telefono no es valido");
                    if (string.IsNullOrWhiteSpace(comboBox1.Text)) throw new Exception("Debe seleccionar el rol");
                    string rol = comboBox1.Text;
                    string dniPrefijo = dni.Substring(0, 3);
                    string login = $"{dniPrefijo}{nombre}";
                    string password = $"{dniPrefijo}{apellido}";
                    bllUsuario_460AS.GuardarUsuario_460AS(new Usuario_460AS(dni, nombre, apellido, login, password, rol, telefono, false, true, 0, DateTime.Now));
                    textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox4.Clear();
                }
                else if (estadoActual == FormEstado_460AS.Modificar)
                {
                    if (dataGridView1.SelectedRows.Count == 0) throw new Exception("Debe seleccionar un usuario");
                    var user = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    Usuario_460AS u = bllUsuario_460AS.ObtenerUsuarios460AS().FirstOrDefault(u => u.DNI_460AS == user);
                    if (textBox2.Text.Length == 0) throw new Exception("Debe ingresar el nombre");
                    u.Nombre_460AS = textBox2.Text;
                    if (textBox3.Text.Length == 0) throw new Exception("Debe ingresar el apellido");
                    u.Apellido_460AS = textBox3.Text;
                    if (textBox4.Text.Length == 0) throw new Exception("Debe ingresar el telefono");
                    string tel = Regex.Replace(textBox4.Text, @"\D", "");
                    if (tel.Length > 8 || tel.Length < 8 || !int.TryParse(tel, out int telefono)) throw new Exception("El telefono no es valido");
                    u.Telefono_460AS = telefono;
                    if (string.IsNullOrWhiteSpace(comboBox1.Text)) throw new Exception("Debe seleccionar el rol");
                    u.Rol_460AS = comboBox1.Text;
                    bllUsuario_460AS.Actualizar_460AS(u);
                    textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox4.Clear();
                }
                else if (estadoActual == FormEstado_460AS.Desbloquear)
                {
                    if (dataGridView1.SelectedRows.Count == 0) throw new Exception("Debe seleccionar un usuario");
                    Usuario_460AS user = bllUsuario_460AS.ObtenerUsuarios460AS().Where(u => u.DNI_460AS.Equals(dataGridView1.SelectedRows[0].Cells[0].Value.ToString())).FirstOrDefault();
                    if (user.Bloqueado_460AS) bllUsuario_460AS.Desbloquear_460AS(user);
                    else throw new Exception("El usuario no esta bloqueado");
                }
                else if (estadoActual == FormEstado_460AS.Activado)
                {
                    if (dataGridView1.SelectedRows.Count == 0) throw new Exception("Debe seleccionar un usuario");
                    Usuario_460AS user = bllUsuario_460AS.ObtenerUsuarios460AS().Where(u => u.DNI_460AS.Equals(dataGridView1.SelectedRows[0].Cells[0].Value.ToString())).FirstOrDefault();
                    if (user.Activo_460AS)
                    {
                        DialogResult resultado = MessageBox.Show($"¿Seguro que desea desactivar al usuario {user.Nombre_460AS} {user.Apellido_460AS}?",
                            "Confirmar desactivacion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (resultado == DialogResult.Yes) bllUsuario_460AS.Desactivar_460AS(user);
                        else return;
                    }
                    else bllUsuario_460AS.Activar_460AS(user);
                }
                ActualizarGrillas_460AS();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox4.Clear();
            label1.Enabled = false;
            label2.Enabled = false;
            label3.Enabled = false;
            label4.Enabled = false;
            label5.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            comboBox1.Enabled = false;
            button1.Enabled = true;
            button2.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            estadoActual = FormEstado_460AS.Consulta;
            label6.Text = "Modo Consulta";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            estadoActual = FormEstado_460AS.Activado;
            label6.Text = "Modo Activado/Desactivado";
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            estadoActual = FormEstado_460AS.Desbloquear;
            label6.Text = "Modo Desbloquear";
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarGrillas_460AS();
        }

        private void GestionUsuarios_460AS_Load(object sender, EventArgs e)
        {
            radioButton2.Checked = true;
            ActualizarGrillas_460AS();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarGrillas_460AS();
        }

        private void GestionUsuarios_460AS_Resize(object sender, EventArgs e)
        {
            panel1.Left = (this.ClientSize.Width - panel1.Width) / 2;
            panel1.Top = (this.ClientSize.Height - panel1.Height) / 2;
        }
    }
}
