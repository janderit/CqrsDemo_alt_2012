using System;

namespace CqrsModel.Cqrs
{
    public class EventEnvelope
    {
        public Event Event { get; set; }
        public Guid Source { get; set; }
        public int SourceVersion { get; set; }
        public int Inkrement { get; set; }
        public DateTime Zeit { get; set; }
    }
}