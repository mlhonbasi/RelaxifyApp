using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class MusicContentRepository(RelaxifyDbContext context) : GenericRepository<MusicContent>(context),IMusicContentRepository
    {
    }
}
