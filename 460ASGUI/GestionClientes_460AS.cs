using _460ASBE;
using _460ASBLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _460ASGUI
{
    public partial class GestionClientes_460AS : Form
    {
        private BLL460AS_Cliente bllCliente_460AS;
        private FormEstado estadoActual = FormEstado.Consulta;
        public GestionClientes_460AS()
        {
            InitializeComponent();
            bllCliente_460AS = new BLL460AS_Cliente();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            label8.Text = "Modo Consulta";
            dataGridView1.DataSource = bllCliente_460AS.ObtenerClientes_460AS();
        }

        private enum FormEstado
        {
            Consulta,
            Agregar,
            Modificar,
            Eliminar
        }

        private void HabilitarCampos(bool habilitar)
        {
            textBox1.Enabled = habilitar;
            textBox2.Enabled = habilitar;
            textBox3.Enabled = habilitar;
            textBox4.Enabled = habilitar;
            textBox5.Enabled = habilitar;
            dateTimePicker1.Enabled = habilitar;
            button4.Enabled = true;
            button5.Enabled = true;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
        }

        private void LimpiarCampos()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            dateTimePicker1.Value = DateTime.Today;
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = false;
            button5.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label8.Text = "Modo Agregar";
            estadoActual = FormEstado.Agregar;
            HabilitarCampos(true);
            textBox1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label8.Text = "Modo Modificar";
            estadoActual = FormEstado.Modificar;
            HabilitarCampos(true);
            textBox1.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label8.Text = "Modo Eliminar";
            estadoActual = FormEstado.Eliminar;
            HabilitarCampos(false);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (estadoActual == FormEstado.Agregar)
                {
                    if (textBox1.Text.Length == 0) throw new Exception("Debe ingresar el DNI");
                    string dni = textBox1.Text;
                    if (!Regex.IsMatch(dni, @"^[0-9]{8}$")) throw new Exception("El DNI no es valido");
                    if (bllCliente_460AS.ObtenerClientes_460AS().Any(x => x.DNI_460AS == dni)) throw new Exception("El DNI esta repetido");
                    if (textBox2.Text.Length == 0) throw new Exception("Debe ingresar el nombre");
                    string nombre = textBox2.Text;
                    if (textBox3.Text.Length == 0) throw new Exception("Debe ingresar el apellido");
                    string apellido = textBox3.Text;
                    if (textBox4.Text.Length == 0) throw new Exception("Debe ingresar el telefono");
                    string tel = Regex.Replace(textBox4.Text, @"\D", "");
                    if (tel.Length > 8 || tel.Length < 8 || !int.TryParse(tel, out int telefono)) throw new Exception("El numero de telefono no es valido");
                    DateTime fechaNacimiento = dateTimePicker1.Value;
                    if (textBox5.Text.Length == 0) throw new Exception("Debe ingresar el numero de pasaporte");
                    string nroPasaporte = textBox5.Text;
                    if (!Regex.IsMatch(nroPasaporte, @"^[0-9]{10}$")) throw new Exception("El numero de pasaporte no es valido");
                    if (fechaNacimiento > DateTime.Now) throw new Exception("La fecha de nacimiento no es valida");
                    int edad = DateTime.Now.Year - fechaNacimiento.Year;
                    if (fechaNacimiento.Date > DateTime.Now.AddYears(-edad)) edad--;
                    if (edad < 18) throw new Exception("El cliente debe ser mayor de 18 años");
                    bllCliente_460AS.GuardarCliente_460AS(new Cliente_460AS(dni, nombre, apellido, fechaNacimiento, telefono, nroPasaporte));
                    MessageBox.Show("Cliente registrado correctamente");
                }
                else if (estadoActual == FormEstado.Modificar)
                {
                    if (dataGridView1.SelectedRows.Count == 0) throw new Exception("Debe seleccionar un cliente");
                    var dni = dataGridView1.SelectedRows[0].Cells[0].ToString();
                    Cliente_460AS c = bllCliente_460AS.ObtenerClientes_460AS().FirstOrDefault(c => c.DNI_460AS == dni);
                    if (textBox2.Text.Length == 0) throw new Exception("Debe ingresar el nombre");
                    c.Nombre_460AS = textBox2.Text;
                    if (textBox3.Text.Length == 0) throw new Exception("Debe ingresar el apellido");
                    c.Apellido_460AS = textBox3.Text;
                    if (textBox4.Text.Length == 0) throw new Exception("Debe ingresar el telefono");
                    string tel = Regex.Replace(textBox4.Text, @"\\D", "");
                    if (tel.Length != 8 || !int.TryParse(tel, out int telefono)) throw new Exception("El número de teléfono no es válido");
                    c.Telefono_460AS = telefono;
                    c.FechaNacimiento_460AS = dateTimePicker1.Value;
                }
                else if (estadoActual == FormEstado.Eliminar)
                {
                    if (dataGridView1.SelectedRows.Count == 0) throw new Exception("Debe seleccionar un cliente");
                    var dni = dataGridView1.SelectedRows[0].Cells[0].ToString();
                    bllCliente_460AS.EliminarCliente(dni);
                }
                dataGridView1.DataSource = null; dataGridView1.DataSource = bllCliente_460AS.ObtenerClientes_460AS();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            label8.Text = "Modo Consulta";
            estadoActual = FormEstado.Consulta;
            LimpiarCampos();
            HabilitarCampos(false);
        }
    }
}
