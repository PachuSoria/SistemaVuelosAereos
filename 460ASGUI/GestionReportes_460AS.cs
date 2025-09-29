using _460ASBE;
using _460ASBLL;
using _460ASServicios.Observer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _460ASGUI
{
    public partial class GestionReportes_460AS : Form, IIdiomaObserver_460AS
    {
        private BLL460AS_Comprobante bllComprobante;
        private List<Comprobante_460AS> listaComprobantes;
        public GestionReportes_460AS()
        {
            InitializeComponent();
            bllComprobante = new BLL460AS_Comprobante();
            CargarComprobantes();
            dataGridView1.CellDoubleClick -= dataGridView1_CellDoubleClick;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
            IdiomaManager_460AS.Instancia.RegistrarObserver(this);
            ActualizarIdioma();
        }

        public void ActualizarIdioma()
        {
            label1.Text = IdiomaManager_460AS.Instancia.Traducir("label_comprobantes"); 

            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns["CodComprobante"].HeaderText = IdiomaManager_460AS.Instancia.Traducir("Codigo_comprobante");
                dataGridView1.Columns["CodReserva"].HeaderText = IdiomaManager_460AS.Instancia.Traducir("Codigo_reserva");
                dataGridView1.Columns["Monto"].HeaderText = IdiomaManager_460AS.Instancia.Traducir("Monto");
                dataGridView1.Columns["TipoPago"].HeaderText = IdiomaManager_460AS.Instancia.Traducir("Tipo_pago");
                dataGridView1.Columns["FechaPago"].HeaderText = IdiomaManager_460AS.Instancia.Traducir("Fecha_pago");
            }
        }

        private void CargarComprobantes()
        {
            listaComprobantes = bllComprobante.ObtenerComprobantes_460AS(); 

            dataGridView1.DataSource = listaComprobantes.Select(c => new
            {
                CodComprobante = c.CodComprobante_460AS,
                CodReserva = c.Reserva_460AS.CodReserva_460AS,
                Monto = c.Monto_460AS,
                TipoPago = c.TipoPago_460AS,
                FechaPago = c.FechaPago_460AS.ToString("dd/MM/yyyy HH:mm")
            }).ToList();

            dataGridView1.AutoResizeColumns();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < listaComprobantes.Count)
            {
                var comprobante = listaComprobantes[e.RowIndex];
                string rutaCarpeta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Comprobantes");
                string rutaArchivo = Path.Combine(rutaCarpeta, $"Comprobante_{comprobante.CodComprobante_460AS}.pdf");

                if (!File.Exists(rutaArchivo))
                {
                    Reportes_460AS reportes = new Reportes_460AS();
                    rutaArchivo = reportes.GenerarComprobantePDF(comprobante, rutaCarpeta);
                }

                if (rutaArchivo != null && File.Exists(rutaArchivo))
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = rutaArchivo,
                        UseShellExecute = true
                    });
                    Task.Delay(5000).ContinueWith(_ =>
                    {
                        try
                        {
                            File.Delete(rutaArchivo);
                        }
                        catch {  }
                    });
                }
                else
                {
                    MessageBox.Show(IdiomaManager_460AS.Instancia.Traducir("msg_no_pdf"));
                }
            }
        }
    }
}
