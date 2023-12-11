﻿using Microsoft.EntityFrameworkCore;

namespace TaHooK.IdentityProvider.DAL.Migrators;

public class SqlDbMigrator : IDbMigrator
{
    private readonly IDbContextFactory<IdentityProviderDbContext> _dbContextFactory;

    public SqlDbMigrator(IDbContextFactory<IdentityProviderDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public void Migrate(bool isDev = false)
    {
        MigrateAsync(CancellationToken.None, isDev).GetAwaiter().GetResult();
    }

    public async Task MigrateAsync(CancellationToken cancellationToken, bool isDev = false)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        if (isDev)
        {
            await dbContext.Database.EnsureDeletedAsync(cancellationToken);
        }
        await dbContext.Database.MigrateAsync(cancellationToken);
        
        // await dbContext.SeedDatabaseAsync();
    }
}
