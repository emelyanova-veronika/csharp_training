using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests
{
    [TestFixture]
    public class GroupModificationTests : GroupTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            GroupData newData = new GroupData("asdasd");
            newData.Header = null;
            newData.Footer = null;

            
            app.Navigator.GoToGroupsPage();
            if (!app.Groups.ExistGroupVerification())
            {
                app.Groups.Create(new GroupData("888"));
            }

            List<GroupData> oldGroups = GroupData.GetAll();
            GroupData toBeChanged = oldGroups[0];
            app.Groups.Modify(toBeChanged, newData);

            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());

            List<GroupData> newGroups = GroupData.GetAll();
            oldGroups[0].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                if (group.Id == toBeChanged.Id)
                {
                    Assert.AreEqual(newData.Name, group.Name);
                }
            }
        }
    }
}
