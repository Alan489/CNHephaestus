using CNHephaestus.Objects.CNHService;
using CNHephaestus.Pages.Components.Windows;
using Microsoft.AspNetCore.Components.Web;
using Shared.CNH.Shared.Communication.Authentication;
using System.Timers;
using System.ComponentModel;
using Timer = System.Timers.Timer;

namespace CNHephaestus.Services
{
 public class CNHSystemService
 {
  private ClientStatus _status = new();
  private ProxyService _proxyService;
  private AuthenticationService _authService;
  public string? SYSURL
  {
   get
   {
    return _status.SystemURL;
   }
   set
   {
    _status.SystemURL = value;
   }
  }
  public Session? CURRENTSESSION
  { get
   {
    return _status.currentSession;
   }
   set
   {
    _status.currentSession = value;
   }
  }

  public List<Window> ActiveWindows { get; set; } = new List<Window>();

  public Window? ActiveWindow { get; set; }

  public event EventHandler<MouseEventArgs> rightClickContextMenu;
  public event EventHandler<EventArgs> WindowsChanged;


  private readonly Timer _minuteTimer = new();
  private readonly Timer _secondTimer = new();

  public event EventHandler<EventArgs> MinuteFire;
  public event EventHandler<EventArgs> SecondFire;

  public CNHSystemService(ProxyService ps)
  {
   _proxyService = ps;
   ProxyService._sys = this;
   _authService = new AuthenticationService(this, ps);
   _minuteTimer.Interval = 60000;
   _secondTimer.Interval = 1000;

   _minuteTimer.Elapsed += (object? sender, ElapsedEventArgs e) =>
   {
    MinuteFire?.Invoke(this, EventArgs.Empty);
   };
   _minuteTimer.Enabled = true;

   _secondTimer.Elapsed += (object? sender, ElapsedEventArgs e) =>
   {
    SecondFire?.Invoke(this, EventArgs.Empty);
   };
   _secondTimer.Enabled = true;

  }

  private void TimeThread()
  {
   int seconds = 0;
   while(true)
   {
    Thread.Sleep(1000);
    SecondFire?.Invoke(this, EventArgs.Empty);
    seconds++;
    if (seconds >= 60)
    {
     MinuteFire?.Invoke(this, EventArgs.Empty);
     seconds = 0;
    }
   }
  }

  public async Task<HttpResponseMessage?> checkForCNHapi(string url)
  {
   if (string.IsNullOrEmpty(url)) return null;

   if (url.Contains("/")) return null;

   url = "https://" + url;
   
   ProxyRequest request = new ProxyRequest($"{url}/CNH/APITest", ProxyService.Method.GET);
   int responseCode = await request.SendRequest();

   return request.Response;
  }

  public void FireRightClick(MouseEventArgs mea)
  {
   rightClickContextMenu?.Invoke(this, mea);
  }

  public void FireRefresh()
  {
   WindowsChanged?.Invoke(this, EventArgs.Empty);
  }

  public void SetActiveWindow(Window wb)
  {
   if (!ActiveWindows.Contains(wb))
    ActiveWindows.Add(wb);

   ActiveWindow = wb;
   FireRefresh();
  }

  public void CloseWindow(Window wb)
  {
   if (!ActiveWindows.Contains(wb))
    return;
   ActiveWindows.Remove(wb);

   if (ActiveWindow == wb)
    ActiveWindow = null;

   FireRefresh();
  }


  public async Task<bool> initAuthenticate(string url, string username, string password)
  {

   return await _authService.InitAuthenticate(url, username, password);
  }

 }
}
