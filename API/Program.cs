using API.Controllers.ServerHub;
using API.Data;
using API.DBContext;
using API.Enity;
using API.Repositories;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
   .AddNegotiate();
var con = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DB>(options =>
    options.UseMySql(con,ServerVersion.AutoDetect(con))
);
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<DB>()
                .AddDefaultTokenProviders();
builder.Services.AddCors(policy =>
{
    policy.AddPolicy("CorsPolicy", opt => opt
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
        .SetIsOriginAllowed((host) => true));
});

builder.Services.AddScoped<HttpClient>();

builder.Services.AddScoped<IUserRepositories, UserRepository>();
builder.Services.AddScoped<IExamRepositories, ExamRepository>();
builder.Services.AddScoped<IQuestionRepositories, QuestionRepository>();
builder.Services.AddScoped<ISentenceRepositories, SentenceRepository>();
builder.Services.AddScoped<IQuestionRepositories, QuestionRepository>();
builder.Services.AddScoped<IQuestionCompleteRepositories, QuestionCompleteRepository>();
builder.Services.AddScoped<ISentenceCompleteRepositories, SentenceCompleteRepository>();
builder.Services.AddScoped<IFileRepositories, FileRepository>();

builder.Services.AddTransient<FileService>();
builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<ExamService>();
builder.Services.AddTransient<QuestionService>();
builder.Services.AddTransient<SentenceService>();
builder.Services.AddTransient<SentenceCompServices>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
                };
            });

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddSignalR();

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
          new[] { "application/octet-stream" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("CorsPolicy");

app.MapControllers();

app.MapHub<StreamingFile>("streaming-file");
app.UseResponseCompression();

app.Run();
