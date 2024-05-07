using AutoMapper;
using MediatR;
using Ordering.Application.Common.Interfaces;
using Serilog;
using Shared.SeedWork;

namespace Ordering.Application.Features.V1.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, ApiResult<long>>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _repository;
        private readonly ILogger _logger;

        public DeleteOrderCommandHandler(IMapper mapper, IOrderRepository repository, ILogger logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger;
        }

        private const string MethodName = "DeleteOrderCommandHandler";

        public async Task<ApiResult<long>> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            _logger.Information($"BEGIN: {MethodName} - Id: {command.Id}");

            await _repository.DeleteOrder(command.Id);

            _logger.Information($"END: {MethodName} - Id: {command.Id}");

            return new ApiSuccesResult<long>(command.Id);
        }
    }
}
