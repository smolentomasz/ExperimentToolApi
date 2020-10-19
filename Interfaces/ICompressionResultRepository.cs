using System.Collections.Generic;
using ExperimentToolApi.Models;

namespace ExperimentToolApi.Interfaces
{
    public interface ICompressionResultRepository
    {
        CompressionResult Create(CompressionResult newTest);
        List<CompressionResult> GetListByTest(int testId);
        List<CompressionResult> GetListByAttempt(int attemptNumber, int testId);
        bool isResultForTestPresent(int testId);
    }
}