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
        public TensileResult Create(TensileResult newResult)
        {
            _context.TensileResults.Add(newResult);
            _context.SaveChanges();
            return newResult;
        }

        public List<TensileResult> GetListByAttempt(int attemptNumber, int testId)
        {
            return _context.TensileResults.Include("TensileTest").Where(test => test.TensileTestId.Equals(testId)).Where(attempt => attempt.AttemptNumber.Equals(attemptNumber)).ToList();

        }

        public List<TensileResult> GetListByTest(int testId)
        {
            return _context.TensileResults.Where(test => test.TensileTestId.Equals(testId)).ToList();
        }
    }
}