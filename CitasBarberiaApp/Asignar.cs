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
    public partial class Asignar : Form
    {
        private GestorCitas gestorCitas;
        public Asignar()
        {
            InitializeComponent();
            gestorCitas = new GestorCitas();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Asignar_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(txtCedula.Text.Length == 0) {
                //Mostrar warning en la interfaz
                MessageBox.Show("Debe ingresar su cédula", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtNombres.Text.Length == 0)
            {
                //Mostrar warning en la interfaz
                MessageBox.Show("Debe ingresar su nombre", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Cita nuevaCita = new Cita
            {
                Cedula = txtCedula.Text,
                Nombres = txtNombres.Text,
                Apellidos = txtApellidos.Text,
                Edad = txtEdad.Text,
                FechaHora = dtpFechaHora.Value,
            };
            gestorCitas.AsignarCita(nuevaCita);
        }

        private void txtCedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si el carácter ingresado no es un dígito ni una tecla de control (como retroceso)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Suprime el evento de tecla
            }
        }

        private void txtEdad_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si el carácter ingresado no es un dígito ni una tecla de control (como retroceso)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Suprime el evento de tecla
            }
        }
    }
}
