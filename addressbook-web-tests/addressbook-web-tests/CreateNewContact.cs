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
            OpenHomePage();
            Login(new AccountData("admin", "secret"));
            InitNewContact();
            ContactData contact = new ContactData("ccc");
            contact.Middlename = "qqq";
            FillContactsForm(contact);
            SubmitNewContact();
            ReturnToContactsForm();
        }
    }
}
