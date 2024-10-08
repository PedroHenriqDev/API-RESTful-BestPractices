﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Catalogue.Application.Categories.Queries.Requests;
using Catalogue.Application.Categories.Queries.Responses;
using Catalogue.Application.Pagination;
using Catalogue.Domain.Entities;
using Catalogue.Domain.Interfaces;
using MediatR;

namespace Catalogue.Application.Categories.Queries.Handlers;
public sealed class GetCategoriesQueryHandler
    : IRequestHandler<GetCategoriesQueryRequest, GetCategoriesQueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCategoriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetCategoriesQueryResponse> Handle(GetCategoriesQueryRequest request,
                                                         CancellationToken cancellationToken)
    {
        IQueryable<Category>? categories = _unitOfWork.CategoryRepository.GetAll();

        var categoriesPaged = await PagedList<GetCategoryQueryResponse>
            .ToPagedListAsync(request.Parameters!.PageNumber,
                              request.Parameters.PageSize,
                              categories.ProjectTo<GetCategoryQueryResponse>(_mapper.ConfigurationProvider));

        return new GetCategoriesQueryResponse(categoriesPaged);
    }
}
