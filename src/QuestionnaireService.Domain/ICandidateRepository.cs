using QuestionnaireService.Domain.Models;

namespace QuestionnaireService.Domain;

public interface ICandidateRepository
{
    Candidate GetCandidateById(int id);
}