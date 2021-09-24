using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SqlServerNoEntity.Models;

namespace SqlServerNoEntity
{
    public class PersonRepository
    {
        private string _connectionString { get; set; }

        public PersonRepository()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
            _connectionString = configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
        }
        public void Create(String name, int age)
        {
            // okay next we create skeleton for the code


            using SqlConnection connection = new(_connectionString);
            try
            {

                connection.Open();

                SqlTransaction SqlTransaction = connection.BeginTransaction();

                string SQL = "INSERT INTO person (name, age) VALUES (@name,@age);";
                SqlCommand SqlCommand = new(SQL, connection,SqlTransaction);
                // we add some parameter
                SqlCommand.Parameters.AddWithValue("@name", name);
                SqlCommand.Parameters.AddWithValue("@age", age);
                int totalRecord = SqlCommand.ExecuteNonQuery();
                // we have error here .. 
                SqlTransaction.Commit();
                if(totalRecord == 0)
                {
                    throw new Exception("something wrong ");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Work fine");

                }

                // c;ear some memory 
                SqlCommand.Dispose();

            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }




        }
        public List<PersonModel> Read()
        {
            List<PersonModel> personModels = new();

            // for reading we don't need transaction ya .. 


            using (SqlConnection connection = new(_connectionString))
            {
                try
                {

                    connection.Open();
                    string SQL = "SELECT * FROM person ";
                    SqlCommand SqlCommand = new(SQL, connection);
                    using (var reader = SqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // it all depend , for ease we just send all data as string it all depend on web api to translate to "" or number only 
                            personModels.Add(new PersonModel()
                            {

                                Name = reader["name"].ToString(),
                                Age = Convert.ToInt32(reader["age"]),
                                PersonId = Convert.ToInt32(reader["personId"])
                            });
                        }
                    }


                    // c;ear some memory 
                    SqlCommand.Dispose();

                }
                catch (SqlException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    throw new Exception(ex.Message);
                    /// you may throw new exception here or use any log debugger to text file 
                }
            }


            return personModels;
        }
        public void Update(String name, int age, int personId)
        {

            using SqlConnection connection = new(_connectionString);
            try
            {

                connection.Open();
                SqlTransaction SqlTransaction = connection.BeginTransaction();

                string SQL = "UPDATE person SET name = @name, age = @age WHERE personId = @personId ";
                SqlCommand SqlCommand = new SqlCommand(SQL, connection, SqlTransaction);
                // we add some parameter
                SqlCommand.Parameters.AddWithValue("@name", name);
                SqlCommand.Parameters.AddWithValue("@age", age);
                SqlCommand.Parameters.AddWithValue("@personId", personId);

                SqlCommand.ExecuteNonQuery();

                SqlTransaction.Commit();
                // c;ear some memory 
                SqlCommand.Dispose();

            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw new Exception(ex.Message);
                /// you may throw new exception here or use any log debugger to text file 
            }
        }
        public void Delete(int personId)
        {


            // the old using is using(xxx) { }  now it's weirder 
            using SqlConnection connection = new(_connectionString);
            try
            {

                connection.Open();
                // sorry we tend to forget sometimes..  but with error like this you all can remember what's error if failure not all working  fine :P
                SqlTransaction SqlTransaction = connection.BeginTransaction();

                string SQL = "DELETE FROM person WHERE personId  = @personId;";
                SqlCommand SqlCommand = new SqlCommand(SQL, connection, SqlTransaction);
                // we add some parameter
                SqlCommand.Parameters.AddWithValue("@personId", personId);
                SqlCommand.ExecuteNonQuery();

                SqlTransaction.Commit();
                // c;ear some memory 
                SqlCommand.Dispose();

            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                /// you may throw new exception here or use any log debugger to text file 
            }
        }
    }

}
