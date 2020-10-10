using ExperimentToolApi.Models;

namespace ExperimentToolApi.Interfaces
{
    public interface ITextureAnalysisRepository
    {
        TextureAnalysis Create(TextureAnalysis newTextureAnalysis);
        TextureAnalysis GetAnalysisByMaterialId(int materialId);
    }
}