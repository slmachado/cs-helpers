using System.Net;

namespace Helpers;

public class SslHelper
{
    public static void IgnoreCertificateWarning()
    {
        ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
    }
}