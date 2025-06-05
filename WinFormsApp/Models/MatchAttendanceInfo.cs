namespace WinFormsApp.Models;

public class MatchAttendanceInfo
{
    public DateTimeOffset DateTime { get; set; }
    public string HomeTeam { get; set; }
    public string AwayTeam { get; set; }
    public string Venue { get; set; }
    public long Attendance { get; set; }
}
