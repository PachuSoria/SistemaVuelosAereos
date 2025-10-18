using _460ASBLL;
using _460ASServicios.Observer;
using iTextSharp.text;
using iTextSharp.text.pdf;
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
    public partial class AuditoriaEventos_460AS : Form, IIdiomaObserver_460AS
    {
        BLL460AS_Evento _bllEvento;
        public AuditoriaEventos_460AS()
        {
            InitializeComponent();
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            _bllEvento = new BLL460AS_Evento();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; dataGridView1.MultiSelect = false; dataGridView1.ReadOnly = true;
            IdiomaManager_460AS.Instancia.RegistrarObserver(this);
            ActualizarIdioma();
            CargarComboActividades();
        }

        private static bool EsTodos(string s) => !string.IsNullOrWhiteSpace(s) && s.StartsWith("Todos");

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
                    MessageBox.Show(IdiomaManager_460AS.Instancia.Traducir("msg_fechas_invalidas"),
                                    IdiomaManager_460AS.Instancia.Traducir("msg_titulo_rango_invalido"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    dateTimePicker1.Value = DateTime.Today.AddDays(-3);
                    dateTimePicker2.Value = DateTime.Today;
                    return;
                }

                string usuario = string.IsNullOrWhiteSpace(textBox1.Text) ? null : textBox1.Text.Trim();
                string modulo = GetComboFiltro(comboBox1);
                string actividad = GetComboFiltro(comboBox2);
                int? criticidad = GetComboCriticidad(comboBox3);

                var eventos = _bllEvento.FiltrarEventos_460AS(desde, hasta, actividad, usuario, modulo, criticidad);
                if (eventos == null || eventos.Count == 0)
                {
                    MessageBox.Show(
                        IdiomaManager_460AS.Instancia.Traducir("msg_no_eventos"),
                        IdiomaManager_460AS.Instancia.Traducir("msg_titulo_aviso"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    dataGridView1.DataSource = eventos;
                }
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
                    throw new Exception(IdiomaManager_460AS.Instancia.Traducir("msg_no_datos"));
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

                    MessageBox.Show(IdiomaManager_460AS.Instancia.Traducir("msg_pdf"),
                                    IdiomaManager_460AS.Instancia.Traducir("msg_titulo_exito"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            var colUsuario = new DataGridViewTextBoxColumn();
            colUsuario.DataPropertyName = "Usuario_460AS";
            colUsuario.HeaderText = IdiomaManager_460AS.Instancia.Traducir("label_usuario");
            dataGridView1.Columns.Add(colUsuario);

            var colFecha = new DataGridViewTextBoxColumn();
            colFecha.DataPropertyName = "Fecha_460AS";
            colFecha.HeaderText = IdiomaManager_460AS.Instancia.Traducir("label_fecha");
            dataGridView1.Columns.Add(colFecha);

            var colModulo = new DataGridViewTextBoxColumn();
            colModulo.DataPropertyName = "Modulo_460AS";
            colModulo.HeaderText = IdiomaManager_460AS.Instancia.Traducir("label_modulo");
            dataGridView1.Columns.Add(colModulo);

            var colActividad = new DataGridViewTextBoxColumn();
            colActividad.DataPropertyName = "Actividad_460AS";
            colActividad.HeaderText = IdiomaManager_460AS.Instancia.Traducir("label_actividad");
            dataGridView1.Columns.Add(colActividad);

            var colCriticidad = new DataGridViewTextBoxColumn();
            colCriticidad.DataPropertyName = "Criticidad_460AS";
            colCriticidad.HeaderText = IdiomaManager_460AS.Instancia.Traducir("label_criticidad");
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
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
        }

        public void ActualizarIdioma()
        {
            button1.Text = IdiomaManager_460AS.Instancia.Traducir("boton_aplicar");
            button2.Text = IdiomaManager_460AS.Instancia.Traducir("boton_imprimir");
            button3.Text = IdiomaManager_460AS.Instancia.Traducir("boton_limpiar");
            button4.Text = IdiomaManager_460AS.Instancia.Traducir("boton_salir");

            label1.Text = IdiomaManager_460AS.Instancia.Traducir("label_eventos");
            label2.Text = IdiomaManager_460AS.Instancia.Traducir("label_usuario");
            label3.Text = IdiomaManager_460AS.Instancia.Traducir("label_modulo");
            label4.Text = IdiomaManager_460AS.Instancia.Traducir("label_actividad");
            label5.Text = IdiomaManager_460AS.Instancia.Traducir("label_criticidad");
            label6.Text = IdiomaManager_460AS.Instancia.Traducir("label_fechaIni");
            label7.Text = IdiomaManager_460AS.Instancia.Traducir("label_fechaFin");
        }

        private void CargarComboActividades()
        {
            comboBox2.Items.Clear();

            var actividades = new List<string>
            {
                "Activación de usuario",
                "Alta de cliente",
                "Alta de usuario",
                "Alta de vuelo",
                "Baja de cliente",
                "Bloqueo automatico por intentos fallidos",
                "Cambio de contraseña",
                "Creación de familia",
                "Creación de perfil",
                "Desactivacion de usuario",
                "Desbloqueo de usuario",
                "Eliminación de familia",
                "Eliminación de perfil",
                "Eliminación de vuelo",
                "Generación de comprobante",
                "Intento de login fallido",
                "Login exitoso",
                "Login fallido por usuario bloqueado",
                "Login fallido por usuario inactivo",
                "Logout",
                "Modificacion de usuario",
                "Modificación de cliente",
                "Modificación de vuelo",
                "Registro de reserva"
            };

            var actividadesOrdenadas = actividades
                .Where(a => a != "Todos")
                .OrderBy(a => a)
                .ToList();

            actividadesOrdenadas.Insert(0, "Todos");

            foreach (var act in actividadesOrdenadas)
            {
                comboBox2.Items.Add(act);
            }

            comboBox2.SelectedIndex = 0;
        }

        private Dictionary<string, List<string>> eventosPorModulo = new Dictionary<string, List<string>>
        {
            { "Usuarios", new List<string> { "Login exitoso", "Intento de login fallido", "Bloqueo automatico por intentos fallidos",
                                             "Login fallido por usuario bloqueado", "Login fallido por usuario inactivo",
                                             "Logout", "Cambio de contraseña", "Activación de usuario", "Desactivacion de usuario",
                                             "Desbloqueo de usuario", "Alta de usuario", "Modificacion de usuario" } },

            { "Clientes", new List<string> { "Alta de cliente", "Modificación de cliente", "Baja de cliente" } },

            { "Vuelos", new List<string> { "Alta de vuelo", "Modificación de vuelo", "Eliminación de vuelo" } },

            { "Reservas", new List<string> { "Registro de reserva", "Generación de comprobante" } },

            { "Perfiles/Familias", new List<string> { "Creación de familia", "Creación de perfil", "Eliminación de familia", "Eliminación de perfil" } }
        };

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();

            if (comboBox1.SelectedItem == null) return;

            string modulo = comboBox1.SelectedItem.ToString();

            if (eventosPorModulo.ContainsKey(modulo))
            {
                comboBox2.Items.Add($"Todos ({modulo})");

                foreach (var evento in eventosPorModulo[modulo])
                {
                    comboBox2.Items.Add(evento);
                }

                comboBox2.SelectedIndex = 0;
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var fila = dataGridView1.SelectedRows[0];

                string loginUsuario = fila.Cells[0].Value?.ToString();

                var bllUsuario = new BLL460AS_Usuario();
                var usuario = bllUsuario.ObtenerUsuarios460AS()
                                        .FirstOrDefault(u => u.Login_460AS == loginUsuario || u.DNI_460AS == loginUsuario);

                if (usuario != null)
                    textBox1.Text = $"{usuario.Nombre_460AS} {usuario.Apellido_460AS}";
                else
                    textBox1.Text = loginUsuario; 
            }
        }
    }
}