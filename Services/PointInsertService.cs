using MappingApp.Interfaces;
using Npgsql;
using System.Data;
using System.Net;

namespace MappingApp.Services
{
    public class PointInsertService: IPointService
    {
        private readonly string _connectionString;
        public PointInsertService(string connectionString)
        {
            _connectionString = connectionString;
        }

        private Response<T> CreateResponse<T>(T values, string status, string message)
        {
            return new Response<T>(values, status, message);
        }

         private T ExecuteCommand<T>(string query, Action<NpgsqlCommand> prepareCommand)
        {
            if (prepareCommand == null)
            {
                throw new ArgumentNullException(nameof(prepareCommand), "değerlerim null olamaz.");
            }

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(query, connection))
                {
                    prepareCommand(command);

                    var result = command.ExecuteScalar();

                    if (result == null)
                    {
                        return default(T);
                    }
                    else
                    {
                        return (T)result;
                    }
                }
            }
        }

        public Response<List<Point>> GetAll()
        {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new NpgsqlCommand("SELECT * FROM Points", connection))
                    {
                        using (var reader = command.ExecuteReader())// ExecuteReader : sql sorgusu atmaya yarayan yrdmc method.
                        {
                            var points = new List<Point>();
                            while (reader.Read())
                            {
                                points.Add(new Point
                                {
                                    Id = reader.IsDBNull("Id") ? 0 : reader.GetInt32(reader.GetOrdinal("Id")),
                                    PointX = reader.IsDBNull("PointX") ? 0.0 : reader.GetDouble(reader.GetOrdinal("PointX")),
                                    PointY = reader.IsDBNull("PointY") ? 0.0 : reader.GetDouble(reader.GetOrdinal("PointY")),
                                    Name = reader.IsDBNull("Name") ? null : reader.GetString(reader.GetOrdinal("Name")),
                                    Description = reader.IsDBNull("Description") ? null : reader.GetString(reader.GetOrdinal("Description"))
                                });

                            }
                            return CreateResponse(points, "200", "Go");
                        }
                    }
                }
        }
        public Response<Point> AddPoint(Point point)
        {
            try
            {
                var id = ExecuteCommand<int>("INSERT INTO Points (PointX, PointY, Name, Description) VALUES (@PointX, @PointY, @Name, @Description) RETURNING Id", command =>
                {
                    command.Parameters.AddWithValue("@PointX", point.PointX);
                    command.Parameters.AddWithValue("@PointY", point.PointY);
                    command.Parameters.AddWithValue("@Name", point.Name);
                    command.Parameters.AddWithValue("@Description", point.Description);
                });

                point.Id = id;

                return CreateResponse(point, "201", "Go");
            }
            catch(Exception ex)
            {
                return CreateResponse(default(Point), "500", $"Hata: {ex.Message}");
            }
           
        }
        public Response<Point> GetById(int id)
        {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new NpgsqlCommand("SELECT * FROM Points WHERE Id = @Id", connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return CreateResponse(new Point
                                {
                                    Id = reader.IsDBNull(reader.GetOrdinal("Id")) ? 0 : reader.GetInt32(reader.GetOrdinal("Id")),
                                    PointX = reader.IsDBNull(reader.GetOrdinal("PointX")) ? 0.0 : reader.GetDouble(reader.GetOrdinal("PointX")),
                                    PointY = reader.IsDBNull(reader.GetOrdinal("PointY")) ? 0.0 : reader.GetDouble(reader.GetOrdinal("PointY")),
                                    Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString(reader.GetOrdinal("Name")),
                                    Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description"))
                                }, "200", "Go");
                            }
                        }
                    }
                }

            return CreateResponse(default(Point), "400", "Bad");
        }
        public Response<Point> UpdatePoint(int id, Point updatePoint)
        {

            ExecuteCommand<int>("UPDATE Points SET PointX = @PointX, PointY = @PointY, Name = @Name, Description = @Description WHERE Id = @Id", command =>
            {
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@PointX", updatePoint.PointX);
                command.Parameters.AddWithValue("@PointY", updatePoint.PointY);
                command.Parameters.AddWithValue("@Name", updatePoint.Name);
                command.Parameters.AddWithValue("@Description", updatePoint.Description);
            });

            return CreateResponse(updatePoint, "200", "Go");
        }

        public Response<bool> DeletePoint(int id)
        {
            var rowsAffected = ExecuteCommand<int>("DELETE FROM Points WHERE Id = @Id", command =>
            {
                command.Parameters.AddWithValue("@Id", id);
            });

            if (rowsAffected > 0)
            {
                return CreateResponse(true, "404", "Bad");
            }
            else
            {
                return CreateResponse(false, "200", "Go");
             
            }
        }

    }
}




//command.CommandText = "INSERT INTO Student (LastName, FirstName, Address, City) 
//                          VALUES(@ln, @fn, @add, @cit)";

//    command.Parameters.AddWithValue("@ln", lastName);
//command.Parameters.AddWithValue("@fn", firstName);
//command.Parameters.AddWithValue("@add", address);
//command.Parameters.AddWithValue("@cit", city);

//public void UpdateProduct(Product product) { using var connection = new NpgsqlConnection(_connectionString); connection.Open(); using var cmd = new NpgsqlCommand("UPDATE products SET name = @name, price = @price WHERE id = @id", connection); cmd.Parameters.AddWithValue("id", product.Id); cmd.Parameters.AddWithValue("name", product.Name); cmd.Parameters.AddWithValue("price", product.Price); cmd.ExecuteNonQuery(); }
