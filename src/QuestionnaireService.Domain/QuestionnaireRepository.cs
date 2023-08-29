using Newtonsoft.Json;
using QuestionnaireService.Domain.Models;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace QuestionnaireService.Domain;

public class QuestionnaireRepository: IQuestionnaireRepository
{
    private readonly Dictionary<string, Questionnaire> _questionnaireLookup;

    public QuestionnaireRepository()
    {
        _questionnaireLookup = new Dictionary<string, Questionnaire>();
        PopulateLookupFromFile();
    }

    private void PopulateLookupFromFile()
    {
        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        var filePath = Path.Combine(basePath, "Questionnaires.json");
        using ( var file = File.OpenText(filePath))
        {
            var serializer = new JsonSerializer();
            var questionnaires = serializer.
                Deserialize<IEnumerable<Questionnaire>>(new JsonTextReader(file));
            if (questionnaires != null)
                foreach (var questionnaire in questionnaires)
                {
                    _questionnaireLookup.Add(questionnaire.QuestionnaireId, questionnaire);
                }
            else
            {
                throw new Exception("Unable to read questionnaire data");
            }
        }
    }

    public Questionnaire GetQuestionnaireById(string id)
    {
        if (!_questionnaireLookup.TryGetValue(id, out var questionnaire))
        {
            throw new Exception($"Unable to find questionnaire with the id {id}");
        }

        return questionnaire;
    }
}