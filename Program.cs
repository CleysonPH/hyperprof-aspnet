using HyperProf.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.RegisterDatabase();
builder.Services.RegisterRepositories();
builder.Services.RegisterUnitOfWork();
builder.Services.RegisterMappers();
builder.Services.RegisterServices(builder.Configuration);
builder.Services.RegisterControllers();
builder.Services.RegisterValidators();
builder.Services.RegisterAuthentication(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.RegisterConfigProperties(builder.Configuration);
builder.Services.RegisterCors();

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

app.RegisterMiddlewares();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.Run();
