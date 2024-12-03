using TestApplication.Data.DTO;
using TestApplication.Data.Interfaces.IService;

namespace TestApplication.Services
{
    public class FileLogger: IFileLogger
    {
        public void LogMessage(IEnumerable<PersonDTO> messageGroups, string filePath)
        {
            var sortedPerson = messageGroups
                .OrderBy(p => p.Age)
                .ThenBy(p => p.FirstName)
                .ThenBy(p => p.LastName)
                .ToList();
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, append: true))
                {

                    foreach (var message in sortedPerson)
                    {
                        writer.WriteLine($"{DateTime.Now}: Age: {message.Age}, LastName: {message.LastName}, FirstName: {message.FirstName}");
                    }
                }
                Console.WriteLine("Message écrit avec succès dans le fichier.");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Erreur lors de l'écriture dans le fichier : {ex.Message}");
            }
        }

        
    }
}
