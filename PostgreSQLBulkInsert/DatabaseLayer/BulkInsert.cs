using LinqToDB.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PostgreSQLBulkInsert
{
    public class DatabaseOperations
    {
        private readonly PostgresDataConnectionForLinq2Db _connection;

        public DatabaseOperations(PostgresDataConnectionForLinq2Db connection)
        {
            _connection = connection;
        }

        public async Task BulkInsert(IEnumerable<Person> persons, CancellationToken cancellationToken)
        {
            await _connection.BulkCopyAsync(persons, cancellationToken);
            var addresses = persons.SelectMany(x => x.Addresses);
            await _connection.BulkCopyAsync(addresses.ToList(), cancellationToken);
        }
    }
}