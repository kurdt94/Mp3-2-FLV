using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
       

        }

        public Boolean audio_ready = false;
        public Boolean image_ready = false;
        public Boolean ready = false;
   

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // SUMBMIT BUTTON CLICK 
        private void submit_Click(object sender, EventArgs e)
        {
            // ffmpeg -r 1 -loop 1 -i image.jpg -i audio.mp3 -acodec copy -r 1 -shortest -vf scale=1280:720 ep1.flv

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.FileName = Application.StartupPath + @"\common\ffmpeg.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = $"-r 1 -loop 1 -i \"{textBox1.Text}\" -i \"{textBox2.Text}\" -acodec copy -r 1 -shortest -vf scale=1280:720 \"{System.IO.Path.ChangeExtension(textBox2.Text, null)}.flv\"";

            textBox3.Text = startInfo.Arguments;
           

            try
            {
                using (Process exeProcess = Process.Start(startInfo))
                {

                    exeProcess.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        

    }

        // Select Image File
        private void image_Click(object sender, EventArgs e)
        {
            textBox3.Text = "button1_Click_1";
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Image Files|*.jpg";

            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                try
                {
                    FileInfo fileinfo = new FileInfo(file);
                    long fsize = fileinfo.Length;

                    textBox1.Text = file;
                    pictureBox1.Image = Image.FromFile(file);

                    label1.Text = Decimal.Round(fsize, 2).ToString();  

                }
                catch (System.IO.IOException)
                {
                }
            }

        }
        
        // Select AUDIO FILE
        private void audio_Click(object sender, EventArgs e)
        {
            int size = -1;
            textBox3.Text = "button2_Click";
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Mp3 Audio Files|*.mp3";

            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                try
                {
                    FileInfo fileinfo = new FileInfo(file);
                    long fsize = fileinfo.Length;

                    textBox2.Text = file;
                    label2.Text = Decimal.Round(fsize, 2).ToString();

                }
                catch (System.IO.IOException)
                {
                }
            }
            Console.WriteLine(size); // <-- Shows file size in debugging mode.
            Console.WriteLine(result); // <-- For debugging use.
        }
        
        // On Change AUDIO file
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(textBox2.Text))
            {
                this.audio_ready = true;
            }
            else {
                this.audio_ready = false;
                return;
            }

            if (this.audio_ready == true && this.image_ready == true) {
                submit.Enabled = true;
            }

        }

        // On Change IMAGE file
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(textBox1.Text)){
                this.image_ready = true;
            }
            else
            {
                this.image_ready = false;
                return;
            }
        }


    }



}
