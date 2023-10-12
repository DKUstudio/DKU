// See https://aka.ms/new-console-template for more information
using DKU_LoginQueue;
using System.Net;
using System.Net.Sockets;

Console.WriteLine("============================");
Console.WriteLine("       DKU Login Queue      ");
Console.WriteLine("============================");

ClientListener listener = new ClientListener();

IPHostEntry entry = Dns.GetHostEntry(Dns.GetHostName());
foreach (var val in entry.AddressList)
{
    if (val.AddressFamily == AddressFamily.InterNetwork)
    {
        Console.WriteLine(val);
        listener.Start(val.ToString(), 53, int.MaxValue);
        break;
    }
}

while (true)
{
    Thread.Sleep(100);
}