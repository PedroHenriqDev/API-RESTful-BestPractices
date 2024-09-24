﻿using Catalogue.API.OpenApi;
using Catalogue.API.Filters;
using Catalogue.Application.Categories.Commands.Requests;
using Catalogue.Application.Categories.Commands.Responses;
using Catalogue.Application.Categories.Queries.Requests;
using Catalogue.Application.Categories.Queries.Responses;
using Catalogue.Application.DTOs.Responses;
using Catalogue.Application.Extensions;
using Catalogue.Application.Pagination;
using Catalogue.Application.Pagination.Parameters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalogue.API.Endpoints;

public static class CategoriesEndpoints
{
    private const string categoriesTag = "Categories";

    public static void MapCategoriesEndpoints(this IEndpointRouteBuilder endpoints)
    {
        #region Get

        /// <summary>
        /// The endpoint to retrieve a list of categories with pagination.
        /// </summary>
        /// <param name="httpContext">The HTTP context of the request.</param>
        /// <param name="parameters">The query parameters defining pagination (page number and page size).</param>
        /// <param name="mediator">The instance of the Mediator to send the request.</param>
        /// <returns>A result containing a paginated list of categories.</returns>
        /// <remarks>
        /// The pagination metadata includes details such as page size, current page, and total item count.
        /// </remarks>
        endpoints.MapGet("categories", async (HttpContext httpContext,
                                              [AsParameters] QueryParameters parameters,
                                              [FromServices] IMediator mediator) =>
        {
            GetCategoriesQueryResponse response =
                        await mediator.Send(new GetCategoriesQueryRequest(parameters));

            httpContext.AppendCategoriesMetaData(response.CategoriesPaged);

            return Results.Ok(response.CategoriesPaged);

        })
        .Produces<PagedList<GetCategoryQueryResponse>>(StatusCodes.Status200OK)
        .WithGetCategoriesDoc();

        /// <summary>
        /// Retrieves a category by its unique identifier (GUID).
        /// </summary>
        /// <param name="id">The unique identifier of the category (GUID).</param>
        /// <param name="mediator">The mediator used to handle the request and retrieve
        /// the category.</param>
        /// <returns>
        /// Returns an HTTP 200 OK response with the category details if found. 
        /// If the category is not found, it returns an HTTP 404 Not Found response with
        /// an error message.
        /// </returns>
        /// <remarks>
        /// This endpoint uses a `GUID` as a route parameter to identify the category.
        /// </remarks>
        endpoints.MapGet("categories/{id:guid}", async ([FromRoute] Guid id,
                                                        [FromServices] IMediator mediator) =>
        {
            GetCategoryQueryResponse response = await mediator.Send(new GetCategoryQueryRequest(id));
            return Results.Ok(response);

        })
        .Produces<GetCategoryQueryResponse>(StatusCodes.Status200OK)
        .Produces<ErrorResponse>(StatusCodes.Status404NotFound)
        .WithName("GetCategoryById")
        .WithGetCategoryByIdDoc();
        
        /// <summary>
        /// Endpoint to retrieve a paginated list of categories along with their associated products.
        /// </summary>
        /// <param name="httpContext">The HTTP context containing information about the request and
        /// response.</param>
        /// <param name="parameters">Pagination parameters, such as page number and page size.</param>
        /// <param name="mediator">The MediatR instance used to send the request and retrieve the
        /// result.</param>
        /// <returns>
        /// Returns a paginated list of categories with their associated products, along with pagination
        /// metadata in the response header.
        /// </returns>
        /// <response code="200">Returns a paginated list of categories with products.</response>
        /// <response code="400">Returns an error response if there is an issue with the request.</response>
        endpoints.MapGet("categories/products", async (HttpContext httpContext,
                                                       [AsParameters] QueryParameters parameters,
                                                       [FromServices] IMediator mediator) =>
        {
            GetCategoriesWithProdsQueryResponse response =
                           await mediator.Send(new GetCategoriesWithProdsQueryRequest(parameters));

            httpContext.AppendCategoriesMetaData(response.CategoriesPaged);

            return Results.Ok(response.CategoriesPaged);
        })
        .Produces<PagedList<GetCategoryWithProdsQueryResponse>>(StatusCodes.Status200OK)
        .Produces<ErrorResponse>(StatusCodes.Status400BadRequest)
        .WithGetCategoriesWithProductsDoc();

        /// <summary>
        /// Retrieves a category and its associated products based on the category ID.
        /// </summary>
        /// <param name="id">The unique identifier (Guid) of the category.</param>
        /// <param name="mediator">The MediatR instance to handle the query request.</param>
        /// <returns>
        /// Returns the category along with its products if found.
        /// If the category or products are not found, it returns a 404 Not Found response.
        /// </returns>
        /// <response code="200">Category and associated products successfully retrieved.</response>
        endpoints.MapGet("categories/products/{id:Guid}", async ([FromRoute] Guid id,
                                                                [FromServices] IMediator mediator) =>
        {
            GetCategoryWithProdsQueryResponse response =
                           await mediator.Send(new GetCategoryWithProdsQueryRequest(id));

            return Results.Ok(response);

        })
        .Produces<GetCategoryWithProdsQueryResponse>(StatusCodes.Status200OK)
        .Produces<ErrorResponse>(StatusCodes.Status404NotFound)
        .WithName("GetByIdCategoryWithProducts")
        .WithGetCategoryIncludingProductsDoc();
        #endregion

        #region Post

        /// <summary>
        /// Create a new category.
        /// </summary>
        /// <param name="request">An object containing the necessary data to create the category.</param>
        /// <param name="mediator">The MediatR instance responsible for handling the request.</param>
        /// <response code="201">Returns the created category along with its generated 'id' of type <see cref="Guid"/>.</response>
        /// <response code="400">Returns 400 Bad Request if the category data is invalid.</response>
        endpoints.MapPost("categories", async ([FromBody] CreateCategoryCommandRequest request,
                                               [FromServices] IMediator mediator) =>
        {
            CreateCategoryCommandResponse response = await mediator.Send(request);

            return Results.CreatedAtRoute
            (
              routeName: "GetCategoryById",
              routeValues: new { id = response.Id },
              value: response
            );

        })
        .Produces<CreateCategoryCommandResponse>(StatusCodes.Status201Created)
        .Produces<ErrorResponse>(StatusCodes.Status400BadRequest)
        .WithPostCategoryDoc();

        endpoints.MapPost("categories/products", async ([FromBody] CreateCategoryWithProdsCommandRequest request,
                                                        [FromServices] IMediator mediator) =>
        {
            CreateCategoryWithProdsCommandResponse response = await mediator.Send(request);
            return Results.CreatedAtRoute
            (
                routeName: "GetByIdCategoryWithProducts", 
                routeValues: new {id = response.Id}, value: response
            );

        })
        .Produces<CreateCategoryWithProdsCommandResponse>(StatusCodes.Status201Created)
        .Produces<ErrorResponse>(StatusCodes.Status400BadRequest)
        .WithTags(categoriesTag);
        #endregion

        #region Put

        endpoints.MapPut("categories/{id:Guid}", async ([FromBody] UpdateCategoryCommandRequest request,
                                                        [FromRoute] Guid id,
                                                        [FromServices] IMediator mediator) =>
        {
            UpdateCategoryCommandResponse response = await mediator.Send(request);
            return Results.Ok(response);

        })
        .AddEndpointFilter<InjectIdFilter>()
        .Produces<UpdateCategoryCommandResponse>(StatusCodes.Status200OK)
        .Produces<ErrorResponse>(StatusCodes.Status400BadRequest)
        .Produces<ErrorResponse>(StatusCodes.Status404NotFound)
        .WithTags(categoriesTag);

        #endregion

        #region Delete
        endpoints.MapDelete("categories/{id:Guid}", async ([FromRoute] Guid id,
                                                          [FromServices] IMediator mediator) =>
        {
            DeleteCategoryCommandResponse response = await mediator.Send(new DeleteCategoryCommandRequest(id));
            return Results.Ok(response);
        })
        .Produces<DeleteCategoryCommandResponse>(StatusCodes.Status200OK)
        .Produces<ErrorResponse>(StatusCodes.Status404NotFound)
        .WithTags(categoriesTag);
        #endregion
    }
}

