using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
 [ApiController]
 [Route("/CNH/APITest")]
 public class CNHController : ControllerBase
 {

  [HttpGet]
  public string Get()
  {
   return "CNH // Welcome in.";
  }
 }
}
