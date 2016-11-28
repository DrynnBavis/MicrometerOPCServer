using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace OPCServer_Simulator
{
    public class SerialPortConfig
    {
        public static SerialPort mySerialPort = new SerialPort();
        public static string myString = "";
        public bool isOpen { get { return mySerialPort.IsOpen; } }

        public void createPort (string portName)
        {
            mySerialPort.PortName = portName;
            mySerialPort.Close();
            mySerialPort.BaudRate = 2400; //Depending on the hardware used this may change, mitutoyo input tool asks for 2400 baud
            mySerialPort.Parity = Parity.None;
            mySerialPort.StopBits = StopBits.One;
            mySerialPort.DataBits = 8;
            mySerialPort.Handshake = Handshake.RequestToSend; //DO NOT SET TO NONE

            mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
        }

        public void openSerialPort()
        {
            mySerialPort.Open();
        }

        public void closeSerialPort()
        {
            mySerialPort.Close();
        }


        private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting(); //stores the char that fired the event into 'indata'
            if (indata == "\r") //check to see if char received indicates end of measurement, yes tells main form to add measurement, no tells to add char to string
            {
                Form1.pendingMeasurement = true;
            }
            else
                myString += indata;
        }
    }
}
