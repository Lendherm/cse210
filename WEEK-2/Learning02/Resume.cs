// Resume.cs

using System.Collections.Generic;

public class Resume
{
    // Member variables
    private string _personName;
    private List<Job> _jobList;

    // Constructor
    public Resume(string personName)
    {
        _personName = personName;
        _jobList = new List<Job>();
    }

    // Properties for access to member variables
    public string PersonName
    {
        get { return _personName; }
        set { _personName = value; }
    }

    public List<Job> JobList
    {
        get { return _jobList; }
        set { _jobList = value; }
    }
    // New method to display resume details
    public void DisplayResumeDetails()
    {
        Console.WriteLine($"Resume Details for {_personName}:");
        
        foreach (Job job in _jobList)
        {
            job.DisplayJobDetails();
        }
    }
}
