using System;
using System.Threading;
using System.IO.Ports;
using System.Diagnostics;

namespace IWSK_RS232
{
    class PortSettings
    {
        public string name;
        public int baudRate;
        public System.IO.Ports.Parity parity;
        public int dataBits;
        public System.IO.Ports.StopBits stopBits;
    }

    class SerialPortMessage
    {
        public string message;
        public bool isTest;
        public bool isPingResponse;
    }

    class SerialPortProgram
    {
        private SerialPort port;
        private string endMessageChar = "B";
        public string messageBuffer;

        // [STAThread]
        // static void Main(string[] args)
        // {
        //     new SerialPortProgram();
        // }

        private SerialPortProgram()
        {
            Console.WriteLine("Program called");
            setupPort(new PortSettings { name = "COM4", baudRate = 9600, dataBits = 7, parity = Parity.Even, stopBits = StopBits.One });
            // setupPortSender(new PortSettings { name = "COM4", baudRate = 9600, dataBits = 7, parity = Parity.Even, stopBits = StopBits.One }, portSender);
            port.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            // portReceiver.NewLine = "O";
            // WriteToPort(new SerialPortMessage { isTest = false, message = "tesk" }, portSender);
            // WriteToPort(new SerialPortMessage { isTest = false, message = "tesk" }, portSender);
            // WriteToPort("hello");
            // WriteToPort("hello");
            // WriteToPort("hello");

            // ping();
            Console.Read();
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {

            Console.WriteLine("COM3 incoming data:");
            // Thread.Sleep(300);
            string incoming = port.ReadExisting();

            messageBuffer += incoming;

            if (incoming == endMessageChar)
            {
                // pelna wiadomosc
                processMessage(messageBuffer);
                messageBuffer = String.Empty;
            }
            // if (incoming == endMessageChar.ToCharArray()[0])
            // {
            //     Console.WriteLine("end line");
            // }
            // Console.WriteLine(incoming);
            // Console.WriteLine("----");
        }

        private string ConstructMessage(SerialPortMessage messageData)
        {
            int informationChar = 0;

            if (messageData.isTest) {
                informationChar = 1;
            }

            if (messageData.isPingResponse) {
                informationChar = 2;
            }

            return informationChar.ToString() + messageData.message;
        }

        public void WriteToPort(SerialPortMessage messageData)
        {
            string message = ConstructMessage(messageData);
            port.WriteLine(message);
        }

        public void setupPort(PortSettings portSettings)
        {
            port = new SerialPort(portSettings.name, portSettings.baudRate, portSettings.parity, portSettings.dataBits, portSettings.stopBits);
            port.NewLine = endMessageChar;
            port.Open();
        }

        public void loopback()
        {
            // WriteToPort("test");
        }

        public void ping()
        {
            var stopwatch = new Stopwatch();

            // WriteToPort("test");

            stopwatch.Stop();

            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }

        private void processMessage(string message)
        {
            char messageType = message[0];
            string parsedMessage = message.Substring(1, message.Length - 2);

            if (messageType == '0')
            {
                Console.WriteLine("nie testowa");
            }

            if (messageType == '1')
            {
                WriteToPort(new SerialPortMessage { isTest = false, message = "PONG", isPingResponse = true });
            }

            Console.WriteLine(parsedMessage);
        }
    }
}
