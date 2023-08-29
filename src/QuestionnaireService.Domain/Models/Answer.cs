namespace QuestionnaireService.Contracts;

public class Answer
{
    public int QuestionId { get; set; }
    public int[] Selection { get; set; }
}