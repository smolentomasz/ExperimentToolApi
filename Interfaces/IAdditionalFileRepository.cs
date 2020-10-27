using System.Collections.Generic;
using ExperimentToolApi.Models;

namespace ExperimentToolApi.Interfaces
{
    public interface IAdditionalFileRepository
    {
        AdditionalFile Create(AdditionalFile newFile);
        List<AdditionalFile> GetList();
        List<AdditionalFile> GetByReference(string reference);
        bool isPresent(string reference);
    }
}