
using API.Services.Crypto;
using API.Services.User;

namespace API
{
 public class Program
 {
  public static void Main(string[] args)
  {
   var builder = WebApplication.CreateBuilder(args);

   builder.Services.AddCors(options =>
   {
    options.AddPolicy("AllowAll", policy =>
    {
     policy
         .AllowAnyOrigin()    // Allow any origin
         .AllowAnyMethod()    // GET, POST, PUT, DELETE, etc.
         .AllowAnyHeader();   // Any headers
    });
   });

   // Add services to the container.

   builder.Services.AddControllers();
   // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
   builder.Services.AddEndpointsApiExplorer();
   builder.Services.AddSwaggerGen();

   builder.Services.AddScoped<CNHCryptoService>();
   builder.Services.AddScoped<AuthenticationService>();
   builder.Services.AddScoped<SessionService>();

   var app = builder.Build();

   // Configure the HTTP request pipeline.
   if (app.Environment.IsDevelopment())
   {
    app.UseSwagger();
    app.UseSwaggerUI();
   }

   app.UseHttpsRedirection();

   app.UseAuthorization();

   app.UseCors("AllowAll");

   app.MapControllers();

   app.Run();
  }
 }
}
