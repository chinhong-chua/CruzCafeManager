namespace CafeBackend.Application.Common
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key) : base($"{name} : ({key}) was not found.")
        {

        }
    }
}
