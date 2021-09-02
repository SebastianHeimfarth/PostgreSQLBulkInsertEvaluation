using LinqToDB;
using LinqToDB.Configuration;
using LinqToDB.Data;

namespace PostgreSQLBulkInsert
{
    public class PostgresDataConnectionForLinq2Db : DataConnection
    {
        public PostgresDataConnectionForLinq2Db(LinqToDbConnectionOptions<PostgresDataConnectionForLinq2Db> options)
        : base(options)
        {
        }

        internal ITable<Person> Persons => GetTable<Person>();
        internal ITable<Address> Addresses => GetTable<Address>();
    }
}