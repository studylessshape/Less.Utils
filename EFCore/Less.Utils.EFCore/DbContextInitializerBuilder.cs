using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Less.Utils.EFCore
{
    internal class DbContextInitializerBuilder : IDbContextInitializerBuilder
    {
        internal List<Type> DbContextTypes { get; }

        public DbContextInitializerBuilder()
        {
            DbContextTypes = new List<Type>();
        }

        public IDbContextInitializerBuilder RegistDbContext<TDbContext>() where TDbContext : DbContext
        {
            DbContextTypes.Add(typeof(TDbContext));
            return this;
        }
    }
}
