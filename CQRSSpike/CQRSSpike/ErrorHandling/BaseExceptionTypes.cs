using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace CQRSSpike.ErrorHandling
{
    public class ExceptionLogger : IExceptionLogger
    {
        public virtual Task LogAsync(ExceptionLoggerContext context,
                                     CancellationToken cancellationToken)
        {
            if (!ShouldLog(context))
            {
                return Task.FromResult(0);
            }

            return LogAsyncCore(context, cancellationToken);
        }

        public virtual Task LogAsyncCore(ExceptionLoggerContext context,
                                         CancellationToken cancellationToken)
        {
            LogCore(context);
            return Task.FromResult(0);
        }

        public virtual void LogCore(ExceptionLoggerContext context)
        {
        }

        public virtual bool ShouldLog(ExceptionLoggerContext context)
        {
            IDictionary exceptionData = context.ExceptionContext.Exception.Data;

            if (!exceptionData.Contains("MS_LoggedBy"))
            {
                exceptionData.Add("MS_LoggedBy", new List<object>());
            }

            ICollection<object> loggedBy = ((ICollection<object>)exceptionData["MS_LoggedBy"]);

            if (!loggedBy.Contains(this))
            {
                loggedBy.Add(this);
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class ExceptionHandler : IExceptionHandler
    {
        public virtual Task HandleAsync(ExceptionHandlerContext context,
                                        CancellationToken cancellationToken)
        {
            if (!ShouldHandle(context))
            {
                return Task.FromResult(0);
            }

            return HandleAsyncCore(context, cancellationToken);
        }

        public virtual Task HandleAsyncCore(ExceptionHandlerContext context,
                                           CancellationToken cancellationToken)
        {
            HandleCore(context);
            return Task.FromResult(0);
        }

        public virtual void HandleCore(ExceptionHandlerContext context)
        {
        }

        public virtual bool ShouldHandle(ExceptionHandlerContext context)
        {
            return context.ExceptionContext.CatchBlock.IsTopLevel;
        }
    }

    public class TextPlainErrorResult : IHttpActionResult
    {
        public HttpRequestMessage Request { get; set; }

        public string Content { get; set; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response =
                             new HttpResponseMessage(HttpStatusCode.InternalServerError);
            response.Content = new StringContent(Content);
            response.RequestMessage = Request;
            return Task.FromResult(response);
        }
    }
}