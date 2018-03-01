using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace colorEyes
{
    public partial class Form1 : Form
    {
        PictureBox[] allPBs;
        Dictionary<int, List<Color>> allLEDs = new Dictionary<int, List<Color>>(); // #фрейма - набор из 24 цветов 
        MemoryStream ms = new MemoryStream();
        int currentFrameNumber = 0;
        public Form1()
        {
            InitializeComponent();

            allPBs = new[] {
                Box01,                Box02,                Box03,                Box04,
                Box05,                Box06,                Box07,                Box08,
                Box09,                Box10,                Box11,                Box12,
                Box13,                Box14,                Box15,                Box16,
                p17,                p18,p19,p20,                p21,p22,p23,p24,
                p25,
                 p26,
                  p27,
                   p28,
                    p29,
                p30,
                p31,
                p32,
                p33,
                p34,
                p35,
                p36,
                p37,
                p38,
                p39,
                p40,
                p41,
                p42,
                p43,
                p44,
                p45,
                p46,
                p47,
                p48
            };
            
            resetColors(0);
        }

        void resetColors(int currentFrame)
        {
            allLEDs[currentFrame] = new List<Color>();

            foreach (PictureBox p in allPBs)
            {
                allLEDs[currentFrame].Add(Color.Black);
            }

            foreach (PictureBox p in allPBs)
            {
                p.BackColor = Color.Black;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            trackBar1.Maximum++;
            trackBar1.Value = trackBar1.Maximum;
        }


        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;


            List<byte> frame = new List<byte>();

            StringBuilder текстоваяИнвформация = new StringBuilder();
            foreach (int frameNumber in allLEDs.Keys)
            {
                foreach (Color c in allLEDs[frameNumber])
                {
                    frame.Add(c.G);
                    frame.Add(c.R);
                    frame.Add(c.B);

                    текстоваяИнвформация.Append(c.G.ToString() + ",");
                    текстоваяИнвформация.Append(c.R.ToString() + ",");
                    текстоваяИнвформация.Append(c.B.ToString() + ",");
                }
            }
            File.WriteAllBytes(saveFileDialog1.FileName, frame.ToArray());
            File.WriteAllText(saveFileDialog1.FileName + ".txt", текстоваяИнвформация.ToString());
        }

        private void pb01_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                ((PictureBox)sender).BackColor = colorDialog1.Color;
            };

            saveState(trackBar1.Value);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (trackBar1.Value != trackBar1.Maximum)
                trackBar1.Value++;
            else
            {
                trackBar1.Value = 0;
                //timer1.Stop();
            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {

          //  currentFrameNumber = trackBar1.Value;
            labelFrame.Text = trackBar1.Value.ToString();
            labelFramesCount.Text = trackBar1.Maximum.ToString();
            if (!allLEDs.ContainsKey(trackBar1.Value))
            {
                allLEDs.Add(trackBar1.Value, new List<Color>());
                resetColors(trackBar1.Value);
            }
            else
            {
                loadState(trackBar1.Value);
            }
        }

        void saveState(int currentFrame)
        {
            allLEDs[currentFrame] = new List<Color>();
            foreach (PictureBox p in allPBs)
            {
                allLEDs[currentFrame].Add(p.BackColor);
            }
        }

        void loadState(int currentFrame)
        {
            int i = 0;
            foreach (Color c in allLEDs[currentFrame])
            {
                allPBs[i].BackColor = c;
                i++;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            timer1.Interval = int.Parse(textBoxInterval.Text);
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }
    }
}
