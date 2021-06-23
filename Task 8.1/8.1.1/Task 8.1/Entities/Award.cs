namespace Entities
{
    public class Award : CommonEntity
    {
        static int awardCount = 0;


        public string Title { get; set; }


        static Award() => awardCount++;

        protected Award() : base(awardCount) { }

        public Award(string title) : base(awardCount)
        {
            Title = title;
        }

        public override string ToString()
        {
            return "Названеи награды:" + System.Environment.NewLine + Title;
        }

    }
}
