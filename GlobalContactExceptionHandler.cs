using Microsoft.AspNetCore.Diagnostics;

namespace Contact_Management
{
    public class GlobalContactExceptionHandler:IExceptionHandlerFeature
    {
        public GlobalContactExceptionHandler() { }

        public Exception Error => throw new NotImplementedException();
    }
}
