using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace addressbook_web_tests
{
    [TestFixture]
    public class ContactCreationtTests : TestBase
    {
        
        [Test]
        public void ContactCreationtTest()
        {
            ContactData contact = new ContactData("ccc");
            contact.Middlename = "qqq";

            app.Contacts.Create(contact);
        }

        [Test]
        public void EmptyContactCreationtTest()
        {
            ContactData contact = new ContactData("");
            contact.Middlename = "";

            app.Contacts.Create(contact);
        }
    }
}
