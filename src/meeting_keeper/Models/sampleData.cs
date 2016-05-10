using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.Entity;

namespace meeting_keeper.Models
{
    public class sampleData
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApplicationDbContext>();

            context.Database.Migrate();
            if (!context.Client.Any())
            {

                context.Client.Add(
                    new Client()
                    {
                        id = 1,
                        dateCreated = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond,
                        dateModified = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond,
                        name = "Client 1"
                    }
                );

                //context.Client.AddRange(
                    
                //    new Client()
                //    {
                //        id = 2,
                //        //dateCreated = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond,
                //        //dateModified = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond,
                //        name = "Cient 2"
                //    },
                //    new Client()
                //    {
                //        id = 3,
                //        //dateCreated = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond,
                //        //dateModified = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond,
                //        name = "BLABLA"
                //    },
                //    new Client()
                //    {
                //        id = 4,
                //        //dateCreated = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond,
                //        //dateModified = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond,
                //        name = "Last client"
                //    }
                //);

                context.SaveChanges();
            }
        }

    }
}
