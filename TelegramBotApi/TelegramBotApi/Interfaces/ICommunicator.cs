using nerderies.TelegramBotApi.DTOS;

namespace nerderies.TelegramBotApi.Interfaces
{
    public interface ICommunicator
    {
        T GetMultiPartReply<T>(string operationName, params MultiPartParameter[] parameters) where T : Reply;
        T GetReply<T>(string operationName, params QueryStringParameter[] parameters) where T : Reply;
        byte[] GetFileContent(string filePath);
    }
}
