
using ServiceCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBroker.Abstracts
{

    /// <summary>
    /// Represents a client interface on a RemoteProcedureCall channel
    /// </summary>
    public interface IRPCClient
    {

        /// <summary>
        /// Get the response for the provided request on the provided hostName and queueName
        /// </summary>     
        public Task<ServiceResult<TResponse>> GetResponse<TRequest, TResponse>(string hostname, string queuename, TRequest request);

        /// <summary>
        /// Get the response as plain text for the provided request on the provided hostName and queueName
        /// </summary> 
        public Task<string> GetResponseAsString<TRequest, TResponse>(string hostname, string queuename, TRequest request);

    }
}
