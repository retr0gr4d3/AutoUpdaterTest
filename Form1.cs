using System.Net;
using System.IO;
using System.Diagnostics;
using System.IO.Compression;

namespace UpdaterProcessTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "Removing old zip if present.";
            File.Delete(@".\RetroTest_latest.zip");
            label1.Text = "Downloading latest version.";
            WebClient client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
            client.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(client_DownloadFileCompleted);
            client.DownloadFileAsync(new Uri("https://github.com/retr0gr4d3/AutoUpdaterTest/releases/download/Stable/RetroTest_latest.zip"), @".\update.zip");
        }

        void client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            label1.Text = "File downloaded. Deleting old version.";
            File.Delete(@".\\RetroTest.png");
            label1.Text = "File deleted. Unpacking...";
            string zipPath = @".\update.zip";
            string extractPath = @".\";
            try
            {
                ZipFile.ExtractToDirectory(zipPath, extractPath);
            }
            catch
            {
                MessageBox.Show("Error! Something went wrong. Opening Bugsplatter.");
                Process.Start("Bugsplatter_Beta.exe");
            }
            label1.Text = "Deleting update.zip...";
            File.Delete(@".\update.zip");
            label1.Text = "Update complete!";
            Process.Start(@".\Bugsplatter_Beta.exe");
            this.Close();
        }

        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Maximum = (int)e.TotalBytesToReceive / 100;
            progressBar1.Value = (int)e.BytesReceived / 100;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}