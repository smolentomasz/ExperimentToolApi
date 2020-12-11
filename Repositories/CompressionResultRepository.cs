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
        public void Create(List<CompressionResult> resultList)
        {
            _context.CompressionResults.AddRange(resultList);
            _context.SaveChanges();
        }

        public List<CompressionResult> GetListByAttempt(int attemptNumber, int testId)
        {
           return _context.CompressionResults.Where(test => test.CompressionTestId.Equals(testId)).Include("CompressionTest").Where(attempt => attempt.AttemptNumber.Equals(attemptNumber)).ToList();
        }

        public List<int> GetListByTest(int testId)
        {
            return _context.CompressionResults.Where(test => test.CompressionTestId.Equals(testId)).Select(result => result.AttemptNumber).Distinct().ToList();
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
        public bool isAttemptForTestPresent(int attemptNumber, int testId)
        {
            if(isResultForTestPresent(testId)){
                if(_context.CompressionResults.Where(test => test.CompressionTestId.Equals(testId)).ToList().Any(result => result.AttemptNumber.Equals(attemptNumber))){
                    return true;
                }
                else{
                    return false;
                }
            }
            else{
                return false;
            }
        }
    }
}