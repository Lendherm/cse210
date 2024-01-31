public class WritingAssignment : Assignment
{
    private string title;

    public WritingAssignment(string studentName, string topic, string title)
        : base(studentName, topic)
    {
        this.title = title;
    }

    // Remove the call to GetSummary here
    public string GetWritingInformation()
    {
        return $"{base.GetSummary()}\n{title} by {GetStudentName()}";
    }
}
