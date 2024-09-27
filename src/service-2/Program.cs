using service_2;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

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
    .WithName("GetServiceInfo");

app.Run();