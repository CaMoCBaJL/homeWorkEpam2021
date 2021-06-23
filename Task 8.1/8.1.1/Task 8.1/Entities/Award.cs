using Newtonsoft.Json;

namespace Entities
{
    public class Award : CommonEntity
    {
        static int awardCount = 0;

        [JsonProperty]
        public string Title { get; private set; }


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
