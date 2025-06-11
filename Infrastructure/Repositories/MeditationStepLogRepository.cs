using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using System;

namespace Infrastructure.Repositories
{
    public class MeditationStepLogRepository(RelaxifyDbContext context) : GenericRepository<MeditationStepLog>(context), IMeditationStepLogRepository
    {

    }

}
