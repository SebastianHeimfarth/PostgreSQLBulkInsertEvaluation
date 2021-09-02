using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PostgreSQLBulkInsert
{
    public class Tests
    {
        private Dependencies _dependencies;

        [SetUp]
        public void SetUp()
        {
            _dependencies = new Dependencies();
        }

        [Test]
        [TestCase(10, 1000)]
        [TestCase(100, 1000)]
        [TestCase(500, 5000)]
        [TestCase(1000, 10000)]
        public async Task TestBulkInsertPerformance(int numberOfPersons, int maxDurationInMilliseconds)
        {
            //arrage
            using var dataConnectiopn = _dependencies.GetService<PostgresDataConnectionForLinq2Db>();

            var sut = new DatabaseOperations(dataConnectiopn);
            var transaction = dataConnectiopn.BeginTransaction();

            var persons = CreatePersons(numberOfPersons).ToList();

            
            try
            {
                //act
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                await sut.BulkInsert(persons, CancellationToken.None);
                stopWatch.Stop();

                Console.WriteLine($"Time needed: {stopWatch.ElapsedMilliseconds} ms");

                //assert
                stopWatch.ElapsedMilliseconds.ShouldBeLessThanOrEqualTo(maxDurationInMilliseconds);
            }
            finally
            {
                await transaction.RollbackAsync();
            }
        }

        private IEnumerable<Person> CreatePersons(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return CreatePerson(i);
            }
        }

        private static Person CreatePerson(int counter)
        {
            var person = new Person()
            {
                Id = Guid.NewGuid(),
                FirstName = $"Sebastian_{counter}",
                SecondName = $"Sebastianson_{counter}",
                DateOfBirth = DateTime.UtcNow,
                Addresses = new List<Address>()
            };

            for (int i = 0; i < 100; i++)
            {
                var address = new Address
                {
                    Id = Guid.NewGuid(),
                    Person = person,
                    PersonId = person.Id,
                    Number = i.ToString(),
                    Street = "Sesamstreet",
                    PostCode = "523123"
                };

                person.Addresses.Add(address);
            }

            return person;
        }
    }
}