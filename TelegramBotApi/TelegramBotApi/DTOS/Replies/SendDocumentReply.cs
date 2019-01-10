using System.Xml.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    [XmlRootAttribute(ElementName = "root")]
    public class SendDocumentReply : Reply
    {
        [XmlElement(ElementName = "ok")]
        public bool OK;

        public Message sentMessage;
    }
}
