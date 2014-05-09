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
            rtbCodeView.Clear();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                tbxHexFile.Text = openFileDialog1.FileName;
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        for (int a = 0; a < myStream.Length; a++)
                        {
                            //.ToString("X") konvertiert eine zahl zu String(aber als hex)
                            int b = myStream.ReadByte();
                            if (b < 16) rtbCodeView.AppendText("0");

                            rtbCodeView.AppendText(b.ToString("X") + " ");
                        }
                    }
                    listBox1.Items.Add("File loaded ...");
                    listBox1.SelectedIndex = listBox1.Items.Count - 1;
                }
                catch (Exception ex)
                {
                    listBox1.Items.Add("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void btn_burn_Click(object sender, EventArgs e)
        {
            if (tbxHexFile.Text!="")
            {
                //Streamposition zurücksetzen damit vom ersten byte gelesen wird
                myStream.Position = 0;
                
                //Byte 51 senden damit der Arduino weiß, dass geschrieben werden soll
                byte cmd = 51;
                byte[] b = BitConverter.GetBytes(cmd);
                serialPort1.Write(b, 0, 1);

                //Es sollen 8192 Byte gesendet werden
                int laenge = 8192;               
                
                //die äussere schleife wird in 1024 gesendeten Byte intervallen aufgerufen
                for (int a = 0; a < laenge/1024; a++)
                {
                    //Die innere schleife sendet 1024 Byte hintereinander
                    for (int ii = 0; ii < 1024; ii++)
                    {
                        //sende byte wenn der Stream beendet ist, soll eine 0 gesendet werden
                        if (myStream.Length <= a*1024+ii)
                            cmd = Convert.ToByte(0);
                        else
                            cmd = Convert.ToByte(myStream.ReadByte());
                        //Ausgeben was gesendet wurde
                        listBox1.Items.Add("Sending Byte[" + (a*1024+ii) + "]: " + cmd.ToString("X"));
                        byte[] bb = BitConverter.GetBytes(cmd);
                        //senden
                        serialPort1.Write(bb, 0, 1);
                    }
                    //Es wird gewartet, dass der Arduino fertig geschrieben hat und dies mitteilt
                    serialPort1.Read(b,0,1);
                }
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            int a = 0;
            // ein Byte mit "50" Senden damit der arduino weiß, dass gelesen werden soll
            byte cmd = 50;
            byte[] b = BitConverter.GetBytes(cmd);
            serialPort1.Write(b, 0, 1);

            //Es sollen 8192 Byte gelesen werden
            long laenge = 8192;
            
            while (a<laenge)
            {
                try
                {
                    //empfangen der Bytes vom arduino
                    serialPort1.Read(b, 0, 1);
                }
                catch (TimeoutException to)
                {
                    //userinfo falls der arduino nicht antwortet
                    listBox1.Items.Add("Arduino not responding ... - CANCEL!");
                    break;
                }
                //Byte in int convertieren damit es ausgegeben werden kann
                int readByte = BitConverter.ToInt32(b, 0);

                //Byte ausgeben
                listBox1.Items.Add("Byte[" + (a) + "]: " + readByte.ToString("X"));
                a++;
            }
        }

        private void cbxCom_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Close();        //todesangst unddo
            }
            catch { };
            serialPort1.PortName = cbxCom.Text;
            serialPort1.ReadTimeout = 1000000;
            serialPort1.WriteTimeout = 100000;
            serialPort1.Open();
        }
    }
}
