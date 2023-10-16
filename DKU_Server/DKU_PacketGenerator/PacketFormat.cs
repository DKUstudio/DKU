using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_PacketGenerator
{
    // {0}: 클라이언트 패킷 타입
    // {1}: 서버 패킷 타입
    // {2}: 대기열
    // {3}: 본섭
    public class PacketFormat
    {
        public static string ServerCore_PacketType =
@"  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_ServerCore.Packets
{{
    [Serializable]
    public enum PacketType
    {{
        TYPE_NONE = -1,
        
{0}

{1}

{2}

{3}
        PACKET_COUNT
    }}
}}
";


        // {0}: type
        public static string Packet_Handler_Case =
@"
                case PacketType.{0}:
                    {0}_Impl(packet);
                    break;
";
        // {0}: type
        public static string Packet_Handler_Func =
@"
        void {0}_Impl(SPacket packet)
        {{
            {0}_Handler.Method(packet);
        }}
";
        // {0}: switch case
        // {1}: implements
        public static string Server_Packet_Handler =
@"
using DKU_Server.Connections;
using DKU_Server.Connections.Tokens;
using DKU_Server.Worlds;
using DKU_Server.Packets.var;
using DKU_ServerCore;
using DKU_ServerCore.Packets;
using DKU_ServerCore.Packets.var.client;
using DKU_ServerCore.Packets.var.server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_Server.Packets
{{
    public class GamePacketHandler
    {{
        public void ParsePacket(SPacket packet)
        {{
            switch ((PacketType)packet.m_type)
            {{
{0}
            default:
                break;
            }}
        }}
{1}
    }}
}}
";

        // {0}: type
        public static string Server_Packet_Handler_Handle =
@"
using DKU_ServerCore.Packets.var.client;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DKU_Server.Connections;

namespace DKU_Server.Packets.var
{{
    public class {0}_Handler
    {{
        public static void Method(SPacket packet)
        {{
            {0} req = Data<{0}>.Deserialize(packet.m_data);

        }}
    }}
}}
";
        // {0}: case
        // {1}: impl
        public static string DummyClient_Packet_Handler =
    @"
using DKU_DummyClient.Packets.var;
using DKU_ServerCore;
using DKU_ServerCore.Packets;
using DKU_ServerCore.Packets.var.server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_DummyClient.Packets
{{
    public class GamePacketHandler
    {{
        public void ParsePacket(Packet packet)
        {{
            switch ((PacketType)packet.m_type)
            {{
{0}
            default:
                break;
            }}
        }}
{1}
    }}
}}
";
        // {0}: type
        public static string DummyClient_Packet_Handler_Handle =
@"
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKU_DummyClient.Packets.var
{{
    public class {0}_Handler
    {{
        public static void Method(Packet packet)
        {{
            {0} res = Data<{0}>.Deserialize(packet.m_data);

        }}
    }}
}}
";
        // {0}: case
        // {1}: impl
        public static string unity_Packet_Handler =
    @"
using DKU_ServerCore;
using DKU_ServerCore.Packets;
using DKU_ServerCore.Packets.var.server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GamePacketHandler
{{
    public void ParsePacket(Packet packet)
    {{
        switch ((PacketType)packet.m_type)
        {{
{0}
            default:
                break;
        }}
    }}
{1}
}}
";
        // {0}: type
        public static string unity_Packet_Handler_Handle =
@"
using DKU_ServerCore.Packets.var.server;
using DKU_ServerCore.Packets.var.queue;
using DKU_ServerCore.Packets;
using UnityEngine;

public class {0}_Handler
{{
    public static void Method(Packet packet)
    {{
        {0} res = Data<{0}>.Deserialize(packet.m_data);

    }}
}}
";
        // {0}: type
        public static string unity_Packet_Handler_Case =
@"
            case PacketType.{0}:
                {0}_Impl(packet);
                break;
";
        // {0}: type
        public static string unity_Packet_Handler_Func =
@"
    void {0}_Impl(Packet packet)
    {{
        {0}_Handler.Method(packet);
    }}
";

    // {0}: type
    public static string q_Packet_Handler_Handle =
@"
using DKU_ServerCore.Packets.var.gserver;
using DKU_ServerCore.Packets.var.qclient;
using DKU_ServerCore.Packets;

public class {0}_Handler
{{
    public static void Method(Packet packet)
    {{
        {0} res = Data<{0}>.Deserialize(packet.m_data);

    }}
}}
";

    }

}