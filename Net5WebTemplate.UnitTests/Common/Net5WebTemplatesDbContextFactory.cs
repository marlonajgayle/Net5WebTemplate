using Microsoft.EntityFrameworkCore;
using Net5WebTemplate.Domain.Entities;
using Net5WebTemplate.Domain.ValueObjects;
using Net5WebTemplate.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net5WebTemplate.UnitTests.Common
{
    public class Net5WebTemplatesDbContextFactory
    {
        public static Net5WebTemplateDbContext Create()
        {
            var options = new DbContextOptionsBuilder<Net5WebTemplateDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var _context = new Net5WebTemplateDbContext(options);

            _context.Database.EnsureCreated();

            _context.Profiles.AddRange(new[]
            {
               new Profile
               {           
                    FirstName = "Bob",
                    LastName = "Marley",
                    Address= new Address("102","Shanty Town","Kingston"),
                    PhoneNumber = "8769873456"
               },
               new Profile
               {
                    FirstName = "Bob",
                    LastName = "Narley",
                    Address= new Address("103","Shanty Town","Kingston"),
                    PhoneNumber = "8769873456"
               },
               new Profile
               {
                    FirstName = "Bill",
                    LastName = "Marley",
                    Address= new Address("104","Shanty Town","Kingston"),
                    PhoneNumber = "8769873456"
               }
            });

            _context.SaveChanges();

            return _context;
        }

        public static void Destroy(Net5WebTemplateDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
