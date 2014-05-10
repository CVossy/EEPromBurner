using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;
using System.IO;
using System.IO.Ports;

namespace EEPROM_burner_vossy
{
    public partial class Form1 : Form
    {
        private Stream myStream = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnHexRead_Click(object sender, EventArgs e)
        {
            //clear the textbox to write new stuff
            rtbCodeView.Clear();
            //open a filedialog
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                tbxHexFile.Text = openFileDialog1.FileName;
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        for (int a = 0; a < myStream.Length; a++)
                        {
                            //Read the filestream bytewise an put it in the codebox
                            int b = myStream.ReadByte();
                            if (b < 16) rtbCodeView.AppendText("0");
                            //.ToString("X") conterts an int to a string(in hex)
                            rtbCodeView.AppendText(b.ToString("X") + " ");
                        }
                    }
                    //shows that reading is finished
                    listBox1.Items.Add("File loaded ...");
                    listBox1.SelectedIndex = listBox1.Items.Count - 1;
                }
                catch (Exception ex)
                {
                    //if an exeption occurs it is showed in the list
                    listBox1.Items.Add("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void btn_burn_Click(object sender, EventArgs e)
        {
            //checking if a file is loaded
            if (tbxHexFile.Text!="")
            {
                //setting the streamposition to 0 to start from the beginning of the stream
                myStream.Position = 0;
                
                //sending byte 51 to get the arduino in burning mode
                byte cmd = 51;
                byte[] b = BitConverter.GetBytes(cmd);
                serialPort1.Write(b, 0, 1);

                //8192 Byte should be written
                int laenge = 8192;               
                
                //the outter loop steps with 1024 Bytes
                for (int a = 0; a < laenge/1024; a++)
                {
                    //the inner loop writes 1024 Byte Blocks
                    for (int ii = 0; ii < 1024; ii++)
                    {
                        //send the Stream Bytes except when the stream is finished, then fill with "00" Bytes
                        if (myStream.Length <= a*1024+ii)
                            cmd = Convert.ToByte(0);
                        else
                            cmd = Convert.ToByte(myStream.ReadByte());
                        //write every Byte in the list
                        listBox1.Items.Add("Sending Byte[" + (a*1024+ii) + "]: " + cmd.ToString("X"));
                        byte[] bb = BitConverter.GetBytes(cmd);
                        //send
                        serialPort1.Write(bb, 0, 1);
                    }
                    //wait until the arduino sends the ready byte
                    serialPort1.Read(b,0,1);
                }
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            int a = 0;
            // Send byte 50 to set the arduino into "read mode"
            byte cmd = 50;
            byte[] b = BitConverter.GetBytes(cmd);
            serialPort1.Write(b, 0, 1);

            //8192Bytes should be read
            long laenge = 8192;
            //while not everything is read
            while (a<laenge)
            {
                try
                {
                    //receiving single bytes from the arduino
                    serialPort1.Read(b, 0, 1);
                }
                catch (TimeoutException to)
                {
                    //userinfo if anything goes wrong
                    listBox1.Items.Add("Arduino not responding ... - CANCEL!");
                    break;
                }
                //convert byte to int
                int readByte = BitConverter.ToInt32(b, 0);

                //Byte output
                listBox1.Items.Add("Byte[" + (a) + "]: " + readByte.ToString("X"));
                a++;
            }
        }

        private void cbxCom_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    //if there is an open connection it should be closed
                    serialPort1.Close();        
                }
                catch { };
                serialPort1.PortName = cbxCom.Text;
                serialPort1.ReadTimeout = 100000;
                serialPort1.WriteTimeout = 10000;
                serialPort1.Open();
            }
            catch
            {
                MessageBox.Show("This is no active Comport :(");
            }
        }
    }
}
