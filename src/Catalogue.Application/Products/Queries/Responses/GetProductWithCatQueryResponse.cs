﻿using Catalogue.Application.Abstractions;
using Catalogue.Application.DTOs.Responses;

namespace Catalogue.Application.Products.Queries.Responses;

public sealed class GetProductWithCatQueryResponse : ProductBase
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public CategoryResponse? Category { get; set; }

    public GetProductWithCatQueryResponse()
    {
    }
}
