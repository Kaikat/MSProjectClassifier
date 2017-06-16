using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Accord.Statistics.Models.Regression;
using Accord.Statistics.Models.Regression.Fitting;
using Accord.Statistics.Models.Regression.Linear;
using Accord.Math.Optimization.Losses;
using System.IO;

namespace Classifier
{
    class Program
    {
        //EXAMPLE: http://accord-framework.net/docs/html/T_Accord_Statistics_Models_Regression_Linear_MultivariateLinearRegression.htm
        static void Main(string[] args)
        {
            const string OUTPUT_FILE = "WeightMatrix.csv";
            /*const int NUM_ROWS = 5;
            const int NUM_TOPICS = 12;
            const int NUM_MAJORS = 26;

            double[][] topicRatings = {              
                new double[] { 1.0, 1.0, 1.0 },
                new double[] { 2.1, 1.0, 1.0 },
                new double[] { 3.7, 1.0, 1.0 },
                
                new double[] { 4.2, 1.0, 1.0 },
                new double[] { 5.5, 1.0, 1.0 }
            };
            


            double[][] majorRatings = new double[][] {
                new double[] { 2.0, 3.0 },
                new double[] { 4.0, 6.0 },
                new double[] { 6.0, 9.0 },
                
                new double[] { 8.0, 12.0 },
                new double[] { 10.0, 15.0 }
            };*/

            CSV_Parser parser = new CSV_Parser();
            RegressionData data = parser.ParseDataFile();


            // Use Ordinary Least Squares to create the regression
            OrdinaryLeastSquares ols = new OrdinaryLeastSquares();

            // Now, compute the multivariate linear regression:
            MultivariateLinearRegression regression = ols.Learn(data.InterestRatings, data.MajorRatings);

            // We can obtain predictions using
            double[][] predictions = regression.Transform(data.InterestRatings);

            // The prediction error is
            double error = new SquareLoss(data.MajorRatings).Loss(predictions); // 0

            // We can also check the r-squared coefficients of determination:
            //double[] r2 = regression.CoefficientOfDetermination(topicRatings, majorRatings);
            double[][] r2 = regression.Weights;
            Console.WriteLine("WEIGHTS:");
            string textForFile = "";

            for (int i = 0; i < data.InterestOrder.Count; i++)
            {
                textForFile += i == data.InterestOrder.Count - 1 ? data.InterestOrder[i].ToString() + Environment.NewLine : data.InterestOrder[i].ToString() + ", ";
            }
            for (int i = 0; i < data.MajorOrder.Count; i++)
            {
                textForFile += i == data.MajorOrder.Count - 1 ? data.MajorOrder[i].ToString() + Environment.NewLine : data.MajorOrder[i].ToString() + ", ";
            }
            for (int i = 0; i < r2.Length; i++)
            {
                for (int j = 0; j < r2[i].Length; j++)
                {
                    //Console.Write(r2[i][j] + ", ");
                    textForFile += j == r2[i].Length - 1 ? r2[i][j].ToString() : r2[i][j].ToString() + ", ";
                }
                textForFile += Environment.NewLine;
                //Console.WriteLine();
                //Console.WriteLine();
            }

            File.WriteAllText(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + OUTPUT_FILE, textForFile);

            Console.WriteLine("Coefficient Of Determination");
            double[] r3 = regression.CoefficientOfDetermination(data.InterestRatings, data.MajorRatings);
            for(int i = 0; i < r3.Length; i++)
            {
                Console.WriteLine(r3[i]);
            }









































            /*new [] { 4.0, 3.0, 4.0, 3.0, 4.0, 3.0, 4.0, 5.0, 3.0, 2.0, 3.0, 4.0 },
new [] { 4.0, 4.0, 3.0, 3.0, 4.0, 5.0, 4.0, 5.0, 3.0, 4.0, 2.0, 3.0 },
new [] { 2.0, 5.0, 4.0, 2.0, 3.0, 5.0, 3.0, 5.0, 2.0, 4.0, 3.0, 1.0 },
new [] { 4.0, 4.0, 4.0, 2.0, 4.0, 5.0, 5.0, 5.0, 1.0, 2.0, 5.0, 4.0 },
new [] { 3.0, 5.0, 3.0, 4.0, 3.0, 4.0, 5.0, 5.0, 1.0, 4.0, 5.0, 1.0 }
*/


            /*
new double[] { 3.0, 3.0, 4.0, 4.0, 2.0, 3.0, 2.0, 4.0, 3.0, 4.0, 2.0, 2.0, 2.0, 1.0, 3.0, 1.0, 5.0, 3.0, 3.0, 1.0, 3.0, 2.0, 2.0, 3.0, 1.0, 4.0 },
new double[] { 2.0, 1.0, 4.0, 3.0, 5.0, 3.0, 2.0, 2.0, 3.0, 3.0, 1.0, 1.0, 1.0, 1.0, 2.0, 1.0, 5.0, 3.0, 4.0, 1.0, 1.0, 3.0, 1.0, 3.0, 3.0, 4.0 },
new double[] { 1.0, 1.0, 2.0, 3.0, 2.0, 2.0, 2.0, 4.0, 2.0, 2.0, 3.0, 2.0, 2.0, 1.0, 4.0, 1.0, 4.0, 4.0, 4.0, 1.0, 3.0, 4.0, 4.0, 4.0, 1.0, 5.0 },
new double[] { 2.0, 1.0, 1.0, 4.0, 4.0, 1.0, 3.0, 3.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 5.0, 3.0, 5.0, 4.0, 1.0, 4.0, 1.0, 4.0, 1.0, 4.0, 1.0, 4.0 },
new double[] { 1.0, 1.0, 4.0, 4.0, 2.0, 1.0, 1.0, 1.0, 1.0, 1.0, 4.0, 1.0, 5.0, 4.0, 5.0, 1.0, 5.0, 3.0, 2.0, 1.0, 2.0, 4.0, 3.0, 4.0, 1.0, 3.0 }
*/

































            /* double[] topicsSqrt = new double[NUM_ROWS];
             double[] majorsSqrt = new double[NUM_ROWS];
             for(int i = 0; i < NUM_ROWS; i++)
             {
                 double total = 0;
                 for(int j = 0; j < NUM_TOPICS; j++)
                 {
                     total += (topicRatings[i][j] * topicRatings[i][j]);
                 }
                 topicsSqrt[i] = Math.Sqrt(total);

                 total = 0;
                 for(int k = 0; k < NUM_MAJORS; k++)
                 {
                     total += (majorRatings[i][k] * majorRatings[i][k]);
                 }
                 majorsSqrt[i] = Math.Sqrt(total);
             }
             for(int i = 0; i < NUM_ROWS; i++)
             {
                 for(int j = 0; j < NUM_TOPICS; j++)
                 {
                     topicRatings[i][j] = topicRatings[i][j] / topicsSqrt[i];
                 }
                 for(int k = 0; k < NUM_MAJORS; k++)
                 {
                     majorRatings[i][k] = majorRatings[i][k] / majorsSqrt[i];
                 }
             }
             // Create a new Multinomial Logistic Regression for 3 categories
             var regression = new MultinomialLogisticRegression(inputs: NUM_TOPICS, categories: NUM_MAJORS);

             // Create a estimation algorithm to estimate the regression
             LowerBoundNewtonRaphson lbnr = new LowerBoundNewtonRaphson(regression);
             MultinomialLogisticRegression delta = lbnr.Learn(topicRatings, majorRatings);

             double[] results = new double[NUM_MAJORS];
             results = delta.Probabilities(topicRatings[0], results);

             for(int i = 0; i < results.Length; i++)
             {
                 Console.WriteLine(results[i].ToString());
             }
             */
            Console.Read();
        }
    }
}
