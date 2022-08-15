using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
namespace SqliteUI
{
    class Program
    {
        static void Main(string[] args)
        {
            SqliteCrud sql = new SqliteCrud(GetConnectionString());

  
            //ReadContact(sql, 2);

            //CreateNewContact(sql);

            //UpdateContact(sql);
            //ReadAllContacts(sql);

            RemovePhoneNumberFromContact(sql, 1, 1);

            Console.WriteLine("The SQLite process is done; press any key.");
            Console.ReadKey();
        }

        private static void RemovePhoneNumberFromContact(SqliteCrud sql, int contactId, int phoneNumberId)
        {
            sql.RemovePhoneNumberFromContact(contactId, phoneNumberId);
        }

        private static void UpdateContact(SqliteCrud sql)
        {
            BasicContactModel contact = new BasicContactModel
            {
                Id = 1,
                FirstName = "Daniel",
                LastName = "Krefft"
            };
            sql.UpdateContactName(contact);
        }

        private static void CreateNewContact(SqliteCrud sql)
        {
            FullContactModel user = new FullContactModel
            {
                BasicInfo = new BasicContactModel
                {
                    FirstName = "Patryk",
                    LastName = "Krefft"
                }
            };

            user.EmailAddresses.Add(new EmailAddressModel { EmailAddress = "pat@wp.pl" });
            user.EmailAddresses.Add(new EmailAddressModel { Id = 2, EmailAddress = "me@gmail.com" });

            user.PhoneNumbers.Add(new PhoneNumberModel { Id = 1, PhoneNumber = "48-121212121" });
            user.PhoneNumbers.Add(new PhoneNumberModel { PhoneNumber = "48-989898989" });

            sql.CreateContact(user);
        }

        private static void ReadAllContacts(SqliteCrud sql)
        {
            var rows = sql.GetAllContacts();

            foreach (var row in rows)
            {
                Console.WriteLine($"{ row.Id }: { row.FirstName } {row.LastName }");
            }
        }

        private static void ReadContact(SqliteCrud sql, int contactId)
        {
            var contact = sql.GetFullContactById(contactId);

            Console.WriteLine($"{ contact.BasicInfo.Id }: { contact.BasicInfo.FirstName } {contact.BasicInfo.LastName }");

        }
        private static string GetConnectionString(string connectionStringName = "Default")
        {
            string output = "";

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();

            output = config.GetConnectionString(connectionStringName);

            return output;
        }
    }
}
