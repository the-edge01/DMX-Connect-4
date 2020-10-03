using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Connect4
{
    public partial class Form1 : Form
    {
        public enum PosState
        {
            Neutral,
            Player1,
            Player2
        }
        public PosState[,] state;
        public PosState turn;
        public Button[,] buttons;
        public int[,] address;
        public Button[] drop;
        public Color p1color = Color.Red;
        public Color p2color = Color.Blue;
        public Color ncolor = Color.Black;
        public bool win = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void DMX_Load()
        {
            try
            {
                OpenDMX.start();                                            //find and connect to devive (first found if multiple)
                if (OpenDMX.status == FT_STATUS.FT_DEVICE_NOT_FOUND)       //update status
                {
                    MessageBox.Show("No Enttec USB Device Found");
                    //Application.Exit();
                }
                else if (OpenDMX.status == FT_STATUS.FT_OK)
                {
                    MessageBox.Show("Found DMX on USB");
                }
                else
                {
                    MessageBox.Show("Error Opening Device");
                    Application.Exit();
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp);
                MessageBox.Show("Error Connecting to Enttec USB Device");
                Application.Exit();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DMX_Load();

            createGUI();

            newgame();

            timer1.Start();
        }
        private void createGUI()
        {
            buttons = new Button[7, 6];
            address = new int[7, 6];
            drop = new Button[7];
            for (int x = 0; x < 7; x++)
            {
                for (int y = 0; y < 6; y++)
                {
                    address[x, y] = 6 * (y * 7 + x) + 1;
                    this.buttons[x, y] = new System.Windows.Forms.Button();
                    this.buttons[x, y].Location = new System.Drawing.Point(155 + x * 100, 55 + y * 100);
                    this.buttons[x, y].Name = "button1";
                    this.buttons[x, y].Size = new System.Drawing.Size(80, 80);
                    this.buttons[x, y].TabIndex = 0;
                    this.buttons[x, y].Text = "";
                    this.buttons[x, y].UseVisualStyleBackColor = false;
                    this.Controls.Add(this.buttons[x, y]);
                }
                this.drop[x] = new System.Windows.Forms.Button();
                this.drop[x].Location = new System.Drawing.Point(155 + x * 100, 5);
                this.drop[x].Name = x.ToString();
                this.drop[x].Size = new System.Drawing.Size(80, 40);
                this.drop[x].TabIndex = 0;
                this.drop[x].Text = (x+1).ToString();
                this.drop[x].UseVisualStyleBackColor = true;
                this.drop[x].Click += new System.EventHandler(this.Drop_click);
                this.Controls.Add(this.drop[x]);
            }

        }
        void updateButtons()
        {
            //Player color buttons
            P1ColorButton.BackColor = p1color;
            P2ColorButton.BackColor = p2color;
            NeutralColorButton.BackColor = ncolor;

            //Turn
            Color tc = Color.Black;
            if(turn==PosState.Player1){
                turnButton.Text = "Player 1";
               tc= p1color;
            }
            else  if(turn==PosState.Player2){
                turnButton.Text = "Player 2";
                tc = p2color;
            }
             turnButton.BackColor =tc;
             OpenDMX.setDmxValue(253, (byte)( tc.R*trackBar1.Value/100));
             OpenDMX.setDmxValue(254, (byte)(tc.G * trackBar1.Value / 100));
             OpenDMX.setDmxValue(255, (byte)(tc.B * trackBar1.Value / 100));
             OpenDMX.setDmxValue(260, 0);
            if (win)
            {
                OpenDMX.setDmxValue(253, 0);
                OpenDMX.setDmxValue(254, 0);
                OpenDMX.setDmxValue(255, 0);
            }

            //Buttons
            Random rnd = new Random();//random for twinkling
            for (int x = 0; x < 7; x++)
            {
                for (int y = 0; y < 6; y++)
                {
                    Color c=Color.Black;
                    switch (state[x, y])
                    {
                        case PosState.Neutral:
                            c = ncolor;
                            if (win) //Twinkle if you win
                            {
                                byte v = (byte)(150-rnd.Next(100));
                                c = Color.FromArgb(v,v,v);
                            }
                            break;
                        case PosState.Player1:
                            c = p1color;
                            break;
                        case PosState.Player2:
                            c = p2color;
                            break;
                    }
                    this.buttons[x, y].BackColor=c;
                    OpenDMX.setDmxValue(address[x, y]+0, (byte)(c.R * trackBar1.Value / 100));
                    OpenDMX.setDmxValue(address[x, y]+1, (byte)(c.G * trackBar1.Value / 100));
                    OpenDMX.setDmxValue(address[x, y]+2, (byte)(c.B * trackBar1.Value / 100));
                    //OpenDMX.setDmxValue(address[x, y] + 0, (byte)c.R );
                    //OpenDMX.setDmxValue(address[x, y] + 1, (byte)c.G );
                    //OpenDMX.setDmxValue(address[x, y] + 2, (byte)c.B);
                }
            }
            OpenDMX.writeData();
        }

        void Drop_click(object sender, EventArgs e)
        {
            if (win)
            {
                return;
            }
            int x = Convert.ToInt32(((Button)sender).Name);
            for (int y = 5; y >= 0; y--)
            {
                if (state[x, y] == PosState.Neutral)
                {
                    if (turn == PosState.Player1)
                    {
                        state[x, y] = PosState.Player1;
                        turn = PosState.Player2;
                    } else if (turn == PosState.Player2)
                    {
                        state[x, y] = PosState.Player2;
                        turn = PosState.Player1;
                    }
                    updateButtons();
                    break;
                }
            }
            switch (checkwin())
            {
                case PosState.Neutral:
                    break;
                case PosState.Player1:
                    //MessageBox.Show("Player 1 wins!");
                    win = true;
                    break;
                case PosState.Player2:
                    //MessageBox.Show("Player 2 wins!");
                    win = true;
                    break;
            }
        }


        private void newgame()
        {
            win = false;
            state = new PosState[7, 6];
            turn = PosState.Player1;
            updateButtons();
        }

        PosState checkwin()
        {
            //verticle
            for (int x = 0; x < 7; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (state[x, y] == PosState.Neutral)
                    {
                        continue;
                    }
                    if (state[x, y] == state[x, y + 1] && state[x, y + 1] == state[x, y + 2] && state[x, y + 2] == state[x, y + 3])
                    {
                        return state[x, y];
                    }
                }
            }

            //horizontal
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 6; y++)
                {
                    if (state[x, y] == PosState.Neutral)
                    {
                        continue;
                    }
                    if (state[x, y] == state[x + 1, y] && state[x + 1, y] == state[x + 2, y] && state[x + 2, y] == state[x + 3, y])
                    {
                        return state[x, y];
                    }
                }
            }

            //diogonal left
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (state[x, y] == PosState.Neutral)
                    {
                        continue;
                    }
                    if (state[x, y] == state[x + 1, y + 1] && state[x + 1, y + 1] == state[x + 2, y + 2] && state[x + 2, y + 2] == state[x + 3, y + 3])
                    {
                        return state[x, y];
                    }
                }
            }

            //diogonal right
            for (int x = 0; x < 4; x++)
            {
                for (int y = 3; y < 6; y++)
                {
                    if (state[x, y] == PosState.Neutral)
                    {
                        continue;
                    }
                    if (state[x, y] == state[x + 1, y - 1] && state[x + 1, y - 1] == state[x + 2, y - 2] && state[x + 2, y - 2] == state[x + 3, y - 3])
                    {
                        return state[x, y];
                    }
                }
            }
            return PosState.Neutral;
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            newgame();
        }

        private void P1ColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog c = new ColorDialog();
            c.Color = p1color;
            if (c.ShowDialog() == DialogResult.OK){
             p1color = c.Color;
                updateButtons();
            }
        }

        private void P2ColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog c = new ColorDialog();
            c.Color = p2color;
            if (c.ShowDialog() == DialogResult.OK)
            {
                p2color = c.Color;
                updateButtons();
            }
        }

        private void NeutralColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog c = new ColorDialog();
            c.Color = ncolor;
            if (c.ShowDialog() == DialogResult.OK)
            {
                ncolor = c.Color;
                updateButtons();
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D1:
                    drop[0].PerformClick();
                    break;
                case Keys.D2:
                    drop[1].PerformClick();
                    break;
                case Keys.D3:
                    drop[2].PerformClick();
                    break;
                case Keys.D4:
                    drop[3].PerformClick();
                    break;
                case Keys.D5:
                    drop[4].PerformClick();
                    break;
                case Keys.D6:
                    drop[5].PerformClick();
                    break;
                case Keys.D7:
                    drop[6].PerformClick();
                    break;
                case Keys.N:
                    ResetButton.PerformClick();
                    break;
                case Keys.D8:
                    checkBox1.Checked = !checkBox1.Checked;
                    break;
                case Keys.D9:
                    checkBox4.Checked = !checkBox4.Checked;
                    break;
                default:
                    break;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //if (checkBox1.Checked)
            //{
            //    timer1.Start();
            //}
            //else if (!checkBox2.Checked)
            //{
            //    timer1.Stop();
            //}
        }
        List<byte[,]> connect4sls = new List<byte[,]> { Letters.C, Letters.O, Letters.N, Letters.N, Letters.E, Letters.C, Letters.T, Letters.n4, Letters.space, Letters.space, Letters.S, Letters.L, Letters.S, Letters.space, Letters.space, Letters.space, Letters.space };
        List<byte[,]> springfling2018 = new List<byte[,]> { Letters.S, Letters.P, Letters.R, Letters.I, Letters.N, Letters.G, Letters.space, Letters.F, Letters.L, Letters.I, Letters.N, Letters.G, Letters.space, Letters.n2, Letters.n0, Letters.n1, Letters.n8, Letters.space, Letters.space, Letters.space, Letters.space };
        List<byte[,]> springfling2018connect = new List<byte[,]> { Letters.C, Letters.O, Letters.N, Letters.N, Letters.E, Letters.C, Letters.T, Letters.n4, Letters.space, Letters.space, Letters.S, Letters.L, Letters.S, Letters.space, Letters.space, Letters.space, Letters.space, Letters.S, Letters.P, Letters.R, Letters.I, Letters.N, Letters.G, Letters.space, Letters.F, Letters.L, Letters.I, Letters.N, Letters.G, Letters.space, Letters.n2, Letters.n0, Letters.n1, Letters.n8, Letters.space, Letters.space, Letters.space, Letters.space };


        int letterstate = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                Random rnd = new Random();
                int l = rnd.Next(7);
                int h = rnd.Next(6);
                int v = 7 * h + l;
                byte r = (byte)(rnd.Next(256) * trackBar1.Value/100);
                byte g = (byte)(rnd.Next(256) * trackBar1.Value / 100);
                byte b = (byte)(rnd.Next(256) * trackBar1.Value / 100);
                OpenDMX.setDmxValue(v * 6 + 1 + 0, r);
                OpenDMX.setDmxValue(v * 6 + 1 + 1, g);
                OpenDMX.setDmxValue(v * 6 + 1 + 2, b);

                OpenDMX.writeData();
                buttons[l, h].BackColor = Color.FromArgb(r, g, b);
                //updateButtons();
            }
            else if (checkBox2.Checked || checkBox3.Checked || checkBox4.Checked)
            {
                List<byte[,]> let = null;
                if (checkBox2.Checked)
                {
                    let = connect4sls;
                }
                else if (checkBox3.Checked)
                {
                    let = springfling2018;
                }
                else if (checkBox4.Checked)
                {
                    let = springfling2018connect;
                }
                for (int x = 0; x < 7; x++)
                {
                    for (int y = 0; y < 6; y++)
                    {
                        byte[,] a = let[(letterstate/8)%let.Count];

                        Color c = Color.Black;
                        if (a[y,x] == 0 || letterstate % 8 == 0)
                        {
                            c = ncolor;
                        }
                        else
                        {
                            c = p1color;
                        }
                        
                        this.buttons[x, y].BackColor = c;
                    OpenDMX.setDmxValue(address[x, y]+0, (byte)(c.R * trackBar1.Value / 100));
                    OpenDMX.setDmxValue(address[x, y]+1, (byte)(c.G * trackBar1.Value / 100));
                    OpenDMX.setDmxValue(address[x, y]+2, (byte)(c.B * trackBar1.Value / 100));
                    }
                }
                OpenDMX.writeData();


                letterstate++;
                //updateButtons();
            }
            else
            {
                updateButtons();
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            //if (checkBox2.Checked)
            //{
            //    timer1.Start();
            //}
            //else if (!checkBox1.Checked)
            //{
            //    timer1.Stop();
            //}
        }
    }

    public static class Letters
    {
        public static byte[,] n0 = new byte[,]{
            {0,0,1,1,0,0,0 },
            {0,1,0,0,1,0,0 },
            {0,1,0,0,1,0,0 },
            {0,1,0,0,1,0,0 },
            {0,0,1,1,0,0,0 },
            {0,0,0,0,0,0,0 }
        };
        public static byte[,] n1 = new byte[,]{
            {0,0,0,1,0,0,0 },
            {0,0,1,1,0,0,0 },
            {0,0,0,1,0,0,0 },
            {0,0,0,1,0,0,0 },
            {0,1,1,1,1,0,0 },
            {0,0,0,0,0,0,0 }
        };
        public static byte[,] n2 = new byte[,]{
            {0,0,1,1,0,0,0 },
            {0,1,0,0,1,0,0 },
            {0,0,0,1,0,0,0 },
            {0,0,1,0,0,0,0 },
            {0,1,1,1,1,0,0 },
            {0,0,0,0,0,0,0 }
        };
        public static byte[,] n4 = new byte[,]{
            {0,1,0,0,1,0,0 },
            {0,1,0,0,1,0,0 },
            {0,1,1,1,1,0,0 },
            {0,0,0,0,1,0,0 },
            {0,0,0,0,1,0,0 },
            {0,0,0,0,0,0,0 }
        };
        public static byte[,] n8 = new byte[,]{
            {0,0,1,1,0,0,0 },
            {0,1,0,0,1,0,0 },
            {0,0,1,1,0,0,0 },
            {0,1,0,0,1,0,0 },
            {0,0,1,1,0,0,0 },
            {0,0,0,0,0,0,0 }
        };
        public static byte[,] C = new byte[,]{
            {0,0,0,0,0,0,0 },
            {0,1,1,1,1,0,0 },
            {0,1,0,0,0,0,0 },
            {0,1,0,0,0,0,0 },
            {0,1,1,1,1,0,0 },
            {0,0,0,0,0,0,0 }
        };
        public static byte[,] E = new byte[,]{
            {0,1,1,1,1,0,0 },
            {0,1,0,0,0,0,0 },
            {0,1,1,1,0,0,0 },
            {0,1,0,0,0,0,0 },
            {0,1,1,1,1,0,0 },
            {0,0,0,0,0,0,0 }
        };
        public static byte[,] F = new byte[,]{
            {0,1,1,1,1,0,0 },
            {0,1,0,0,0,0,0 },
            {0,1,1,1,0,0,0 },
            {0,1,0,0,0,0,0 },
            {0,1,0,0,0,0,0 },
            {0,0,0,0,0,0,0 }
        };
        public static byte[,] G = new byte[,]{
            {0,0,1,1,1,0,0 },
            {0,1,0,0,0,0,0 },
            {0,1,0,1,1,0,0 },
            {0,1,0,0,1,0,0 },
            {0,0,1,1,1,0,0 },
            {0,0,0,0,0,0,0 }
        };
        public static byte[,] I = new byte[,]{
            {0,0,1,1,1,0,0 },
            {0,0,0,1,0,0,0 },
            {0,0,0,1,0,0,0 },
            {0,0,0,1,0,0,0 },
            {0,0,1,1,1,0,0 },
            {0,0,0,0,0,0,0 }
        };
        public static byte[,] L = new byte[,]{
            {0,1,0,0,0,0,0 },
            {0,1,0,0,0,0,0 },
            {0,1,0,0,0,0,0 },
            {0,1,0,0,0,0,0 },
            {0,1,1,1,1,0,0 },
            {0,0,0,0,0,0,0 }
        };
        public static byte[,] N = new byte[,]{
            {0,0,0,0,0,0,0 },
            {0,1,0,0,1,0,0 },
            {0,1,1,0,1,0,0 },
            {0,1,0,1,1,0,0 },
            {0,1,0,0,1,0,0 },
            {0,0,0,0,0,0,0 }
        };
        public static byte[,] O = new byte[,]{
            {0,0,0,0,0,0,0 },
            {0,1,1,1,1,0,0 },
            {0,1,0,0,1,0,0 },
            {0,1,0,0,1,0,0 },
            {0,1,1,1,1,0,0 },
            {0,0,0,0,0,0,0 }
        };
        public static byte[,] P = new byte[,]{
            {0,1,1,1,1,0,0 },
            {0,1,0,0,1,0,0 },
            {0,1,1,1,1,0,0 },
            {0,1,0,0,0,0,0 },
            {0,1,0,0,0,0,0 },
            {0,0,0,0,0,0,0 }
        };
        public static byte[,] R = new byte[,]{
            {0,1,1,1,1,0,0 },
            {0,1,0,0,1,0,0 },
            {0,1,1,1,1,0,0 },
            {0,1,0,1,0,0,0 },
            {0,1,0,0,1,0,0 },
            {0,0,0,0,0,0,0 }
        };
        public static byte[,] S = new byte[,]{
            {0,1,1,1,1,0,0 },
            {0,1,0,0,0,0,0 },
            {0,0,1,1,0,0,0 },
            {0,0,0,0,1,0,0 },
            {0,1,1,1,1,0,0 },
            {0,0,0,0,0,0,0 }
        };
        public static byte[,] T = new byte[,]{
            {0,1,1,1,1,1,0 },
            {0,0,0,1,0,0,0 },
            {0,0,0,1,0,0,0 },
            {0,0,0,1,0,0,0 },
            {0,0,0,1,0,0,0 },
            {0,0,0,0,0,0,0 }
        };
        public static byte[,] space = new byte[,]{
            {0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0 }
        };
    }


    public class OpenDMX
    {

        public static byte[] buffer = new byte[513];
        public static uint handle;
        public static bool done = false;
        public static bool Connected = false;
        public static int bytesWritten = 0;
        public static FT_STATUS status;

        public const byte BITS_8 = 8;
        public const byte STOP_BITS_2 = 2;
        public const byte PARITY_NONE = 0;
        public const UInt16 FLOW_NONE = 0;
        public const byte PURGE_RX = 1;
        public const byte PURGE_TX = 2;


        [DllImport("FTD2XX.dll")]
        public static extern FT_STATUS FT_Open(UInt32 uiPort, ref uint ftHandle);
        [DllImport("FTD2XX.dll")]
        public static extern FT_STATUS FT_Close(uint ftHandle);
        [DllImport("FTD2XX.dll")]
        public static extern FT_STATUS FT_Read(uint ftHandle, IntPtr lpBuffer, UInt32 dwBytesToRead, ref UInt32 lpdwBytesReturned);
        [DllImport("FTD2XX.dll")]
        public static extern FT_STATUS FT_Write(uint ftHandle, IntPtr lpBuffer, UInt32 dwBytesToRead, ref UInt32 lpdwBytesWritten);
        [DllImport("FTD2XX.dll")]
        public static extern FT_STATUS FT_SetDataCharacteristics(uint ftHandle, byte uWordLength, byte uStopBits, byte uParity);
        [DllImport("FTD2XX.dll")]
        public static extern FT_STATUS FT_SetFlowControl(uint ftHandle, char usFlowControl, byte uXon, byte uXoff);
        [DllImport("FTD2XX.dll")]
        public static extern FT_STATUS FT_GetModemStatus(uint ftHandle, ref UInt32 lpdwModemStatus);
        [DllImport("FTD2XX.dll")]
        public static extern FT_STATUS FT_Purge(uint ftHandle, UInt32 dwMask);
        [DllImport("FTD2XX.dll")]
        public static extern FT_STATUS FT_ClrRts(uint ftHandle);
        [DllImport("FTD2XX.dll")]
        public static extern FT_STATUS FT_SetBreakOn(uint ftHandle);
        [DllImport("FTD2XX.dll")]
        public static extern FT_STATUS FT_SetBreakOff(uint ftHandle);
        [DllImport("FTD2XX.dll")]
        public static extern FT_STATUS FT_GetStatus(uint ftHandle, ref UInt32 lpdwAmountInRxQueue, ref UInt32 lpdwAmountInTxQueue, ref UInt32 lpdwEventStatus);
        [DllImport("FTD2XX.dll")]
        public static extern FT_STATUS FT_ResetDevice(uint ftHandle);
        [DllImport("FTD2XX.dll")]
        public static extern FT_STATUS FT_SetDivisor(uint ftHandle, char usDivisor);


        public static void start()
        {
            handle = 0;
            status = FT_Open(0, ref handle);
            //setting up the WriteData method to be on it's own thread. This will also turn all channels off
            //this unrequested change of state can be managed by getting the current state of all channels 
            //into the write buffer before calling this function.
            Thread thread = new Thread(new ThreadStart(writeData));
            thread.Start();
        }

        public static void setDmxValue(int channel, byte value)
        {
            if (buffer != null)
            {
                buffer[channel] = value;
            }
        }

        public static void writeData()
        {
            try
            {
                initOpenDMX();
                if (OpenDMX.status == FT_STATUS.FT_OK)
                {
                    status = FT_SetBreakOn(handle);
                    status = FT_SetBreakOff(handle);
                    bytesWritten = write(handle, buffer, buffer.Length);

                    Thread.Sleep(25);      //give the system time to send the data before sending more 

                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp);
            }

        }

        public static int write(uint handle, byte[] data, int length)
        {
            try
            {
                IntPtr ptr = Marshal.AllocHGlobal((int)length);
                Marshal.Copy(data, 0, ptr, (int)length);
                uint bytesWritten = 0;
                status = FT_Write(handle, ptr, (uint)length, ref bytesWritten);
                return (int)bytesWritten;
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp);
                return 0;
            }
        }

        public static void initOpenDMX()
        {
            status = FT_ResetDevice(handle);
            status = FT_SetDivisor(handle, (char)12);  // set baud rate
            status = FT_SetDataCharacteristics(handle, BITS_8, STOP_BITS_2, PARITY_NONE);
            status = FT_SetFlowControl(handle, (char)FLOW_NONE, 0, 0);
            status = FT_ClrRts(handle);
            status = FT_Purge(handle, PURGE_TX);
            status = FT_Purge(handle, PURGE_RX);
        }

    }

    /// <summary>
    /// Enumaration containing the varios return status for the DLL functions.
    /// </summary>
    public enum FT_STATUS
    {
        FT_OK = 0,
        FT_INVALID_HANDLE,
        FT_DEVICE_NOT_FOUND,
        FT_DEVICE_NOT_OPENED,
        FT_IO_ERROR,
        FT_INSUFFICIENT_RESOURCES,
        FT_INVALID_PARAMETER,
        FT_INVALID_BAUD_RATE,
        FT_DEVICE_NOT_OPENED_FOR_ERASE,
        FT_DEVICE_NOT_OPENED_FOR_WRITE,
        FT_FAILED_TO_WRITE_DEVICE,
        FT_EEPROM_READ_FAILED,
        FT_EEPROM_WRITE_FAILED,
        FT_EEPROM_ERASE_FAILED,
        FT_EEPROM_NOT_PRESENT,
        FT_EEPROM_NOT_PROGRAMMED,
        FT_INVALID_ARGS,
        FT_OTHER_ERROR
    };

}
