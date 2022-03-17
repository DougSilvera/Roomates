using System;
using System.Collections.Generic;
using Roommates.Repositories;
using Roommates.Models;
using System.Linq;

namespace Roommates
{
    class Program
    {
        //  This is the address of the database.
        //  We define it here as a constant since it will never change.
        private const string CONNECTION_STRING = @"server=localhost\SQLExpress;database=Roommates;integrated security=true;TrustServerCertificate=true;";

        static void Main(string[] args)
        {
            RoomRepository roomRepo = new RoomRepository(CONNECTION_STRING);
            ChoreRepository choreRepo = new ChoreRepository(CONNECTION_STRING);
            RoommateRepository roommateRepo = new RoommateRepository(CONNECTION_STRING);
            bool runProgram = true;
            while (runProgram)
            {
                string selection = GetMenuSelection();

                switch (selection)
                {
                    case ("Update a chore"):
                        List<Chore> choreList3 = choreRepo.GetAll();
                        foreach (Chore chore3 in choreList3)
                        { 
                                Console.WriteLine($"{chore3.Id}. {chore3.Name}");
                        }
                        Console.Write("Enter the ID of the chore you want to update: ");
                        int selectedChoreToUpdate = int.Parse(Console.ReadLine());
                        Chore selectedChore = choreList3.FirstOrDefault(c => c.Id == selectedChoreToUpdate);
                        Console.Write("New Name: ");
                        selectedChore.Name = Console.ReadLine();

                        choreRepo.UpdateChore(selectedChore);
                        Console.WriteLine("Chore has been successfully updated");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Delete a chore"):
                        List<Chore> choreList2 = choreRepo.GetAll();
                            foreach (Chore c in choreList2)
                            {
                            Console.WriteLine($"{c.Id}. {c.Name}");

                            }
                        Console.Write("Enter the ID of the chore you want to delete: ");
                        int choreToDelete = int.Parse(Console.ReadLine());
                        choreRepo.DeleteChore(choreToDelete);
                        Console.WriteLine("Chore has been deleted");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;


                    case ("Delete a room"):
                        List<Room> roomList = roomRepo.GetAll();
                        foreach (Room room2 in roomList)
                        {
                            Console.WriteLine($"{room2.Id}. {room2.Name} Max Occupancy({room2.MaxOccupancy})");
                        }
                        Console.Write("Enter the ID of the room you want to delete: ");
                        int roomToDelete = int.Parse(Console.ReadLine());
                        roomRepo.deleteRoom(roomToDelete);
                        Console.WriteLine("Room has been deleted");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Update a room"):
                        List<Room> roomOptions = roomRepo.GetAll();
                        foreach (Room r in roomOptions)
                        {
                            Console.WriteLine($"{r.Id} - {r.Name} Max Occupancy({r.MaxOccupancy})");
                        }

                        Console.Write("Which room would you like to update? ");
                        int selectedRoomId = int.Parse(Console.ReadLine());
                        Room selectedRoom = roomOptions.FirstOrDefault(r => r.Id == selectedRoomId);

                        Console.Write("New Name: ");
                        selectedRoom.Name = Console.ReadLine();

                        Console.Write("New Max Occupancy: ");
                        selectedRoom.MaxOccupancy = int.Parse(Console.ReadLine());

                        roomRepo.Update(selectedRoom);

                        Console.WriteLine("Room has been successfully updated");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Assign a chore to a roommate"):
                        List<Chore> choreList = choreRepo.GetAll();
                        foreach (Chore chore1 in choreList)
                        {
                            Console.WriteLine($"{chore1.Id}. {chore1.Name}");

                        }
                        Console.Write("Choose a chore to assign: ");
                        int chosenChore = int.Parse(Console.ReadLine());
                        List<Roommate> roommateList = roommateRepo.GetAll();
                        foreach (Roommate roommate1 in roommateList)
                        {
                            Console.WriteLine($"{roommate1.Id}. {roommate1.FirstName} {roommate1.LastName}");
                        }
                        Console.Write("Assign that chore to a roommate: ");
                        int chosenRoommate = int.Parse(Console.ReadLine());
                        choreRepo.AssignChore(chosenRoommate, chosenChore);
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Show all rooms"):
                        List<Room> rooms = roomRepo.GetAll();
                        foreach (Room r in rooms)
                        {
                            Console.WriteLine($"{r.Name} has an Id of {r.Id} and a max occupancy of {r.MaxOccupancy}");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Show all unassigned chores"):
                        List<Chore> UnassignedChores = choreRepo.GetUnassignedChores();
                        foreach (Chore UnassignedChore in UnassignedChores)
                        {
                            Console.WriteLine($"{UnassignedChore.Name} is not being done by any roomates");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Search for room"):
                        Console.Write("Room Id: ");
                        int id = int.Parse(Console.ReadLine());

                        Room room = roomRepo.GetById(id);

                        Console.WriteLine($"{room.Id} - {room.Name} Max Occupancy({room.MaxOccupancy})");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Search for roommate"):
                        Console.Write("Roommate Id: ");
                        int roommateId = int.Parse(Console.ReadLine());

                        Roommate roommate = roommateRepo.GetById(roommateId);
                        if (roommate.Room == null)
                        {
                            Console.WriteLine($"{roommate.FirstName} pays {roommate.RentPortion} percent but isn't assigned a room!");
                        } else
                        {
                        Console.WriteLine($"{roommate.FirstName} pays {roommate.RentPortion} percent of rent and occupies {roommate.Room.Name}");

                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Search for chore"):
                        Console.Write("Chore Id: ");
                        int choreId = int.Parse(Console.ReadLine());

                        Chore chore = choreRepo.GetById(choreId);
                        Console.WriteLine($"{chore.Id} - {chore.Name}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Add a room"):
                        Console.Write("Room name: ");
                        string name = Console.ReadLine();

                        Console.Write("Max occupancy: ");
                        int max = int.Parse(Console.ReadLine());

                        Room roomToAdd = new Room()
                        {
                            Name = name,
                            MaxOccupancy = max
                        };

                        roomRepo.Insert(roomToAdd);

                        Console.WriteLine($"{roomToAdd.Name} has been added and assigned an Id of {roomToAdd.Id}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Show all Chores"):
                        List<Chore> chores = choreRepo.GetAll();
                        foreach (Chore c in chores)
                        {
                            Console.WriteLine($"{c.Name} has an id of {c.Id}");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Exit"):
                        runProgram = false;
                        break;

                }
            }

        }

        static string GetMenuSelection()
        {
            Console.Clear();

            List<string> options = new List<string>()
            {
                "Show all rooms",
                "Search for room",
                "Update a room",
                "Add a room",
                "Delete a room",
                "Show all Chores",
                "Search for chore",
                "Show all unassigned chores",
                "Assign a chore to a roommate",
                "Delete a chore",
                "Update a chore",
                "Search for roommate",
                "Exit"
            };

            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }

            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("Select an option > ");

                    string input = Console.ReadLine();
                    int index = int.Parse(input) - 1;
                    return options[index];
                }
                catch (Exception)
                {

                    continue;
                }
            }
        }
    }
}