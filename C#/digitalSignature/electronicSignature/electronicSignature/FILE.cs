using GroupDocs.Signature;
using GroupDocs.Signature.Options;
using System;
using System.IO;

namespace electronicSignature
{
    internal class FILE
    {
        public string certificatePath;
        public FileInfo fileInfo;
        public void SetPathFile(string pathFile)
        {
            if(pathFile == string.Empty)
            {
                return;
            }
            fileInfo = new FileInfo(pathFile);
        }
        //GroupDocs.Signature.GroupDocsSignatureException
        public bool MakeSignatureDoc(string outputDir, string password, string name)
        {
            try
            {


                using (Signature signature = new Signature(fileInfo.FullName))
                {
                    DigitalSignOptions digitalSignOptions = new DigitalSignOptions(certificatePath)
                    {
                        Password = password
                    };
                    signature.Sign(outputDir + "\\" + name, digitalSignOptions);
                }
                return true;
            }
            catch(Exception exp)
            {
                return false;
            }
        }
        public bool VerifyDocoment(string password)
        {
            using (Signature signature = new Signature(fileInfo.FullName))
            {
                //TextSignOptions textSignOptions = new TextSignOptions("salavat abdulin");
                DigitalVerifyOptions verifyOptions = new DigitalVerifyOptions(certificatePath)
                {
                    Password = password
                };
                try
                {
                    bool result = signature.Verify(verifyOptions).IsValid;
                    return result;
                }
                catch (GroupDocsSignatureException exp)
                {
                    return false;
                }
            }
        }
    }
}
