public class JournalEntry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Date { get; set; }
    public string Location { get; set; }
    public string Weather { get; set; }
    public string Mood { get; set; }

    public JournalEntry(string prompt, string response, string date, string location, string weather, string mood)
    {
        Prompt = prompt;
        Response = response;
        Date = date;
        Location = location;
        Weather = weather;
        Mood = mood;
    }
}
