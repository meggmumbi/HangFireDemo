using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace HangFireDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {

        [HttpGet]
        [Route("productpayment")]
        public String Productpayment()
        {
            // Fire and Forget Job- this job is executed only once
            var parentjobId = BackgroundJob.Enqueue(() => Console.WriteLine("You have done your payment suceessfully!"));

            //Continuations Job - this job is executed when its parent job is executed
            BackgroundJob.ContinueJobWith(parentjobId, () => Console.WriteLine("Product receipt sent!"));

            return "You have done payment and receipt sent on your mail id!";
        }
    }
}
