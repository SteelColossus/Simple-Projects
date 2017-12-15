namespace Word_Generator
{
    public class PatternLink
    {
        private string fromPattern;
        private string toPattern;
        private int occurrences;

        public string FromPattern
        {
            get { return fromPattern; }
            set { fromPattern = value; }
        }

        public string ToPattern
        {
            get { return toPattern; }
            set { toPattern = value; }
        }

        public int Occurrences
        {
            get { return occurrences; }
            set { occurrences = value; }
        }

        public PatternLink(string fromPattern, string toPattern) : this(fromPattern, toPattern, 1) { }

        public PatternLink(string fromPattern, string toPattern, int occurrences)
        {
            this.fromPattern = fromPattern;
            this.toPattern = toPattern;
            this.occurrences = occurrences;
        }
    }
}