using IWSK_RS232;
using System;
using System.ComponentModel;
using System.IO;
using System.IO.Ports;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace IWSK_FRONTEND
{
    public class ViewModel : INotifyPropertyChanged
    {
        private ICommand _clickCommand;
        private ICommand _sendCommand;
        private ICommand _transactionCommand;
        private ICommand _pingCommand;
        private ICommand _submitChangesCommand;
        private string _selectedPortName;
        private int _bitRate;

        public SerialPortProgram program = new SerialPortProgram();
        public ViewModel()
        {
            program.PublishIncomingData += MessageCame;
            program.PinChangedEventData += PinChangedHandler;
        }

        public string SelectedPortName
        {
            get { return _selectedPortName; }
            set
            {
                _selectedPortName = value;
                NotifyPropertyChanged();
            }
        }
        public int BitRate
        {
            get { return _bitRate; }
            set
            {
                _bitRate = value;
                NotifyPropertyChanged();
            }
        }

        private int _stopBit;
        private ParityTypes _parityType;
        private int _dataBit;
        private int _writeTimeout;

        public int StopBit
        {
            get { return _stopBit; }
            set
            {
                _stopBit = value;
                NotifyPropertyChanged();
            }
        }

        public ParityTypes ParityType
        {
            get => _parityType; set
            {
                _parityType = value;
                NotifyPropertyChanged();
            }
        }

        public int DataBit
        {
            get { return _dataBit; }
            set
            {
                _dataBit = value;
                NotifyPropertyChanged();
            }
        }

        public int WriteTimeout
        {
            get { return _writeTimeout; }
            set
            {
                _writeTimeout = value;
                NotifyPropertyChanged();
            }
        }

        private string _receivedMessage = "";
        private string _messageInput = "";
        public string ReceivedMessage
        {
            get => _receivedMessage; set
            {
                _receivedMessage = value;
                NotifyPropertyChanged();
            }
        }

        public string MessageInput { get => _messageInput; set => _messageInput = value; }
        public ICommand ClickCommand { get => _clickCommand ?? (_clickCommand = new CommandHandler(Button_Click)); }
        public ICommand SendCommand { get => _sendCommand ?? (_sendCommand = new CommandHandler(Button_SendMessage)); }
        public ICommand TransactionCommand { get => _transactionCommand ?? (_transactionCommand = new CommandHandler(Button_Transaction)); }

        public ICommand PingCommand { get => _pingCommand ?? (_pingCommand = new CommandHandler(Button_Ping)); }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void MessageCame(object sender, DataReceivedEventArgs e)
        {
            ReceivedMessage = e.Msg;
            NotifyPropertyChanged();
        }

        private void PinChangedHandler(object sender, PinChangedEventArgs e)
        {
            if (e.Pin == "CTS")
            {
                IsCTSActive = e.state;
            }

            if (e.Pin == "DSR")
            {
                IsDSRActive = e.state;
            }

            if (e.Pin == "DTR")
            {
                IsDTRActive = e.state;
            }

            if (e.Pin == "RTS")
            {
                IsRTSActive = e.state;
            }

            NotifyPropertyChanged();
        }

        private string getTerminator()
        {
            if (TerminatorType == TerminatorTypes.None)
            {
                Terminator = "\n";
            }
            if (TerminatorType == TerminatorTypes.CL)
            {
                Terminator = "\n";
            }
            if (TerminatorType == TerminatorTypes.RF)
            {
                Terminator = "\r";
            }
            if (TerminatorType == TerminatorTypes.CLRF)
            {
                Terminator = "\r\n";
            }

            return Terminator;
        }

        public void Button_Click()
        {
            program.setupPort(new PortSettings { baudRate = BitRate, name = SelectedPortName, dataBits = DataBit, parity = (Parity)ParityType, stopBits = (StopBits)StopBit, handshake = FlowControl, terminator = getTerminator(), timeout = WriteTimeout });
            MessageBox.Show("Connected successfully!");
        }

        public void Button_SendMessage()
        {
            program.WriteToPort(new SerialPortMessage { isPingResponse = false, isTest = false, message = MessageInput });
        }

        public void Button_Transaction()
        {
            program.Transaction(new SerialPortMessage { isPingResponse = false, isTest = false, message = MessageInput, isTransaction = true });
        }

        public void Button_Ping()
        {
            program.Ping();
        }

        public string[] SerialPortNames
        {
            get { return System.IO.Ports.SerialPort.GetPortNames(); }
        }

        public int[] BitRates { get => _bitRates; set => _bitRates = value; }

        private int[] _bitRates = new int[] { 75, 110, 300, 1200, 2400, 4800, 9600, 19200, 38400, 57600, 115200 };

        public int[] DataBits { get => _dataBits; set => _dataBits = value; }

        private int[] _stopBits = new int[] { 1, 2 };
        public int[] StopBits { get => _stopBits; set => _stopBits = value; }

        private int[] _dataBits = new int[] { 7, 8 };

        private FlowControlTypes _flowControl;
        public FlowControlTypes FlowControl
        {
            get { return _flowControl; }
            set
            {
                _flowControl = value;
                if (value == FlowControlTypes.Manual)
                {
                    CanControlSignals = true;
                }
                NotifyPropertyChanged();
            }
        }

        private bool _canControlSignals = false;
        public bool CanControlSignals
        {
            get { return _canControlSignals; }
            set
            {
                _canControlSignals = value;
                NotifyPropertyChanged();
            }
        }


        private bool _isCustomTerminator = false;
        public bool IsCustomTerminator
        {
            get { return _isCustomTerminator; }
            set
            {
                _isCustomTerminator = value;
                NotifyPropertyChanged();
            }
        }

        private TerminatorTypes _terminatorType;
        public TerminatorTypes TerminatorType
        {
            get { return _terminatorType; }
            set
            {
                _terminatorType = value;
                if (value == TerminatorTypes.Custom)
                {
                    IsCustomTerminator = true;
                }
                NotifyPropertyChanged();
            }
        }

        private string _terminator = "\n";
        public string Terminator { get => _terminator; set => _terminator = value; }

        private bool _isDTRActive;
        public bool IsDTRActive
        {
            get { return _isDTRActive; }
            set
            {
                if (_isDTRActive != value)
                {
                    _isDTRActive = value;
                    program.IsDTRActive = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private bool _isDSRActive;
        public bool IsDSRActive
        {
            get { return _isDSRActive; }
            set
            {
                if (_isDSRActive != value)
                {
                    _isDSRActive = value;
                    NotifyPropertyChanged();
                    program.IsDSRActive = value;
                }
            }
        }

        private bool _isRTSActive;
        public bool IsRTSActive
        {
            get { return _isRTSActive; }
            set
            {
                if (_isRTSActive != value)
                {
                    _isRTSActive = value;
                    program.IsRTSActive = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private Stream _streamFile;
        public Stream StreamFile
        {
            get { return _streamFile; }
            set
            {
                _streamFile = value;
                NotifyPropertyChanged();
            }
        }


        private bool _isCTSActive;
        public bool IsCTSActive
        {
            get { return _isCTSActive; }
            set
            {
                if (_isCTSActive != value)
                {
                    _isCTSActive = value;
                    program.IsCTSActive = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _fileNamePath = String.Empty;
        public string FileNamePath
        {
            get { return _fileNamePath; }
            set
            {
                if (_fileNamePath != value)
                {
                    _fileNamePath = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }

    public class CommandHandler : ICommand
    {
        private Action _action;
        public CommandHandler(Action action)
        {
            _action = action;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action();
        }

    }
}
