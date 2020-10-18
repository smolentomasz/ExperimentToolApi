using System.Collections.Generic;
using System.Linq;
using ExperimentToolApi.Interfaces;
using ExperimentToolApi.Models;

namespace ExperimentToolApi.Repositories
{
    public class TensileTestRepository : ITensileTestRepository
    {
        private readonly ExperimentToolDbContext _context;
        public TensileTestRepository(ExperimentToolDbContext context){
            _context = context;
        } 
        public TensileTest Create(TensileTest newTest)
        {
            _context.TensileTests.Add(newTest);
            _context.SaveChanges();
            return newTest;
        }

        public TensileTest GetById(int testId)
        {
            return _context.TensileTests.Where(test => test.Id.Equals(testId)).Single();
        }

        public List<TensileTest> GetList()
        {
            return _context.TensileTests.ToList();
        }

        public bool isTestPresent(int testId)
        {
            if (_context.TensileTests.ToList().Any(test => test.Id.Equals(testId)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}