using System;

namespace Entities
{
    public abstract class CommonEntity
    {
        public int Id { get; set; }


        public CommonEntity() { }

        public CommonEntity(int id)
        {
            Id = id;
        }

        public override string ToString() => $"{Environment.NewLine}ID: {Id.ToString()} {Environment.NewLine}"; 
    }
}
