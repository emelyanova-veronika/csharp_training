using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public ContactHelper Remove(int v)
        {
            manager.Navigator.GoToContactsPage();

            ExistContactVerification();
            SelectContact(v);
            RemoveContact();
            SubmitRemoveContact();
            ReturnToContactsForm();
            return this;
        }

        

        public ContactHelper Modify(int v, ContactData newData)
        {
            manager.Navigator.GoToContactsPage();

            ExistContactVerification();
            InitContactModification(v);
            FillContactsForm(newData);
            SubmitContactModification();
            ReturnToContactsForm();
            return this;
        }

        public void ExistContactVerification()
        {
            if (IsElementPresent(By.CssSelector("tr[name='entry']")))
            {
                return;
            }
            else
            {
                ContactData contact = new ContactData(".,m.m,.m,");
                contact.Middlename = "m,.m,m,";
                Create(contact);
            }
        }
        public ContactHelper InitContactModification(int v)
        {
            driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + v + "]")).Click();
            return this;
        }
        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
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
            Type(By.Name("middlename"), contact.Middlename);
            return this;
        }

        public ContactHelper SubmitNewContact()
        {
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Notes:'])[1]/following::input[1]")).Click();
            return this;
        }
        
        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + "]")).Click();
            return this;
        }
        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            return this;
        }
        public ContactHelper SubmitRemoveContact()
        {
            driver.SwitchTo().Alert().Accept();
            return this;
        }

    }
}
