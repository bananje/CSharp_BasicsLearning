using BenchmarkDotNet.Attributes;
using System.Text;

namespace Benchmarking
{
    public class StringBenchmarks
    { 
        int[] numbers;
        public StringBenchmarks()
        {
            numbers = Enumerable.Range(1,20).ToArray();
        }

        [Benchmark(Baseline = true)]
        public string StringConcatenationTest()
        {
            string str = string.Empty;
            for (int i = 0; i < numbers.Length; i++)
            {
                str += numbers[i] + ", ";
            }
            return str;
        }

        [Benchmark]
        public string StringBuilderTest() 
        {
            StringBuilder builder = new();
            for (int i = 0; i < numbers.Length; i++)
            {
                builder.Append(numbers[i]);
                builder.Append(", ");
            }
            return builder.ToString();
        }
    }
}
