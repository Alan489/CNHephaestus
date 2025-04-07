using Microsoft.AspNetCore.Mvc;
using Shared.CNH.Shared.Communication.System;

namespace API.Controllers
{
 [ApiController]
 [Route("/CNH")]
 public class CNHController : ControllerBase
 {

  private SysInfo _sysInfo;

  public CNHController(IConfiguration conf) 
  { 
   _sysInfo = new SysInfo();
   _sysInfo.WelcomeMessage = conf.GetSection("SystemDetails").GetValue<string>("WelcomeMessage") ?? "<??>";
   _sysInfo.Name = conf.GetSection("SystemDetails").GetValue<string>("SystemName") ?? "<UNKNOWN SYSTEM>";
  }

  [HttpGet("APITest")]
  public string Get()
  {
   return "CNH";
  }

  [HttpGet("SystemInformation")]
  public SysInfo SysInfo()
  {
   return _sysInfo;
  }
 }
}
