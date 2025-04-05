using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.CNH.Shared.Communication.Authentication
{
 public class InitAuthenticationResponse
 {
  public bool? success { get; set; }
  public Session? session { get; set; }
 }
}
