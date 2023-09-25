start ../DKU_PacketGenerator/bin/Debug/net6.0/DKU_PacketGenerator.exe
xcopy /y .\gen\PacketType.cs ..\DKU_ServerCore\Packets\
xcopy /y .\gen\server\GamePacketHandler.cs ..\DKU_Server\Packets
xcopy /s .\gen\server\var ..\DKU_Server\Packets\var