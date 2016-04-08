using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace CQRSSpike.ErrorHandling
{
    public class HalExceptionHandler : ExceptionHandler
    {
        public override void HandleCore(ExceptionHandlerContext context)
        {
            context.Result = new TextPlainErrorResult()
            {
                Content = "Sorry dave, I'm afraid I cant do that",
                Request = context.ExceptionContext.Request,
                
            };
        }
    }
}