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
        private System.IO.StreamWriter m_logfile;
        private string formatter = "{0,16:E7}{1,20}";

        public Form1()
        {
            InitializeComponent();
            m_logfile = new System.IO.StreamWriter("Log.txt");
        }

        public static Bitmap BytesToBitmap(byte[] byteArray)
        {
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                Bitmap img = (Bitmap)Image.FromStream(ms);
                return img;
            }
        }

        private void processFile(String infile, String outfile)
        {
            if (infile.Length < 1)
                return;

            FileStream fs = new FileStream(infile, FileMode.Open, FileAccess.Read);
            BinaryReader r = new BinaryReader(fs);
            long len = fs.Length / sizeof(float);
            m_logfile.WriteLine("Opened " + infile);
            m_logfile.WriteLine("File Length: " + fs.Length);
            m_logfile.WriteLine("Length: " + len.ToString());
            uint[] megagrey = new uint[len];
            float rawflt = 0.0F;
            float last = 0.0F;
            int run = 0;
            List<float> fltlist = new List<float>();
            using(fs)
            {
                bool eof = false;
                while (!eof)
                {
                    try
                    {
                        fltlist.Add(Math.Abs(r.ReadSingle()));
                    }
                    catch (EndOfStreamException)
                    {
                        eof = true;
                    }
                }
            }
            m_logfile.WriteLine("List length: " + fltlist.Count);
            for(int i = 0; i < fltlist.Count; i++)
            {
                last = rawflt;
                rawflt = fltlist[i];
                if (rawflt <= 0.0F || rawflt >= 1.0F)
                    m_logfile.WriteLine("-- line " + i.ToString() + " = " + rawflt.ToString());
                if (i != 0 && rawflt == last)
                {
                    if (run == 0)
                        m_logfile.WriteLine("-- value @" + i.ToString() + " == value @" + (i - 1).ToString() + " : " + rawflt.ToString());
                    else
                        m_logfile.WriteLine("-- value @" + i.ToString() + " == value @" + (i - 1).ToString() + " : " + rawflt.ToString() + " - run for " + (run + 1).ToString());
                    run++;
                }
                else
                    run = 0;
                megagrey[i] = (uint)(fltlist[i] * 255);
            }

            Bitmap newBitmap = new Bitmap(1024, 1024, PixelFormat.Format24bppRgb);
            int pix = 0;
            for (int j = 0; j < 1024; j++)
            {
                for (int i = 0; i < 1024; i++)
                {
                    pix = (int)megagrey[i + (j * 1024)];
                    Color newColor = Color.FromArgb(pix, pix, pix);
                    newBitmap.SetPixel(i, j, newColor);
                }
            }
            pbxImagePreview.Image = newBitmap;

            // MemoryStream bitmapDataStream = new MemoryStream(megagrey);
            //  Bitmap bitmap = new Bitmap(bitmapDataStream);
            // bitmap.Save("medota2", ImageFormat.Bmp);
            m_logfile.WriteLine("Saving bitmap to " + outfile);

            Image img = (Image)newBitmap;
            if (Path.GetFileName(outfile) == "")
                outfile = outfile + "\\image.bmp";
            if (!outfile.ToLower().Contains(".bmp"))
                outfile += ".bmp";
            newBitmap.Save(outfile, ImageFormat.Bmp);
            
            m_logfile.WriteLine("Done processing " + infile);
            m_logfile.Flush();
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            processFile(tbxInput.Text, tbxOutputFile.Text);
        }

        private void btnBrowseInput_Click(object sender, EventArgs e)
        {
            if(ofdInfile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Button btn = (Button)sender;
                if (btn.Name == btnBrowseInput.Name)
                {
                    tbxInput.Text = ofdInfile.FileName;
                    tbxOutputFile.Text = tbxInput.Text.Replace(Path.GetExtension(tbxInput.Text), ".bmp");
                }
                if (btn.Name == btnOpenBMP.Name)
                {
                    tbxBMPIn.Text = ofdInfile.FileName;
                    tbxBMPOut.Text = tbxBMPIn.Text.Replace(Path.GetExtension(tbxBMPIn.Text), ".rfa");
                }
            }
        }

        private void btnBrowseOutput_Click(object sender, EventArgs e)
        {
            if(fbdOutfolder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Button btn = (Button)sender;
                if(btn.Name == btnBrowseOutput.Name)
                    tbxOutputFile.Text = fbdOutfolder.SelectedPath;
                if (btn.Name == btnBrowseOutBMP.Name)
                    tbxBMPOut.Text = fbdOutfolder.SelectedPath;
                if(tbxInput.Text != "")
                {
                    String filename = Path.GetFileName(tbxInput.Text);
                    filename = filename.Replace(Path.GetExtension(filename), ".bmp");
                    tbxOutputFile.Text += "\\" + filename;
                }
                else
                    tbxOutputFile.Text += "\\output.bmp";
            }
        }

        private void btnProcessBMP_Click(object sender, EventArgs e)
        {
            processBMPFile(tbxBMPIn.Text, tbxBMPOut.Text);
        }

        private void processBMPFile(String infile, String outfile)
        {
            if (infile.Length < 1 || outfile.Length < 1)
                return;
            pbxImagePreview.Load(infile);
            int imgWidth = pbxImagePreview.Image.Width;
            int imgHeight = pbxImagePreview.Image.Height;
            long length = imgWidth * imgHeight;
            m_logfile.WriteLine("Opened " + infile);
            m_logfile.WriteLine("Image Width: " + imgWidth);
            m_logfile.WriteLine("Image Height: " + imgHeight);
            m_logfile.WriteLine("Length: " + length.ToString());
            
            CRawColor[] buffer = new CRawColor[length];
            Bitmap bmp = (Bitmap)pbxImagePreview.Image;

            for (int i = 0; i < imgWidth; i++)
            {
                for (int j = 0; j < imgHeight; j++)
                {
                    Color clr = bmp.GetPixel(i, j);
                    float red = (float)clr.R / 255;
                    float grn = (float)clr.G / 255;
                    float blu = (float)clr.B / 255;
                    buffer[i + j * imgWidth] = new CRawColor(red, grn, blu);
                }
            }
            m_logfile.WriteLine("Buffer length: " + buffer.Length);
            m_logfile.WriteLine("Estimated output length (bytes): " + (sizeof(float) * buffer.Length).ToString());
            FileStream fs = new FileStream(outfile, FileMode.Create);
            using (BinaryWriter sw = new BinaryWriter(fs))
            {
                foreach(CRawColor val in buffer)
                    sw.Write(val.Gray);
            }
            m_logfile.WriteLine("Finished dumping " + outfile);
            m_logfile.Flush();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_logfile.Close();
        }
    }

    public class CRawColor
    {
        private float m_red;
        private float m_green;
        private float m_blue;
        private float m_gray;

        public float R
        {
            get { return m_red; }
            set 
            {
                if ((float)value > 255.0F)
                    m_red = (float)value / 255.0F;
                else
                    m_red = (float)value;
                m_gray = (m_red + m_green + m_blue) / 3;
            }
        }

        public float G
        {
            get { return m_green; }
            set
            {
                if ((float)value > 255.0F)
                    m_green = (float)value / 255.0F;
                else
                    m_green = (float)value;
                m_gray = (m_red + m_green + m_blue) / 3;
            }
        }

        public float B
        {
            get { return m_blue; }
            set
            {
                if ((float)value > 255.0F)
                    m_blue = (float)value / 255.0F;
                else
                    m_blue = (float)value;
                m_gray = (m_red + m_green + m_blue) / 3;
            }
        }

        public float Gray
        {
            get { return m_gray; }
        }

        public CRawColor(int red, int green, int blue)
        {
            m_red = (float)(red / 255.0);
            m_green = (float)(green / 255.0);
            m_blue = (float)(blue / 255.0);
            m_gray = (m_red + m_green + m_blue) / 3;
        }

        public CRawColor(float red, float green, float blue)
        {
            if (red > 1.0F)
                m_red = 1.0F / red;
            else
                m_red = red;
            if (green > 1.0F)
                m_green = 1.0F / green;
            else
                m_green = green;
            if (m_blue > 1.0F)
                m_blue = 1.0F / blue;
            else
                m_blue = blue;
            m_gray = (m_red + m_green + m_blue) / 3;
        }

        override public String ToString()
        {
            return m_red.ToString() + " " + m_green.ToString() + " " + m_blue.ToString();
        }
    }
}
