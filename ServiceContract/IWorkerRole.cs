using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContract
{
    [ServiceContract]
    public interface IWorkerRole
    {
        [OperationContract]
        void Start(String containerId,string port);
        [OperationContract]
        void Stop();
    }

}
