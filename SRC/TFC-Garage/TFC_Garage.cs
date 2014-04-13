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
using System.IO.Ports;
using System.Net;
using System.Web;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using OxyPlot.Annotations;
using System.IO;
using System.Net.Sockets;

namespace TFC_Garage
{
    public partial class MainForm : Form
    {

        string SerialPortConnectionState = "Not Connected";
        string TCP_PortConnectionState = "Not Connected";

        Thread SerialPortConnectionManager;
        SerialPort MySerialPort;
        Thread TCP_PortConnectionManager;
        TcpClient MyTCP_Port;

        bool KillAllThreads = false;

        System.Windows.Forms.Timer FormUpdateTimer = new System.Windows.Forms.Timer();

        Queue<int[]> Camera0Queue = new Queue<int[]>();
        Queue<int[]> Camera1Queue = new Queue<int[]>();

        OxyPlot.Series.StairStepSeries Camera0Series = new StairStepSeries("Camera 0");
        OxyPlot.Series.StairStepSeries Camera1Series = new StairStepSeries("Camera 1");

        LineAnnotation [] CenterOfMassAnnotation = new LineAnnotation[2];
        LineAnnotation[] CenterOfMassAnnotationHorizontal = new LineAnnotation[2];

        TextAnnotation[] NumEdgesAnnotation = new TextAnnotation[2];
        StreamReader MyTCP_Stream;

        int[] NumEdges = new int[2];

        int[] CenterOfMass = new int[2];


        public MainForm()
        {
            InitializeComponent();

            SerialPortConnectionManager = new Thread(new ThreadStart(SerialPortManager));

            SerialPortConnectionManager.Start();

            TCP_PortConnectionManager = new Thread(new ThreadStart(TCP_PortManager));
            TCP_PortConnectionManager.Start();

            COM_PortComboBox.Items.Clear();

            foreach (string S in SerialPort.GetPortNames())
            {
                COM_PortComboBox.Items.Add(S);
            }

            if (COM_PortComboBox.Items.Count > 0)
                COM_PortComboBox.SelectedIndex = 0;


            FormUpdateTimer.Tick += new EventHandler(FormUpdateTimer_Tick);
            FormUpdateTimer.Interval = 50;
            FormUpdateTimer.Start();

            var myModel = new PlotModel("");

            this.CameraPlot.Model = myModel;
            myModel.PlotAreaBackground = OxyColors.White;

            Camera0Series.Color = OxyColors.Green;
            Camera1Series.Color = OxyColors.Blue;
            Camera0Series.StrokeThickness = 2.0;
            Camera1Series.StrokeThickness = 2.0;
            Camera0Series.LineJoin = OxyPenLineJoin.Round;
    
            CameraPlot.Model.Series.Add(Camera0Series);
            CameraPlot.Model.Series.Add(Camera1Series);


            var Y_Axis = new LinearAxis();
            Y_Axis.MajorGridlineStyle = LineStyle.Solid;
            Y_Axis.MajorGridlineColor = OxyColor.FromRgb(245,245,245);

            Y_Axis.MinorGridlineStyle = LineStyle.Dot;
            Y_Axis.MinorGridlineColor = OxyColor.FromRgb(250, 250, 250);
           // Y_Axis.AbsoluteMinimum = 0;
            //Y_Axis.AbsoluteMaximum = 4096;
            Y_Axis.Minimum = 0;
            Y_Axis.Maximum = 4400;
            Y_Axis.IsZoomEnabled = false;

            myModel.Axes.Add(Y_Axis);


            var X_Axis = new LinearAxis();
            X_Axis.MajorGridlineStyle = LineStyle.Solid;
            X_Axis.MinorGridlineStyle = LineStyle.Dot;
            X_Axis.Position = AxisPosition.Bottom;
            X_Axis.MajorGridlineColor = OxyColor.FromRgb(245, 245, 245);
            X_Axis.MinorGridlineColor = OxyColor.FromRgb(250, 250, 250);
            X_Axis.AbsoluteMinimum = 0;
            X_Axis.AbsoluteMaximum = 128;
     
            X_Axis.IsZoomEnabled = false;
            myModel.Axes.Add(X_Axis);


            CenterOfMassAnnotation[0] = new LineAnnotation();

            CenterOfMassAnnotation[0].Type = LineAnnotationType.Vertical;
            CenterOfMassAnnotation[0].X = 64;
            CenterOfMassAnnotation[0].LineStyle = LineStyle.Dot;
            CenterOfMassAnnotation[0].StrokeThickness = 3.0;
            CenterOfMassAnnotation[0].Color = OxyColors.Green;
            CenterOfMassAnnotation[0].MaximumY = 8000;
            CenterOfMassAnnotation[0].TextPosition = 0.25;
            CenterOfMassAnnotation[0].Text = "Center of Mass 0";

            myModel.Annotations.Add(CenterOfMassAnnotation[0]);


            CenterOfMassAnnotation[1] = new LineAnnotation();
            CenterOfMassAnnotation[1].Type = LineAnnotationType.Vertical;
            CenterOfMassAnnotation[1].X = 64;
            CenterOfMassAnnotation[1].LineStyle = LineStyle.Dot;
            CenterOfMassAnnotation[1].StrokeThickness = 3.0;
            
            CenterOfMassAnnotation[1].TextPosition = 0.45;
            CenterOfMassAnnotation[1].Color = OxyColors.Blue;
            CenterOfMassAnnotation[1].FontWeight = 20.0;
            CenterOfMassAnnotation[1].MaximumY = 8000;
            CenterOfMassAnnotation[1].Text = "Center of Mass 1";

            

            myModel.Annotations.Add(CenterOfMassAnnotation[1]);


            NumEdgesAnnotation[0] = new TextAnnotation();
            NumEdgesAnnotation[1] = new TextAnnotation();

            NumEdgesAnnotation[0].TextColor = OxyColors.Green;
            NumEdgesAnnotation[1].TextColor = OxyColors.Blue;
            NumEdgesAnnotation[0].StrokeThickness = 0;
            NumEdgesAnnotation[1].StrokeThickness = 0;

            NumEdgesAnnotation[0].FontWeight= 30.0;



            NumEdgesAnnotation[0].Position = new DataPoint(10, 4200);
            NumEdgesAnnotation[1].Position = new DataPoint(25, 4200);


            ResizeGraph();
            CameraDataSelect_CB.SelectedIndex = 0;

        }

