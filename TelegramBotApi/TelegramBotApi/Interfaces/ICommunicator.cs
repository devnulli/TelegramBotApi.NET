using nerderies.TelegramBotApi.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace nerderies.TelegramBotApi.Interfaces
{
    public interface ICommunicator
    {
        T GetMultiPartReply<T>(string operationName, params QueryStringParameter[] parameters) where T : Reply;
        T GetReply<T>(string operationName, params QueryStringParameter[] parameters) where T : Reply;
    }
}
