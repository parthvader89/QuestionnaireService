using QuestionnaireService.Domain;

public partial class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        {
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            // Add services to the container.
            builder.Services.AddSingleton<IQuestionnaireRepository, QuestionnaireRepository>();
            builder.Services.AddSingleton<ICandidateRepository, CandidateRepository>();
            builder.Services.AddTransient<IQuestionnaireResponseGenerator, QuestionnaireResponseGenerator>();
            builder.Services.AddTransient<IAnswerSheetMapper, AnswerSheetMapper>();
            builder.Services.AddTransient<IAnswerMapper, AnswerMapper>();
        }

        var app = builder.Build();
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}



