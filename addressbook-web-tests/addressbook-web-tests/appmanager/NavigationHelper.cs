using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace addressbook_web_tests
{
    public class NavigationHelper : HelperBase
    {
        private string baseURL;

        public NavigationHelper(ApplicationManager manager, string baseURL) : base(manager)
        {
            this.baseURL = baseURL;
        }
        public void OpenHomePage()
        {
            if (driver.Url == baseURL + "/addressbook")
            {
                return;
            }
                driver.Navigate().GoToUrl(baseURL + "/addressbook");
        }
        public void GoToGroupsPage()
        {
            if (driver.Url == baseURL + "/addressbook/group.php"
                && IsElementPresent(By.Name("new")))
            {
                return;
            }
            driver.FindElement(By.LinkText("groups")).Click();
        }
        public void GoToContactsPage()
        {
            if (driver.Url == baseURL + "/addressbook"
                && IsElementPresent(By.XPath("//input[@value='Send e-Mail']")))
            {
                return;
            }
                driver.FindElement(By.LinkText("home")).Click();
        }
    }
}
