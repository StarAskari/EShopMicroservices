using MediatR;

namespace BuildingBlocks.CQRS
{
    //create a wrapper on Mediator.IRequest for changing in DB
    public interface ICommand : IRequest<Unit> //without any out put
    {

    }
    public interface ICommand<out TResponse> : IRequest<TResponse> //with output
    {
    }
}
