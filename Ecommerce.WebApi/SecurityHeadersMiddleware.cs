using Microsoft.Extensions.Primitives;//StringValues

    public class SecurityHeaders
    {
        //a function can process http request
        //return a task represent the completion of request processing
        private readonly RequestDelegate next;
        public SecurityHeaders(RequestDelegate next)
        {
            this.next = next;
        }
        public Task Invoke(HttpContext context)
        {
            // add any HTTP response headers you want here
            context.Response.Headers.Add(
            "super-secure", new StringValues("enable"));
            return next(context);
        }

    }
