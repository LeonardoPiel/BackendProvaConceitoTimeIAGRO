using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Models
{
    public class APIMessage<T>
    {
        public List<string>? Errors { get; set; }
        public object Data { get; set; }
        public APIMessage()
        { 
            Errors = new List<string>();
            Data = new object();
        }
        public ActionResult NotFound(DefaultService<T> service)
        {
            try
            {
                Errors = service.Errors;
                Data = service.SingleData == null || service.Data.Count == 0 ? new List<T>() : service.Data;
                Errors.Add("Nenhum registro encontrado");
            }
            catch (Exception ex) 
            {
                Errors.Add("Mensagem =>" + ex);
            }
            return new NotFoundObjectResult(this);
        }
        public ActionResult Ok(DefaultService<T> service)
        {
            try
            {
                Errors = service.Errors;
                Data = service.SingleData != null ? service.SingleData : service.Data;
            }
            catch (Exception ex)
            {
                Errors.Add("Mensagem =>" + ex);
            }
            return new OkObjectResult(this);
        }
        public ActionResult Ok(DefaultService<T> service, object data)
        {
            try
            {
                Errors = service.Errors;
                Data = data;
            }
            catch (Exception ex)
            {
                Errors.Add("Mensagem =>" + ex);
            }
            return new OkObjectResult(this);
        }
    }
}
