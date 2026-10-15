using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.CQRS
{
    //create a wrapper on Mediator.IRequest for reading from DB
    public interface IQuery<out TResponse> : IRequest<TResponse>
        where TResponse : notnull
    {
    }
}
