using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.Ports;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace IWSK_RS232
{
    public enum ParityTypes
    {
        None = 0,
        Odd = 1,
        Even = 2,
    }

    public enum TerminatorTypes
    {
        None,
        CL,
        RF,
        CLRF,
        Custom
    }

    public enum FlowControlTypes
    {
        None = 0,
        XOnXOff = 1,
        RequestToSend = 2,
        Manual = 3
    }

    public class DataReceivedEventArgs : EventArgs
    {
        public string Msg { get; set; }
    }

    public class PinChangedEventArgs : EventArgs
    {
        public string Pin { get; set; }
        public bool state { get; set; }
    }


    public class PortSettings
    {
        public string name;
        public int baudRate;
        public System.IO.Ports.Parity parity;
        public int dataBits;
        public System.IO.Ports.StopBits stopBits;
        public FlowControlTypes handshake;
        public string terminator;
        public int timeout;
    }

    public class SerialPortMessage
    {
        public string message;
        public bool isTest;
        public bool isPingResponse;
        public bool isTransaction;
    }

    public class SerialPortProgram : INotifyPropertyChanged
    {
        private bool _isRTSActive;
        private bool _isCTSActive;
        private bool _isDTRActive;
        private bool _isDSRActive;
        private string receivedFullMessage;
        private char receivedFullMessageType;
        private FlowControlTypes _handshake;

        public SerialPort port = new SerialPort();
        public string messageBuffer;
        public Stopwatch stopwatch = new Stopwatch();
        public event EventHandler<DataReceivedEventArgs> PublishIncomingData;
        public event EventHandler<PinChangedEventArgs> PinChangedEventData;
        public delegate void DataReceived(string data);
        public event DataReceived OnDataReceived;
        public event PropertyChangedEventHandler PropertyChanged;
        public SerialPortProgram()
        {
            port.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            InitPinChangedEventHandler();
            Console.Read();
        }
        public bool IsDTRActive
        {
            get { return _isDTRActive; }
            set
            {
                if (_isDTRActive != value)
                {
                    _isDTRActive = value;
                    port.DtrEnable = value;
                }
            }
        }

        public bool IsDSRActive
        {
            get { return _isDSRActive; }
            set
            {
                if (_isDSRActive != value)
                {
                    _isDSRActive = value;
                }
            }
        }

        public bool IsRTSActive
        {
            get { return _isRTSActive; }
            set
            {
                if (_isRTSActive != value)
                {
                    _isRTSActive = value;
                    port.RtsEnable = value;
                }
            }
        }

        public bool IsCTSActive
        {
            get { return _isCTSActive; }
            set
            {
                if (_isCTSActive != value)
                {
                    _isCTSActive = value;
                }
            }
        }

        public FlowControlTypes Handshake { get => _handshake; set => _handshake = value; }


        private string ConstructMessage(SerialPortMessage messageData)
        {
            int informationChar = 0;

            if (messageData.isTest)
            {
                informationChar = 1;
            }

            if (messageData.isPingResponse)
            {
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
            port.PortName = portSettings.name;
            port.BaudRate = portSettings.baudRate;
            port.Parity = portSettings.parity;
            port.DataBits = portSettings.dataBits;
            port.StopBits = portSettings.stopBits;
            port.NewLine = portSettings.terminator;
            port.ReadTimeout = portSettings.timeout;

            if (portSettings.handshake == FlowControlTypes.Manual || portSettings.handshake == FlowControlTypes.None)
            {
                port.Handshake = System.IO.Ports.Handshake.None;
            }
            else
            {
                port.Handshake = (Handshake)portSettings.handshake;
            }

            Handshake = portSettings.handshake;

            port.Open();
        }

        public void Ping()
        {
            stopwatch.Start();

            WriteToPort(new SerialPortMessage { isTest = true, message = "PING REQUEST!" });

            stopwatch.Stop();

            PublishIncomingData(this, new DataReceivedEventArgs { Msg = "Ping: " + stopwatch.ElapsedMilliseconds });
            OnDataReceived?.Invoke("Ping: " + stopwatch.ElapsedMilliseconds);
            OnPropertyChanged();
        }

        public async void Transaction(SerialPortMessage messageData)
        {
            WriteToPort(messageData);

            receivedFullMessage = String.Empty;

            var t = Task.Run(() =>
            {
                while (receivedFullMessage.Length <= 0)
                { }
            });

            if (t.Wait(port.ReadTimeout))
            {
                PublishIncomingData(this, new DataReceivedEventArgs { Msg = receivedFullMessage });
                OnDataReceived?.Invoke(receivedFullMessage);
                OnPropertyChanged();
                return;
            }


            PublishIncomingData(this, new DataReceivedEventArgs { Msg = "Transaction timeout" });
            OnDataReceived?.Invoke("Transaction timeout");
            OnPropertyChanged();

        }

        private string processMessage(string message)
        {
            char messageType = message[0];
            string parsedMessage = message.Substring(1, message.Length - 2);

            if (messageType == '0')
            {
                receivedFullMessageType = '0';
            }

            if (messageType == '1')
            {
                WriteToPort(new SerialPortMessage { isTest = false, message = "PONG", isPingResponse = true });
                receivedFullMessageType = '1';
            }

            if (messageType == '2')
            {
                receivedFullMessageType = '2';
                stopwatch.Stop();
            }

            if (messageType == '3')
            {
                receivedFullMessageType = '3';
            }

            return parsedMessage;
        }
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {

            Console.WriteLine("COM3 incoming data:");
            string incoming = port.ReadExisting();

            messageBuffer += incoming;

            if (messageBuffer.EndsWith(port.NewLine))
            {
                string finalmessage = processMessage(messageBuffer);
                receivedFullMessage = finalmessage;
                messageBuffer = String.Empty;

                if (receivedFullMessageType != '3' && receivedFullMessageType != '2')
                {
                    PublishIncomingData(this, new DataReceivedEventArgs { Msg = finalmessage });
                    OnDataReceived?.Invoke(finalmessage);
                    OnPropertyChanged();
                }


            }

        }
        private void InitPinChangedEventHandler()
        {
            port.PinChanged += (o, e) =>
            {
                var serialPinChange = e.EventType;
                switch (serialPinChange)
                {
                    case SerialPinChange.CtsChanged:
                        IsCTSActive = !IsCTSActive;
                        PinChangedEventData(this, new PinChangedEventArgs { Pin = "CTS", state = IsCTSActive });
                        break;
                    case SerialPinChange.DsrChanged:
                        IsDSRActive = !IsDSRActive;
                        PinChangedEventData(this, new PinChangedEventArgs { Pin = "DSR", state = IsDSRActive });
                        break;
                }
            };
            // Initialize timer to check DTR and RTS pins each 100ms
            new Timer(handshake =>
            {
                if ((int)handshake != 3)
                {
                    IsDTRActive = port.DtrEnable;
                    IsRTSActive = port.RtsEnable;
                    PinChangedEventData(this, new PinChangedEventArgs { Pin = "DTR", state = IsDTRActive });
                    PinChangedEventData(this, new PinChangedEventArgs { Pin = "RTS", state = IsRTSActive });
                }
            }, Handshake, TimeSpan.FromMilliseconds(0), TimeSpan.FromMilliseconds(100));
        }
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
