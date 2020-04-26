﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContract
{
    [ServiceContract]
    public interface IRoleEnvironment
    {
        [OperationContract]
        //String GetAddress(String myAssemblyName, String containerId);
        String GetAddress(String myAssemblyName, String containerId);

        [OperationContract]
        String[] BrotherInstances(String myAssemblyName, String myAddress);
    }
}
