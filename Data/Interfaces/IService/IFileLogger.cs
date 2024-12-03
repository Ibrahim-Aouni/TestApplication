using TestApplication.Data.DTO;

namespace TestApplication.Data.Interfaces.IService
{
    public interface IFileLogger
    {
        void LogMessage(IEnumerable<PersonDTO> messageGroups, string filePath);
    }
}
