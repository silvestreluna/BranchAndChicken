using BranchAndChicken.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace BranchAndChicken.Api.DataAccess
{
    public class TrainerRepository
    {




        string _connectionString = "Server=localhost;Database=BranchAndChicken;Trusted_Connection=True;";

        public List<Trainer> GetAll()
        {
            using (var db = new SqlConnection(_connectionString))
            {
                //db.Open();

                var trainers = db.Query<Trainer>("SELECT * FROM Trainer");

                // ado.net below
                //var cmd = new SqlCommand("", connection);

                //var cmd = db.CreateCommand();
                //cmd.CommandText = "SELECT * FROM Trainer";

                //var dataReader = cmd.ExecuteReader();

                //var trainers = new List<Trainer>();

                //while (dataReader.Read())
                //{
                //    trainers.Add(GetTrainerFromDataReader(dataReader));
                //}

                return trainers.ToList();
            }

        }

        public Trainer Get(string name)
        {
            using (var db = new SqlConnection(_connectionString))
            {

                var sql = @"SELECT *
                                    FROM trainer 
                                    WHERE trainer.Name = @trainerName";


                var trainer = db.QueryFirst<Trainer>(sql, new { trainerName = name });
                return trainer;


                //connection.Open();
                //var cmd = connection.CreateCommand();
                //cmd.CommandText = $"SELECT * FROM Trainer where name = @trainerName";

                //cmd.Parameters.AddWithValue("trainerName", name);

                //var dataReader = cmd.ExecuteReader();

                //if(dataReader.Read())
                //{
                //    return GetTrainerFromDataReader(dataReader);
                //}


            }


            //var trainer = _trainers.firstordefault(t => t.name == name);
            //return trainer;
        }

        public bool Remove(string name)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"delete 
                                    from Trainer 
                                    where name = @name";
                return db.Execute(sql, new { name }) == 1;

                //connection.Open();

                //var cmd = connection.CreateCommand();
                //cmd.CommandText = @"delete 
                //                    from Trainer 
                //                    where name = @name";

                //cmd.Parameters.AddWithValue("name", name);

                //return cmd.ExecuteNonQuery() == 1;

            }
        }

        public Trainer Update(Trainer updatedTrainer, int id)
        {

            using (var db = new SqlConnection(_connectionString))
            {
               var sql = @"
                           UPDATE [dbo].[Trainer]
                           SET [Name] = @name
                             ,[YearsOfExperience] = @yearOfExperience
                              ,[Specialty] = @specialty
                              output inserted.*
                              WHERE [id] = @id";


                //var parameters = new
                //{
                //    Id = id,
                //    updatedTrainer.Name,
                //    updatedTrainer.YearOfExperience,
                //    updatedTrainer.Specialty
                //};

                updatedTrainer.Id = id;

                var trainer = db.QueryFirst<Trainer>(sql, updatedTrainer);

                return trainer;

                //connection.Open();

                //var cmd = connection.CreateCommand();

                //cmd.CommandText = @"
                //                   UPDATE [dbo].[Trainer]
                //                   SET [Name] = @name
                //                   ,[YearsOfExperience] = @yearOfExperience
                //                   ,[Specialty] = @specialty
                //                   output inserted.*
                //                   WHERE [id] = @id";

                //cmd.Parameters.AddWithValue("name", updatedTrainer.Name);
                //cmd.Parameters.AddWithValue("yearOfExperience", updatedTrainer.YearOfExperience);
                //cmd.Parameters.AddWithValue("specialty", updatedTrainer.Specialty);
                //cmd.Parameters.AddWithValue("id", id);

                //var reader = cmd.ExecuteReader();
                //if (reader.Read())
                //{
                //    return GetTrainerFromDataReader(reader);
                //}

                //return null;
            }
        }

        public Trainer Add(Trainer newTrainer)
        {
            using (var db = new SqlConnection(_connectionString))
            {
               
                var sql = @"
                        INSERT INTO [dbo].[Trainer]
                        ([Name]
                         ,[YearsOfExperience]
                          ,[Specialty])
                            output inserted.*
                        VALUES (@name ,@yearOfExperience ,@specialty)";


                return db.QueryFirst<Trainer>(sql, newTrainer);



                //connection.Open();

                //var cmd = connection.CreateCommand();

                //cmd.CommandText = @"
                //        INSERT INTO [dbo].[Trainer]
                //        ([Name]
                //         ,[YearsOfExperience]
                //          ,[Specialty])
                //            output inserted.*
                //        VALUES (@name ,@yearOfExperience ,@specialty)";

                //cmd.Parameters.AddWithValue("name", newTrainer.Name);
                //cmd.Parameters.AddWithValue("yearOfExperience", newTrainer.YearOfExperience);
                //cmd.Parameters.AddWithValue("specialty", newTrainer.Specialty);

                //var reader = cmd.ExecuteReader();

                //if (reader.Read())
                //{
                //    return GetTrainerFromDataReader(reader);
                //}

                //return null;
            }

        }


        //Trainer GetTrainerFromDataReader(SqlDataReader dataReader)
        //{
        //    var id = (int)dataReader["id"];

        //    // implicit cast
        //    var returnedName = dataReader["name"] as string;

        //    // Convert to 
        //    var yearOfExperience = Convert.ToInt32(dataReader["YearsOfExperience"]);

        //    // try parse
        //    Enum.TryParse<Specialty>(dataReader["Specialty"].ToString(), out var speciality);

        //    // var trainers = new List<Trainer>();
        //    var trainer = new Trainer
        //    {
        //        Specialty = speciality,
        //        Id = id,
        //        Name = returnedName,
        //        YearOfExperience = yearOfExperience
        //    };

        //    return trainer;

        //}
    }

}
