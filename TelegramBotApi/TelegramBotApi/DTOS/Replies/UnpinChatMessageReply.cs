using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    [XmlRootAttribute(ElementName = "root")]
    public class SuccessReply : Reply
    {
        [XmlElement(ElementName = "ok")]
        public bool OK;
    }
}
