﻿using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Queue
{
    public class MassTransitProvider : IQueueProvider
    {
        readonly IServiceProviderSettings _settings;


        public MassTransitProvider(string hostname = "localhost", UInt16 port = 5672, string username = "guest", string password = "guest")
        {
            if (string.IsNullOrWhiteSpace(hostname))
                throw new ArgumentNullException("hostname");
            if (port <= 0)
                throw new ArgumentOutOfRangeException("port");

            _settings = new ServiceProviderSettings(hostname, port);
        }

        public IQueue<T> GetQueue<T>(string queueName) where T : class, IQueueMessage
        {
            if (string.IsNullOrWhiteSpace(queueName))
                throw new ArgumentNullException("queueName");

            return new MassTransitQueue<T>(_settings, queueName);
        }

        public IQueue<T> GetQueue<T>() where T : class, IQueueMessage
        {
            var queueName = GetQueueFromType<T>();

            return new MassTransitQueue<T>(_settings, queueName);
        }

        private string GetQueueFromType<T>()
        {
            var attr = typeof(T).GetCustomAttributes(typeof(QueuedEntityAttribute), false).FirstOrDefault() as QueuedEntityAttribute;
            if (attr == null || string.IsNullOrWhiteSpace(attr.EntityName))
                return typeof(T).Name;

            return attr.EntityName;
        }
    }
}
