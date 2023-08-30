using WatchDog;
using WatchDog.src.Enums;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddWatchDogServices(opt =>
{
    opt.IsAutoClear = false;
    //opt.ClearTimeSchedule = WatchDogAutoClearScheduleEnum.Weekly;
    opt.SetExternalDbConnString = builder.Configuration.GetConnectionString("PostgreSQL");
    opt.DbDriverOption = WatchDogDbDriverEnum.PostgreSql;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseWatchDogExceptionLogger();
app.UseWatchDog(opt =>
{
    opt.WatchPageUsername = "admin";
    opt.WatchPagePassword = "12345678";
});
app.Run();
