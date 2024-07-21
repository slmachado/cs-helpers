using System.Net;

namespace Helpers;

public class SSLHelper
{
    public static void IgnoreCertificateWarning()
    {
        ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
    }
}