using System.Collections.Generic;
using System.Linq;
using ExperimentToolApi.Interfaces;
using ExperimentToolApi.Models;

namespace ExperimentToolApi.Repositories
{
    public class AdditionalFileRepository : IAdditionalFileRepository
    {
        private readonly ExperimentToolDbContext _context;
        public AdditionalFileRepository(ExperimentToolDbContext context){
            _context = context;
        } 
        public AdditionalFile Create(AdditionalFile newFile)
        {
            _context.AdditionalFiles.Add(newFile);
            _context.SaveChanges();
            return newFile;
        }

        public List<AdditionalFile> GetByReference(string reference)
        {
            return _context.AdditionalFiles.Where(file => file.Reference.Equals(reference)).ToList();
        }

        public List<AdditionalFile> GetList()
        {
            return _context.AdditionalFiles.ToList();
        }

        public bool isPresent(string reference)
        {
            if(_context.AdditionalFiles.ToList().Any(file => file.Reference.Equals(reference))){
                return true;
            }
            else{
                return false;
            }
        }
    }
}