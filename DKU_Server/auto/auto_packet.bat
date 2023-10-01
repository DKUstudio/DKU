@REM servercore
xcopy /y ..\DKU_ServerCore\CommonDefine.cs ..\..\DKU_Client\Assets\Network\ServerCore\CommonDefine.cs
xcopy /y ..\DKU_ServerCore\Packets\var\ ..\..\DKU_Client\Assets\Network\ServerCore\Packets\ /e /h /k 
xcopy /y ..\DKU_ServerCore\Packets\ChatData.cs ..\..\DKU_Client\Assets\Network\ServerCore\Packets\ChatData.cs 
xcopy /y ..\DKU_ServerCore\Packets\JVector3.cs ..\..\DKU_Client\Assets\Network\ServerCore\Packets\JVector3.cs 
xcopy /y ..\DKU_ServerCore\Packets\Data.cs ..\..\DKU_Client\Assets\Network\ServerCore\Packets\Data.cs 
xcopy /y ..\DKU_ServerCore\Packets\MessageResolver.cs ..\..\DKU_Client\Assets\Network\ServerCore\Packets\MessageResolver.cs 
xcopy /y ..\DKU_ServerCore\Packets\Packet.cs ..\..\DKU_Client\Assets\Network\ServerCore\Packets\Packet.cs 
xcopy /y ..\DKU_ServerCore\Packets\PacketType.cs ..\..\DKU_Client\Assets\Network\ServerCore\Packets\PacketType.cs 
xcopy /y ..\DKU_ServerCore\Packets\UserData.cs ..\..\DKU_Client\Assets\Network\ServerCore\Packets\UserData.cs 


@REM client
start ../DKU_PacketGenerator/bin/Debug/net6.0/DKU_PacketGenerator.exe