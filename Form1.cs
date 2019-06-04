using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Galery
{
    public partial class Form1 : Form
    {
        // List<Image> imlist = new List<Image>();
        List<string> comments;
        int index = 0;
        public Form1()
        {
            InitializeComponent();
            comments = new List<string>();
            //Serial();
            Deserial();
        }

        private void bSearch_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            tbFolder.Text = folderBrowserDialog1.SelectedPath.ToString();

            DirectoryInfo dir = new DirectoryInfo(tbFolder.Text);
           
            var images = Directory.GetFiles(tbFolder.Text, "*.jpg");
            string[] directory = Directory.GetFiles(tbFolder.Text, "*.jpg", SearchOption.TopDirectoryOnly);
            var direc = dir.GetDirectories();
            foreach (var item in directory)
            {
               // var lvi = new ListViewItem(item.Name);
                lbFiles.Items.Add(images.ToString());
                pbImage.Image = Image.FromFile(images[index]);
                pbImage.SizeMode = PictureBoxSizeMode.StretchImage;
               
            }
           
           
        }

        private void pbImage_Click(object sender, EventArgs e)
        {
            
        }

        private void bRight_Click(object sender, EventArgs e)
        {
            var images = Directory.GetFiles(tbFolder.Text, "*.jpg");
            if (index < images.Count()-1)
            {
                index++;
                pbImage.Image = Image.FromFile(images[index]);
                pbImage.SizeMode = PictureBoxSizeMode.StretchImage;
              //  lbComments.Items.Clear();
            }
        }

        private void bLeft_Click(object sender, EventArgs e)
        {
            var images = Directory.GetFiles(tbFolder.Text, "*.jpg");
            if (index >0)
            {
                index--;
                pbImage.Image = Image.FromFile(images[index]);
                pbImage.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            lbComments.Items.Add(tbComment.Text);
            tbComment.Clear();
            comments.Add(tbComment.Text);
        }

        BinaryFormatter bw = new BinaryFormatter();
        public void Serial()
        {
            using (FileStream fs = new FileStream("Comments.txt", FileMode.OpenOrCreate))
            {
                Serialization ob1 = new Serialization();
                ob1.comments = comments;
                bw.Serialize(fs, ob1);
            }
        }

        public void Deserial()
        {
            using (FileStream fs = new FileStream("Comments.txt", FileMode.OpenOrCreate))
            {
                Serialization ob2 = (Serialization)bw.Deserialize(fs);
                comments = ob2.comments;
               
            }
        }
    }

    [Serializable]
    class Serialization
    {
       public List<string> comments { get; set; }

    }
}
