using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_tests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string firstname;
        private string middlename ="";

        public ContactData(string firstname)
        {
            this.firstname = firstname;
        }

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return ((Firstname + Middlename) == (other.Firstname + other.Middlename));
        }

        public override int GetHashCode()
        {
            return (Firstname + Middlename).GetHashCode();
        }

        public override string ToString()
        {
            return "surname = " + Firstname 
                 + "\n name = " + Middlename;
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            if (Firstname.CompareTo(other.Firstname) == 0)
            {
                return Middlename.CompareTo(other.Middlename);
            }
            return Firstname.CompareTo(other.Firstname);

        }
        public string Firstname
        {
            get { return firstname; }
            set { firstname = value; }
        }

        public string Middlename
        {
            get { return middlename; }
            set { middlename = value; }
        }
    }
}
