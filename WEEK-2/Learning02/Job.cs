// Job.cs

public class Job
{
    // Member variables
    private string _jobTitle;
    private string _companyName;
    private int _startYear;
    private int? _endYear;  // Nullable to represent jobs without an end year

    // Constructor
    public Job(string jobTitle, string companyName, int startYear, int? endYear = null)
    {
        _jobTitle = jobTitle;
        _companyName = companyName;
        _startYear = startYear;
        _endYear = endYear;
    }

    // Properties for access to member variables
    public string JobTitle
    {
        get { return _jobTitle; }
        set { _jobTitle = value; }
    }

    public string CompanyName
    {
        get { return _companyName; }
        set { _companyName = value; }
    }

    public int StartYear
    {
        get { return _startYear; }
        set { _startYear = value; }
    }

    public int? EndYear
    {
        get { return _endYear; }
        set { _endYear = value; }
    }

    // Method to display job information
    public string DisplayJobInfo()
    {
        if (_endYear.HasValue)
        {
            return $"{_jobTitle} ({_companyName}) {_startYear}-{_endYear.Value}";
        }
        else
        {
            return $"{_jobTitle} ({_companyName}) {_startYear}-Present";
        }
    }
    // New method to display job details
    public void DisplayJobDetails()
    {
        if (EndYear.HasValue)
        {
            Console.WriteLine($"{JobTitle} ({CompanyName}) {StartYear}-{EndYear.Value}");
        }
        else
        {
            Console.WriteLine($"{JobTitle} ({CompanyName}) {StartYear}-Present");
        }
    }
}
