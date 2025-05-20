using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class StressAnswerRepository(RelaxifyDbContext context) : GenericRepository<StressAnswer>(context), IStressAnswerRepository
    {

    }
}
