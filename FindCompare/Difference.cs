using System;
using System.Text;

namespace FindCompare
{
    public class Difference
    {
        public bool LineAdded => string.IsNullOrEmpty(Original) && !string.IsNullOrEmpty(Changed);
        public bool LineRemoved => string.IsNullOrEmpty(Changed) && !string.IsNullOrEmpty(Original);
        public bool LineChanged => !string.IsNullOrEmpty(Changed) && !string.IsNullOrEmpty(Original) && !Changed.Equals(Original, StringComparison.InvariantCulture);

        public bool NoChange => (Changed == null && Original == null) ||
                                (Changed != null && Changed.Equals(Original, StringComparison.InvariantCulture));
        public Difference(string original, string changed)
        {
            Original = original;
            Changed = changed;
        }
        public string Original { get; }
        public string Changed { get; }
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Was: {Original}");
            sb.AppendLine($"Is: {Changed}");
            if (NoChange) sb.AppendLine("No Change");
            if (LineAdded) sb.AppendLine("Line Added");
            if (LineRemoved) sb.AppendLine("Line Removed");
            if (LineChanged) sb.AppendLine("Line Changed");
            return sb.ToString();
        }

        public string ToSingleLineString()
        {
            var singleLineString = $"Changed from \"{Original}\" to \"{Changed}\". ";
            if (NoChange) singleLineString += "No Change.";
            if (LineAdded) singleLineString += "Line Added.";
            if (LineRemoved) singleLineString += "Line Removed.";
            if (LineChanged) singleLineString += "Line Changed.";
            return singleLineString;
        }
    }
}