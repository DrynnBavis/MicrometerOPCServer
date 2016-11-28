using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;
using NDI.SLIKDA.Interop;
using System.IO.Ports;

namespace OPCServer_Simulator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            fillMeasView();
        }

        private ISLIKTags myOpcTags;
        private Random randomizer = new Random();
        private int itemIndex = 1;
        private bool refreshNeeded = false;
        private static bool _pendingMeasurement = false;
        public static bool pendingMeasurement
        {
            get
            {
                return _pendingMeasurement;
            }
            set
            {
                _pendingMeasurement = value;
                //somehow addMeasurement() needs to be called here
            }
        }
        public static event Action<bool> BoolChanged;

        public SerialPortConfig mySerialPort = new SerialPortConfig();

        private void Form1_Load(object sender, EventArgs e)
        {
            //Load the items from non-volatile memory into voltatile for use
            comPortCombo.Text = Properties.Settings.Default.portName;
            numMeasBox.Text = Properties.Settings.Default.numItems.ToString();
            mySerialPort.createPort(Properties.Settings.Default.portName);

            //Populating comboBox with available COM ports
            updatePorts();

            // create a new instance of an empty tag database
            myOpcTags = slikServer1.SLIKTags;

            // define a simple read/write access-right, saves us OR'ing this
            // enumeration multiple times
            AccessPermissionsEnum readWriteAccess = AccessPermissionsEnum.sdaReadAccess | AccessPermissionsEnum.sdaWriteAccess;


            // A Tag name is "just" a name. But when you prefix that name with a '.'
            // then the OPC Server toolkit will automatically place that tag within
            // a group. This simplifies the OPC Client experience when they browse
            // your OPC Servers address-space.
            // In this case, we will define a group: "USER"
            // .... you will see it prefixed to the tagnames.
            myOpcTags.Add("MicrometerTags.LogSet", (int)readWriteAccess, 0, 0, DateTime.Now, null);
            myOpcTags["MicrometerTags.LogSet"].DataType = (short)VariantType.Boolean;
            myOpcTags["MicrometerTags.LogSet"].SetVQT(Convert.ToInt32(true), 192, DateTime.Now);
            myOpcTags.Add("MicrometerTags.index", (int)readWriteAccess, 0, 0, DateTime.Now, null);
            myOpcTags["MicrometerTags.index"].DataType = (short)VariantType.Integer;
            myOpcTags["MicrometerTags.index"].SetVQT((0.0000), 192, DateTime.Now);

            // In this example, we will also register and start the server
            // Under normal circumstances you would NOT do this, because this is a
            // step that should be done once and once-only, at the end of an
            // installation routine. Most OPC Server implementations allow the use
            // of a command-line argument to call this function.
            //slikServer1.RegisterServer();

            // Now START the OPC Server interface. This is called whenever your
            // application starts.
            slikServer1.StartServer();

            if (!mySerialPort.isOpen)
            {
                try
                {
                    mySerialPort.openSerialPort(); //specifies to open the port with the name last saved in settings
                    portActiveStatusLbl.Text = Properties.Settings.Default.portName + " OPEN";
                    portActiveStatusLbl.ForeColor = Color.Green;
                }
                catch
                {
                    MessageBox.Show("Error: Serial Port in memory is not available. Please update port in 'Micrometer Settings'.", "Invalid Serial Port Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void fillMeasView()
        {
            for (int i = 1; i <= OPCServer_Simulator.Properties.Settings.Default.numItems; i++)
            {
                ListViewItem lvi = new ListViewItem(i.ToString());
                lvi.SubItems.Add("null");
                lvi.SubItems.Add("null");
                measView.Items.Add(lvi);
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Close the OPC Server and Exit?", "Exit?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes))
            {
                try
                {
                    // Notify the OPC Clients that you want to shut down. This gives
                    // each OPC Client enough time to gracefully disconnect.
                    slikServer1.RequestDisconnect("User Requested Shutdown");

                    // This function, just like the ".RegisterServer" as seen in the 
                    // Form_Load() event is a function that should not be called under
                    // normal circumstances. This should be done by the installer
                    // when the application is being removed from the computer.
                    //SlikServer1.UnregisterServer()
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                finally
                {
                    Application.Exit();
                }
            }

        }

        private void SlikServer1_OnClientConnect(object sender, SLIKServer.OnClientConnectEventArgs eventArgs)
        {
            ConnectedClients.Text = eventArgs.NumClients.ToString();
        }

        private void SlikServer1_OnClientDisconnect(object sender, SLIKServer.OnClientDisconnectEventArgs eventArgs)
        {
            ConnectedClients.Text = eventArgs.NumClients.ToString();
        }

        private void SlikServer1_OnWrite(object sender, SLIKServer.OnWriteEventArgs eventArgs)
        {
            for (int i = 0; i <= (eventArgs.Count - 1); i++)
            {
                // Get the "next" item in the list
                ISLIKTag currentItem = eventArgs.Tags[i];
                object currentValue = eventArgs.Values[i];

                // We will look at the name of the OPC Tag to then figure out WHERE
                // to SEND the data to....

                // Specify that the Item at *this* position is NOT in error!
                eventArgs.Errors[i] = (int)OPCDAErrorsEnum.sdaSOK;
            }

            // Now specify that we completed this event successfully
            eventArgs.Result = (int)OPCDAErrorsEnum.sdaSOK;

        }

        private void SlikServer1_OnRead(object sender, SLIKServer.OnReadEventArgs eventArgs)
        {
            //
            // Iterate thru each/every item that the OPC Client has requested us to READ

            {

                for (int i = 0; i <= (eventArgs.Count - 1); i++)
                {
                    // Get the "next" item in the list
                    ISLIKTag currentItem = eventArgs.Tags[i];

                    // Now to return the value of this tag, along with
                    // GOOD quality, and a current timestamp
                    //   (Quality codes are: 192=GOOD; 0=BAD)
                    //
                    // We will look at the name of the OPC Tag to then figure out WHERE
                    // to get the data from....

                    switch ((currentItem.Name))
                    {
                        // Checkbox on the LEFT side of the form
                        case "MicrometerTags.LogSet":
                            eventArgs.Tags[i].SetVQT(true, 192, DateTime.Now);

                            break;
                        default:
                            eventArgs.Tags[i].SetVQT(Convert.ToDouble(100), 192, DateTime.Now);

                            break;
                    }


                    // Specify that the Item at *this* position is NOT in error!
                    eventArgs.Errors[i] = (int)OPCDAErrorsEnum.sdaSOK;
                }

                // Now specify that we completed this event successfully
                eventArgs.Result = (int)OPCDAErrorsEnum.sdaSOK;

            }


        }

        private void addMeasurement()
        {
            if (pendingMeasurement)
            {
                measView.Items[itemIndex - 1].SubItems[1].Text = SerialPortConfig.myString;
                if (myOpcTags != null)
                {
                    myOpcTags["MicrometerTags.meas" + itemIndex].SetVQT(Convert.ToDouble(SerialPortConfig.myString), 192, DateTime.Now);
                }
                if (itemIndex == OPCServer_Simulator.Properties.Settings.Default.numItems)
                {
                    itemIndex = 1;
                    if (myOpcTags != null)
                    {
                        myOpcTags["MicrometerTags.LogSet"].SetVQT(true, 192, DateTime.Now); //this bit is toggled here to tell the client to save the set
                        myOpcTags["MicrometerTags.index"].SetVQT(0, 192, DateTime.Now); //update HMI to view newly added set
                        refreshNeeded = true;
                    }
                }
                else
                {
                    itemIndex++;
                    if (refreshNeeded)
                    {
                        bool skippedOnce = false;
                        foreach (ListViewItem item in measView.Items)
                        {
                            if (skippedOnce)
                                item.SubItems[1].Text = "0.0000";
                            else
                                skippedOnce = true;
                        }
                        for (int i = 2; i <= OPCServer_Simulator.Properties.Settings.Default.numItems; i++) //clear all other values (1 is skipped because we just gave that a value for this new set)
                            myOpcTags["MicrometerTags.meas" + i].SetVQT((0.0000), 192, DateTime.Now);
                    }
                    if (myOpcTags != null)
                    {
                        myOpcTags["MicrometerTags.LogSet"].SetVQT(false, 192, DateTime.Now);
                        refreshNeeded = false;
                    }
                }

            }
        }

        private void updatePorts() //displays currently available ports
        {
            comPortCombo.Items.Clear();
            string[] comPortsNames = null;
            int index = -1;
            string comPortName = null;

            comPortsNames = SerialPort.GetPortNames();
            Array.Sort(comPortsNames);
            do
            {
                index += 1;
                comPortCombo.Items.Add(comPortsNames[index]);
            }
            while (!((comPortsNames[index] == comPortName) ||
                                (index == comPortsNames.GetUpperBound(0))));
        }

        private void updateOPCItems()
        {
            AccessPermissionsEnum readWriteAccess = AccessPermissionsEnum.sdaReadAccess | AccessPermissionsEnum.sdaWriteAccess;

            myOpcTags.Clear();
            myOpcTags.Add("MicrometerTags.LogSet", (int)readWriteAccess, 0, 0, DateTime.Now, null);
            myOpcTags["MicrometerTags.LogSet"].DataType = (short)VariantType.Boolean;
            myOpcTags["MicrometerTags.LogSet"].SetVQT(Convert.ToInt32(true), 192, DateTime.Now);
            myOpcTags.Add("MicrometerTags.index", (int)readWriteAccess, 0, 0, DateTime.Now, null);
            myOpcTags["MicrometerTags.index"].DataType = (short)VariantType.Integer;
            myOpcTags["MicrometerTags.index"].SetVQT((0.0000), 192, DateTime.Now);
            for (int i = 1; i <= Convert.ToInt32(numMeasBox.Text); i++) //adding the specified number of items to OPC server
            {
                myOpcTags.Add("MicrometerTags.meas" + i, (int)readWriteAccess, 0, 0, DateTime.Now, null);
                myOpcTags["MicrometerTags.meas" + i].DataType = (short)VariantType.Double;
                myOpcTags["MicrometerTags.meas" + i].SetVQT((0.0000), 192, DateTime.Now);
            }
        }

        private void updateDisplayItems()
        {
            measView.Items.Clear();
            fillMeasView();
        }

        private void chngNumItemsBtn_Click(object sender, EventArgs e)
        {
            OPCServer_Simulator.Properties.Settings.Default.numItems = Convert.ToUInt32(numMeasBox.Text); //update value in settings
            Properties.Settings.Default.Save();
            updateOPCItems();
            updateDisplayItems();
        }

        private void numberOnly_KeyPress(object sender, KeyPressEventArgs e) //event only allows numbers
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void updatePortsBtn_Click(object sender, EventArgs e) //This updates the portName in SETTINGS (AKA saves the selected port as the port it will try to use on startup)
        {
            OPCServer_Simulator.Properties.Settings.Default.portName = comPortCombo.Text;
            Properties.Settings.Default.Save();
        }

        private void comPortCombo_MouseClick(object sender, EventArgs e) //event handlet fires when combobox dropdown is clicked, updates items to avaialable COM ports
        {
            updatePorts();
        }

        private void openPortBtn_Click(object sender, EventArgs e)
        {
            try
            {
                mySerialPort.openSerialPort();
            }
            catch
            {
                MessageBox.Show("Error: Could not open Serial port " + Properties.Settings.Default.portName, "Port Opening Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (mySerialPort.isOpen)
            {
                portActiveStatusLbl.Text = Properties.Settings.Default.portName + " OPEN";
                portActiveStatusLbl.ForeColor = Color.Green;
            }
        }

        private void closePortBtn_Click(object sender, EventArgs e)
        {
            try
            {
                mySerialPort.closeSerialPort();
            }
            catch
            {
                MessageBox.Show("Error: Could not close Serial port " + Properties.Settings.Default.portName, "Port Closure Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (!mySerialPort.isOpen)
            {
                portActiveStatusLbl.Text = Properties.Settings.Default.portName + " CLOSED";
                portActiveStatusLbl.ForeColor = Color.Red;
            }
        }

        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            // Determine if text has changed in the textbox by comparing to original text.
            if (mySerialPort.isOpen)
            {
                mySerialPort.closeSerialPort();
            }
        }
    }
}
