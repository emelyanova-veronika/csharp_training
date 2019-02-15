using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests
{
    [TestFixture]
    public class ContactModificationTests : ContactTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            ContactData newData = new ContactData("modification", null);

            app.Navigator.GoToContactsPage();

            if (!app.Contacts.ExistContactVerification())
            {
                app.Contacts.Create(new ContactData("555", "666"));
            }

            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData toBeChanged = oldContacts[0];
            app.Contacts.Modify(toBeChanged, newData);

            Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactCount());

            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts[0].Firstname = newData.Firstname;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                if (contact.Id == toBeChanged.Id)
                {
                    Assert.AreEqual(newData.Firstname, contact.Firstname);
                }
            }
        }
    }
}
