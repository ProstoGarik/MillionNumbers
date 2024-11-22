using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionNumbersClassLib
{
    public class NumberList
    {
        private List<int> numberListMain;
        private Random random;
        Stopwatch stopWatch;
        public NumberList()
        {
            random = new Random();
            numberListMain = new List<int>();
            stopWatch = new Stopwatch();
        }
        public List<int> NumberListMain { get => numberListMain; set => numberListMain = value; }

        public void InitializeList(int count)
        {
            numberListMain.Clear();
            for(int i = 0; i<count; i++)
            {
                numberListMain.Add(random.Next(-100,100));
            }
        }

        public void GetSumBasic(int start, int end)
        {
            int sum = 0;
            for(int i=start; i < end; i++)
            {
                sum += numberListMain[i];
            }
        }

        public int GetSumPar(int processors)
        {
            int[] partialSums = new int[processors];

            Parallel.For(0, processors, (p) =>
            {
                int sum = 0;
                int partitionSize = numberListMain.Count / processors;
                int start = p * partitionSize;
                int end = (p == processors - 1) ? numberListMain.Count : start + partitionSize;

                for (int i = start; i < end; i++)
                {
                    sum += numberListMain[i];
                }
                partialSums[p] = sum;
            });

            int totalSum = 0;
            foreach (var partialSum in partialSums)
            {
                totalSum += partialSum;
            }
            return totalSum;
        }

    }
}
