using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            RabbitMQService rabbitMQService = new RabbitMQService();
            IConnection connection = rabbitMQService.GetRabbitMQConnection();
            IModel model = connection.CreateModel();

            IBasicProperties basicProperties = model.CreateBasicProperties();
            basicProperties.SetPersistent(false);

            //enviando a mensagem no formato de matriz de bytes
            byte[] payload = Encoding.UTF8.GetBytes("This message from Visual Studio");

            //construindo o endereço
            PublicationAddress address = new PublicationAddress(ExchangeType.Topic, "exchangeFromVisualStudio", "superstar");

            //enviando a mensagem
            model.BasicPublish(address, basicProperties, payload);

        }
        private static void SetupInitialTopicQueue(IModel model)
        {
            //criando a fila
            model.QueueDeclare("queueFromVisualStudio", true, false, false, null);
            //criando a troca
            model.ExchangeDeclare("exchangeFromVisualStudio", ExchangeType.Topic);
            //ligando a fila a troca
            model.QueueBind("queueFromVisualStudio", "exchangeFromVisualStudio", "superstar");
        }



    }
}
