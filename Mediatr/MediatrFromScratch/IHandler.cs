﻿using System.Threading.Tasks;

namespace MediatrFromScratch
{
    public interface IHandler<in TRequest, TResponse> where TRequest: IRequest<TResponse>
    {
        Task<TResponse> HandleAsync(TRequest request);
    }
}