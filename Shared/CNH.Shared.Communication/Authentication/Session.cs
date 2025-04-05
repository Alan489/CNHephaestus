using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.CNH.Shared.Communication.Authentication
{
 public class Session
 {
  public DateTime start {get; set;}
  public DateTime end {get; set;}
  public Guid sessionId {get; set;}
  public string sessionHash {get; set;}
  public RightsMap map {get; set;}
  public string username {get; set;}
  public string firstName {get; set;}
  public string lastName { get; set; }
 }
}
