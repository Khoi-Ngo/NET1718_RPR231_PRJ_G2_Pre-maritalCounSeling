using Microsoft.EntityFrameworkCore;
using NET1718_RPR231_PRJ_G2_Pre_maritalCounSeling.GrpcService.Services;
using Pre_maritalCounSeling.BAL;
using Pre_maritalCounSeling.BAL.ServiceQuiz;
using Pre_maritalCounSeling.DAL.Entities;
using Pre_maritalCounSeling.DAL.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureAppServices();


// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<QuizResultServices>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
