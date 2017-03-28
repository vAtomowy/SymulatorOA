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
        /// <summary>
        ///  Utworzenie obiektu zawierającego metody do obsługi portu szeregowego
        /// </summary>
        SerialPort COMport = new SerialPort();

        private delegate void UpdateUiDelegate(int msg);

        /// <summary>
        /// Zmienna przechowująca ostatnią otrzymaną wartość
        /// </summary>
        public byte recived_data { get; set; }

        /// <summary>
        /// Konstruktor w którym zostaje skonfigurowane połaczenie
        /// </summary>
        public Hardware()
        {
            COMport.BaudRate = 9600;
            COMport.Handshake = Handshake.None;
            COMport.Parity = Parity.None;
            COMport.DataBits = 8;
            COMport.StopBits = StopBits.One;
            COMport.ReadTimeout = 200;
            COMport.WriteTimeout = 50;
            
            // Przypisanie funkcji odpowadającej za przetworzenie odpowiedzi otrzymanej od urządzenia
            COMport.DataReceived += new SerialDataReceivedEventHandler(Recive);

        }

        /// <summary>
        /// Funkcja sprawdza czy urządzenie jest podłączone. Wysyła 0xFF i oczekuje otrzymania 0x01
        /// </summary>
        /// <returns>True jeżeli otrzyma w odpowiedzi 0x01, False jeżeli inną wartość</returns>
        public bool isConnected()
        {
            Send_Data((byte)255);
            Thread.Sleep(5);

            if (recived_data == (byte)1)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Funkcja ustawia nazwę portu COM przez który będzie się łączyć.
        /// </summary>
        /// <param name="port">Nazwa portu</param>
        public void setPort(string port)
        {
            COMport.Close();
            COMport.PortName = port;
            COMport.Open();
        }

        /// <summary>
        /// Zamyka połączenie.
        /// </summary>
        public void Disconnect()
        {
            COMport.Close();
        }
        
        #region Recive
        /// <summary>
        /// Funkcja odczytuje odpowiedź i zapisuje do zmiennej 'recived_data'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Recive(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(1000);
            recived_data = Convert.ToByte(COMport.ReadByte());            
        }

        /// <summary>
        /// Funkcja pomagająca w debugowaniu. Wypisuje podaną wartość w konsoli. 
        /// </summary>
        /// <param name="msg">Wartość do wypisania w konsoli</param>
        private void WriteData(int msg)
        {
            Console.WriteLine(msg);
        }
        #endregion

        #region Send
        /// <summary>
        /// Wysyła dane po wcześniejszym upewnieniu się, że port jest otwarty. W innym przypadku wyświetla okno dialogowe z informacją o błędzie.
        /// </summary>
        /// <param name="data">Bajt do wysłania</param>
        public void Send_Data(byte data)
        {
            if(COMport.IsOpen)
            {
                try
                {
                    byte[] _data = new byte[] { data };
                    COMport.Write(_data, 0, 1);
                    Thread.Sleep(50);
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
        /// <summary>
        /// Funkcja służąca do wysłania jednocześnie 2 bajtów danych.
        /// </summary>
        /// <param name="data1">Bajt 1.</param>
        /// <param name="data2">Bajt 2.</param>
        public void Send_Data(byte data1, byte data2)
        {
            Send_Data(data1);
            Send_Data(data2);
        }

        #endregion


    }
}
