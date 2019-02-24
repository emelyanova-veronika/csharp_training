using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace addressbook_web_tests
{
    public class AddingContactToGroupsTests : AuthTestBase
    {
        [Test]
        public void TestAddingContactToGroup()
        {
            
            List<GroupData> listOfGroup = GroupData.GetAll();
            if (listOfGroup.Count() == 0)
            {
                app.Groups.Create(new GroupData("help"));
            }
            List<ContactData> listOfContacts = ContactData.GetAll();
            if (listOfContacts.Count() == 0)
            {
                app.Contacts.Create(new ContactData("help", "me"));
            }
            
            ContactData contact1 = ContactData.GetAll()[0];
            if (GroupData.GetAll().All(x => x.GetContacts().Contains(contact1)))
            {
                app.Contacts.Create(new ContactData("111", "222"));
            }


            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();
            ContactData contact = ContactData.GetAll().Except(oldList).First();

            app.Contacts.AddContactToGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
}
