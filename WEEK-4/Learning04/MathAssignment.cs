using System;
public class MathAssignment : Assignment
{
    // Private member variable specific to MathAssignment
    private string section;
    private string problems;

    // Constructor
    public MathAssignment(string studentName, string topic, string section, string problems)
        : base(studentName, topic)
    {
        this.section = section;
        this.problems = problems;
    }

    // Method to get the Math homework list
    public string GetHomeworkList()
    {
        return $"Section {section} Problems {problems}";
    }
}
