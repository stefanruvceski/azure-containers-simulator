using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContract
{
    [ServiceContract]
    public interface IContainer
    {
        [OperationContract]
        String Load(String assemblyName);

        [OperationContract]
        String CheckState();

    }
}
