using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests
{
    public class RemoveContactFromGroup : AuthTestBase
    {
        [Test]
        public void TestRemoveContactFromGroup()
        {
            List <GroupData> list_g = GroupData.GetAll();
            if (list_g.Count() == 0)
            {
                app.Groups.Create(new GroupData("help"));
            }
            List<ContactData> list_c = ContactData.GetAll();
            if (list_c.Count() == 0)
            {
                app.Contacts.Create(new ContactData("help", "me"));
            }
            
            GroupData checkGroup = GroupData.GetAll()[0];
            List<ContactData> checkList = checkGroup.GetContacts();
            ContactData checkContacts = ContactData.GetAll()[0];
            if (checkList.Count() == 0)
            {
                app.Contacts.AddContactToGroup(checkContacts, checkGroup);
            }

            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();
            //ContactData contact = ContactData.GetAll().Except(oldList).First();
            ContactData contact = ContactData.GetAll()[0];

            app.Contacts.RemoveContactFromGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Remove(contact);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);

        }
        
    }
}
