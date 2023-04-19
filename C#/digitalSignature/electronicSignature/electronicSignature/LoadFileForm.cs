using System;
using System.Windows.Forms;

namespace electronicSignature
{
    public partial class LoadFileForm : Form
    {
        private FILE file = new FILE();
        private CERTIFICATE certificate = new CERTIFICATE();
        public LoadFileForm()
        {
            InitializeComponent();
        }

        // ЗАГРУЗКА ФАЙЛА
        private void buttonLoad_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            if(openFileDialog.FileName == null || openFileDialog.FileName == string.Empty)
            {
                return;
            }
            file.SetPathFile(openFileDialog.FileName);
            textBoxFileName.Text = file.fileInfo.Name;
            textBoxFileSize.Text = file.fileInfo.Length.ToString();
        }

        private void buttonMakeSignature_Click(object sender, System.EventArgs e)
        {
            if(textBoxPassword.Text == string.Empty)
            {
                MessageBox.Show("Укажите пароль", "Не указан пароль", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            string password = textBoxPassword.Text;
            if (textBoxOutputFileName.Text == string.Empty)
            {
                MessageBox.Show("Укажите выходной файл", "Не указан файл", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            FolderBrowserDialog saveFileDialog = new FolderBrowserDialog();
            var result = saveFileDialog.ShowDialog();
            if(result == DialogResult.OK)
            {
                bool result_sign = file.MakeSignatureDoc(saveFileDialog.SelectedPath, password, textBoxOutputFileName.Text);
                if (result_sign)
                {
                    MessageBox.Show("Signed", "Document Signed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Not Signed", "Document Not Signed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ЗАГРУЗКА СЕРТИФИКАТА
        private void button1_Click(object sender, EventArgs e)
        {
            if(file.fileInfo == null)
            {
                MessageBox.Show("Сначала укажите файл", "Не указан файл", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                var result = openFileDialog.ShowDialog();
                if(result == DialogResult.OK)
                {
                    textBoxCertName.Text = file.certificatePath = openFileDialog.FileName;
                }
            }
        }

        private void buttonVerify_Click(object sender, EventArgs e)
        {
            if (textBoxPassword.Text == string.Empty)
            {
                MessageBox.Show("Укажите пароль", "Не указан пароль", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            string password = textBoxPassword.Text;
            if (file.VerifyDocoment(password))
            {
                MessageBox.Show("Verified", "Подпись ок", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Not Verified", "Подпись нет", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCreateCert_Click(object sender, EventArgs e)
        {
            string password = textBoxCertPassword.Text;
            string name = textBoxCertOutput.Text;
            FolderBrowserDialog saveFileDialog = new FolderBrowserDialog();
            var result = saveFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                bool result_cert = certificate.createCertificate(saveFileDialog.SelectedPath, password, name);
                if (result_cert)
                {
                    MessageBox.Show("Created", "Cert Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Not Created", "Cert Not Created", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
