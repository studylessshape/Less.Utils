using Microsoft.EntityFrameworkCore;

namespace Less.Utils.EFCore
{
    public interface IDbContextInitializerBuilder
    {
        IDbContextInitializerBuilder RegistDbContext<TDbContext>() where TDbContext : DbContext;
    }
}
