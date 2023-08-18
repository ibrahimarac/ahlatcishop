namespace Ahlatci.Shop.UI.Models
{
    public enum Roles
    {
        User = 1,
        Admin = 2
    }

    public enum OrderStatus
    {
        OrderCreated = 1,
        PaymentComplated = 2,
        Pending = 3,
        OrderDelivering = 4,
        CargoDelivered = 5,
        Complated = 6
    }

    public enum Gender
    {
        Male = 1,
        Female = 2
    }

}
