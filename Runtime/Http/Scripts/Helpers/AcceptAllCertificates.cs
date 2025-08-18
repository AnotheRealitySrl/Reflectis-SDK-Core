using UnityEngine.Networking;

namespace Reflectis.SDK.Http
{
    public class AcceptAllCertificates : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] _) => true;
    }
}
