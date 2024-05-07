using AutoMapper;
using MediatR;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.Common.Models;
using Ordering.Domain.Entities;
using Serilog;
using Shared.SeedWork;

namespace Ordering.Application.Features.V1.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, ApiResult<OrderDto>>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _repository;
        private readonly ILogger _logger;

        public UpdateOrderCommandHandler(IMapper mapper, IOrderRepository repository, ILogger logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger;
        }

        private const string MethodName = "UpdateOrderCommandHandler";

        public async Task<ApiResult<OrderDto>> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            _logger.Information($"BEGIN: {MethodName} - Username: {command.UserName}");

            var orderEntity = _mapper.Map<Order>(command);
            await _repository.UpdateOrder(orderEntity);
            var orderDto = _mapper.Map<OrderDto>(orderEntity);

            _logger.Information($"END: {MethodName} - Username: {command.UserName}");

            return new ApiSuccesResult<OrderDto>(orderDto);
        }
    }
}
