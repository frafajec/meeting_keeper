using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace meeting_keeper.Models
{
    public class sampleData
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            var _dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();          

            if (!_dbContext.Client.Any())
            {
                _dbContext.Client.AddRange(
                    new Client()
                    {
                        dateCreated = 0,
                        dateModified = 0,
                        name = "Joe Budder",
                        numberOfContracts = 2,
                        address = "First street, New York",
                        email = "joe.budder@gmail.com",
                        earliestDate = 12
                    },
                    new Client()
                    {
                        dateCreated = 0,
                        dateModified = 0,
                        name = "Franz Meihn",
                        numberOfContracts = 13,
                        address = "Beerstraße, Munich",
                        email = "franz.meihn@gmail.com",
                        earliestDate = 0
                    },
                    new Client()
                    {
                        dateCreated = 0,
                        dateModified = 0,
                        name = "Ivan Ivanovich",
                        numberOfContracts = 0,
                        address = "Staljinov trg, Moscow",
                        email = "ivan.the.russian@gmail.com",
                        earliestDate = 0
                    },
                    new Client()
                    {
                        dateCreated = 0,
                        dateModified = 0,
                        name = "Philiph Track",
                        numberOfContracts = 3,
                        address = "Queens alley, London",
                        email = "philiph.track@gmail.com",
                        earliestDate = 0
                    }
                );

                _dbContext.SaveChanges();
            }

            if (!_dbContext.Contract.Any())
            {
                _dbContext.Contract.AddRange(
                    new Contract()
                    {
                        dateCreated = 0,
                        dateModified = 0,
                        name = "Joe Budder's private"
                    },
                    new Contract()
                    {
                        dateCreated = 0,
                        dateModified = 0,
                        name = "Franz Meihn's private"
                    },
                    new Contract()
                    {
                        dateCreated = 0,
                        dateModified = 0,
                        name = "Ivan Ivanovich proposal"
                    }
                );

                _dbContext.SaveChanges();
            }

        }

    }
}
