using System.Collections.Generic;
using System.Linq;
using ExperimentToolApi.Interfaces;
using ExperimentToolApi.Models;

namespace ExperimentToolApi.Repositories
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly ExperimentToolDbContext _context;
        public MaterialRepository(ExperimentToolDbContext context)
        {
            _context = context;
        }
        public Material Create(Material newMaterial)
        {
            _context.Materials.Add(newMaterial);
            _context.SaveChanges();
            return newMaterial;
        }

        public List<Material> GetList()
        {
            return _context.Materials.ToList();
        }

        public Material GetMatrialById(int materialId)
        {
            return _context.Materials.Where(material => material.Id.Equals(materialId)).Single();
        }

        public bool isMaterialPresent(int materialId)
        {
            if (_context.Materials.ToList().Any(material => material.Id.Equals(materialId)))
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