using PokemonAPI.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<PokeApiSettings>(builder.Configuration.GetSection("PokeApiSettings"));
builder.Services.AddControllers();
builder.Services.AddHttpClient();

// Enable CORS and specify the allowed origin (your React app)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.WithOrigins("http://localhost:3000") // React app origin
                        .AllowAnyMethod() // Allow any HTTP method
                        .AllowAnyHeader() // Allow any headers
                        .AllowCredentials()); // If you need to allow credentials like cookies
});

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

// Ensure CORS is enabled before other middleware
app.UseHttpsRedirection();

// Apply the CORS policy
app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
