using Contacts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Test
{
    public class DummyDataDBInitializer
    {
        public DummyDataDBInitializer()
        {
        }

        public void Seed(ContactsDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.contacts.AddRange(
                new dbContact() { Name = "CSHARP", FirstName = "csharp" },
                new dbContact() { Name = "VISUAL STUDIO", FirstName = "visualstudio" },
                new dbContact() { Name = "ASP.NET CORE", FirstName = "aspnetcore" },
                new dbContact() { Name = "SQL SERVER", FirstName = "sqlserver" }
            );

        
            context.SaveChanges();
        }

    }   
}
