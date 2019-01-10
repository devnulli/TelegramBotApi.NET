using System.Xml.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    [XmlRootAttribute(ElementName = "root")]
    public class SendMessageReply : Reply
    {
        [XmlElement(ElementName = "ok")]
        public bool OK;

        [XmlElement(ElementName = "result")]
        public Message sentMessage;
    }
}
