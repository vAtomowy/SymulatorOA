using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace SymulatorObiektow_WPF_
{
    public class Hardware
    {
        SerialPort COMport = new SerialPort();
        //Thread COMcomm;
        private delegate void UpdateUiDelegate(int msg);

        public byte recived_data { get; set; }

        public Hardware()
        {
            //COMport.PortName = "COM1";
            COMport.BaudRate = 9600;
            COMport.Handshake = Handshake.None;
            COMport.Parity = Parity.None;
            COMport.DataBits = 8;
            COMport.StopBits = StopBits.One;
            COMport.ReadTimeout = 200;
            COMport.WriteTimeout = 50;
            COMport.DataReceived += new SerialDataReceivedEventHandler(Recive);

            //COMport.Open();
        }

        public bool isConnected()
        {
            Send_Data((byte)255);
            Thread.Sleep(5);

            if (recived_data == (byte)0)
                return true;
            else
                return false;
        }

        public void setPort(string port)
        {
            COMport.Close();
            COMport.PortName = port;
            COMport.Open();
        }

        public void Disconnect()
        {
            COMport.Close();
        }

        #region Recive
        private void Recive(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(1000);
            recived_data = Convert.ToByte(COMport.ReadByte());
            //Dispatcher.Invoke(DispatcherPriority.Send, new UpdateUiDelegate(WriteData), recived_data); // używany do aktualizacji wyglądu
            
        }
        private void WriteData(int msg)
        {
            Console.WriteLine(msg);//tutaj będzie zmieniać wygląd 
        }
        #endregion

        #region Send

        public void Send_Data(byte data)
        {
            if(COMport.IsOpen)
            {
                try
                {
                    byte[] _data = new byte[] { data };
                    COMport.Write(_data, 0, 1);
                    Thread.Sleep(20);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("FAILED TO SEND" + data + ": " + ex);
                }
            }
            else
            {
                Console.WriteLine("Not Connected");
            }
        }

        #endregion


    }
}
