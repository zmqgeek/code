    public partial class Form1 : Form
    {
         SerialPort _serialPort;
    
         // delegate is used to write to a UI control from a non-UI thread
         private delegate void SetTextDeleg(string text);
    
         private void Form1_Load(object sender, EventArgs e)
         {
               // all of the options for a serial device
               // can be sent through the constructor of the SerialPort class
               // PortName = "COM1", Baud Rate = 19200, Parity = None, 
               // Data Bits = 8, Stop Bits = One, Handshake = None
               _serialPort = new SerialPort("COM1", 19200, Parity.None, 8, StopBits.One);
               _serialPort.Handshake = Handshake.None;
               _serialPort.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
               _serialPort.ReadTimeout = 500;
               _serialPort.WriteTimeout = 500;
               _serialPort.Open();
         }
    
         private void btnStart_Click(object sender, EventArgs e)
         {
               // Makes sure serial port is open before trying to write
               try
               {
                    if(!_serialPort.IsOpen)
                         _serialPort.Open();
    
                    _serialPort.Write("SI\r\n");
               }
               catch (Exception ex)
               {
                    MessageBox.Show("Error opening/writing to serial port :: " + ex.Message, "Error!");
               }
         }
    
         void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
         {
              Thread.Sleep(500);
              string data = _serialPort.ReadLine();
              this.BeginInvoke(new SetTextDeleg(si_DataReceived), new object[] { data });
         }
    
         private void si_DataReceived(string data)
         {
               textBox1.Text = data.Trim();
         }
    }