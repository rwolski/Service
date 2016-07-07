﻿using Autofac;
using System.Threading.Tasks;

namespace Framework.ServiceBus
{
    public interface IServiceEvent
    {
        ILifetimeScope Scope { get; set; }

        Task Action();
    }

    public interface IServiceContractAction<TContract>
    {
        TContract Contract { get; }

        Task Action();
    }

    public interface IServiceContract
    {
    }
}