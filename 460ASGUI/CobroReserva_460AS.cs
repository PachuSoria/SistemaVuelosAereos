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
    public partial class CobroReserva_460AS : Form
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(comboBox1.Text)) throw new Exception("Debe seleccionar el tipo de pago");
                string tipoPago = comboBox1.Text;
                if (textBox1.Text.Length == 0) throw new Exception("Debe ingresar el numero de tarjeta");
                string nroTarj = textBox1.Text;
                if (!Regex.IsMatch(nroTarj, @"^[0-9]{10}$")) throw new Exception("El numero de tarjeta no es valido");
                if (textBox2.Text.Length == 0) throw new Exception("Debe ingresar el nombre del titular");
                string nombre = textBox2.Text;
                if (textBox3.Text.Length == 0) throw new Exception("Debe ingresar el apellido del titular");
                string apellido = textBox3.Text;
                if (textBox4.Text.Length == 0) throw new Exception("Debe ingresar el codigo de seguridad");
                if (textBox4.Text.Length != 3) throw new Exception("El codigo de seguridad no es valido");
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
                MessageBox.Show("Pago registrado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
