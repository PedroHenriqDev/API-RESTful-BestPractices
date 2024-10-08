﻿using Catalogue.Domain.Entities;
using Catalogue.Domain.Interfaces;
using Catalogue.Infrastructure.Abstractions;
using Catalogue.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Catalogue.Infrastructure.Repositories;

public sealed class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {}

    public async Task<Category?> GetByIdWithProductsAsync(Guid id)
    {
        return await entities.Select(c => new Category 
        {
            Id = c.Id,
            CreatedAt = c.CreatedAt,
            Description = c.Description,
            Name = c.Name,
            Products = c.Products.Select(p => new Product 
            {
                Name = p.Name,
                Description = p.Description,
                Id = p.Id,
                CategoryId  = p.CategoryId,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                CreatedAt = p.CreatedAt,
            }).ToList(),
        }).FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IQueryable<Category>> GetWithProductsAsync()
    {
        return entities.Select(c => new Category
        {
            Id = c.Id,
            CreatedAt = c.CreatedAt,
            Description = c.Description,
            Name = c.Name,
            Products = c.Products.Select(p => new Product 
            {
                Name = p.Name,
                Description = p.Description,
                Id = p.Id,
                CategoryId  = p.CategoryId,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                CreatedAt = p.CreatedAt,
            }).ToList()
        });
    }
}
