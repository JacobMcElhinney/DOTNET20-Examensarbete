using Microsoft.EntityFrameworkCore;
using TestApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configuare data context object options.
builder.Services.AddDbContext<JobDbContext>(
    optionsAction => optionsAction.UseSqlServer(
        //Get connection string value by Json property name from appsettings.json
        builder.Configuration.GetConnectionString("SqlTestServer")));

//Cors policy
var developmentPolicy = "AllowAnyOrigin";

//Configure CORS with named policy and middleware
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: developmentPolicy,
        policy =>
        {
                policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            
            /* TO BE REPLACED WITH:
             (name: allowSpecificOrigins, 
             policy => 
             { 
                 policy.WithOrigins(provide origin..
             });*/
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(developmentPolicy);

app.UseAuthorization();

app.MapControllers();

app.Run();
