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
    public partial class RegistroCliente_460AS : Form
    {
        BLL460AS_Cliente bllCliente_460AS;
        public RegistroCliente_460AS()
        {
            InitializeComponent();
            bllCliente_460AS = new BLL460AS_Cliente();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
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
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
