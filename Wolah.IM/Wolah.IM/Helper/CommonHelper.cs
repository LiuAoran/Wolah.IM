using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wolah.IM.Helper
{
    internal enum Commands{
        CmdRegister = 0,
        CmdLogin,
        CmdLogout,
        CmdFriendSearch,
        CmdAddFriendRequest,
        CmdAddFriendResponse,
        CmdFriendList,
        CmdFriendChat,

        CmdGroupCreate,
        CmdGroupSearch,
        CmdGroupJoinRequest,
        CmdGroupJoinResponse,
        CmdGroupList,
        CmdGroupChat,
        CmdGroupMemberList,
        CmdGroupMemberAdd,
        CmdGroupMemberDel,
        CmdSetIcon
    };
    class CommonHelper
    {
    }
}
