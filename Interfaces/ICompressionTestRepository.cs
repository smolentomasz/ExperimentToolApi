using System.Collections.Generic;
using ExperimentToolApi.Models;

namespace ExperimentToolApi.Interfaces
{
    public interface ICompressionTestRepository
    {
        CompressionTest GetById(int testId);
        List<CompressionTest> GetList();
        CompressionTest Create(CompressionTest newTest);
    }
}