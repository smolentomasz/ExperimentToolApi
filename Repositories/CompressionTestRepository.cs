using System.Collections.Generic;
using System.Linq;
using ExperimentToolApi.Interfaces;
using ExperimentToolApi.Models;

namespace ExperimentToolApi.Repositories
{
    public class CompressionTestRepository : ICompressionTestRepository
    {
        private readonly ExperimentToolDbContext _context;
        public CompressionTestRepository(ExperimentToolDbContext context){
            _context = context;
        } 
        public CompressionTest Create(CompressionTest newTest)
        {
            _context.CompressionTests.Add(newTest);
            _context.SaveChanges();
            return newTest;
        }

        public CompressionTest GetById(int testId)
        {
            return _context.CompressionTests.Where(test => test.Id.Equals(testId)).Single();
        }

        public List<CompressionTest> GetList()
        {
            return _context.CompressionTests.ToList();
        }

        public bool isTestPresent(int testId)
        {
            if (_context.CompressionTests.ToList().Any(test => test.Id.Equals(testId)))
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