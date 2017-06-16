using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classifier
{
    public class RegressionData
    {
        public readonly double[][] InterestRatings;
        public readonly double[][] MajorRatings;
        public readonly List<Interest> InterestOrder;
        public readonly List<Major> MajorOrder;

        public RegressionData(double[][] interestRatings, double[][] majorRatings, List<Interest> interestOrder, List<Major> majorOrder)
        {
            InterestRatings = interestRatings;
            MajorRatings = majorRatings;
            InterestOrder = interestOrder;
            MajorOrder = majorOrder;
        }
    }
}
