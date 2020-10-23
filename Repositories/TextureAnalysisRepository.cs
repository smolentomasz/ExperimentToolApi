using System.Collections.Generic;
using System.Linq;
using ExperimentToolApi.Interfaces;
using ExperimentToolApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ExperimentToolApi.Repositories
{
    public class TextureAnalysisRepository : ITextureAnalysisRepository
    {
        private readonly ExperimentToolDbContext _context;
        public TextureAnalysisRepository(ExperimentToolDbContext context){
            _context = context;
        } 
        public TextureAnalysis Create(TextureAnalysis newTextureAnalysis)
        {
            _context.TextureAnalyses.Add(newTextureAnalysis);
            _context.SaveChanges();
            return newTextureAnalysis;
        }

        public TextureAnalysis GetAnalysisByMaterialId(int materialId)
        {
            return _context.TextureAnalyses.Include("Material").Where(analyse => analyse.MaterialId.Equals(materialId)).Single();
        }

        public List<TextureAnalysis> GetList()
        {
            return _context.TextureAnalyses.ToList();
        }
    }
}