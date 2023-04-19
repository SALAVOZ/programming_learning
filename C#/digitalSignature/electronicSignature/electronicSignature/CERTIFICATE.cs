using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace electronicSignature
{
    internal class CERTIFICATE
    {
        public bool createCertificate(string dirPath, string password, string name)
        {
            var ecdsa = ECDsa.Create(); // generate asymmetric key pair
            var req = new CertificateRequest("cn=foobar", ecdsa, HashAlgorithmName.SHA256);
            var cert = req.CreateSelfSigned(DateTimeOffset.Now, DateTimeOffset.Now.AddYears(5));
            try
            {
                // Create PFX (PKCS #12) with private key
                File.WriteAllBytes(dirPath + "\\" + name + ".pfx", cert.Export(X509ContentType.Pfx, password));

                // Create Base 64 encoded CER (public key only)
                File.WriteAllText(dirPath + $"\\{name}.cer",
                    "-----BEGIN CERTIFICATE-----\r\n"
                    + Convert.ToBase64String(cert.Export(X509ContentType.Cert), Base64FormattingOptions.InsertLineBreaks)
                    + "\r\n-----END CERTIFICATE-----");
                return true;
            }
            catch(Exception exp)
            {
                return false;
            }
        }
    }
}
