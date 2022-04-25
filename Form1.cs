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
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //    label1.Text = "Removing old zip if present."; // shhhhhh not yet a full package ;)
            //    File.Delete(@".\Bugsplatter.zip"); // That'll be used later.
            label1.Text = "Deleting old version if present.";
            File.Delete(@".\Bugsplatter.exe");
            label1.Text = "Downloading latest version.";
            WebClient client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
            client.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(client_DownloadFileCompleted);
            client.DownloadFileAsync(new Uri("https://github.com/retr0gr4d3/QuickFix/releases/download/working/Bugsplatter_latest.exe"), @".\Bugsplatter.exe");
        }

        void client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            label1.Text = "File(s) downloaded.";
        //    File.Delete(@".\Bugsplatter.exe");
        //    label1.Text = "File deleted. Unpacking...";
        //    string zipPath = @".\update.zip";
        //    string extractPath = @".\";
        //    try
        //    {
        //        ZipFile.ExtractToDirectory(zipPath, extractPath);
        //    }
        //    catch
        //    {
        //        MessageBox.Show("Error! Something went wrong. Opening Bugsplatter.");
        //        Process.Start("Bugsplatter_Beta.exe");
        //    }
        //    label1.Text = "Deleting update.zip...";
        //    File.Delete(@".\update.zip");
            label1.Text = "Update complete!";
            Process.Start(@".\Bugsplatter.exe");
            this.Close();
        }

        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Maximum = (int)e.TotalBytesToReceive / 100;
            progressBar1.Value = (int)e.BytesReceived / 100;
        }

    }
}