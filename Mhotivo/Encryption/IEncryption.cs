using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace Mhotivo.Encryption
{
    public interface IEncryption
    {
        string EncryptData(string message);

        string DecryptData(string message);
    }
}