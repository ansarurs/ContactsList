using Contacts.Data;
using Contacts.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Contacts.Test
{
    public class ServiceTest
    {
        private IContactsService repository;
        public static DbContextOptions<ContactsDbContext> dbContextOptions { get; }
        public static string connectionString = "server=(localdb)\\MSSQLLocalDB;database=ContactsDb;trusted_connection=true";
        ContactsDbContext context = null;


        static ServiceTest()    
        {
            dbContextOptions = new DbContextOptionsBuilder<ContactsDbContext>()
                .UseSqlServer(connectionString)
                .Options;
        }
        public ServiceTest()
        {
            context = new ContactsDbContext(dbContextOptions);
            DummyDataDBInitializer db = new DummyDataDBInitializer();
            db.Seed(context);

            repository = new ContactsService(context);
        }

        [Fact]
        public async void Task_GetContacts_Return_OkResult()
        {
            //Arrange
            var service = new ContactsService(context);
           
            //Act
            var data = await service.GetContacts();

            //Assert
            Assert.NotNull(data);
        }

        [Fact]
        public async void Task_GetContacts_Return_NotFoundResult()
        {
            //Arrange
            var service = new ContactsService(context);
            var contact = 13;

            //Act
            var data = await service.FindContact(contact);

            //Assert
            Assert.Null(data);
        }

        //[Fact]
        //public async void Task_GetPostById_Return_BadRequestResult()
        //{
        //    //Arrange
        //    var controller = new PostController(repository);
        //    int? postId = null;

        //    //Act
        //    var data = await controller.GetPost(postId);

        //    //Assert
        //    Assert.IsType<BadRequestResult>(data);
        //}

        [Fact]
        public async void Task_GetPostById_MatchResult()
        {
            //Arrange
            var controller = new ContactsService(context);
            int postId = 1;

            //Act
            var data = await controller.FindContact(postId);

            //Assert
            Assert.NotNull(data);

         
        }
    }
}
