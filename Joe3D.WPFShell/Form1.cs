using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace Joe3D.WPFShell
{
    public partial class Form1 : Form
    {
        private ElementHost ctrlHost;
        private Joe3D.ViewControl.JoeViewPanel joepanel = new ViewControl.JoeViewPanel();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ctrlHost = new ElementHost();
            ctrlHost.Dock = DockStyle.Fill;
            panel1.Controls.Add(ctrlHost);
            
                     
            ctrlHost.Child = joepanel;
            joepanel.JoeModel = Joe3D.Utilities.Generator.GetCube();
            joepanel.JoeLights = Joe3D.Utilities.Generator.GetLight();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            joepanel.X = Convert.ToDouble(txt_X.Text);
            joepanel.Y = Convert.ToDouble(txt_Y.Text);
            joepanel.Z = Convert.ToDouble(txt_Z.Text);
            joepanel.Distance = Convert.ToDouble(txt_Dist.Text);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            var mi = new Joe3D.Utilities.ModelImporter();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = Path.Combine(Environment.CurrentDirectory, "3DLibrary");
            openFileDialog1.Filter = "obj files (*.obj)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            joepanel.JoeModel = mi.Load(openFileDialog1.FileName, null, true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
            
        }
    }
}
