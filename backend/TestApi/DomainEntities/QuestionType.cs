namespace TestApi.DomainEntities
{
    /// <summary>
    /// Specifies the type of question presented to a user in a survey or form.
    /// </summary>
    public enum QuestionType
    {
        SingleChoice,   // 1 variant (radio)
        MultipleChoice, // checkbox
        Text            // answer in text form
    }
}
