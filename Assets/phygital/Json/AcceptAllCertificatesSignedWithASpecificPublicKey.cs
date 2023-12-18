using System.Security.Cryptography.X509Certificates;
using phygital.Scripts;
using UnityEngine.Networking;

namespace phygital.Json
{
    public class AcceptAllCertificatesSignedWithASpecificPublicKey : CertificateHandler
    {
        // Encoded RSAPublicKey
        private static string PUB_KEY = "<RSAKeyValue><Modulus>vceJ0icWP5+E/JXuT9uIPkp50GljEAvTm1ItKN5KRq5qMeVu7pEcY/3hzo/sSa3coc4ScuXXywzH5C50MV7Edpdaz+1J1w8/0xXqTGRiPMfO5CjUWsGNaJ7jbUjMqpZNI6QAdPd/k3c7UrSXM9CW239WyYzqnLwtayR/QNv+qYOGyOa/36Y6vy2WdxO95QWxHv6Ok2vgLKnbLe1Gak7EC+PrCaYHOw9ZUiYuav51OiXAy1MiUrVBNHbIOje/UE/IIymn5tCcvLdNNZ+m8TQgqU9PPUIB8M97taowWDSS8XBJTmoFg/5aCc0aAeqSZ/r0ScV2l//3giKxAhMcmopmGQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        private static string PUB_KEY_TEST = "<RSAKeyValue><Modulus>8ObzZeYRjQUtg6zatyBPMNqW5ainSbPcpKasXUVXXaKe+Jvqa029g7OZnnHYpiLdWhp3lZ6/OpmGQ1z4ulcTI+XDDwOxOK+EaVytNx33mVX/EHRR0RRMDuCW3fTXEMhm4O11Fw3NJegI7HgCgB6WDhF0538p6xSIlrZ3Q5BnWySEb5uVlpfLlxjtbg70ZoXRNL3mlNapETRJ6B3rLnvdVJJd7Xylmznyo0uCVogFjFqNIMF6DbDfmo6NyhkY0eC9KUGCjyjtr9AN6125dBm9VBEsXwedWNfKajE4/3G2z/v0Ww93I7R2NzrDt+HRhJRgfPfR2sMMkvSBGH1ugdHamQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

        protected override bool ValidateCertificate(byte[] certificateData)
        {
            var certificate = new X509Certificate2(certificateData);

            var pk = certificate.GetRSAPublicKey().ToXmlString(false);
            if (Env.ENV == "LOCAL" || Env.ENV == "PROD") return true;
            return pk.Equals(PUB_KEY) || pk.Equals(PUB_KEY_TEST);
        }
    }
}