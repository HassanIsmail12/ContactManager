
using System;
using System.Collections.Generic;
using System.IO;

namespace ContactManager
{
    class Program
    {
        private static readonly string filePath = "contacts.txt";

        static void Main(string[] args)
        {
            var contacts = LoadContacts();
            bool running = true;

            while (running)
            {
                Console.Clear();
                PrintHeader("Contact Manager");
                Console.WriteLine("1. View Contacts");
                Console.WriteLine("2. Add Contact");
                Console.WriteLine("3. Remove Contact");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ViewContacts(contacts);
                        break;
                    case "2":
                        AddContact(contacts);
                        break;
                    case "3":
                        RemoveContact(contacts);
                        break;
                    case "4":
                        running = false;
                        break;
                    default:
                        PrintError("Invalid option. Please enter a number between 1 and 4.");
                        break;
                }
            }

            SaveContacts(contacts);
        }

        static void PrintHeader(string title)
        {
            Console.WriteLine(new string('-', 30));
            Console.WriteLine(title.ToUpper());
            Console.WriteLine(new string('-', 30));
        }

        static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.WriteLine("Press any key to return to the main menu.");
            Console.ReadKey();
        }

        static List<string> LoadContacts()
        {
            var contacts = new List<string>();
            if (File.Exists(filePath))
            {
                contacts.AddRange(File.ReadAllLines(filePath));
            }
            return contacts;
        }

        static void SaveContacts(List<string> contacts)
        {
            File.WriteAllLines(filePath, contacts);
        }

        static void ViewContacts(List<string> contacts)
        {
            Console.Clear();
            PrintHeader("Contacts List");

            if (contacts.Count == 0)
            {
                Console.WriteLine("No contacts found.");
            }
            else
            {
                for (int i = 0; i < contacts.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {contacts[i]}");
                }
            }

            Console.WriteLine("\nPress any key to return to the main menu.");
            Console.ReadKey();
        }

        static void AddContact(List<string> contacts)
        {
            Console.Clear();
            PrintHeader("Add New Contact");

            Console.Write("Enter the contact's name: ");
            string name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name))
            {
                contacts.Add(name);
                PrintMessage("Contact added successfully.");
            }
            else
            {
                PrintError("Invalid name. Contact was not added.");
            }

            Console.ReadKey();
        }

        static void RemoveContact(List<string> contacts)
        {
            Console.Clear();
            PrintHeader("Remove Contact");
            ViewContacts(contacts);

            if (contacts.Count > 0)
            {
                Console.Write("\nEnter the number of the contact to remove: ");

                if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= contacts.Count)
                {
                    contacts.RemoveAt(index - 1);
                    PrintMessage("Contact removed successfully.");
                }
                else
                {
                    PrintError("Invalid contact number. No contact was removed.");
                }
            }
            else
            {
                PrintMessage("No contacts available to remove.");
            }

            Console.ReadKey();
        }

        static void PrintMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
