﻿using AspNetCoreHero.Boilerplate.Application.DTOs.Entities.Catalog;
using AspNetCoreHero.Boilerplate.Application.Interfaces.Repositories;
using AspNetCoreHero.Boilerplate.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly IRepositoryAsync<Brand> _repository;
        private readonly IDistributedCache _distributedCache;

        public BrandRepository(IDistributedCache distributedCache, IRepositoryAsync<Brand> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<Brand> Brands => _repository.Entities;

        public async Task<List<BrandDto>> GetListAsync()
        {
            var brandList = await _repository.Entities.Select(d => new BrandDto
            {
                Id = d.Id,
                Name = d.Name,
                Tax = d.Tax
            }).ToListAsync();

            return brandList;
        }
    }
}
