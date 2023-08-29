using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using QuestionnaireService.Contracts;
using QuestionnaireService.Domain;

namespace QuestionnaireService.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ScoringController : ControllerBase
{
    private readonly IQuestionnaireResponseGenerator _questionnaireResponseGenerator;

    public ScoringController(IQuestionnaireResponseGenerator questionnaireResponseGenerator)
    {
        _questionnaireResponseGenerator = questionnaireResponseGenerator;
    }

    [HttpPost("/scoring")]
    public IActionResult ScoreAnswers([FromBody] QuestionnaireScoringRequest request)
    {
        var result = _questionnaireResponseGenerator.GenerateResponse(request);
        return new OkObjectResult(result);
    }
}
