var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>();

var app = builder.Build();

app.MapGraphQL();

app.Run();

public class Query
{
    public StringMatch GetStringMatch() => new StringMatch { Equals = "test value" };
}

public class StringMatch
{
    public string? Equals { get; set; }
}
