using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace addressbook_web_tests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }


        public ContactHelper Create(ContactData contact)
        {
            InitNewContact();
            FillContactsForm(contact);
            SubmitNewContact();
            ReturnToContactsForm();
            return this;
        }

        public void AddContactToGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.GoToContactsPage();
            ClearGroupFilter();
            SelectContact(contact.Id);
            SelectGroupToAdd(group.Name);
            CommitAddingContactToGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);

        }

        public void RemoveContactFromGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.GoToContactsPage();
            ClearGroupFilter();
            OpenViewForm(contact.Id);
            OpenGroupFormWithContacts(group.Id);
            SelectContact(contact.Id);
            RemoveContactFromGroup();
           // ReturnToGroupFormWithContacts();
        }

        /*public void ReturnToGroupFormWithContacts()
        {
            driver.FindElement(By.LinkText("home")).Click();
        }*/

        public void RemoveContactFromGroup()
        {
            driver.FindElement(By.Name("remove")).Click();
        }

        public void OpenGroupFormWithContacts(String id)
        {
            driver.FindElement(By.TagName("i")).FindElement(By.Id(id)).Click();
        }
        public ContactHelper OpenViewForm(String id)
        {
            driver.FindElements(By.Name("entry"))[Convert.ToInt32(id)].FindElements(By.TagName("td"))[6].FindElement(By.TagName("a")).Click();
            return this;
        }

        public void CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        public void SelectGroupToAdd(string groupName)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(groupName);
        }

        public void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }

        public ContactHelper Remove(int v)
        {
            manager.Navigator.GoToContactsPage();
            SelectContact(v);
            RemoveContact();
            SubmitRemoveContact();
            ReturnToContactsForm();
            return this;
        }

        public ContactHelper Remove(ContactData contact)
        {
            manager.Navigator.GoToContactsPage();
            SelectContact(contact.Id);
            RemoveContact();
            SubmitRemoveContact();
            ReturnToContactsForm();
            return this;
        }

        public ContactHelper Modify(int v, ContactData newData)
        {
            manager.Navigator.GoToContactsPage();
            
            InitContactModification(v);
            FillContactsForm(newData);
            SubmitContactModification();
            ReturnToContactsForm();
            return this;
        }

        public ContactHelper Modify(ContactData contact, ContactData newData)
        {
            manager.Navigator.GoToContactsPage();

            InitContactModification(contact.Id);
            FillContactsForm(newData);
            SubmitContactModification();
            ReturnToContactsForm();
            return this;
        }
        public bool ExistContactVerification()
        {
            return IsElementPresent(By.CssSelector("tr[name='entry']"));
        }
        
        public ContactHelper InitContactModification(int v)
        {
            driver.FindElements(By.Name("entry"))[v].FindElements(By.TagName("td"))[7].FindElement(By.TagName("a")).Click();
            //driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + (v + 1) + "]")).Click();
            return this;
        }
        public ContactHelper InitContactModification(String id)
        {
            driver.FindElements(By.Name("entry"))[Convert.ToInt32(id)].FindElements(By.TagName("td"))[7].FindElement(By.TagName("a")).Click();
            //driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + id + "]")).Click();
            return this;
        }
        public ContactHelper OpenViewForm(int index)
        {
            driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"))[6].FindElement(By.TagName("a")).Click();
            return this;
        }
        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this;
        }
        public ContactHelper ReturnToContactsForm()
        {
            driver.FindElement(By.LinkText("home")).Click();
            return this;
        }

        public ContactHelper InitNewContact()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }
        public ContactHelper FillContactsForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("lastname"), contact.Lastname);
            return this;
        }

        public ContactHelper SubmitNewContact()
        {
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Notes:'])[1]/following::input[1]")).Click();
            contactCache = null;
            return this;
        }
        
        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index+1) + "]")).Click();
            return this;
        }

        public ContactHelper SelectContact(String id)
        {
            //driver.FindElement(By.XPath("(//input[@name='selected[]' and  @value='" + (Convert.ToInt32(id)) + "'])")).Click();
            //driver.FindElement(By.XPath("(//input[@name='selected[]' and  @value='" + id + "'])")).Click();
            driver.FindElement(By.Id(id)).Click();
            return this;
        }
        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            Thread.Sleep(1000);
            return this;
        }
        public ContactHelper SubmitRemoveContact()
        {
            driver.SwitchTo().Alert().Accept();
            contactCache = null;
            return this;
        }
        private List<ContactData> contactCache = null;

        public List<ContactData> GetContactList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();
                manager.Navigator.GoToContactsPage();

                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("tr[name='entry']"));
                foreach (IWebElement element in elements)
                {
                    var cells = element.FindElements(By.CssSelector("td"));
                    contactCache.Add(new ContactData(cells[2].Text, cells[1].Text)
                    {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                    });
                }
            }

            return new List<ContactData> (contactCache);
        }


        public int GetContactCount()
        {
            return driver.FindElements(By.CssSelector("tr[name='entry']")).Count;
        }

        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.OpenHomePage();
            InitContactModification(index);
            string firstname = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastname = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");

            string firstEmail = driver.FindElement(By.Name("email")).GetAttribute("value");
            string secondEmail = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string thirdEmail = driver.FindElement(By.Name("email3")).GetAttribute("value");

            return new ContactData(firstname, lastname)
            {
                Address = address,
                FirstEmail = firstEmail,
                SecondEmail = secondEmail,
                ThirdEmail = thirdEmail,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone
            };
        }

        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.OpenHomePage();
            IList <IWebElement> cells = driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"));
            string lastname = cells[1].Text;
            string firstname = cells[2].Text;
            string address = cells[3].Text;
            string allEmails = cells[4].Text;
            string allPhones = cells[5].Text;
            return new ContactData(firstname, lastname)
            {
                Address = address,
                AllEmails = allEmails,
                AllPhones = allPhones
            };
        }

        public string GetContactInformationFromViewForm(int index)
        {
            manager.Navigator.OpenHomePage();
            OpenViewForm(index);
            string allText = driver.FindElement(By.Id("content")).Text;
            return CleanUpText(allText).Trim();
        }


        public string GetContactInfoFromEditFormForView(int index)
        {
            manager.Navigator.OpenHomePage();
            InitContactModification(index);
            string firstname = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastname = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");

            string firstEmail = driver.FindElement(By.Name("email")).GetAttribute("value");
            string secondEmail = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string thirdEmail = driver.FindElement(By.Name("email3")).GetAttribute("value");

            //string allInfo = firstname + lastname + address + "H: " + homePhone + "M: " + mobilePhone + "W: " + workPhone + firstEmail + secondEmail + thirdEmail;
            string allInfo = firstname + lastname + address + (string.IsNullOrEmpty(homePhone) ? "" : ("H: " + homePhone)) + (string.IsNullOrEmpty(mobilePhone) ? "" : ("M: " + mobilePhone)) + (string.IsNullOrEmpty(workPhone) ? "" : ("W: " + workPhone)) + firstEmail + secondEmail + thirdEmail;
            return CleanUpText(allInfo).Trim();
        }
        private string CleanUpText(string text)
        {
            if (text == null || text == "")
            {
                return "";
            }
            //return Regex.Replace(text, "[ -()H:M:W:]\r\n", "");
            return Regex.Replace(text, "[ -()\r\n]", "");
        }

        
    }
}
