using _460ASServicios.Observer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _460ASGUI
{
    public partial class RegistrarSeguroViaje_460AS : Form, IIdiomaObserver_460AS
    {
        public string SeguroSeleccionado => seguroSeleccionado;
        private DateTime fechaSalidaVuelo;
        private Dictionary<string, decimal> preciosSeguros = new()
        {
            { "Premium", 80m },
            { "Intermedio", 55m },
            { "Basico", 40m }
        };
        private string seguroSeleccionado = string.Empty;
        public decimal PrecioSeleccionado { get; private set; } = 0m;
        public DateTime FechaVencimiento { get; private set; }
        public RegistrarSeguroViaje_460AS(DateTime fechaSalida)
        {
            InitializeComponent();
            fechaSalidaVuelo = fechaSalida;
            var fechaMinima = fechaSalidaVuelo.AddDays(7);
            dateTimePicker1.Value = DateTime.Today > fechaMinima ? DateTime.Today : fechaMinima;
            dateTimePicker1.MinDate = fechaMinima;
            dateTimePicker1.Value = fechaMinima;
            radioButton1.CheckedChanged += radioButton1_CheckedChanged;
            radioButton2.CheckedChanged += radioButton1_CheckedChanged;
            radioButton3.CheckedChanged += radioButton1_CheckedChanged;
            IdiomaManager_460AS.Instancia.RegistrarObserver(this);
            ActualizarIdioma();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(seguroSeleccionado)) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_seleccionar_seguro"));

                FechaVencimiento = dateTimePicker1.Value;
                if (FechaVencimiento < fechaSalidaVuelo.AddDays(7))
                    throw new Exception(string.Format(
                        IdiomaManager_460AS.Instancia.Traducir("msg_seguro_fecha"),
                        fechaSalidaVuelo.ToString("dd/MM/yyyy")
                    ));

                PrecioSeleccionado = preciosSeguros[seguroSeleccionado];

                MessageBox.Show(
                       string.Format(IdiomaManager_460AS.Instancia.Traducir("msg_seguro_reg"), seguroSeleccionado) + "\n" +
                       string.Format(IdiomaManager_460AS.Instancia.Traducir("msg_vto_seguro"), FechaVencimiento.ToString("dd/MM/yyyy")) + "\n" +
                       string.Format(IdiomaManager_460AS.Instancia.Traducir("msg_precio_seguro"), PrecioSeleccionado.ToString("0.00")),
                       IdiomaManager_460AS.Instancia.Traducir("msg_servicio_agregado"),
                       MessageBoxButtons.OK, MessageBoxIcon.Information
                 );

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                seguroSeleccionado = "Premium";
            else if (radioButton2.Checked)
                seguroSeleccionado = "Intermedio";
            else if (radioButton3.Checked)
                seguroSeleccionado = "Basico";
            else
                seguroSeleccionado = string.Empty;

            if (!string.IsNullOrEmpty(seguroSeleccionado))
                textBox1.Text = $"{preciosSeguros[seguroSeleccionado]:0.00} USD";
            else
                textBox1.Clear();
        }

        public void ActualizarIdioma()
        {
            label2.Text = IdiomaManager_460AS.Instancia.Traducir("label_tipo_pre");
            label3.Text = IdiomaManager_460AS.Instancia.Traducir("label_equi_pre");
            label4.Text = IdiomaManager_460AS.Instancia.Traducir("label_asis_pre");
            label5.Text = IdiomaManager_460AS.Instancia.Traducir("label_precio_pre");
            label9.Text = IdiomaManager_460AS.Instancia.Traducir("label_tipo_int");
            label8.Text = IdiomaManager_460AS.Instancia.Traducir("label_equi_int");
            label7.Text = IdiomaManager_460AS.Instancia.Traducir("label_asis_int");
            label6.Text = IdiomaManager_460AS.Instancia.Traducir("label_precio_int");
            label14.Text = IdiomaManager_460AS.Instancia.Traducir("label_tipo_bas");
            label13.Text = IdiomaManager_460AS.Instancia.Traducir("label_equi_bas");
            label12.Text = IdiomaManager_460AS.Instancia.Traducir("label_asis_bas");
            label11.Text = IdiomaManager_460AS.Instancia.Traducir("label_precio_bas");
            label16.Text = IdiomaManager_460AS.Instancia.Traducir("label_fecha_vencimiento");
            label17.Text = IdiomaManager_460AS.Instancia.Traducir("label_precio");
            button1.Text = IdiomaManager_460AS.Instancia.Traducir("boton_registrar");
            button2.Text = IdiomaManager_460AS.Instancia.Traducir("boton_salir");
            radioButton2.Text = IdiomaManager_460AS.Instancia.Traducir("radiobutton_int");
            radioButton3.Text = IdiomaManager_460AS.Instancia.Traducir("radiobutton_bas");
        }
    }
}