        void FormUpdateTimer_Tick(object sender, EventArgs e)
        {
            switch (SerialPortConnectionState)
            {
                default:
                case "Not Connected":
                    ConnectButton.Text = "Serial Connect";
                    break;

                case "Connected":
                    ConnectButton.Text = "Serial Disconnect";
                    break;
            }

            switch (TCP_PortConnectionState)
            {
                default:
                case "Not Connected":
                    TCP_ConnectButton.Text = "TCP Connect";
                    break;

                case "Connected":
                    TCP_ConnectButton.Text = "TCP Disconnect";
                    break;
            }
           
            CenterOfMassAnnotation[0].X = CenterOfMass[0] + 63;
            CenterOfMassAnnotation[1].X = CenterOfMass[1] + 63;

            NumEdgesAnnotation[0].Text = "Edges Found: " + NumEdges[0];
            NumEdgesAnnotation[1].Text = "Edges Found: " + NumEdges[1];


            while(Camera0Queue.Count>0)
            {

                Camera0Series.Points.Clear();

                int[] D = Camera0Queue.Dequeue();
                if (DisplayCamera0_CB.Checked == true)
                {
                    for (int i = 0; i < D.Length; i++)
                    {
                        Camera0Series.Points.Add(new DataPoint((double)i, D[i]));
                    }
                }
            }

            while (Camera1Queue.Count > 0)
            {

                Camera1Series.Points.Clear();

                int[] D = Camera1Queue.Dequeue();

                if (DisplayCamera1_CB.Checked == true)
                {
                    for (int i = 0; i < D.Length; i++)
                    {
                        Camera1Series.Points.Add(new DataPoint((double)i, D[i]));
                    }
                }
            }


            CameraPlot.Model.Annotations.Clear();
            
            if (DisplayCamera0_CB.Checked == true)
            {
                CameraPlot.Model.Annotations.Add(CenterOfMassAnnotation[0]);
                CameraPlot.Model.Annotations.Add(NumEdgesAnnotation[0]);
            }
            if (DisplayCamera1_CB.Checked == true)
            {
                CameraPlot.Model.Annotations.Add(CenterOfMassAnnotation[1]);
                CameraPlot.Model.Annotations.Add(NumEdgesAnnotation[1]);
            }


            CameraPlot.RefreshPlot(true);
        }

        void DecodeString(string NextLineIn)
        {
      
            string[] Splits = NextLineIn.Split(':');

            if (Splits.Length >= 2)
            {
                switch (Splits[0])
                {
                    default:

                        break;


                    case "E":
                        int[] E = ConvertHexToInts(Splits[1]);

                        E = ConvertHexToInts(Splits[1]);

                        if (E.Length == 2)
                        {
                            NumEdges[0] = E[0];
                            NumEdges[1] = E[1];

                        }
                        break;
                    case "C":
                        int[] C = ConvertHexToInts(Splits[1]);

                        C = ConvertHexToInts(Splits[1]);

                        if (C.Length == 2)
                        {
                            CenterOfMass[0] = C[0];
                            CenterOfMass[1] = C[1];

                        }
                        break;

                    case "L":

                        //Grab the line data
                        int[] LineData = ConvertHexToInts(Splits[1]);

                        if (LineData.Length == 256)
                        {
                            int[] Camera0Data = new int[128];
                            int[] Camera1Data = new int[128];

                            for (int i = 0; i < 128; i++)
                            {
                                Camera0Data[i] = LineData[i];
                            }

                            for (int i = 128; i < 256; i++)
                            {
                                Camera1Data[i - 128] = LineData[i];
                            }

                            Camera0Queue.Enqueue(Camera0Data);
                            Camera1Queue.Enqueue(Camera1Data);

                        }

                        break;

                }

            }


        }

