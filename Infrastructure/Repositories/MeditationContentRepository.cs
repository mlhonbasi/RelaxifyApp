using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class MeditationContentRepository(RelaxifyDbContext context) :GenericRepository<MeditationContent>(context), IMeditationContentRepository
    {
    }
}
