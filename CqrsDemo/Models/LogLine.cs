using System;

namespace CqrsDemo.Models
{
    public class LogLine
    {
        public int Lfd { get; set; }
        public DateTime Zeit { get; set; }
        public string Source { get; set; }
        public int Version { get; set; }
        public string Ereignis { get; set; }
    }
}