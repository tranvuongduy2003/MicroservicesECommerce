namespace EventBus.Messages.Interfaces
{
    public interface IBasketCheckoutEvent : IIntegrationBaseEvent
    {
        string UserName { get; set; }
        decimal TotalPrice { get; set; }
        string LastName { get; set; }
        string FirstName { get; set; }
        string EnailAddress { get; set; }
        string ShippingAddress { get; set; }
        string InvoiceAddress { get; set; }
    }
}
