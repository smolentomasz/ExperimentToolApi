using System.Collections.Generic;
using ExperimentToolApi.Models;

namespace ExperimentToolApi.Interfaces
{
    public interface ITensileTestRepository
    {
         TensileTest GetById(int testId);
         List<TensileTest> GetList();
         TensileTest Create(TensileTest newTest);
    }
}