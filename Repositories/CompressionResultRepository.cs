using System.Collections.Generic;
using System.Linq;
using ExperimentToolApi.Interfaces;
using ExperimentToolApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ExperimentToolApi.Repositories
{
    public class CompressionResultRepository : ICompressionResultRepository
    {
        private readonly ExperimentToolDbContext _context;
        public CompressionResultRepository(ExperimentToolDbContext context){
            _context = context;
        } 
        public CompressionResult Create(CompressionResult newResult)
        {
            _context.CompressionResults.Add(newResult);
            _context.SaveChanges();
            return newResult;
        }

        public List<CompressionResult> GetListByAttempt(int attemptNumber, int testId)
        {
           return _context.CompressionResults.Include("CompressionTest").Where(test => test.CompressionTestId.Equals(testId)).Where(attempt => attempt.AttemptNumber.Equals(attemptNumber)).ToList();
        }

        public List<CompressionResult> GetListByTest(int testId)
        {
            return _context.CompressionResults.Where(test => test.CompressionTestId.Equals(testId)).ToList();
        }

        public bool isResultForTestPresent(int testId)
        {
            if(_context.CompressionResults.ToList().Any(result => result.CompressionTestId.Equals(testId))){
                return true;
            }
            else{
                return false;
            }
        }
    }
}