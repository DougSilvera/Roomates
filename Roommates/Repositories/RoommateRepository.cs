using Microsoft.Data.SqlClient;
using Roommates.Models;
using System.Collections.Generic;

namespace Roommates.Repositories
{
    public class RoommateRepository : BaseRepository
    {
        public RoommateRepository(string connectionString) : base(connectionString) { }

        public Roommate GetById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT FirstName, RentPortion, RoomId, Room.Name FROM Roommate JOIN Room on Roommate.RoomId=Room.Id WHERE Roommate.Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Roommate roommate = null;
                        Room room = null;

                        if (reader.Read())
                        {
                           
                            room = new Room
                            {
                               Id = reader.GetInt32(reader.GetOrdinal("RoomId")),
                               Name = reader.GetString(reader.GetOrdinal("Name")),
                            };
                            roommate = new Roommate
                            {
                                Id = id,
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                RentPortion = reader.GetInt32(reader.GetOrdinal("RentPortion")),
                                RoomId = room
                                

                            };
                        }
                        return roommate ;
                    }

                }
            }
        }
    }
}
