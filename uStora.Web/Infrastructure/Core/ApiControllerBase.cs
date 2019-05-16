using uStora.Model.Models;
using uStora.Service;
using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace uStora.Web.Infrastructure.Core
{
    public class ApiControllerBase : ApiController
    {
        private IErrorService _errorService;

        public ApiControllerBase(IErrorService errorService)
        {
            this._errorService = errorService;
        }

        protected HttpResponseMessage CreateHttpResponse(HttpRequestMessage requestMessage, Func<HttpResponseMessage> function)
        {
            HttpResponseMessage respone = null;
            try
            {
                respone = function.Invoke();
            }
            catch (DbEntityValidationException dbEnValEx)
            {
                foreach (var eve in dbEnValEx.EntityValidationErrors)
                {
                    string mess = "Entity of type \"" + eve.Entry.Entity.GetType().Name + "\" in state \"" + eve.Entry.State + "\" has the following validations erros.";
                    Trace.WriteLine(mess);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        string mess2 = "Property: \"" + ve.PropertyName + "\", Errors: \"" + ve.ErrorMessage + "\"";
                        Trace.WriteLine(mess2);
                    }
                }
                LogError(dbEnValEx);
                respone = requestMessage.CreateResponse(HttpStatusCode.BadRequest, dbEnValEx.InnerException.Message);
            }
            catch (DbUpdateException dbEx)
            {
                LogError(dbEx);
                respone = requestMessage.CreateResponse(HttpStatusCode.BadRequest, dbEx.InnerException.Message);
            }
            catch (Exception ex)
            {
                LogError(ex);
                respone = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return respone;
        }

        private void LogError(Exception ex)
        {
            try
            {
                Error error = new Error();
                error.Message = ex.Message;
                error.CreatedDate = DateTime.Now;
                error.StackTrace = ex.StackTrace;

                _errorService.Create(error);
                _errorService.SaveChange();
            }
            catch
            {
            }
        }
    }
}