using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace addressbook_web_tests
{
    [Table(Name = "address_in_groups")]
    public class GroupContactRelation
    {
        [Column(Name = "group_id")]
        public String GroupId { get; set; }

        [Column(Name = "id")]
        public String ContactId { get; set; }

    }
}
