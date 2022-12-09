namespace OvertimePolicies.Services.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base($"موجودیت \"{name}\" با کد ({key}) یافت نشد.")
        {
        }
    }
}