using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using addressbook_web_tests;


namespace addressbook_test_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            string type = args[0];
            int count = Convert.ToInt32(args[1]);
            StreamWriter writer = new StreamWriter(args[2]);
            string format = args[3];

            if (type == "group")
            {
                List<GroupData> groups = new List<GroupData>();
                for (int i = 0; i < count; i++)
                {
                    groups.Add(new GroupData(TestBase.GenerateRandomString(10))
                    {
                        Header = TestBase.GenerateRandomString(20),
                        Footer = TestBase.GenerateRandomString(20)
                    });
                }

                if (format == "csv")
                {
                    writeGroupsToCsv(groups, writer);
                }
                else if (format == "xml")
                {
                    writeGroupsToXml(groups, writer);
                }
                else
                {
                    System.Console.Out.Write("ERROR" + format);
                }
            }
            else if (type == "contact")
            {
                List<ContactData> contacts = new List<ContactData>();
                for (int i = 0; i < count; i++)
                {
                    contacts.Add(new ContactData(TestBase.GenerateRandomString(10), TestBase.GenerateRandomString(10)));
                }

                if (format == "csv")
                {
                    writeContactsToCsv(contacts, writer);
                }
                else if (format == "xml")
                {
                    writeContactsToXml(contacts, writer);
                }
                else
                {
                    System.Console.Out.Write("ERROR" + format);
                }
            }
            else
            {
                System.Console.Out.Write("ERROR" + type);
            }
                writer.Close();
        }

        static void writeGroupsToCsv(List<GroupData> groups, StreamWriter writer)
        {
            foreach (GroupData group in groups)
            {
                writer.WriteLine(String.Format("${0},${1},${2}",
                    group.Name, group.Header, group.Footer));
            }
        }

        static void writeGroupsToXml(List<GroupData> groups, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(writer,groups);
        }

        static void writeContactsToXml(List<ContactData> contacts, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<ContactData>)).Serialize(writer, contacts);
        }
        static void writeContactsToCsv(List<ContactData> contacts, StreamWriter writer)
        {
            foreach (ContactData contact in contacts)
            {
                writer.WriteLine(String.Format("${0},${1}",
                    contact.Firstname, contact.Lastname));
            }
        }
    }
}
