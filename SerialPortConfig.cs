using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Forms;
using System.Drawing;

namespace OPCServer_Simulator
{
    public static class SerialPortConfig
    {
        public static SerialPort mySerialPort = new SerialPort();
        public static string myString = "";
        public static bool isOpen { get { return mySerialPort.IsOpen; } }
        public static string portName
        {
            get
            {
                return mySerialPort.PortName;
            }
            set
            {
                mySerialPort.PortName = value;
            }
        }


        public static void createPort (string portName)
        {
            if (portName == "")
                portName = "COM1";
            mySerialPort.PortName = portName;
            mySerialPort.BaudRate = 2400; //Depending on the hardware used this may change, mitutoyo input tool asks for 2400 baud
            mySerialPort.Parity = Parity.None;
            mySerialPort.StopBits = StopBits.One;
            mySerialPort.DataBits = 8;
            mySerialPort.Handshake = Handshake.RequestToSend; //DO NOT SET TO NONE

            mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
        }

        public static void openSerialPort()
        {
            mySerialPort.Open();
        }

        public static void closeSerialPort()
        {
            mySerialPort.Close();
        }

        private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting(); //stores the char that fired the event into 'indata'
            myString += indata;
            if (indata.Contains("\r")) //check to see if char received indicates end of measurement
            {
                if (myString == "911\r") //911 is the code given when the micrometer is off, so we have it do nothing
                    myString = "";
                else
                {
                    myString = myString.Substring(4); //removes the first 4 chars
                    myString = myString.Replace("\r", "");
                    Form1.instance.pendingMeasurement = true;
                }
            }
        }
    }
}
