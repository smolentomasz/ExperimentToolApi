using System.Collections.Generic;
using ExperimentToolApi.Models;

namespace ExperimentToolApi.Interfaces
{
    public interface IMaterialRepository
    {
        Material Create(Material newMaterial);
        List<Material> GetList();
        Material GetMaterialById(int materialId);
        bool isMaterialPresent(int materialId);
    }
}