        void SerialPortManager()
        {


            while (KillAllThreads == false)
            {
                Thread.Sleep(1);

                switch (SerialPortConnectionState)
                {
                    default:
                    case "Not Connected":
                        break;

                    case "Connected":

                        lock (MySerialPort)
                        {
                            if (MySerialPort != null && MySerialPort.IsOpen == true)
                            {

                                try
                                {

                                    string NextLineIn = MySerialPort.ReadLine().Trim();

                                    DecodeString(NextLineIn);
      
                                    

                                }
                                catch
                                {

                                }
                            }
                        }

                        break;
                }
            }

            return;
        }

        void TCP_PortManager()
        {


            while (KillAllThreads == false)
            {
                Thread.Sleep(1);

                switch (TCP_PortConnectionState)
                {
                    default:
                    case "Not Connected":
                        break;

                    case "Connected":

                        lock (MyTCP_Port)
                        {
                            if (MyTCP_Port != null && MyTCP_Port.Connected == true)
                            {

                                try
                                {

                                   string NextLineIn = MyTCP_Stream.ReadLine();

                                   DecodeString(NextLineIn);



                                }
                                catch
                                {

                                }
                            }
                        }

                        break;
                }
            }

            return;
        }


        int[] ConvertHexToInts(string In)
        {
            string[] Hex = In.Split(',');

            int[] Values = new int[Hex.Length];

            for (int i = 0; i < Values.Length; i++)
            {
                try
                {
                    Values[i] = Convert.ToInt32(Hex[i], 16);
                }
                catch (Exception Ex)
                {
                    Values[i] = 0;

                }
            }

            return Values;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
             switch(SerialPortConnectionState)
            {
                case "Not Connected":

                    try
                    {
                        MySerialPort = new SerialPort();

                        MySerialPort.PortName = COM_PortComboBox.SelectedItem.ToString();

                        MySerialPort.BaudRate = 115200;

                        MySerialPort.NewLine = "\r";
                        MySerialPort.ReadTimeout = 1000;

                        MySerialPort.Open();

                     
                        SerialPortConnectionState = "Connected";
                    }
                   catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message, "Connection Error");
                    }
                        
                    break;

                case "Connected":
                    if(MySerialPort!=null)
                    {
                        try
                        {

                            MySerialPort.Close();
                            SerialPortConnectionState = "Not Connected";
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show(Ex.Message, "Disconnection Error");
                        }
                    }
                    break;

            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
         KillAllThreads = true;

            if (MySerialPort != null)
            {
                MySerialPort.Close();
            }
        }

        private void plot1_Click(object sender, EventArgs e)
        {

        }

        private void TCP_ConnectButton_Click(object sender, EventArgs e)
        {
           switch(TCP_PortConnectionState)
            {
                case "Not Connected":

                    try
                    {
                      
                        MyTCP_Port = new TcpClient();
                    
                        MyTCP_Port.Connect(new IPEndPoint(IPAddress.Parse(IP_TB.Text),Convert.ToInt32(TCP_PORT_TB.Text)));

                        MyTCP_Stream = new StreamReader(MyTCP_Port.GetStream());


                        TCP_PortConnectionState = "Connected";
                    }
                   catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message,"Connection Error");
                    }
                        
                    break;

                case "Connected":
                    if(TCP_PortConnectionState!=null)
                    {
                        try
                        {

                            MySerialPort.Close();
                            SerialPortConnectionState = "Not Connected";
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show(Ex.Message, "Disconnection Error");
                        }
                    }
                    break;

            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            ResizeGraph();
                 this.Invalidate();
        }

        void ResizeGraph()
        {
            Size S = new Size();

            S.Width = this.Width - 50;
            S.Height = this.Height - CameraPlot.Location.Y - 50;
            
            CameraPlot.Size = S;

        }

        private void CameraDataSelect_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(MySerialPort!= null && MySerialPort.IsOpen == true)
            {
                switch(CameraDataSelect_CB.SelectedIndex)
                {
                    case 0:

                        MySerialPort.WriteLine("R_Cam");
                    break;

                    case 1:
                      MySerialPort.WriteLine("D_Cam");
                    break;

                    case 2:
                      MySerialPort.WriteLine("T_Cam");
                    break;

                    default:

                    break;

                }

            }
        }
    }
}
