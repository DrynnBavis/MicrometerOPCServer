using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
            instance = this;

            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                switch (args[1])
                {
                    case "register":
                        try
                        {
                            slikServer1.RegisterServer();
                            MessageBox.Show("Server registered successfully!");
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show("Error: Could not register server." + "\nAdditional Information: " + ex.Message, "Registering Server Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        Environment.Exit(0);
                        break;
                    case "unregister":
                        try
                        {
                            slikServer1.UnregisterServer();
                            MessageBox.Show("Server unregistered successfully!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: Could not unregister server." + "\nAdditional Information: " + ex.Message, "Unregistering Server Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            }
                
        }

        private ISLIKTags myOpcTags;
        private int itemIndex = 0;
        private int histIndex = 0;
        private bool _pendingMeasurement = false;
        public bool pendingMeasurement
        {
            get
            {
                return _pendingMeasurement;
            }
            set
            {
                _pendingMeasurement = value;
                if(_pendingMeasurement)
                {
                    addMeasurement();
                    _pendingMeasurement = false;
                    SerialPortConfig.myString = "";
                }
            }
        }
        public static Form1 instance;

        private void Form1_Load(object sender, EventArgs e)
        {
            //Load the items from non-volatile memory into voltatile for use
            comPortCombo.Text = Properties.Settings.Default.portName;
            numMeasBox.Text = Properties.Settings.Default.numItems.ToString();

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
            myOpcTags.Add("MicrometerTags.dateTime", (int)readWriteAccess, 0, 0, DateTime.Now, null);
            myOpcTags["MicrometerTags.dateTime"].DataType = (short)VariantType.String;
            myOpcTags["MicrometerTags.dateTime"].SetVQT("", 192, DateTime.Now);
            myOpcTags.Add("MicrometerTags.index", (int)readWriteAccess, 0, 0, DateTime.Now, null);
            myOpcTags["MicrometerTags.index"].DataType = (short)VariantType.Integer;
            myOpcTags["MicrometerTags.index"].SetVQT((0.0000), 192, DateTime.Now);
            myOpcTags["MicrometerTags.index"].AccessPermissions = 3;

            slikServer1.StartServer();

            //Populate tags with number of items the user requested
            updateOPCItems();

            //viewtags on client as well
            updateDisplayItems();
            
            SerialPortConfig.createPort(Properties.Settings.Default.portName);

            if (!SerialPortConfig.isOpen)
            {
                try
                {
                    SerialPortConfig.openSerialPort();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not open Serial port " + Properties.Settings.Default.portName + "\nAdditional Information: " + ex.Message, "Port Opening Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (SerialPortConfig.isOpen)
                {
                    portActiveStatusLbl.Text = Properties.Settings.Default.portName + " OPEN";
                    portActiveStatusLbl.ForeColor = Color.Green;
                }
                else
                {
                    portActiveStatusLbl.Text = Properties.Settings.Default.portName + " CLOSED";
                    portActiveStatusLbl.ForeColor = Color.Red;
                }
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
                if (itemIndex == OPCServer_Simulator.Properties.Settings.Default.numItems)
                {
                    updateDisplayItems();
                    itemIndex = 1;
                }
                else
                {
                    itemIndex++;
                }
                var value = Properties.Settings.Default["meas" + itemIndex] as System.Collections.Specialized.StringCollection;
                value[0] = SerialPortConfig.myString;
                double dValue = Convert.ToDouble(value[0]);
                updateDisplayItems(); //update before shifting set of data so the recorded set remains on screen instead of n number of 0s
                updateOPCItems();

                if (itemIndex == Properties.Settings.Default.numItems)
                {
                    Properties.Settings.Default.dateTime[0] = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                    shiftDataIndex();
                    Properties.Settings.Default.Save();
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
            if (myOpcTags != null)
                myOpcTags.Clear();
            myOpcTags.Add("MicrometerTags.index", (int)readWriteAccess, 0, 0, DateTime.Now, null);
            myOpcTags["MicrometerTags.index"].DataType = (short)VariantType.Integer;
            myOpcTags["MicrometerTags.index"].SetVQT((0), 192, DateTime.Now);
            myOpcTags.Add("MicrometerTags.dateTime", (int)AccessPermissionsEnum.sdaReadAccess, 0, 0, DateTime.Now, null);
            myOpcTags["MicrometerTags.dateTime"].DataType = (short)VariantType.String;
            myOpcTags["MicrometerTags.dateTime"].SetVQT(Properties.Settings.Default.dateTime[histIndex], 192, DateTime.Now);
            for (int i = 1; i <= Convert.ToInt32(numMeasBox.Text); i++) //adding the specified number of items to OPC server
            {
                myOpcTags.Add("MicrometerTags.meas" + i.ToString(), (int)readWriteAccess, 0, 0, DateTime.Now, null);
                myOpcTags["MicrometerTags.meas" + i.ToString()].DataType = (short)VarEnum.VT_R8;
                var value = Properties.Settings.Default["meas" + i] as System.Collections.Specialized.StringCollection;
                double dValue = Convert.ToDouble(value[histIndex]);
                myOpcTags["MicrometerTags.meas" + i.ToString()].SetVQT((dValue), 192, DateTime.Now);
            }
        }

        private void updateDisplayItems()
        {
            if (measView.InvokeRequired)
            {
                measView.Invoke(new MethodInvoker(delegate { measView.Items.Clear(); }));
            }
            for (int i = 1; i <= OPCServer_Simulator.Properties.Settings.Default.numItems; i++)
            {
                ListViewItem lvi = new ListViewItem(i.ToString());
                try
                {
                    var value = Properties.Settings.Default["meas" + i] as System.Collections.Specialized.StringCollection;
                    if (value[histIndex] == "0")
                        value[histIndex] = "0.000";
                    lvi.SubItems.Add(value[histIndex]);
                }
                catch
                {
                    lvi.SubItems.Add("0.000");
                }
                if (measView.InvokeRequired)
                {
                    measView.Invoke(new MethodInvoker(delegate { measView.Items.Add(lvi); }));
                }
                else
                {
                    measView.Items.Add(lvi);
                }
            }
        }

        private void shiftDataIndex()
        {
            for (int i = 20; i >= 1; i--) // 20 measurements to be affected
            {
                for (int k = 19; k > 0; k--) // 20 history values but only swapping 19 because the 0th will be the new value from micrometer
                {
                    var value = Properties.Settings.Default["meas" + i] as System.Collections.Specialized.StringCollection;
                    value[k] = value[k - 1];
                }
            }
            for (int i = 20; i >= 1; i--) //initalize first set to 0 for new data
            {
                var value = Properties.Settings.Default["meas" + i] as System.Collections.Specialized.StringCollection;
                value[0] = "0";
            }
            for (int i = 19; i > 0; i--)
            {
                Properties.Settings.Default.dateTime[i] = Properties.Settings.Default.dateTime[i - 1];
            }
        }

        private void chngNumItemsBtn_Click(object sender, EventArgs e)
        {
            OPCServer_Simulator.Properties.Settings.Default.numItems = Convert.ToUInt32(numMeasBox.Text); //update value in settings
            Properties.Settings.Default.Save();
            updateOPCItems();
            updateDisplayItems();
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
                SerialPortConfig.openSerialPort();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not open Serial port " + Properties.Settings.Default.portName + "\nAdditional Information: " + ex.Message, "Port Opening Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (SerialPortConfig.isOpen)
            {
                portActiveStatusLbl.Text = Properties.Settings.Default.portName + " OPEN";
                portActiveStatusLbl.ForeColor = Color.Green;
            }
        }

        private void closePortBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SerialPortConfig.closeSerialPort();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: Could not close Serial port " + Properties.Settings.Default.portName + "\nAdditional Information: " + ex.Message, "Port Closure Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (!SerialPortConfig.isOpen)
            {
                portActiveStatusLbl.Text = Properties.Settings.Default.portName + " CLOSED";
                portActiveStatusLbl.ForeColor = Color.Red;
            }
        }

        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            if (SerialPortConfig.isOpen)
            {
                SerialPortConfig.closeSerialPort();
            }
        }

        private void numberOnly_KeyPress(object sender, KeyPressEventArgs e) //event only allows numbers
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
