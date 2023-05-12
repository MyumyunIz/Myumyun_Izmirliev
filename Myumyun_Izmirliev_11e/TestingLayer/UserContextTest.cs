using BusinessLayer;
using DataLayer;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingLayer
{
    [TestFixture]
    public class UserContextTest
    {
        private UserContext context = new UserContext(SetupFixture.dbContext);
        private User user;
        private Game g1, g2;

        [SetUp]
        public void CreateUser()
        {
            user = new User("Hektor","Daradjanski",32,"Hektor","123456789","hektor@gmail.com");
            g1 = new Game("Monopoly");
            g2 = new Game("Chess");

            user.Games.Add(g1);
            user.Games.Add(g2);

            context.Create(user);
        
        }
        [TearDown]
        public void DropUser()
        {
            foreach (User item in SetupFixture.dbContext.Users)
            {
                SetupFixture.dbContext.Users.Remove(item);
            }

            SetupFixture.dbContext.SaveChanges();
        }
        [Test]
        public void Create()
        {
            User newUser = new User("Ivan", "Iliev", 25, "VIV", "123456789", "IvanIliev@gmail.com");

 
            int usersBefore = SetupFixture.dbContext.Users.Count();
            context.Create(newUser);

            
            int usersAfter = SetupFixture.dbContext.Users.Count();
            Assert.IsTrue(usersBefore + 1 == usersAfter, "Create() does not work!");
        }

        [Test]
        public void Read()
        {
            User readUser = context.Read(user.ID);

            Assert.AreEqual(user, readUser, "Read does not return the same object!");
        }

        [Test]
        public void ReadWithNavigationalProperties()
        {
            User readUser = context.Read(user.ID, true);

            Assert.That(readUser.Games.Contains(g1) && readUser.Games.Contains(g2), "G1 and G2 is not in the Games list!");
            
        }
        [Test]
        public void ReadAll()
        {
            List<User> users = (List<User>)context.ReadAll();

            Assert.That(users.Count != 0, "ReadAll() does not return brands!");
        }
        [Test]
        public void Update()
        {
            User changedUser = context.Read(user.ID);

            changedUser.Name = "U" + user.Name;
            changedUser.Email = "Darajanski@gmail.com";

            context.Update(changedUser);
            
            
            user = context.Read(user.ID);

            Assert.AreEqual(changedUser, user, "Update() does not work!");
        }
        [Test]
        public void Delete()
        {
            int usersBefore = SetupFixture.dbContext.Users.Count();

            context.Delete(user.ID);
            int usersAfter = SetupFixture.dbContext.Users.Count();

            Assert.IsTrue(usersBefore - 1 == usersAfter, "Delete() does not work! 👎🏻");
        }
        public void TestMethod()
        {
            // TODO: Add your test code here
            var answer = 42;
            Assert.That(answer, Is.EqualTo(42), "Some useful error message");
        }
    }
}
