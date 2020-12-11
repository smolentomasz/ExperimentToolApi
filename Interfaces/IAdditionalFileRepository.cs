using System.Collections.Generic;
using ExperimentToolApi.Models;

namespace ExperimentToolApi.Interfaces
{
    public interface IAdditionalFileRepository
    {
        AdditionalFile Create(AdditionalFile newFile);
        List<AdditionalFile> GetList();
        AdditionalFile GetFileById(int id);
        List<AdditionalFile> GetByReference(string reference);
        bool isPresentById(int id);
        bool isPresent(string reference);
    }
}