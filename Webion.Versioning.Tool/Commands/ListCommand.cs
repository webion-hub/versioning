namespace Webion.Versioning.Commands;

internal sealed class ListCommand : Command
{
    public ListCommand() : base(
        name: "list",
        description: "Display all app versions"
    )
    {
        this.SetHandler(HandleAsync);
    }

    public async Task HandleAsync()
    {
        var repo = new AppsRepository();
        var apps = await repo.ListAllAsync(default);

        foreach (var app in apps)
        {
            Console.WriteLine($"{app.Name}\t-> {app.ToString()}");
        }
    }
}