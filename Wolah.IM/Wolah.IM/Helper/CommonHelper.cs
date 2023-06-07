using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wolah.IM.Helper
{
    enum commands{
        cmd_regist = 0,
        cmd_login,
        cmd_logout,
        cmd_friend_search,
        cmd_add_friend_request,
        cmd_add_friend_response,
        cmd_friend_list,
        cmd_friend_chat,

        cmd_group_create,
        cmd_group_search,
        cmd_group_join_request,
        cmd_group_join_response,
        cmd_group_list,
        cmd_group_chat,
        cmd_group_member_list,
        cmd_group_member_add,
        cmd_group_member_del,
        cmd_set_icon
    };
    class CommonHelper
    {
    }
}
