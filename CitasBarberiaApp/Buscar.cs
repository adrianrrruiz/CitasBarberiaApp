using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CitasBarberiaApp
{
    public partial class Buscar : Form
    {
        private GestorCitas gestorCitas;
        public Buscar()
        {
            InitializeComponent();
            gestorCitas = new GestorCitas();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void LlenarDataGridView(DateTime fecha)
        {
            // Asume que gestorCitas es una instancia de GestorCitas y dataGridView1 es tu DataGridView
            List<Cita> citas = gestorCitas.BuscarCita(fecha);

            if (citas.Count > 0)
            {
                // Configura el DataSource del DataGridView
                dataGridView1.DataSource = citas.Select(c => new
                {
                    Cedula = c.Cedula,
                    Nombres = c.Nombres,
                    FechaHora = c.FechaHora.ToShortTimeString()
                }).ToList();
                dataGridView1.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LlenarDataGridView(dateTimePicker1.Value);
        }
    }
}
