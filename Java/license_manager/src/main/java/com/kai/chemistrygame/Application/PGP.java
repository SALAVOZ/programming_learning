package com.kai.chemistrygame.Application;

import com.kai.chemistrygame.Domain.JsonLicense;
import org.bouncycastle.bcpg.ArmoredInputStream;
import org.bouncycastle.jce.provider.BouncyCastleProvider;
import org.bouncycastle.openpgp.*;
import org.bouncycastle.openpgp.operator.jcajce.JcaKeyFingerprintCalculator;

import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.security.Provider;
import java.security.Security;
import java.util.Iterator;

public class PGP {
    static PGPPublicKey readPublicKey(InputStream inputStream) throws IOException, PGPException
    {
        PGPPublicKeyRingCollection pgpPublicKeyRings = new PGPPublicKeyRingCollection(
                PGPUtil.getDecoderStream(inputStream), new JcaKeyFingerprintCalculator()
        );

        Iterator keyRingIter = pgpPublicKeyRings.getKeyRings();
        while (keyRingIter.hasNext())
        {
            PGPPublicKeyRing keyRing = (PGPPublicKeyRing) keyRingIter.next();
            Iterator keyIter = keyRing.getPublicKeys();
            while (keyIter.hasNext())
            {
                PGPPublicKey key = (PGPPublicKey)keyIter.next();

                if (key.isEncryptionKey())
                {
                    return key;
                }
            }
        }

        throw new IllegalArgumentException("Can't find encryption key in key ring.");
    }

    public static PGPPublicKey readPublicKeyFromString (String armoredPublicPGPkeyBlock) throws IOException, PGPException {

        InputStream in = new ByteArrayInputStream(armoredPublicPGPkeyBlock.getBytes());

        PGPPublicKey pgpPublicKey = readPublicKey(in);

        in.close();

        return pgpPublicKey;
    }

      private static JsonLicense readVanillaLicense(byte[] licenseKey) throws LicenseReadingException {
             ArmoredInputStream armoredInputStream;
          boolean verified;
            JsonLicense license;
             if (licenseKey == null) {
                }
            try {
                   armoredInputStream = new ArmoredInputStream(new ByteArrayInputStream(Utf8Utils.getBytes("-----BEGIN PGP PUBLIC KEY BLOCK-----\nVersion: GnuPG v1.4.11 (GNU/Linux)\n\n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\n=TmBA\n-----END PGP PUBLIC KEY BLOCK-----")));
                 } catch (IOException e) {
                 throw new LicenseReadingException(LicenseData.State.ERROR_KEY);
                 }


            Security.addProvider((Provider)new BouncyCastleProvider());

            ByteArrayOutputStream dataOut = new ByteArrayOutputStream();

            try {
                   InputStream in = new ByteArrayInputStream(licenseKey);
                  verified = ClearSignedFileProcessor.verifyFile(in, (InputStream)armoredInputStream, dataOut);
                 } catch (Exception e) {
                   throw new LicenseReadingException(LicenseData.State.ERROR_DATA);
                 }
             if (!verified) {
                  throw new LicenseReadingException(LicenseData.State.INVALID_SIGNATURE);
                 }

             try {
                   String stringLicense = Utf8Utils.newString(dataOut.toByteArray());

                   license = (JsonLicense)JsonUtil.valueFromString(new TypeReference<JsonLicense>() {  }, stringLicense);
                 } catch (Exception e) {
                                String a = "";
             }
             if (license == null) {
                   String a = "";
                }

            return license;
           }




}

