using Newtonsoft.Json;
using QuestionnaireService.Domain.Models;

namespace QuestionnaireService.Domain;

public class CandidateRepository: ICandidateRepository
{
    private readonly Dictionary<int, Candidate> _candidateLookup;

    public CandidateRepository()
    {
        _candidateLookup = new Dictionary<int, Candidate>();
        PopulateLookupFromFile();
    }

    private void PopulateLookupFromFile()
    {
        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        var filePath = Path.Combine(basePath, "Candidates.json");
        using ( var file = File.OpenText(filePath))
        {
            var serializer = new JsonSerializer();
            var candidates = serializer.
                Deserialize<IEnumerable<Candidate>>(new JsonTextReader(file));
            if (candidates != null)
                foreach (var candidate in candidates)
                {
                    _candidateLookup.Add(candidate.Id, candidate);
                }
            else
            {
                throw new Exception("Unable to read candidates data");
            }
        }
    }

    public Candidate GetCandidateById(int id)
    {
        if (!_candidateLookup.TryGetValue(id, out var candidate))
        {
            throw new Exception($"Unable to find a candidate with the id {id}");
        }

        return candidate;
    }
}