using AutoMapper;
using MediatR;
using Ordering.Application.Common.Interfaces;
using Ordering.Domain.Entities;
using Serilog;
using Shared.SeedWork;

namespace Ordering.Application.Features.V1.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ApiResult<long>>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _repository;
        private readonly ILogger _logger;

        public CreateOrderCommandHandler(IMapper mapper, IOrderRepository repository, ILogger logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger;
        }

        private const string MethodName = "CreateOrderCommandHandler";

        public async Task<ApiResult<long>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            _logger.Information($"BEGIN: {MethodName} - Username: {command.UserName}");

            var orderEntity = _mapper.Map<Order>(command);
            await _repository.CreateOrder(orderEntity);

            _logger.Information($"END: {MethodName} - Username: {command.UserName}");

            return new ApiSuccesResult<long>(orderEntity.Id);
        }
    }
}
