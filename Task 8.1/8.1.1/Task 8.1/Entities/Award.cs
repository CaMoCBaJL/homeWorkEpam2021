using System;

namespace Entities
{
    public class Award : CommonEntity
    {
        public string Title { get; set; }

        public Award() { }

        public Award(string title) : base()
        {
            Title = title;
        }

    }
}
