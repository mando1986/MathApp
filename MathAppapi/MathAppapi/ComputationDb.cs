using Microsoft.EntityFrameworkCore;

class ComputationDb : DbContext
{
    public ComputationDb(DbContextOptions<ComputationDb> options)
        : base(options) { }

    public DbSet<Computation> Computations => Set<Computation>();
}

