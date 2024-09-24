using Npgsql;
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
        private string connectionString = "Host=localhost;Username=adrian;Password=nairda;Database=mydb";

        public void AsignarCita(Cita cita)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    // Verificar si ya existe una cita para el cliente en esa fecha
                    string checkQuery = "SELECT COUNT(*) FROM Citas WHERE Cedula = @Cedula AND FechaHora::DATE = @Fecha::DATE";
                    using (var checkCommand = new NpgsqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@Cedula", cita.Cedula);
                        checkCommand.Parameters.AddWithValue("@Fecha", cita.FechaHora);
                        long count = (long)checkCommand.ExecuteScalar();

                        if (count > 0)
                        {
                            var result = MessageBox.Show("Ya tiene una cita agendada para ese día, ¿desea cambiar los datos de la cita?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.Yes)
                            {
                                // Actualizar la cita
                                string updateQuery = "UPDATE Citas SET Nombres = @Nombres, Apellidos = @Apellidos, Edad = @Edad, FechaHora = @FechaHora WHERE Cedula = @Cedula";
                                using (var updateCommand = new NpgsqlCommand(updateQuery, connection))
                                {
                                    updateCommand.Parameters.AddWithValue("@Nombres", cita.Nombres);
                                    updateCommand.Parameters.AddWithValue("@Apellidos", cita.Apellidos);
                                    updateCommand.Parameters.AddWithValue("@Edad", cita.Edad);
                                    updateCommand.Parameters.AddWithValue("@FechaHora", cita.FechaHora);
                                    updateCommand.Parameters.AddWithValue("@Cedula", cita.Cedula);
                                    updateCommand.ExecuteNonQuery();
                                }
                                MessageBox.Show("Cita actualizada con éxito", "¡Nos vemos pronto!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            // Insertar nueva cita
                            string insertQuery = "INSERT INTO Citas (Cedula, Nombres, Apellidos, Edad, FechaHora) VALUES (@Cedula, @Nombres, @Apellidos, @Edad, @FechaHora)";
                            using (var insertCommand = new NpgsqlCommand(insertQuery, connection))
                            {
                                insertCommand.Parameters.AddWithValue("@Cedula", cita.Cedula);
                                insertCommand.Parameters.AddWithValue("@Nombres", cita.Nombres);
                                insertCommand.Parameters.AddWithValue("@Apellidos", cita.Apellidos);
                                insertCommand.Parameters.AddWithValue("@Edad", cita.Edad);
                                insertCommand.Parameters.AddWithValue("@FechaHora", cita.FechaHora);
                                insertCommand.ExecuteNonQuery();
                            }
                            MessageBox.Show("Cita agendada con éxito", "¡Nos vemos pronto!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al asignar la cita: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public List<Cita> BuscarCita(DateTime fecha)
        {
            List<Cita> citasEncontradas = new List<Cita>();

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string selectQuery = "SELECT * FROM Citas WHERE FechaHora::DATE = @Fecha::DATE";
                    using (var command = new NpgsqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Fecha", fecha);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Cita cita = new Cita
                                {
                                    Cedula = reader["Cedula"].ToString(),
                                    Nombres = reader["Nombres"].ToString(),
                                    Apellidos = reader["Apellidos"].ToString(),
                                    Edad = reader["Edad"].ToString(),
                                    FechaHora = Convert.ToDateTime(reader["FechaHora"])
                                };
                                citasEncontradas.Add(cita);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar citas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (citasEncontradas.Count == 0)
            {
                MessageBox.Show("No hay citas agendadas para la fecha seleccionada");
            }

            return citasEncontradas;
        }
    }
}
