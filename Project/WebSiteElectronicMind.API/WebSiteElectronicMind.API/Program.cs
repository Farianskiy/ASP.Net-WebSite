using OfficeOpenXml;
using WebSiteElectronicMind.Application.Services;
using WebSiteElectronicMind.Application.Services.RenderingPdf;
using WebSiteElectronicMind.ML;
using WebSiteElectronicMind.ML.AdditionalMethods;
using WebSiteElectronicMind.ML.Entities;
using WebSiteElectronicMind.ML.Format;
using WebSiteElectronicMind.ML.Format.Abstractions;
using WebSiteElectronicMind.ML.Format.ClassFormat;
using WebSiteElectronicMind.ML.Manager;
using WebSiteElectronicMind.ML.Repositories;
using WebSiteElectronicMind.Rendering.Methods;
using WebSiteElectronicMind.Rendering.Methods.Input;
using WebSiteElectronicMind.Rendering.Methods.OL;
using WebSiteElectronicMind.Rendering.Methods.Shina;
using WebSiteElectronicMind.Rendering.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:7014");

// Add services to the container.
builder.Services.AddControllers();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.WithOrigins("http://localhost:5117") // Укажите адрес фронтенда
              .AllowAnyHeader()                    // Разрешить любые заголовки
              .AllowAnyMethod();                   // Разрешить любые HTTP-методы
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<FileExcelService>();
builder.Services.AddScoped<FileMetadataService>();

builder.Services.AddScoped<IReadDataFromExcelRepositories, ReadDataFromExcelRepositories>();
builder.Services.AddScoped<IReadDataFromExcelService, ReadDataFromExcelService>();

builder.Services.AddSingleton<ML>();

builder.Services.AddScoped<TypeEquipmentAutomat>();
builder.Services.AddTransient<GetCharacteristic>();

builder.Services.AddScoped<IGetCharacteristicRepositories, GetCharacteristicRepositories>();

builder.Services.AddScoped<IDifNameFormat, DifNameFormat>();
builder.Services.AddScoped<IModNameFormat, ModNameFormat>();
builder.Services.AddScoped<IPowerNameFormat, PowerNameFormat>();
builder.Services.AddScoped<IRubNameFormat, RubNameFormat>();
builder.Services.AddScoped<IUZONameFormat, UZONameFormat>();
builder.Services.AddScoped<INameFormatter, NameFormatter>();

builder.Services.AddScoped<DesignationOnDiagram>();
builder.Services.AddScoped<RenderingOnDiagram>();
builder.Services.AddScoped<WidthAutomat>();
builder.Services.AddScoped<WireSectionAutomat>();

builder.Services.AddScoped<IPositionRepositories, PositionRepositories>();
builder.Services.AddScoped<IPositionService, PositionService>();

builder.Services.AddScoped<IWriteDataToExcelRepositories, WriteDataToExcelRepositories>();
builder.Services.AddScoped<IWriteDataToExcelService, WriteDataToExcelService>();

builder.Services.AddTransient<LoadModel>();

builder.Services.AddScoped<ILoadModelRepositories, LoadModelRepositories>();
builder.Services.AddScoped<LoadModelService>();

builder.Services.AddScoped<Pattern>();
builder.Services.AddScoped<Note>();
builder.Services.AddScoped<NoteMoreSeven>();
builder.Services.AddScoped<Input>();
builder.Services.AddScoped<OL>();
builder.Services.AddScoped<Shina>();
builder.Services.AddScoped<ISchemeDetailsRepositories, SchemeDetailsRepositories>();
builder.Services.AddScoped<IPdfGeneratorRepositories, PdfGeneratorRepositories>();
builder.Services.AddScoped<IPdfGeneratorMoreSevenRepositories, PdfGeneratorMoreSevenRepositories>();
builder.Services.AddScoped<GeneratorPdfService>();
builder.Services.AddTransient<HistoryPdfService>();

// Установка LicenseContext из конфигурации
var licenseContext = builder.Configuration["EPPlus:ExcelPackage:LicenseContext"];
if (!string.IsNullOrEmpty(licenseContext))
{
    ExcelPackage.LicenseContext = licenseContext == "Commercial" ? LicenseContext.Commercial : LicenseContext.NonCommercial;
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable CORS
app.UseCors("AllowLocalhost");

app.UseAuthorization();

app.MapControllers();

// Вызов асинхронного метода StartupMLModel
await app.Services.CreateScope().ServiceProvider.GetRequiredService<LoadModelService>().StartupMLModel();

app.Run();
