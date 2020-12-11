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
            return _context.AdditionalFiles.Where(file => file.ReferenceType.Equals(reference)).ToList();
        }

        public AdditionalFile GetFileById(int id)
        {
            if(isPresentById(id)){
                return _context.AdditionalFiles.Where(file => file.Id.Equals(id)).Single();
            }
            else{
                return null;
            }
        }

        public List<AdditionalFile> GetList()
        {
            return _context.AdditionalFiles.ToList();
        }

        public bool isPresent(string reference)
        {
            if(_context.AdditionalFiles.ToList().Any(file => file.ReferenceType.Equals(reference))){
                return true;
            }
            else{
                return false;
            }
        }

        public bool isPresentById(int id)
        {
           if(_context.AdditionalFiles.ToList().Any(file => file.Id.Equals(id))){
                return true;
            }
            else{
                return false;
            }
        }
    }
}