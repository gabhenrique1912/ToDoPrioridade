using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary3
{
    public interface IRabbitMQProducer
    {
        void SendProductMessage<T>(T message);
    }
}
