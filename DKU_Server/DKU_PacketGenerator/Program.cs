using DKU_PacketGenerator;

class Program
{
    static void Main(string[] args)
    {
        PacketGenerator.Gen_ServerCore_PacketType();
        PacketGenerator.Gen_Server_Packets();
        PacketGenerator.Gen_DummyClient_Packets();
        PacketGenerator.Gen_Unity_Packets();
        PacketGenerator.Gen_Queue_Packets();
        PacketGenerator.Copy();
    }
}