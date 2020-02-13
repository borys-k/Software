using Microsoft.EntityFrameworkCore;
using Software.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Software.Application.Tests.Common
{
    public class MainDbContextFactory
    {
        public static IMainDbContext Create()
        {
            var options = new DbContextOptionsBuilder<MainDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new MainDbContext(options);

            context.Database.EnsureCreated();

            context.SaveChanges();

            return context;
        }

        public static void Destroy(IMainDbContext context)
        {
            (context as MainDbContext).Database.EnsureDeleted();

            (context as MainDbContext).Dispose();
        }
    }
}
