using System.Collections.Generic;
using ExperimentToolApi.Models;

namespace ExperimentToolApi.Interfaces
{
    public interface ICompressionResultRepository
    {
        void Create(List<CompressionResult> resultsList);
        List<int> GetListByTest(int testId);
        List<CompressionResult> GetListByAttempt(int attemptNumber, int testId);
        bool isResultForTestPresent(int testId);
        bool isAttemptForTestPresent(int attemptNumber, int testId);
    }
}