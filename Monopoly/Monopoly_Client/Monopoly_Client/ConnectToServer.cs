using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;

namespace Monopoly_Client
{
    public partial class ConnectToServer : Form
    {
        private Client m_client;
        public ConnectToServer()
        {
            InitializeComponent();
            //GetLocalIPv4Address();
        }

        //private void GetLocalIPv4Address()
        //{
        //    try
        //    {
        //        string hostName = Dns.GetHostName();
        //        IPAddress[] addresses = Dns.GetHostAddresses(hostName);
        //        foreach (IPAddress address in addresses)
        //        {
        //            if (address.AddressFamily == AddressFamily.InterNetwork)
        //            {
        //                ipAddressBox.Text = address.ToString();
        //                break;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Không thể lấy địa chỉ IPv4: " + ex.Message);
        //    }
        //}

        private void connectButton_Click(object sender, EventArgs e)
        {
            // Check if the user entered anything in the text box, entered too much, or entered command.
            if (nameBox.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập tên để kết nối tới server.", "Lỗi");
                return;
            }
            if (nameBox.Text.Length > 21)
            {
                MessageBox.Show("Ten khong duoc qua 20 ki tu.", "Loi");
                return;
            }
            if (nameBox.Text.Contains(','))
            {
                MessageBox.Show("Vui long nhap ten khong co ki tu dac biet.", "Loi");
                return;
            }
            // Create a client.
            if (m_client == null)
            {
                m_client = new Client();
            }
            m_client.Name = nameBox.Text;
            this.Text = "Dang ket noi...";
            // Connect to the server. Was it a success?
            if (m_client.Connect("127.0.0.1"))
            {
                // Show a message upon successful connection.
                this.Text = "Thanh cong";
                MessageBox.Show("Da ket noi thanh cong!");
                // Set the client's name to the name they connected with.
                // Open the main window, then close this window.
                this.Hide();
                MainWindow main = new MainWindow(m_client);
                main.ShowDialog();
                this.Close();
            }
            else
            {
                this.Text = "Kêt nối tới Server";
                m_client.RefreshSocket();
            }
        }

        
    }
}
