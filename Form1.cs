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

        private void Form1_Load(object sender, EventArgs e)
        {
            // ***timer for log-data button created**
            logDataBtn.Tag = new Stopwatch();
            logDataBtn.MouseDown += new MouseEventHandler(button_MouseDown);
            logDataBtn.MouseUp += new MouseEventHandler(button_MouseUp);
            // create a new instance of an empty tag database
            myOpcTags = SlikServer1.SLIKTags;

            // define a simple read/write access-right, saves us OR'ing this
            // enumeration multiple times
            AccessPermissionsEnum readWriteAccess = AccessPermissionsEnum.sdaReadAccess | AccessPermissionsEnum.sdaWriteAccess;


            // A Tag name is "just" a name. But when you prefix that name with a '.'
            // then the OPC Server toolkit will automatically place that tag within
            // a group. This simplifies the OPC Client experience when they browse
            // your OPC Servers address-space.
            // In this case, we will define a group: "USER"
            // .... you will see it prefixed to the tagnames.

            // add the user-tags. These are the tags that represent the 3 form
            // controls on the LEFT side of the form
            myOpcTags.Add("User.LogSet", (int)readWriteAccess, 0, 0, DateTime.Now, null);
            myOpcTags["User.LogSet"].DataType = (short)VariantType.Boolean;
            myOpcTags["User.LogSet"].SetVQT(Convert.ToInt32(true), 192, DateTime.Now);
            myOpcTags.Add("User.meas1", (int)readWriteAccess, 0, 0, DateTime.Now, null);
            myOpcTags["User.meas1"].DataType = (short)VariantType.Double;
            myOpcTags["User.meas1"].SetVQT((0.0000), 192, DateTime.Now);
            myOpcTags.Add("User.meas2", (int)readWriteAccess, 0, 0, DateTime.Now, null);
            myOpcTags["User.meas2"].DataType = (short)VariantType.Double;
            myOpcTags["User.meas2"].SetVQT((0.0000), 192, DateTime.Now);
            myOpcTags.Add("User.meas3", (int)readWriteAccess, 0, 0, DateTime.Now, null);
            myOpcTags["User.meas3"].DataType = (short)VariantType.Double;
            myOpcTags["User.meas3"].SetVQT((0.0000), 192, DateTime.Now);
            myOpcTags.Add("User.meas4", (int)readWriteAccess, 0, 0, DateTime.Now, null);
            myOpcTags["User.meas4"].DataType = (short)VariantType.Double;
            myOpcTags["User.meas4"].SetVQT((0.0000), 192, DateTime.Now);
            myOpcTags.Add("User.meas5", (int)readWriteAccess, 0, 0, DateTime.Now, null);
            myOpcTags["User.meas5"].DataType = (short)VariantType.Double;
            myOpcTags["User.meas5"].SetVQT((0.0000), 192, DateTime.Now);
            myOpcTags.Add("User.meas6", (int)readWriteAccess, 0, 0, DateTime.Now, null);
            myOpcTags["User.meas6"].DataType = (short)VariantType.Double;
            myOpcTags["User.meas6"].SetVQT((0.0000), 192, DateTime.Now);
            myOpcTags.Add("User.meas7", (int)readWriteAccess, 0, 0, DateTime.Now, null);
            myOpcTags["User.meas7"].DataType = (short)VariantType.Double;
            myOpcTags["User.meas7"].SetVQT((0.0000), 192, DateTime.Now);
            myOpcTags.Add("User.meas8", (int)readWriteAccess, 0, 0, DateTime.Now, null);
            myOpcTags["User.meas8"].DataType = (short)VariantType.Double;
            myOpcTags["User.meas8"].SetVQT((0.0000), 192, DateTime.Now);
            myOpcTags.Add("User.meas9", (int)readWriteAccess, 0, 0, DateTime.Now, null);
            myOpcTags["User.meas9"].DataType = (short)VariantType.Double;
            myOpcTags["User.meas9"].SetVQT((0.0000), 192, DateTime.Now);
            myOpcTags.Add("User.meas10", (int)readWriteAccess, 0, 0, DateTime.Now, null);
            myOpcTags["User.meas10"].DataType = (short)VariantType.Double;
            myOpcTags["User.meas10"].SetVQT((0.0000), 192, DateTime.Now);

            // In this example, we will also register and start the server
            // Under normal circumstances you would NOT do this, because this is a
            // step that should be done once and once-only, at the end of an
            // installation routine. Most OPC Server implementations allow the use
            // of a command-line argument to call this function.

            SlikServer1.RegisterServer();

            // Now START the OPC Server interface. This is called whenever your
            // application starts.
            SlikServer1.StartServer();
        }

        private void fillMeasView()
        {
            for (int i = 1; i <= 10; i++)
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
                    SlikServer1.RequestDisconnect("User Requested Shutdown");

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
                        case "User.LogSet":
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

        private void swapUnits() //not sure if I want this feature
        {
            foreach (ListViewItem item in measView.Items)
            {
                if (item.SubItems[1].Text.Contains("inch"))
                {
                    string[] splitItem = item.SubItems[1].Text.Split(' ');
                    double value = Convert.ToDouble(splitItem[0]);
                    value = value * 25.4;
                    item.SubItems[1].Text = value.ToString("0.0000") + " mm";
                }
                else if (item.SubItems[1].Text.Contains("mm"))
                {
                    string[] splitItem = item.SubItems[1].Text.Split(' ');
                    double value = Convert.ToDouble(splitItem[0]);
                    value = value / 25.4;
                    item.SubItems[1].Text = value.ToString("0.0000") + " inch";

                }
                else
                    continue;
            }
        }

        private void inchmm_Click(object sender, EventArgs e)
        {
            //swapUnits();
        }

        private void simulateDataEntry()
        {
            double value = randomizer.Next(8000, 12000);
            value = value / 10000;
            measView.Items[itemIndex - 1].SubItems[1].Text = value.ToString("0.0000") + " inch";
            if (myOpcTags != null)
            {
                myOpcTags["User.meas" + itemIndex].SetVQT(Convert.ToDouble(value), 192, DateTime.Now);
            }
        }

        void button_MouseUp(object sender, MouseEventArgs e)
        {
            Stopwatch watch = ((sender as Button).Tag as Stopwatch);
            watch.Stop();
            simulateDataEntry();
            if (watch.Elapsed.TotalMilliseconds > 500)
            {
                itemIndex = 1;
                MessageBox.Show("Button pressed for longer than 500, data log being saved by client. Index reset to 1.");
                if (myOpcTags != null)
                    myOpcTags["User.LogSet"].SetVQT(true, 192, DateTime.Now); //this bit is toggled here to tell the client to save the set
                foreach (ListViewItem item in measView.Items)
                {
                    item.SubItems[2].Text = item.SubItems[1].Text;
                    item.SubItems[2].BackColor = Color.Gray;
                    item.SubItems[1].Text = "null";
                }
                myOpcTags["User.meas1"].SetVQT((0.0000), 192, DateTime.Now);
                myOpcTags["User.meas2"].SetVQT((0.0000), 192, DateTime.Now);
                myOpcTags["User.meas3"].SetVQT((0.0000), 192, DateTime.Now);
                myOpcTags["User.meas4"].SetVQT((0.0000), 192, DateTime.Now);
                myOpcTags["User.meas5"].SetVQT((0.0000), 192, DateTime.Now);
                myOpcTags["User.meas6"].SetVQT((0.0000), 192, DateTime.Now);
                myOpcTags["User.meas7"].SetVQT((0.0000), 192, DateTime.Now);
                myOpcTags["User.meas8"].SetVQT((0.0000), 192, DateTime.Now);
                myOpcTags["User.meas9"].SetVQT((0.0000), 192, DateTime.Now);
                myOpcTags["User.meas10"].SetVQT((0.0000), 192, DateTime.Now);
            }
            else if (itemIndex == 10)
            {
                itemIndex = 10;
                if (myOpcTags != null)
                    myOpcTags["User.LogSet"].SetVQT(false, 192, DateTime.Now);
            }
            else
            {
                itemIndex++;
                if (myOpcTags != null)
                    myOpcTags["User.LogSet"].SetVQT(false, 192, DateTime.Now);
            }

            watch.Reset();

        }

        void button_MouseDown(object sender, MouseEventArgs e)
        {
            ((sender as Button).Tag as Stopwatch).Start();
        }
    }
}
