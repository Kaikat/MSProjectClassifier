using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classifier
{
    public class CSV_Parser
    {
        const string FILENAME = "\\CollegeStudentsData.csv";
        readonly int TOTAL_INTERESTS = Enum.GetNames(typeof(Interest)).Length;
        readonly int TOTAL_MAJORS = Enum.GetNames(typeof(Major)).Length;

        public RegressionData ParseDataFile()
        {
            StreamReader reader = File.OpenText(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + FILENAME);
            string line = reader.ReadLine();
            List<Interest> interestOrder = GetInterestsInCSVOrder(line);
            List<Major> majorOrder = GetMajorsInCSVOrder(line);
            interestOrder.PrintAll<Interest>("Interest");
            majorOrder.PrintAll<Major>("Major");

            List<List<double>> interestValuesList = new List<List<double>>();
            List<List<double>> majorValuesList = new List<List<double>>();
            int row = 0;
            while ((line = reader.ReadLine()) != null)
            {
                string[] items = line.Split(',');
                interestValuesList.Add(new List<double>());
                majorValuesList.Add(new List<double>());
                for (int i = 1; i < TOTAL_INTERESTS + 1; i++)
                {
                    double interestValue = Convert.ToDouble(items[i].Enumerize<Rating>()) + 1.0;
                    interestValuesList[row].Add(interestValue);
                }
                for (int i = TOTAL_INTERESTS + 1; i < TOTAL_INTERESTS + TOTAL_MAJORS; i++)
                {
                    double majorValue = Convert.ToDouble(items[i].Enumerize<Rating>()) + 1.0;
                    majorValuesList[row].Add(majorValue);
                }
                row++;
            }

            return new RegressionData(
                interestValuesList.Select(a => a.ToArray()).ToArray(), 
                majorValuesList.Select(a => a.ToArray()).ToArray(), 
                interestOrder, majorOrder);
        }

        private List<Interest> GetInterestsInCSVOrder(string line)
        {
            List<Interest> interestOrder = new List<Interest>();
            string[] tokens = line.Split(',');
            for (int i = 1; i < TOTAL_INTERESTS + 1; i++)
            {
                if (tokens[i].Contains("]"))
                {
                    string interest = tokens[i].Split('[', ']')[1];
                    interest = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(interest.ToLower());
                    interest = interest.Replace(' ', '_');
                    interestOrder.Add(interest.ToEnum<Interest>());
                }
            }

            return interestOrder;
        }

        private List<Major> GetMajorsInCSVOrder(string line)
        {
            List<Major> majorOrder = new List<Major>();
            string[] tokens = line.Split(',');
            for (int i = TOTAL_INTERESTS + 1; i < TOTAL_INTERESTS + TOTAL_MAJORS + 1; i++)
            {
                if (tokens[i].Contains("]"))
                {
                    string major = tokens[i].Split('[', ']')[1];
                    major = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(major.ToLower());
                    major = major.Replace(" ", string.Empty);
                    majorOrder.Add(major.ToEnum<Major>());
                }
            }

            return majorOrder;
        }
    }
}
