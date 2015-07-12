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
        public Form1()
        {
            InitializeComponent();
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
            if (infile.Length < 1 || outfile.Length < 1)
                return;
            System.IO.StreamWriter file = new System.IO.StreamWriter("Log.txt");

            FileStream fs = new FileStream(infile, FileMode.Open, FileAccess.Read);
            BinaryReader r = new BinaryReader(fs);
            long len = fs.Length / 4;
            file.WriteLine("Opened " + infile);
            file.WriteLine("File Length: " + (len * 4).ToString());
            file.WriteLine("Length: " + len.ToString());
            uint[] megagrey = new uint[len];
            float rawflt = 0.0F;
            float last = 0.0F;
            int run = 0;
            for (int i = 0; i < len; i++)
            {
                last = rawflt;
                rawflt = Math.Abs(r.ReadSingle());
                if (rawflt == 0.0F || rawflt == 1.0F)
                    file.WriteLine("-- line " + i.ToString() + " = " + rawflt.ToString());
                if (i != 0 && rawflt == last)
                {
                    if (run == 0)
                        file.WriteLine("-- value " + i.ToString() + " is identical to " + (i - 1).ToString() + " : " + rawflt.ToString());
                    else
                        file.WriteLine("-- value " + i.ToString() + " is identical to " + (i - 1).ToString() + " : " + rawflt.ToString() + " - run of " + run.ToString() + " values");
                    run++;
                }
                else
                    run = 0;
                megagrey[i] = (uint)(rawflt * 255);
            }

            Bitmap newBitmap = new Bitmap(1024, 1024, PixelFormat.Format24bppRgb);
            int pix = 0;
            for (int j = 0; j < 1024; j++)
            {
                for (int i = 0; i < 1024; i++)
                {
                    pix = (int)megagrey[i + j * 1024];
                    Color newColor = Color.FromArgb(pix, pix, pix);
                    newBitmap.SetPixel(i, j, newColor);
                }
            }
            pbxImagePreview.Image = newBitmap;

            // MemoryStream bitmapDataStream = new MemoryStream(megagrey);
            //  Bitmap bitmap = new Bitmap(bitmapDataStream);
            // bitmap.Save("medota2", ImageFormat.Bmp);
            file.WriteLine("Saving bitmap to " + outfile);

            Image img = (Image)newBitmap;
            if (Path.GetFileName(outfile) == "")
                outfile = outfile + "\\image.bmp";
            if (!outfile.ToLower().Contains(".bmp"))
                outfile += ".bmp";
            newBitmap.Save(outfile, ImageFormat.Bmp);
            
            file.WriteLine("Done processing " + infile);
            file.Close();
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
                if(btn.Name == btnBrowseInput.Name)
                    tbxInput.Text = ofdInfile.FileName;
                if (btn.Name == btnOpenBMP.Name)
                    tbxBMPIn.Text = ofdInfile.FileName;
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
            using (StreamWriter sw = new StreamWriter(outfile))
            {
                for (int i = 0; i < length; i++)
                {
                    if (i < (length - 1))
                        sw.Write(buffer[i].Gray + " ");
                    else
                        sw.Write(buffer[i].Gray.ToString());
                }
            }
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
