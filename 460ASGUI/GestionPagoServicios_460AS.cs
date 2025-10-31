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
    public partial class GestionPagoServicios_460AS : Form, IIdiomaObserver_460AS
    {
        private readonly BLL460AS_Pago bllPago;
        private List<Pago_460AS> listaPagos;
        public GestionPagoServicios_460AS()
        {
            InitializeComponent();
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.RowHeadersVisible = false;
            bllPago = new BLL460AS_Pago();
            CargarPagosServicios();
            dataGridView1.CellDoubleClick -= dataGridView1_CellDoubleClick;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; dataGridView1.MultiSelect = false;
            IdiomaManager_460AS.Instancia.RegistrarObserver(this);
            ActualizarIdioma();
        }

        public void ActualizarIdioma()
        {
            label1.Text = IdiomaManager_460AS.Instancia.Traducir("label_pagos_servicios");

            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns["CodPago"].HeaderText = IdiomaManager_460AS.Instancia.Traducir("col_pago");
                dataGridView1.Columns["Monto"].HeaderText = IdiomaManager_460AS.Instancia.Traducir("col_monto");
                dataGridView1.Columns["TipoPago"].HeaderText = IdiomaManager_460AS.Instancia.Traducir("col_tipoPago");
                dataGridView1.Columns["FechaPago"].HeaderText = IdiomaManager_460AS.Instancia.Traducir("col_fechaPago");
                dataGridView1.Columns["CodReserva"].HeaderText = IdiomaManager_460AS.Instancia.Traducir("col_reserva");
                dataGridView1.Columns["DNICliente"].HeaderText = IdiomaManager_460AS.Instancia.Traducir("label_dni");
                if (dataGridView1.Columns.Contains("Servicios"))
                    dataGridView1.Columns["Servicios"].HeaderText = IdiomaManager_460AS.Instancia.Traducir("col_servicios");
            }
        }

        private void CargarPagosServicios()
        {
            listaPagos = bllPago.ObtenerPagosServicios_460AS(); 

            var data = listaPagos.Select(p => new
            {
                CodPago = p.CodPago_460AS,
                CodReserva = p.Reserva_460AS?.CodReserva_460AS ?? "-",
                DNICliente = p.Reserva_460AS?.Cliente_460AS?.DNI_460AS ?? "-",
                Monto = p.Monto_460AS,
                TipoPago = p.TipoPago_460AS,
                FechaPago = p.FechaPago_460AS.ToString("dd/MM/yyyy HH:mm"),
                Servicios = string.Join(", ", bllPago.ObtenerNombresServiciosDePago_460AS(p.CodPago_460AS))
            }).ToList();

            dataGridView1.DataSource = data;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; 
            dataGridView1.AutoResizeColumns();
            dataGridView1.Columns["Servicios"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; 
            dataGridView1.Columns["Servicios"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;  
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells; 
            dataGridView1.ScrollBars = ScrollBars.Both; 
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.RowIndex >= listaPagos.Count)
                    return;

                var pago = listaPagos[e.RowIndex];
                string rutaCarpeta = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    "ComprobantesServicios"
                );
                Directory.CreateDirectory(rutaCarpeta);

                Reportes_460AS reportes = new Reportes_460AS();
                string rutaArchivo = reportes.GenerarComprobanteServiciosPDF(pago, rutaCarpeta);

                if (File.Exists(rutaArchivo))
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = rutaArchivo,
                        UseShellExecute = true
                    });
                }
                else
                {
                    MessageBox.Show(IdiomaManager_460AS.Instancia.Traducir("msg_no_pdf"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar el comprobante: {ex.Message}");
            }
        }
    }
}
