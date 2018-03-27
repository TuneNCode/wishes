using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FindCompare
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var output = Compare("C:\\delete\\original.txt", "C:\\delete\\changed.txt", "anything", true, false);
        }

        public static List<Difference> Compare(string fileName1, string fileName2, string phrase = null, bool trimOpt = false,
            bool singleLineMode = true)
        {
            var lines1 = File.ReadAllLines(fileName1).ToList();
            var lines2 = File.ReadAllLines(fileName2).ToList();
            if (trimOpt)
            {
                lines1 = lines1.Select(x => x.Trim()).ToList();
                lines2 = lines2.Select(x => x.Trim()).ToList();
            }

            var diff = new List<Difference>();
            int i;
            for (i = 0; i < lines1.Count; i++)
            {
                if (lines2.Count > i)
                {

                    var difference = new Difference(lines1[i], lines2[i]);
                    if (!difference.NoChange) diff.Add(difference);

                }
                else
                {
                    diff.Add(new Difference(lines1[i], null));
                }
            }

            if (i <= lines2.Count - 1)
            {
                for (var j = i; j < lines2.Count; j++)
                {
                    diff.Add(new Difference(null, lines2[j]));
                }
            }

            var finalList = new StringBuilder();
            foreach (var difference in diff)
            {
                if (!string.IsNullOrEmpty(phrase) && !string.IsNullOrEmpty(difference.Original) &&
                    !string.IsNullOrEmpty(difference.Changed))
                {
                    if (difference.Original.Contains(phrase) || difference.Changed.Contains(phrase))
                    {
                        if (singleLineMode)
                        {
                            finalList.AppendLine(difference.ToSingleLineString());
                           // Console.WriteLine(difference.ToSingleLineString());
                        }
                        else
                        {
                            finalList.AppendLine(difference.ToString());
                            //Console.WriteLine(difference.ToString());
                        }
                    }
                }
                else
                {
                    if (singleLineMode)
                    {
                        finalList.AppendLine(difference.ToSingleLineString());
                        //Console.WriteLine(difference.ToSingleLineString());
                    }
                    else
                    {
                        finalList.AppendLine(difference.ToString());
                        //Console.WriteLine(difference.ToString());
                    }
                }
            }

            return diff;
            //return finalList.ToString();
            //File.WriteAllText($"C:\\delete\\diff_{DateTime.Now.ToFileTime()}.txt", finalList.ToString());

        }
    }

}

