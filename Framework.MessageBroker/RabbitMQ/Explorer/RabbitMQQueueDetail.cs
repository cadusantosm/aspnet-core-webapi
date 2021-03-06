﻿namespace Framework.MessageBroker.RabbitMQ.Explorer
{
    public class RabbitMQQueueDetail
    {
        public bool auto_delete { get; set; }
        public bool durable { get; set; }
        public bool exclusive { get; set; }
        public string idle_since { get; set; }
        public string name { get; set; }
        public string node { get; set; }
        public string state { get; set; }
        public string vhost { get; set; }
    }
}
