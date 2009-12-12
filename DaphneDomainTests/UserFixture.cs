using DaphneDomain;
using MbUnit.Framework;
using System.Linq;
using NDbUnit.Core;
using NDbUnit.Core.SqlClient;


namespace DaphneDomainTests
{
    public class FixtureBase
    {
        [SetUp]
        public void SetUp()
        {
            WipeOutDatabase();
        }

        private static void WipeOutDatabase()
        {
            SqlDbUnitTest testDb = new SqlDbUnitTest(@"Data Source=ISPG341\SQLEXPRESS;Initial Catalog=Daphne;Integrated Security=True");
            testDb.ReadXmlSchema(@"..\..\database.xsd");
            testDb.PerformDbOperation(DbOperationFlag.DeleteAll);
        }
    }

    [TestFixture]
    public class UserFixture : FixtureBase
    {
        [Test]
        public void should_be_able_to_save_user()
        {
            using (var context = new DaphneEntities())
            {
                

                User user = new User();
                user.UserName = "suhair";
                user.Password = "password";
                context.AddToUsers(user);
                context.SaveChanges();

                var saveduser = ((from u in context.Users
                  select u)).FirstOrDefault();

                Assert.IsNotNull(saveduser, "The user object was failed to save in database");

            }
            

        }
    }
}
