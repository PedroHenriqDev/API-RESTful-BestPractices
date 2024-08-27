﻿using Catalogue.Application.Products.Commands.Responses;
using MediatR;
using System.Text.Json.Serialization;

namespace Catalogue.Application.Products.Commands.Requests;

public class DeleteProductCommandRequest : IRequest<DeleteProductCommandResponse>
{
    [JsonIgnore]
    public int Id { get; set; }

    public DeleteProductCommandRequest(int id)
    {
        Id = id;
    }
}
