using Microsoft.Data.SqlClient;
using Roommates.Models;
using System.Collections.Generic;
using System;

namespace Roommates.Repositories
{
    public class ChoreRepository : BaseRepository
    {
        public ChoreRepository(string connectionString) : base(connectionString) { }

        public List<Chore> GetAll()
        {
            using (SqlConnection conn=Connection)
            {
                conn.Open();
                using (SqlCommand cmd=conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, Name FROM Chore";
                    using (SqlDataReader reader=cmd.ExecuteReader())
                    {
                        List<Chore> chores = new List<Chore>();
                        while (reader.Read())
                        {
                            int idColumnPosition = reader.GetOrdinal("Id");
                            int idValue = reader.GetInt32(idColumnPosition);
                            int nameColumnPosition = reader.GetOrdinal("Name");
                            string nameValue = reader.GetString(nameColumnPosition);
                            Chore chore = new Chore()
                            {
                                Id = idValue,
                                Name = nameValue,
                            };
                            chores.Add(chore);
                        }
                        return chores;
                    }
                }
            }
        }
      public Chore GetById(int id)
        {
            using (SqlConnection conn=Connection)
            {
                conn.Open();
                using (SqlCommand cmd=conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Name FROM Chore WHERE Id=@id";
                    cmd.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader=cmd.ExecuteReader())
                    {
                        Chore chore = null;

                        if (reader.Read())
                        {
                            chore = new Chore
                            {
                                Id = id,
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                            };
                        }
                        return chore;
                    }
                }
            }
        }
      public List<Chore> GetUnassignedChores()
        {
            using (SqlConnection conn=Connection)
            {
                conn.Open ();
                using (SqlCommand cmd=conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Name FROM Chore LEFT JOIN RoommateChore on RoommateChore.ChoreId=Chore.Id WHERE RoommateChore.RoommateId is null";
                    using (SqlDataReader reader=cmd.ExecuteReader())
                    {
                        List<Chore> chores = new List<Chore>();
                        while (reader.Read())
                        {
                            Chore chore= new Chore
                            {
                           Name = reader.GetString(reader.GetOrdinal("Name")), 
                            };
                            chores.Add(chore);
                        }
                        return chores;
                    }
                }
            }
        }
        public void AssignChore(int roomateId, int choreId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO RoommateChore (RoommateId, ChoreId) VALUES (@roommateId, @choreId)";
                    cmd.Parameters.AddWithValue("@roommateId", roomateId);
                    cmd.Parameters.AddWithValue("@choreId", choreId);
                    cmd.ExecuteNonQuery();
                }
            }
            Console.WriteLine("Chore Assigned!");
        }
    }

}
