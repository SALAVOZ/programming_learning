package com.kai.chemistrygame.Application;
 
import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.security.NoSuchAlgorithmException;
import java.security.NoSuchProviderException;
import java.security.SignatureException;
import java.util.Iterator;
import org.bouncycastle.bcpg.ArmoredInputStream;
import org.bouncycastle.bcpg.ArmoredOutputStream;
import org.bouncycastle.bcpg.BCPGOutputStream;
import org.bouncycastle.openpgp.PGPException;
import org.bouncycastle.openpgp.PGPPrivateKey;
import org.bouncycastle.openpgp.PGPPublicKey;
import org.bouncycastle.openpgp.PGPPublicKeyRingCollection;
import org.bouncycastle.openpgp.PGPSecretKey;
import org.bouncycastle.openpgp.PGPSecretKeyRing;
import org.bouncycastle.openpgp.PGPSecretKeyRingCollection;
import org.bouncycastle.openpgp.PGPSignature;
import org.bouncycastle.openpgp.PGPSignatureGenerator;
import org.bouncycastle.openpgp.PGPSignatureList;
import org.bouncycastle.openpgp.PGPSignatureSubpacketGenerator;
import org.bouncycastle.openpgp.PGPUtil;
import org.bouncycastle.openpgp.jcajce.JcaPGPObjectFactory;
import org.bouncycastle.openpgp.operator.KeyFingerPrintCalculator;
import org.bouncycastle.openpgp.operator.PGPContentSignerBuilder;
import org.bouncycastle.openpgp.operator.PGPContentVerifierBuilderProvider;
import org.bouncycastle.openpgp.operator.jcajce.JcaKeyFingerprintCalculator;
import org.bouncycastle.openpgp.operator.jcajce.JcaPGPContentSignerBuilder;
import org.bouncycastle.openpgp.operator.jcajce.JcaPGPContentVerifierBuilderProvider;
import org.bouncycastle.openpgp.operator.jcajce.JcePBESecretKeyDecryptorBuilder;
 

 
public class ClearSignedFileProcessor
{
    private static PGPSecretKey readSecretKey(InputStream in) throws IOException, PGPException
    {
        PGPSecretKeyRingCollection pgpSec = new PGPSecretKeyRingCollection(PGPUtil.getDecoderStream(in), (KeyFingerPrintCalculator)new JcaKeyFingerprintCalculator());
        PGPSecretKey key = null;
        Iterator<?> rIt = pgpSec.getKeyRings();

        while (key == null && rIt.hasNext())
        {
            PGPSecretKeyRing kRing = (PGPSecretKeyRing)rIt.next();
            Iterator<?> kIt = kRing.getSecretKeys();
            while (key == null && kIt.hasNext())
            {
                PGPSecretKey k = (PGPSecretKey)kIt.next();
                if (k.isSigningKey())
                {
                    key = k;
                }
            }
        }
        if (key == null)
        {
            throw new IllegalArgumentException("Can't find signing key in key ring.");
        }
        return key;
    }
    private static int readInputLine(ByteArrayOutputStream bOut, InputStream fIn) throws IOException {
         bOut.reset();
         int lookAhead = -1;
         int ch;
         while ((ch = fIn.read()) >= 0)
         {
             bOut.write(ch);
             if (ch == 13 || ch == 10)
             {
                 lookAhead = readPassedEOL(bOut, ch, fIn);
                 break;
             }
         }
         return lookAhead;
    }


    private static int readInputLine(ByteArrayOutputStream bOut, int lookAhead, InputStream fIn) throws IOException
    {
         bOut.reset();
         int ch = lookAhead;
         do
         {
            bOut.write(ch);
            if (ch == 13 || ch == 10)
            {
            lookAhead = readPassedEOL(bOut, ch, fIn);
                 break;
            }
          }
         while ((ch = fIn.read()) >= 0);
         return lookAhead;
    }


    private static int readPassedEOL(ByteArrayOutputStream bOut, int lastCh, InputStream fIn) throws IOException
    {
         int lookAhead = fIn.read();
         if (lastCh == 13 && lookAhead == 10)
         {
             bOut.write(lookAhead);
             lookAhead = fIn.read();
         }
         return lookAhead;
    }


