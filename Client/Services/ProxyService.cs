using Microsoft.AspNetCore.Components.WebAssembly.Http;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace CNHephaestus.Services
{
 public class ProxyService
 {
  private readonly HttpClient _httpClient;
  private static readonly Dictionary<string, string> DummyHeaders = new();
  public static CNHSystemService _sys;
  public static ProxyService Instance { get; private set; }
  public enum Method 
  { 
   GET, 
   POST 
  }

  public ProxyService(HttpClient httpClient)
  {
   _httpClient = httpClient;
   Instance = this;
  }

  public async Task<HttpResponseMessage> SendRequestAsync(ProxyRequest proxyRequest)
  {

   HttpRequestMessage request = new();
   string url = proxyRequest.Url;

   if (!string.IsNullOrEmpty(_sys.SYSURL))
    url = _sys.SYSURL + url;


   request.RequestUri = new Uri(url);
   if (proxyRequest.Content != null)
    request.Content = JsonContent.Create<object>(proxyRequest.Content);
   
   switch (proxyRequest.Method)
   {
    case Method.POST:

     request.Method = HttpMethod.Post;

     break;

    default:
    case Method.GET:
     request.Content = null;
     request.Method = HttpMethod.Get;

     break;

   }

   foreach (var header in proxyRequest.Headers ?? DummyHeaders)
   {
    request.Headers.Add(header.Key, header.Value);
   }
   try
   {
    HttpResponseMessage httpResponseMessage = await _httpClient.SendAsync(request);
    
    return httpResponseMessage;
   }
   catch(Exception ex)
   {
    Console.WriteLine(ex.ToString());
    throw;
   }

  }
 }

 public class ProxyRequest
 {
  public string Url { get; private set; }
  public ProxyService.Method Method { get; private set; }
  public Dictionary<string, string>? Headers { get; private set; }
  public object? Content { get; private set; }

  public bool pending { get; private set; } = true;
  public HttpResponseMessage Response { get; private set; }

  public ProxyRequest(
   string? URL = null, 
   ProxyService.Method method = ProxyService.Method.GET, 
   Dictionary<string, string>? Headers = null, object? Content = null
   )
  {
   Url = URL ?? "";
   Method = method;
   this.Headers = Headers;
   this.Content = Content;
  }

  public async Task<int> SendRequest()
  {
   if (!pending) return -1;
   
   pending = false;

   try
   {
    Response = await ProxyService.Instance.SendRequestAsync(this);
   } catch(Exception ex)
   {
    return 0;
   }
   

   return ((int)Response.StatusCode);
  }
 }
}