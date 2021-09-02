using LinqToDB.Mapping;
using System;
using System.Collections.Generic;

namespace PostgreSQLBulkInsert
{
    [Table("Persons")]
    public class Person
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        [Column]
        public string FirstName { get; set; }

        [Column]
        public string SecondName { get; set; }

        [Column]
        public DateTime DateOfBirth { get; set; }

        [Association(ThisKey = nameof(Id), OtherKey = nameof(Address.PersonId))]
        public List<Address> Addresses { get; set; }
    }
}