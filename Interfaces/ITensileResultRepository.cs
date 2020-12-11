using System.Collections.Generic;
using ExperimentToolApi.Models;

namespace ExperimentToolApi.Interfaces
{
    public interface ITensileResultRepository
    {
        void Create(List<TensileResult> resultsList);
        List<int> GetListByTest(int testId);
        List<TensileResult> GetListByAttempt(int attemptNumber, int testId);
        bool isResultForTestPresent(int testId);
        bool isAttemptForTestPresent(int attemptNumber, int testId);
    }
}