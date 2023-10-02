using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBroker.Abstracts
{

    /// <summary>
    /// Represents a server interface on a RemoteProcedureCall channel
    /// </summary>
    public interface IRPCServer
    {

        /// <summary>
        /// Start the service to answer with the provided func on the provided hostName and queueName
        /// </summary>        
        void Start<TRequest, TResponse>(string hostName, string queueName, Func<TRequest, Task<TResponse>> func);


        

    }
}
