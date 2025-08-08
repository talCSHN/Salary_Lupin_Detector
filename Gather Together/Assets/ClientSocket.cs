using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


public class ClientSocket
{
    private ClientSocket()
    {

    }

    private Socket socket;
    private static ClientSocket instance = new ClientSocket();
    public static ClientSocket Instance
    {
        get => instance;
    }

    private byte[] sendBuf;
    public byte[] SendBuf
    {
        get => sendBuf;
        set => sendBuf = value;
    }
    private byte[] recvBuf;
    public byte[] RecvBuf
    {
        get => recvBuf;
        set => recvBuf = value;
    }
    public string PlayerName
    {
        get;
        private set;
    }

    public event Action<string> MessageReceived;

    public async Task MakeConnection(string ip, int port, string name)
    {
        await Task.Run(async () =>
        {
            try
            {
                PlayerName = name;
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                EndPoint serverEP = new IPEndPoint(IPAddress.Parse(ip), port);
                socket.Connect(serverEP);
                await StartReceiving();
            }
            catch (Exception ex)
            {
            }
        });
    }
    public async Task SendToServer(string message)
    {
        try
        {
            SendBuf = Encoding.UTF8.GetBytes(message);
            await Task.Run(() => socket.Send(SendBuf));
            //await socket.SendAsync(new ArraySegment<byte>(SendBuf));
            if (socket == null)
            {
                Console.WriteLine("소켓이 아직 연결되지 않았음");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Send Error: {ex.Message}");
        }
    }
    private Queue<Action> mainThreadQueue = new Queue<Action>();
    public void Update()
    {
        lock (mainThreadQueue)
        {
            while (mainThreadQueue.Count > 0)
            {
                mainThreadQueue.Dequeue().Invoke();
            }
        }
    }
    public async Task StartReceiving()
    {
        await Task.Run(() =>
        {
            while (true)
            {
                try
                {
                    RecvBuf = new byte[1024];
                    int size = socket.Receive(RecvBuf);
                    if (size == 0) break;

                    string msg = Encoding.UTF8.GetString(RecvBuf, 0, size).Trim();
                    lock (mainThreadQueue)
                    {
                        mainThreadQueue.Enqueue(() =>
                        {
                            MessageReceived?.Invoke(msg);
                        });
                    }
                }
                catch (Exception ex)
                {
                        
                }
            }
        });
    }
}

