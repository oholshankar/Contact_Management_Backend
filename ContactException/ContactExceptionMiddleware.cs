
using System.Net;
using System.Text.Json;

namespace Contact_Management.ContactException
{
    public class ContactExceptionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public ContactExceptionMiddleware(RequestDelegate requestDelegate) {

            _requestDelegate= requestDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);

            }
            catch (Exception ex)
            {
                await HandleExceptionAsyc(context,ex);
            }
        }

        public  async Task HandleExceptionAsyc(HttpContext context,Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;

            ContactErrorResponse contactError = new ContactErrorResponse();
            switch (exception)
            {
                case ApplicationException ex:
                    contactError.ErrorMessage = "Application Level Error";
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case FileNotFoundException ex:
                    contactError.ErrorMessage = "Resource Not Found";
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
               default:
                    contactError.ErrorMessage = "Internal Server Error";
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            var exResult=JsonSerializer.Serialize(contactError);
            await response.WriteAsync(exResult);
        }
    }
}
