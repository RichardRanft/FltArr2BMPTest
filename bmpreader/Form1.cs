using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bmpreader
{
    public partial class Form1 : Form
    {
        private float min_val = 1000;
        private float max_val = -1000;
        public static Bitmap BytesToBitmap(byte[] byteArray)
        {
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                Bitmap img = (Bitmap)Image.FromStream(ms);
                return img;
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void processFile(String infile, String outfile)
        {
            if (infile.Length < 1 || outfile.Length < 1)
                return;
            FileStream fs = new FileStream(infile, FileMode.Open, FileAccess.Read);
            BinaryReader r = new BinaryReader(fs);
            long len = fs.Length / 4;
            float[] buffer = new float[len];
            byte[] megagrey = new byte[len];

            for (int i = 0; i < len; i++)
            {
                // byte[] myData = new byte[4];
                //r.Read(myData,0,4);
                //Single myvalue = BitConverter.ToSingle(myData, 0);
                buffer[i] = r.ReadSingle();
                //buffer[i] = 
                if (min_val > buffer[i])
                {
                    min_val = buffer[i];
                }
                if (max_val < buffer[i])
                {
                    max_val = buffer[i];
                }
            }
            for (int index = 0; index < 1048576; index++)
            {
                float max_min = (max_val + min_val);
                float temp = (buffer[index] + min_val);
                float div = ((temp / max_min) * 255);
                //div += 1/2;
                if (div < 0)
                {
                    div = 0;
                }
                megagrey[index] = System.Convert.ToByte(div);
                //megagrey[index] = System.Convert.ToByte(buffer[index]*255);
            }

            Bitmap newBitmap = new Bitmap(1024, 1024, PixelFormat.Format24bppRgb);

            for (int j = 0; j < 1024; j++)
            {
                for (int i = 0; i < 1024; i++)
                {
                    Color newColor = Color.FromArgb((int)megagrey[i + j * 1024], (int)megagrey[i + j * 1024], (int)megagrey[i + j * 1024]);
                    //Color newColor = Color.FromArgb((int)(i/4), (int)(j/4), (int)0);
                    newBitmap.SetPixel(i, j, newColor);
                }
            }
            pbxImagePreview.Image = newBitmap;

            // MemoryStream bitmapDataStream = new MemoryStream(megagrey);
            //  Bitmap bitmap = new Bitmap(bitmapDataStream);
            // bitmap.Save("medota2", ImageFormat.Bmp);
            Image img = (Image)newBitmap;
            if (Path.GetFileName(outfile) == "")
                outfile = outfile + "\\image.bmp";
            if (!outfile.ToLower().Contains(".bmp"))
                outfile += ".bmp";
            newBitmap.Save(outfile, ImageFormat.Bmp);
            //int[,] var = new int[1024,1024];
            float[] var = new float[1024];
            string line;
            System.IO.StreamWriter file = new System.IO.StreamWriter("Log.txt");
            for (int j = 0; j < 1024; j++)
            {
                for (int i = 0; i < 1024; i++)
                {
                    //printf((int)megagrey[i + j * 1024]);
                    //var[j,i] = ((int)megagrey[i + j * 1024]);
                    var[i] = ((float)buffer[i + j * 1024]);
                    //line = System.Convert.ToString(var[i]);
                    line = var[i].ToString("R");
                    file.Write(line);
                    line = " ";
                    file.Write(line);
                }
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            processFile(tbxInput.Text, tbxOutputFile.Text);
        }

        private void btnBrowseInput_Click(object sender, EventArgs e)
        {
            if(ofdInfile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbxInput.Text = ofdInfile.FileName;
            }
        }

        private void btnBrowseOutput_Click(object sender, EventArgs e)
        {
            if(fbdOutfolder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbxOutputFile.Text = fbdOutfolder.SelectedPath + "\\output.bmp";
            }
        }
    }
}
