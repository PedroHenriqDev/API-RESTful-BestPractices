using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.OpenApi.Models;

namespace Catalogue.API.OpenApi;

public static class EndpointConventionBuilderExtension
{
    public static void WithLoginDoc(this IEndpointConventionBuilder builder)
    {
        builder.WithOpenApi(operation => new(operation)
        {
            Summary = "Login User",
            Description = "Authenticates a user and generates a JWT token if the login is successful.",
            Tags = new List<OpenApiTag> { new OpenApiTag() {Name = "Authentication"}}
        });
    }

    public static void WithRegisterDoc(this IEndpointConventionBuilder builder)
    {
        builder.WithOpenApi(operation => new(operation)
        {
            Summary = "Register User",
            Description = "Registers a new user.",
            Tags = new List<OpenApiTag>{new OpenApiTag() {Name = "Authentication"}}
        });      
    }

    public static void WithPutRoleDoc(this IEndpointConventionBuilder builder)
    {
        builder.WithOpenApi(operation => new(operation)
        {
            Summary = "Update user role",

            Description = "Updates the role of a user specified by their ID. This endpoint is restricted to users with the 'AdminOnly' policy.",

            Tags = new List<OpenApiTag>{new OpenApiTag() {Name = "Authentication"}}
        });    
    }

    public static void WithPutUserDoc(this IEndpointConventionBuilder builder)
    {
        builder.WithOpenApi(operation => new(operation)
        {
            Summary = "Updates user informations",

            Description = @"Updates the user. This endpoint use a custom endpoint filter,
            is used to inject the current authenticated user's name into the request object
            before it's processed.",

            Tags = new List<OpenApiTag>{new OpenApiTag() {Name = "Authentication"}}
        });    
    }

    public static void WithGetCategoriesDoc(this IEndpointConventionBuilder builder)
    {
        builder.WithOpenApi(operation => new(operation)
        {
            Summary = "Get categories paged",

            Description = @"The endpoint to retrieve a list of categories with pagination and,
            The pagination metadata includes details such as page size, current page, and total
            item count.",

            Tags = new List<OpenApiTag>{new OpenApiTag(){Name = "Categories" }}
        });
    }

    public static void WithGetCategoryByIdDoc(this IEndpointConventionBuilder builder)
    {
        builder.WithOpenApi(operation => new(operation)
        {
            Summary = "Retrieves a category by its unique identifier (GUID).",

            Description = @"This endpoint uses a `GUID` as a route parameter to
                            identify the category.",

            Tags = new List<OpenApiTag>(){new OpenApiTag(){Name = "Categories"}}
        });
    }

    public static void WithGetCategoriesWithProductsDoc(this IEndpointConventionBuilder builder)
    {
        builder.WithOpenApi(operation => new(operation)
        {
            Summary = "Get categories with their associated products.",
            Description = @"This endpoint Returns a paginated list of
                            categories with their associated products, along with pagination
                            metadata in the response header.",
            Tags = new List<OpenApiTag>(){new OpenApiTag(){Name = "Categories"}}
        });
    }
}