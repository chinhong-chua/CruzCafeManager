﻿using CafeBackend.Domain.Entities;

namespace CafeBackend.Application.Contracts.Persistence
{
    public interface ICafeRepository : IGenericRepository<Cafe>
    {
        Task<Cafe> GetByIdAsync(Guid id);
    }
}
