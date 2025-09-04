using _460ASBE;
using _460ASBLL;
using _460ASServicios;
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
using System.Xml.Serialization;

namespace _460ASGUI
{
    public partial class GestionClientes_460AS : Form, IIdiomaObserver_460AS
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
            ActualizarClientes();
            IdiomaManager_460AS.Instancia.RegistrarObserver(this);
            ActualizarIdioma();
            HabilitarCampos(false);
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
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

            button1.Enabled = !habilitar;
            button2.Enabled = !habilitar;
            button3.Enabled = !habilitar;
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

        private void RestaurarBotonesModoConsulta()
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = false;
            button5.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            estadoActual = FormEstado.Agregar;
            ActualizarIdioma();
            HabilitarCampos(true);
            textBox1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            estadoActual = FormEstado.Modificar;
            ActualizarIdioma();
            HabilitarCampos(true);
            textBox1.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            estadoActual = FormEstado.Eliminar;
            ActualizarIdioma();
            HabilitarCampos(false);
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = true;
            button5.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (estadoActual == FormEstado.Agregar)
                {
                    if (textBox1.Text.Length == 0) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_ex_dni_vacio"));
                    string dni = textBox1.Text;
                    if (!Regex.IsMatch(dni, @"^[0-9]{8}$")) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_ex_dni_invalido"));
                    if (bllCliente_460AS.ObtenerClientes_460AS().Any(x => x.DNI_460AS == dni)) throw new Exception("El DNI esta repetido");
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
                    if (bllCliente_460AS.ObtenerClientes_460AS().Any(c => c.NroPasaporte_460AS == Cifrado_460AS.EncriptarPasaporteAES_460AS(nroPasaporte))) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_pasaporte_repetido"));
                    if (fechaNacimiento > DateTime.Now) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_nacimiento_invalido"));
                    int edad = DateTime.Now.Year - fechaNacimiento.Year;
                    if (fechaNacimiento.Date > DateTime.Now.AddYears(-edad)) edad--;
                    if (edad < 18) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_mas18"));
                    bllCliente_460AS.GuardarCliente_460AS(new Cliente_460AS(dni, nombre, apellido, fechaNacimiento, telefono, nroPasaporte));
                    MessageBox.Show(IdiomaManager_460AS.Instancia.Traducir("msg_cliente_registrado"));
                }
                else if (estadoActual == FormEstado.Modificar)
                {
                    if (dataGridView1.SelectedRows.Count == 0) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_cliente_vacio"));
                    var dni = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    Cliente_460AS c = bllCliente_460AS.ObtenerClientes_460AS().FirstOrDefault(c => c.DNI_460AS == dni);
                    if (textBox2.Text.Length == 0) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_ex_nombre_vacio"));
                    c.Nombre_460AS = textBox2.Text;
                    if (textBox3.Text.Length == 0) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_ex_apellido_vacio"));
                    c.Apellido_460AS = textBox3.Text;
                    if (textBox4.Text.Length == 0) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_ex_telefono_vacio"));
                    string tel = Regex.Replace(textBox4.Text, @"\\D", "");
                    if (tel.Length != 8 || !int.TryParse(tel, out int telefono)) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_ex_telefono_invalido"));
                    c.Telefono_460AS = telefono;
                    c.FechaNacimiento_460AS = dateTimePicker1.Value;
                    bllCliente_460AS.ActualizarCliente_460AS(c);
                }
                else if (estadoActual == FormEstado.Eliminar)
                {
                    if (dataGridView1.SelectedRows.Count == 0) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_cliente_vacio"));
                    var dni = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    bllCliente_460AS.EliminarCliente(dni);
                }
                ActualizarClientes();
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
            RestaurarBotonesModoConsulta();
        }

        public void ActualizarClientes()
        {
            bool mostrarEncriptado = checkBox1.Checked;
            if (mostrandoDesdeXML && clientesDeserializados != null)
            {
                dataGridView1.DataSource = clientesDeserializados.Select(c => new
                {
                    DNI = c.DNI_460AS,
                    Nombre = c.Nombre_460AS,
                    Apellido = c.Apellido_460AS,
                    FechaNacimiento = c.FechaNacimiento_460AS.ToShortDateString(),
                    Telefono = c.Telefono_460AS,
                    Pasaporte = mostrarEncriptado
                        ? c.NroPasaporte_460AS
                        : Cifrado_460AS.DesencriptarPasaporteAES_460AS(c.NroPasaporte_460AS)
                }).ToList();
            }
            else
            {
                dataGridView1.DataSource = bllCliente_460AS.ObtenerClientes_460AS().Select(c => new
                {
                    DNI = c.DNI_460AS,
                    Nombre = c.Nombre_460AS,
                    Apellido = c.Apellido_460AS,
                    FechaNacimiento = c.FechaNacimiento_460AS.ToShortDateString(),
                    Telefono = c.Telefono_460AS,
                    Pasaporte = mostrarEncriptado
                    ? c.NroPasaporte_460AS
                    : Cifrado_460AS.DesencriptarPasaporteAES_460AS(c.NroPasaporte_460AS)
                }).ToList();
            }

            dataGridView1.Columns[0].HeaderText = IdiomaManager_460AS.Instancia.Traducir("DNI");
            dataGridView1.Columns[1].HeaderText = IdiomaManager_460AS.Instancia.Traducir("Nombre");
            dataGridView1.Columns[2].HeaderText = IdiomaManager_460AS.Instancia.Traducir("Apellido");
            dataGridView1.Columns[3].HeaderText = IdiomaManager_460AS.Instancia.Traducir("FechaNacimiento");
            dataGridView1.Columns[4].HeaderText = IdiomaManager_460AS.Instancia.Traducir("Telefono");
            dataGridView1.Columns[5].HeaderText = IdiomaManager_460AS.Instancia.Traducir("Pasaporte");
        }

        public void ActualizarIdioma()
        {
            label1.Text = IdiomaManager_460AS.Instancia.Traducir("label_dni");
            label2.Text = IdiomaManager_460AS.Instancia.Traducir("label_nombre");
            label3.Text = IdiomaManager_460AS.Instancia.Traducir("label_apellido");
            label4.Text = IdiomaManager_460AS.Instancia.Traducir("label_telefono");
            label5.Text = IdiomaManager_460AS.Instancia.Traducir("label_pasaporte");
            label6.Text = IdiomaManager_460AS.Instancia.Traducir("label_nacimiento");
            label7.Text = IdiomaManager_460AS.Instancia.Traducir("label_clientes");
            button1.Text = IdiomaManager_460AS.Instancia.Traducir("boton_añadir");
            button2.Text = IdiomaManager_460AS.Instancia.Traducir("boton_modificar");
            button3.Text = IdiomaManager_460AS.Instancia.Traducir("boton_eliminar");
            button4.Text = IdiomaManager_460AS.Instancia.Traducir("boton_guardar");
            button5.Text = IdiomaManager_460AS.Instancia.Traducir("boton_cancelar");
            checkBox1.Text = IdiomaManager_460AS.Instancia.Traducir("checkbox_encriptado");
            switch (estadoActual)
            {
                case FormEstado.Consulta:
                    label8.Text = IdiomaManager_460AS.Instancia.Traducir("modo_consulta");
                    break;
                case FormEstado.Agregar:
                    label8.Text = IdiomaManager_460AS.Instancia.Traducir("modo_añadir");
                    break;
                case FormEstado.Modificar:
                    label8.Text = IdiomaManager_460AS.Instancia.Traducir("modo_modificar");
                    break;
                case FormEstado.Eliminar:
                    label8.Text = IdiomaManager_460AS.Instancia.Traducir("modo_eliminar");
                    break;
            }
            ActualizarClientes();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarClientes();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                List<Cliente_460AS> clientes = bllCliente_460AS.ObtenerClientes_460AS();
                if (clientes == null) MessageBox.Show("No hay clientes para serializar");
                using (var sfd = new SaveFileDialog())
                {
                    sfd.Title = "Guardar clientes";
                    sfd.Filter = "Archivo XML|*.xml";
                    sfd.FileName = $"clientes_{DateTime.Now:yyyyMMdd_HHmm}.xml";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        var serializer = new XmlSerializer(typeof(List<Cliente_460AS>));
                        using (var fs = new FileStream(sfd.FileName, FileMode.Create))
                        {
                            serializer.Serialize(fs, clientes);
                        }
                        listBox1.Items.Clear();
                        string[] lineas = File.ReadAllLines(sfd.FileName);
                        foreach (string linea in lineas)
                        {
                            listBox1.Items.Add(linea);
                        }
                        MessageBox.Show("Clientes serializados correctamente");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private List<Cliente_460AS> clientesDeserializados = null;
        private bool mostrandoDesdeXML = false;
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                using (var ofd = new OpenFileDialog())
                {
                    ofd.Title = "Abrir archivo de clientes";
                    ofd.Filter = "Archivo XML|*.xml";

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        List<Cliente_460AS> clientes = null;

                        var serializer = new XmlSerializer(typeof(List<Cliente_460AS>));
                        using (var fs = new FileStream(ofd.FileName, FileMode.Open))
                        {
                            clientes = (List<Cliente_460AS>)serializer.Deserialize(fs);
                        }

                        if (clientes == null || clientes.Count == 0)
                        {
                            MessageBox.Show("El archivo no contiene clientes");
                            return;
                        }
                        clientesDeserializados = clientes;
                        mostrandoDesdeXML = true;
                        ActualizarClientes();
                        MessageBox.Show("Clientes cargados correctamente");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            mostrandoDesdeXML = false;
            clientesDeserializados = null;
            ActualizarClientes();    
        }
    }
}
