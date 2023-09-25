using DKU_PacketGenerator;

class Program
{
    static void Main(string[] args)
    {
        PacketGenerator.Gen_ServerCore_PacketType();
        PacketGenerator.Gen_Server_Packets();
    }
}