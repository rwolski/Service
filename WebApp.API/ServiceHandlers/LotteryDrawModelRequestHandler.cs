﻿using System.Threading.Tasks;
using WebApp.API.Contracts;
using Framework.Data;
using System;
using System.Collections.Generic;
using Framework.ServiceBus;

namespace WebApp.API.ServiceHandler
{
    /// <summary>
    /// Request handler for service bus requests for lotter draws
    /// </summary>
    /// <seealso cref="Framework.ServiceBus.IMessageRequestHandler{WebApp.API.Contracts.IDrawModelRequestById}" />
    /// <seealso cref="Framework.ServiceBus.IMessageRequestHandler{WebApp.API.Contracts.IDrawModelRequestLatest}" />
    public class LotteryDrawModelRequestHandler : IMessageRequestHandler<IDrawModelRequestById>, IMessageRequestHandler<IDrawModelRequestLatest>
    {
        readonly IEntityStorage<LotteriesDrawModel> _drawModelStorage;

        /// <summary>
        /// Initializes a new instance of the <see cref="LotteryDrawModelRequestHandler"/> class.
        /// </summary>
        public LotteryDrawModelRequestHandler(IDatabaseConnection dataConnection)
        {
            if (dataConnection == null)
                throw new ArgumentNullException("dataConnection");

            _drawModelStorage = dataConnection.GetCollection<LotteriesDrawModel>("Server_LotteriesDraw");
        }

        /// <summary>
        /// Performs the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public Task<object> Request(IDrawModelRequestById request)
        {
            var draw = _drawModelStorage.FindByIdentity(request.DrawId);
            return Task.FromResult((object)draw);
        }

        /// <summary>
        /// Performs the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public Task<object> Request(IDrawModelRequestLatest request)
        {
            var orderBy = new List<OrderBy<LotteriesDrawModel>>()
            {
                new OrderBy<LotteriesDrawModel>()
                {
                    Exp = x => x.Id,
                    Ascending = false
                }
            };

            var lastDraw = _drawModelStorage.FindFirstOrDefault(null, orderBy);
            return Task.FromResult(lastDraw != null ? (object)lastDraw : null);
        }
    }
}