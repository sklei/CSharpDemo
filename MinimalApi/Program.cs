var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var app = builder.Build();

var path = "/Users/steven/DEV/TST/C#10Demo/MinimalApi/Rapport_1.pdf";

app.MapGet("ka/{pow}", (int pow) => Math.Pow(pow, 2));

app.MapGet("/test1", (httpContext) => {
	var content = File.ReadAllBytes(path);
	var response = httpContext.Response;
	response.StatusCode = 200;
	response.ContentType = "application/pdf"; //text/plain
	response.ContentLength = content.Length;
	return response.Body.WriteAsync(content, 0, content.Length);
});

app.MapGet("/test2", async (httpContext) =>	await httpContext.Response.SendFileAsync(path));

app.MapGet("/test3", () => Results.File(path, "application/pdf"));

app.UseHttpsRedirection();

app.Run();
