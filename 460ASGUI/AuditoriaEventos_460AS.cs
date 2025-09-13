using _460ASBLL;
using iTextSharp.text;
using iTextSharp.text.pdf;
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
    public partial class AuditoriaEventos_460AS : Form
    {
        BLL460AS_Evento _bllEvento;
        public AuditoriaEventos_460AS()
        {
            InitializeComponent();
            _bllEvento = new BLL460AS_Evento();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; dataGridView1.MultiSelect = false; dataGridView1.ReadOnly = true;
        }

        private static bool EsTodos(string s) => s == "Todos" || s == "(Todos)";

        private static string GetComboFiltro(ComboBox cb)
        {
            var s = cb.SelectedItem?.ToString();
            return (string.IsNullOrWhiteSpace(s) || EsTodos(s)) ? null : s;
        }

        private static int? GetComboCriticidad(ComboBox cb)
        {
            var s = cb.SelectedItem?.ToString();
            if (string.IsNullOrWhiteSpace(s) || EsTodos(s)) return null;
            return int.TryParse(s, out var n) ? n : (int?)null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime desde = dateTimePicker1.Value.Date;
                DateTime hasta = dateTimePicker2.Value.Date.AddDays(1).AddSeconds(-1);

                if (desde > hasta)
                {
                    MessageBox.Show("La fecha de inicio no puede ser mayor que la fecha de fin",
                                    "Rango inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                string usuario = string.IsNullOrWhiteSpace(textBox1.Text) ? null : textBox1.Text.Trim();
                if (usuario != null && !System.Text.RegularExpressions.Regex.IsMatch(usuario, @"^[a-zA-Z0-9\s]+$"))
                {
                    MessageBox.Show("Usuario solo puede contener letras, números y espacios.",
                                    "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string modulo = GetComboFiltro(comboBox1);
                string actividad = GetComboFiltro(comboBox2);
                int? criticidad = GetComboCriticidad(comboBox3);

                dataGridView1.DataSource =
                    _bllEvento.FiltrarEventos_460AS(desde, hasta, actividad, usuario, modulo, criticidad);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count == 0)
                {
                    throw new Exception("No hay datos para exportar");
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF (*.pdf)|*.pdf",
                    FileName = "AuditoriaEventos.pdf"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    {
                        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 20f, 20f);
                        PdfWriter.GetInstance(pdfDoc, stream);
                        pdfDoc.Open();

                        Paragraph titulo = new Paragraph("Reporte de Auditoría de Eventos\n\n",
                            new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.BOLD));
                        titulo.Alignment = Element.ALIGN_CENTER;
                        pdfDoc.Add(titulo);

                        PdfPTable pdfTable = new PdfPTable(dataGridView1.Columns.Count);
                        pdfTable.WidthPercentage = 100;

                        foreach (DataGridViewColumn column in dataGridView1.Columns)
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText))
                            {
                                BackgroundColor = BaseColor.LIGHT_GRAY
                            };
                            pdfTable.AddCell(cell);
                        }

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    pdfTable.AddCell(cell.Value?.ToString() ?? "");
                                }
                            }
                        }

                        pdfDoc.Add(pdfTable);
                        pdfDoc.Close();
                        stream.Close();
                    }

                    MessageBox.Show("PDF generado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                textBox1.Clear();
                comboBox1.SelectedIndex = 0;
                comboBox2.SelectedIndex = 0;
                comboBox3.SelectedIndex = 0;
                dateTimePicker1.Value = DateTime.Today.AddDays(-3);
                dateTimePicker2.Value = DateTime.Today;
                dataGridView1.DataSource = _bllEvento.ObtenerEventosUltimosTresDias_460AS();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ConfigurarColumnas()
        {
            dataGridView1.AutoGenerateColumns = false; 
            dataGridView1.Columns.Clear();

            DataGridViewTextBoxColumn colUsuario = new DataGridViewTextBoxColumn();
            colUsuario.DataPropertyName = "Usuario_460AS"; 
            colUsuario.HeaderText = "Usuario"; 
            dataGridView1.Columns.Add(colUsuario);

            DataGridViewTextBoxColumn colFecha = new DataGridViewTextBoxColumn();
            colFecha.DataPropertyName = "Fecha_460AS";
            colFecha.HeaderText = "Fecha";
            dataGridView1.Columns.Add(colFecha);

            DataGridViewTextBoxColumn colModulo = new DataGridViewTextBoxColumn();
            colModulo.DataPropertyName = "Modulo_460AS";
            colModulo.HeaderText = "Módulo";
            dataGridView1.Columns.Add(colModulo);

            DataGridViewTextBoxColumn colActividad = new DataGridViewTextBoxColumn();
            colActividad.DataPropertyName = "Actividad_460AS";
            colActividad.HeaderText = "Actividad";
            dataGridView1.Columns.Add(colActividad);

            DataGridViewTextBoxColumn colCriticidad = new DataGridViewTextBoxColumn();
            colCriticidad.DataPropertyName = "Criticidad_460AS";
            colCriticidad.HeaderText = "Criticidad";
            dataGridView1.Columns.Add(colCriticidad);
        }

        private void AuditoriaEventos_460AS_Load(object sender, EventArgs e)
        {
            ConfigurarColumnas();
            if (comboBox1.SelectedIndex < 0 && comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;
            if (comboBox2.SelectedIndex < 0 && comboBox2.Items.Count > 0) comboBox2.SelectedIndex = 0;
            if (comboBox3.SelectedIndex < 0 && comboBox3.Items.Count > 0) comboBox3.SelectedIndex = 0;
            dataGridView1.DataSource = _bllEvento.ObtenerEventosUltimosTresDias_460AS();
            dateTimePicker1.Value = DateTime.Today.AddDays(-3);
            dateTimePicker2.Value = DateTime.Today;
        }
    }
}
