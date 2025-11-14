using TiendaRopaBackend.Data;
using TiendaRopaBackend.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options => {
    options.AddPolicy("PermitirTodo", policy => {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar servicios
builder.Services.AddScoped<DatabaseConnection>();
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<OTPService>();
builder.Services.AddScoped<HistorialService>();
builder.Services.AddScoped<EmailService>();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();
app.UseCors("PermitirTodo");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// --- AGREGA ESTAS 2 LÍNEAS AQUÍ ---
app.UseCors("PermitirTodo");
app.UseDefaultFiles(); // Esto hace que busque "index.html" automáticamente
app.UseStaticFiles();  // Esto permite enviar el CSS y el JS al navegador
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();