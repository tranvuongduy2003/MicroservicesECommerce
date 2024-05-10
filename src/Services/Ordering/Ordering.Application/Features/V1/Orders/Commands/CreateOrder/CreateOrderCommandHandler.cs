using AutoMapper;
using Contracts.Services;
using MediatR;
using Ordering.Application.Common.Interfaces;
using Ordering.Domain.Entities;
using Serilog;
using Shared.SeedWork;
using Shared.Services.Email;

namespace Ordering.Application.Features.V1.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ApiResult<long>>
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly ISmtpEmailService _emailService;

        public CreateOrderCommandHandler(IMapper mapper, IOrderRepository repository, ILogger logger, ISmtpEmailService emailService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }

        private const string MethodName = "CreateOrderCommandHandler";

        public async Task<ApiResult<long>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.Information($"BEGIN: {MethodName} - Username: {request.UserName}");
            var orderEntity = _mapper.Map<Order>(request);
            var addedOrder = await _repository.CreateOrderAsync(orderEntity);
            await _repository.SaveChangesAsync();
            _logger.Information($"Order {addedOrder.Id} is successfully created.");

            SendEmailAsync(addedOrder, cancellationToken);

            _logger.Information($"END: {MethodName} - Username: {request.UserName}");
            return new ApiSuccesResult<long>(addedOrder.Id);
        }

        private async Task SendEmailAsync(Order order, CancellationToken cancellationToken)
        {
            var emailRequest = new MailRequest
            {
                ToAddress = order.EmailAddress,
                Body = "Order was created.",
                Subject = "Order was created"
            };

            try
            {
                await _emailService.SendEmailAsync(emailRequest, cancellationToken);
                _logger.Information($"Sent Created Order to email {order.EmailAddress}");
            }
            catch (Exception ex)
            {

                _logger.Error($"Order {order.Id} failed due to an error with the email service: {ex.Message}");
            }
        }
    }
}
