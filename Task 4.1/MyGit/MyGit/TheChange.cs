using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGit
{
    class TheChange
    {
        public TheChange() { }

        public TheChange(string distName, ChangeType changeType, string changedContent)
        {
            FileName = distName;

            ChangeType = changeType;

            NewContent = changedContent;
        }

        public string FileName { get; init; }

        public ChangeType ChangeType { get; init; }

        public string NewContent { get; init; }

        public override string ToString()
        {
            return $"{ChangeType} {FileName + Environment.NewLine} {NewContent}";
        }
    }
}
