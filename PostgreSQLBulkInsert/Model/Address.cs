using LinqToDB.Mapping;
using System;

namespace PostgreSQLBulkInsert
{
    [Table("Addresses")]
    public class Address
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        public Person Person { get; set; }

        [Column]
        public Guid PersonId { get; set; }

        [Column]
        public string Street { get; set; }

        [Column]
        public string Number { get; set; }

        [Column]
        public string PostCode { get; set; }
    }
}