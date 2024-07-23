using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CitasBarberiaApp
{
    public class GestorCitas
    {
        public void AsignarCita(Cita cita)
        {
   
            string fecha = cita.FechaHora.Year.ToString() + cita.FechaHora.Month.ToString() + cita.FechaHora.Day.ToString();
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            // Construye la ruta del directorio usando el directorio de documentos
            string rutaDirectorio = Path.Combine(baseDirectory, "Citas", fecha);
            // Verifica si el directorio ya existe
            if (!Directory.Exists(rutaDirectorio))
            {
                // Crea el directorio
                Directory.CreateDirectory(rutaDirectorio);
            }
            //Crear un json con el objeto de cita y que el nombre del archivo sea la cedula del cliente
            string rutaArchivo = rutaDirectorio + "\\" + cita.Cedula + ".json";
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(cita);
            //Siya tiene una cita agendada para ese día, preguntar si desea cambiar los datos de la cita
            if (File.Exists(rutaArchivo))
            {
                var result = MessageBox.Show("Ya tiene una cita agendada para ese día, ¿desea cambiar los datos de esa cita?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    File.WriteAllText(rutaArchivo, json);
                    MessageBox.Show("Cita agendada con éxito", "¡Nos vemos pronto!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                File.WriteAllText(rutaArchivo, json);
                MessageBox.Show("Cita agendada con éxito", "¡Nos vemos pronto!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        public List<Cita> BuscarCita(DateTime fecha)
        {
            List<Cita> citasEncontradas = new List<Cita>();
            string nombreDirectorioABuscar = fecha.Year.ToString() + fecha.Month.ToString() + fecha.Day.ToString();
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            // Construye la ruta del directorio usando el directorio de documentos
            string rutaDirectorio = Path.Combine(baseDirectory, "Citas", nombreDirectorioABuscar);
            if (Directory.Exists(rutaDirectorio))
            {
                string[] archivos = Directory.GetFiles(rutaDirectorio);
                foreach (var archivo in archivos)
                {
                    string json = File.ReadAllText(archivo);
                    Cita cita = Newtonsoft.Json.JsonConvert.DeserializeObject<Cita>(json);
                    citasEncontradas.Add(cita);
                }
            }
            else
            {
                MessageBox.Show("No hay citas agendadas para la fecha seleccionada");
            }
            return citasEncontradas;
        }
    }
}
