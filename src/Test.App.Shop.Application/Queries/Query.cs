using MediatR;

namespace Test.App.Shop.Application.Queries;

public abstract class Query<T> : IRequest<T>
{
}
