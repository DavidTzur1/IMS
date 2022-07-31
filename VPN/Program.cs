using VPN.Context;
using VPN.Contracts;
using VPN.Repository;
using VPN.Services;

var builder = WebApplication.CreateBuilder(args);

// Add your logging handler
builder.Logging.AddLog4Net("log4net.config");

// Add services to the container.
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IPABXRepository, PABXRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRangeRepository, RangeRepository>();
builder.Services.AddScoped<IScreeningRepository, ScreeningRepository>();
builder.Services.AddScoped<IScreeningDataRepository, ScreeningDataRepository>();
builder.Services.AddTransient<IVPNRepository, VPNRepository>();
builder.Services.AddHostedService<VPNService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

app.MapControllers();

app.Run();
