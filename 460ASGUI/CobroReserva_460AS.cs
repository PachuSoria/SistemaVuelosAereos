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
    public partial class CobroReserva_460AS : Form, IIdiomaObserver_460AS
    {
        BLL460AS_Comprobante bllComprobante_460AS;
        BLL460AS_Asiento bllAsiento_460AS;
        private Reserva_460AS reservaActual;
        public CobroReserva_460AS(Reserva_460AS reserva)
        {
            InitializeComponent();
            bllComprobante_460AS = new BLL460AS_Comprobante();
            bllAsiento_460AS = new BLL460AS_Asiento();
            this.reservaActual = reserva;
            textBox5.Text = $"${reservaActual.PrecioTotal_460AS} USD";
            IdiomaManager_460AS.Instancia.RegistrarObserver(this);
            ActualizarIdioma();
        }

        public void ActualizarIdioma()
        {
            label1.Text = IdiomaManager_460AS.Instancia.Traducir("label_pago");
            label2.Text = IdiomaManager_460AS.Instancia.Traducir("label_num_tarjeta");
            label3.Text = IdiomaManager_460AS.Instancia.Traducir("label_nombre_titular");
            label4.Text = IdiomaManager_460AS.Instancia.Traducir("label_apellido_titular");
            label6.Text = IdiomaManager_460AS.Instancia.Traducir("label_monto");
            button1.Text = IdiomaManager_460AS.Instancia.Traducir("boton_confirmar_pago");
            comboBox1.Items.Clear();
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";
            comboBox1.Items.Add(new KeyValuePair<string, string>("TarjetaCredito", IdiomaManager_460AS.Instancia.Traducir("Credito")));
            comboBox1.Items.Add(new KeyValuePair<string, string>("TarjetaDebito", IdiomaManager_460AS.Instancia.Traducir("Debito")));
            if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(comboBox1.Text)) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_pago_vacio"));
                string tipoPago = ((KeyValuePair<string, string>)comboBox1.SelectedItem).Key;
                if (textBox1.Text.Length == 0) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_num_tarjeta_vacio"));
                string nroTarj = textBox1.Text;
                if (!Regex.IsMatch(nroTarj, @"^[0-9]{10}$")) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_num_tarjeta_invalido"));
                if (textBox2.Text.Length == 0) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_nombretit_vacio"));
                string nombre = textBox2.Text;
                if (textBox3.Text.Length == 0) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_apellidotit_vacio"));
                string apellido = textBox3.Text;
                if (textBox4.Text.Length == 0) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_cvv_vacio"));
                if (textBox4.Text.Length != 3) throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_cvv_invalido"));
                int cvv = Convert.ToInt32(textBox4.Text);
                DateTime fechaPago = DateTime.Now;
                string codComprobante = bllComprobante_460AS.GenerarCodigoComprobanteUnico_460AS();
                decimal monto = reservaActual.PrecioTotal_460AS;
                Comprobante_460AS comprobante = new Comprobante_460AS(
                    codComprobante,
                    reservaActual,
                    monto,
                    tipoPago,
                    fechaPago);
                bllComprobante_460AS.GuardarComprobante_460AS(comprobante);
                foreach (var asiento in reservaActual.AsientosReservados_460AS)
                {
                    bllAsiento_460AS.AsignarReservaAsiento_460AS(asiento.NumAsiento_460AS, asiento.CodVuelo_460AS, reservaActual.CodReserva_460AS);
                }
                MessageBox.Show(IdiomaManager_460AS.Instancia.Traducir("msg_pago_registrado"), "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
