using System.Collections.Generic;
using ExperimentToolApi.Models;

namespace ExperimentToolApi.Interfaces
{
    public interface ITensileResultRepository
    {
        TensileResult Create(TensileResult newTest);
        List<TensileResult> GetListByTest(int testId);
        List<TensileResult> GetListByAttempt(int attemptNumber);
    }
}