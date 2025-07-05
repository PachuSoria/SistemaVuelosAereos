using _460ASBE;
using _460ASBLL;
using _460ASServicios.Observer;
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
    public partial class RegistroCliente_460AS : Form, IIdiomaObserver_460AS
    {
        BLL460AS_Cliente bllCliente_460AS;
        public RegistroCliente_460AS()
        {
            InitializeComponent();
            bllCliente_460AS = new BLL460AS_Cliente();
            IdiomaManager_460AS.Instancia.RegistrarObserver(this);
            ActualizarIdioma();
        }
        
        public void ActualizarIdioma()
        {
            label1.Text = IdiomaManager_460AS.Instancia.Traducir("label_cliente");
            label2.Text = IdiomaManager_460AS.Instancia.Traducir("label_dni");
            label3.Text = IdiomaManager_460AS.Instancia.Traducir("label_nombre");
            label4.Text = IdiomaManager_460AS.Instancia.Traducir("label_apellido");
            label5.Text = IdiomaManager_460AS.Instancia.Traducir("label_telefono");
            label6.Text = IdiomaManager_460AS.Instancia.Traducir("label_nacimiento");
            label7.Text = IdiomaManager_460AS.Instancia.Traducir("label_pasaporte");
            button1.Text = IdiomaManager_460AS.Instancia.Traducir("boton_registrar");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Length == 0) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_ex_dni_vacio"));
                string dni = textBox1.Text;
                if (!Regex.IsMatch(dni, @"^[0-9]{8}$")) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_ex_dni_invalido"));
                if (bllCliente_460AS.ObtenerClientes_460AS().Any(x => x.DNI_460AS == dni)) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_ex_dni_repetido"));
                if (textBox2.Text.Length == 0) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_ex_nombre_vacio"));
                string nombre = textBox2.Text;
                if (textBox3.Text.Length == 0) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_ex_apellido_vacio"));
                string apellido = textBox3.Text;
                if (textBox4.Text.Length == 0) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_ex_telefono_vacio"));
                string tel = Regex.Replace(textBox4.Text, @"\D", "");
                if (tel.Length > 8 || tel.Length < 8 || !int.TryParse(tel, out int telefono)) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_ex_telefono_invalido"));
                DateTime fechaNacimiento = dateTimePicker1.Value;
                if (textBox5.Text.Length == 0) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_pasaporte_vacio"));
                string nroPasaporte = textBox5.Text;
                if (!Regex.IsMatch(nroPasaporte, @"^[0-9]{10}$")) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_pasaporte_invalido"));
                if (fechaNacimiento > DateTime.Now) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_nacimiento_invalido"));
                int edad = DateTime.Now.Year - fechaNacimiento.Year;
                if (fechaNacimiento.Date > DateTime.Now.AddYears(-edad)) edad--;
                if (edad < 18) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_mas18"));
                bllCliente_460AS.GuardarCliente_460AS(new Cliente_460AS(dni, nombre, apellido, fechaNacimiento, telefono, nroPasaporte));
                MessageBox.Show(IdiomaManager_460AS.Instancia.Traducir("msg_cliente_registrado"));
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
