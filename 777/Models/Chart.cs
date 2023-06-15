using System.Globalization;

namespace _777.Models
{
    public class Chart
    {
        public string Month { get; set; }
        public List<string> Dates { get; set; }
        public List<double> Scores { get; set; }
    }
}
