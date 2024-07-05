namespace Webion.Versioning.Tool.Ui;

public static class Msg
{
    public static string Line(string message)
    {
        return $"> {message}";
    }
    
    public static string Ask(string question)
    {
        return $"{Icons.Ask} [b]{question}[/]";
    }
    
    public static string Warn(string warning)
    {
        return $"{Icons.Warn} [b]{warning}[/]";
    }
    
    public static string Err(string error)
    {
        return $"{Icons.Err} [b]{error}[/]";
    }

    public static string Ok(string message)
    {
        return $"{Icons.Ok} {message}";
    }
}