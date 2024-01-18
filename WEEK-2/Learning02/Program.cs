// Program.cs

using System;

class Program
{
    static void Main()
    {
        // Create job1 instance
        Job job1 = new Job("Software Engineer", "Microsoft", 2019, 2022);

        // Set member variables for job1 using dot notation
        job1.JobTitle = "Senior Software Engineer";
        job1.StartYear = 2017;

        // Create job2 instance
        Job job2 = new Job("Data Scientist", "Google", 2020);
        
        // Set member variables for job2 using dot notation
        job2.EndYear = 2023;

        // Create Resume instance
        Resume myResume = new Resume("John Doe");

        // Add the two jobs to the list of jobs in the resume object
        myResume.JobList.Add(job1);
        myResume.JobList.Add(job2);

        // Display resume details
        myResume.DisplayResumeDetails();
    }
}
