using service_2;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

app.UseHttpsRedirection();


app.MapGet("/", () =>
    {
        var ip = Utils.ExecuteCommand("/bin/hostname", "-i").Trim();
        var processes = Utils.ExecuteCommand("ps", "-ax").Split("\n").Select(s => s.Trim()).Where(s => s.Length != 0);
        var discSpace = Utils.ExecuteCommand("df").Split("\n").Select(s => s.Trim()).Where(s => s.Length != 0);
        var sinceBoot = Utils.ExecuteCommand("uptime").Trim();

        return new
        {
            ip,
            processes,
            discSpace,
            sinceBoot
        };
    })
    .WithName("GetServiceInfo")
    .WithOpenApi();

app.Run();