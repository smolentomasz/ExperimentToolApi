using System.Runtime.Intrinsics.X86;
using System.Collections.Generic;
using System.Linq;
using ExperimentToolApi.Interfaces;
using ExperimentToolApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ExperimentToolApi.Repositories
{
    public class TensileResultRepository : ITensileResultRepository
    {
        private readonly ExperimentToolDbContext _context;
        public TensileResultRepository(ExperimentToolDbContext context){
            _context = context;
        } 
        public void Create(List<TensileResult> resultsList)
        {
            _context.TensileResults.AddRange(resultsList);  
            _context.SaveChanges();
        }

        public List<TensileResult> GetListByAttempt(int attemptNumber, int testId)
        {
            return _context.TensileResults.Where(test => test.TensileTestId.Equals(testId)).Include("TensileTest").Where(attempt => attempt.AttemptNumber.Equals(attemptNumber)).ToList();

        }

        public List<int> GetListByTest(int testId)
        {
            return _context.TensileResults.Where(result => result.TensileTestId.Equals(testId)).Select(result => result.AttemptNumber).Distinct().ToList();
        }
         public bool isResultForTestPresent(int testId)
        {
            if(_context.TensileResults.ToList().Any(result => result.TensileTestId.Equals(testId))){
                return true;
            }
            else{
                return false;
            }
        }
        public bool isAttemptForTestPresent(int attemptNumber, int testId)
        {
            if(isResultForTestPresent(testId)){
                if(_context.TensileResults.Where(test => test.TensileTestId.Equals(testId)).ToList().Any(result => result.AttemptNumber.Equals(attemptNumber))){
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