    public static PGPSignature getFileSignature(InputStream in, InputStream keyIn, OutputStream dataOut) throws Exception
    {
         ArmoredInputStream aIn = new ArmoredInputStream(in);
         ByteArrayOutputStream out = new ByteArrayOutputStream();
         ByteArrayOutputStream lineOut = new ByteArrayOutputStream();
         int lookAhead = readInputLine(lineOut, (InputStream)aIn);
         byte[] lineSep = getLineSeparator();
         if (lookAhead != -1 && aIn.isClearText())
          {
               byte[] line = lineOut.toByteArray();
               out.write(line, 0, getLengthWithoutSeperator(line));
               out.write(lineSep);
               while (lookAhead != -1 && aIn.isClearText())
               {
                   lookAhead = readInputLine(lineOut, lookAhead, (InputStream)aIn);
                   line = lineOut.toByteArray();
                   out.write(line, 0, getLengthWithoutSeperator(line));
                   out.write(lineSep);
              }
     }
         out.close();
         byte[] outBytes = out.toByteArray();
         dataOut.write(outBytes);
         PGPPublicKeyRingCollection pgpRings = new PGPPublicKeyRingCollection(PGPUtil.getDecoderStream(keyIn), (KeyFingerPrintCalculator)new JcaKeyFingerprintCalculator());
         JcaPGPObjectFactory jcaPGPObjectFactory = new JcaPGPObjectFactory((InputStream)aIn);
         PGPSignatureList p3 = (PGPSignatureList)jcaPGPObjectFactory.nextObject();
         PGPSignature sig = p3.get(0);
         PGPPublicKey key = pgpRings.getPublicKey(sig.getKeyID());
         sig.init((PGPContentVerifierBuilderProvider)(new JcaPGPContentVerifierBuilderProvider()).setProvider("BC"), key);
         InputStream sigIn = new ByteArrayInputStream(outBytes);
         lookAhead = readInputLine(lineOut, sigIn);
         processLine(sig, lineOut.toByteArray());
         if (lookAhead != -1)
         {
            do
            {
                 lookAhead = readInputLine(lineOut, lookAhead, sigIn);
                 sig.update((byte)13);
                 sig.update((byte)10);
                 processLine(sig, lineOut.toByteArray());
             }
             while (lookAhead != -1);
        }
         return sig;
    }





    public static boolean verifyFile(InputStream in, InputStream keyIn, OutputStream dataOut) throws Exception
    {
        // return getFileSignature(in, keyIn, dataOut).verify();
        PGPSignature a = getFileSignature(in, keyIn, dataOut);
        return a.verify();
    }

    private static byte[] getLineSeparator()
    {
         String nl = System.getProperty("line.separator");
         byte[] nlBytes = new byte[nl.length()];
         for (int i = 0; i != nlBytes.length; i++)
         {
             nlBytes[i] = (byte)nl.charAt(i);
         }
         return nlBytes;
    }

    public static void signFile(InputStream fIn, InputStream keyIn, OutputStream out, char[] pass, String digestName) throws IOException, NoSuchAlgorithmException, NoSuchProviderException, PGPException, SignatureException
    {
        int digest;
         if (digestName.equals("SHA256")) {
         digest = 8;
         } else if (digestName.equals("SHA384")) {
         digest = 9;
         } else if (digestName.equals("SHA512")) {
         digest = 10;
         } else if (digestName.equals("MD5")) {
         digest = 1;
         } else if (digestName.equals("RIPEMD160")) {
         digest = 3;
        } else {
         digest = 2;
        }

         PGPSecretKey pgpSecKey = readSecretKey(keyIn);
         PGPPrivateKey pgpPrivKey = pgpSecKey.extractPrivateKey((new JcePBESecretKeyDecryptorBuilder()).setProvider("BC").build(pass));
         PGPSignatureGenerator sGen = new PGPSignatureGenerator((PGPContentSignerBuilder)(new JcaPGPContentSignerBuilder(pgpSecKey.getPublicKey().getAlgorithm(), digest)).setProvider("BC"));
         PGPSignatureSubpacketGenerator spGen = new PGPSignatureSubpacketGenerator();

         sGen.init(0, pgpPrivKey);

         Iterator<?> it = pgpSecKey.getPublicKey().getUserIDs();
         if (it.hasNext()) {
         spGen.setSignerUserID(false, (String)it.next());
         sGen.setHashedSubpackets(spGen.generate());
        }
         ArmoredOutputStream aOut = new ArmoredOutputStream(out);
         aOut.beginClearText(digest);

         ByteArrayOutputStream lineOut = new ByteArrayOutputStream();
         int lookAhead = readInputLine(lineOut, fIn);

         processLine((OutputStream)aOut, sGen, lineOut.toByteArray());

         if (lookAhead != -1) {
        do {
         lookAhead = readInputLine(lineOut, lookAhead, fIn);

         sGen.update((byte)13);
         sGen.update((byte)10);

         processLine((OutputStream)aOut, sGen, lineOut.toByteArray());
         } while (lookAhead != -1);
        }

         aOut.endClearText();

         BCPGOutputStream bOut = new BCPGOutputStream((OutputStream)aOut);

         sGen.generate().encode((OutputStream)bOut);

         aOut.close();
    }


    private static void processLine(PGPSignature sig, byte[] line) throws SignatureException, IOException
    {
        int length = getLengthWithoutWhiteSpace(line);
        if (length > 0)
        {
            sig.update(line, 0, length);
        }
    }



    private static void processLine(OutputStream aOut, PGPSignatureGenerator sGen, byte[] line) throws SignatureException, IOException
    {
        int length = getLengthWithoutWhiteSpace(line);
        if (length > 0)
        {
            sGen.update(line, 0, length);
        }
        aOut.write(line, 0, line.length);
    }

    private static int getLengthWithoutSeperator(byte[] line)
    {
        int end = line.length - 1;
        while (end >= 0 && isLineEnding(line[end]))
        {
            end--;
        }
        return end + 1;
    }

    private static boolean isLineEnding(byte b)
    {
         return (b == 13 || b == 10);
    }

    private static int getLengthWithoutWhiteSpace(byte[] line)
    {
         int end = line.length - 1;
         while (end >= 0 && isWhiteSpace(line[end]))
         {
             end--;
         }
         return end + 1;
    }
 
     private static boolean isWhiteSpace(byte b)
     {
          return (b == 13 || b == 10 || b == 9 || b == 32);
     }
}
