namespace Ahlatci.Shop.UI.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message):base(message)
        {

        }

        public UnauthorizedException():base("Bu alana erişim yetkiniz bulunmamaktadır.")
        {

        }
    }
}
