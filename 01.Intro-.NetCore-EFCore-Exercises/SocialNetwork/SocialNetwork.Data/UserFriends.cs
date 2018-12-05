﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.Models
{
    public class UserFriends
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int FriendId { get; set; }

        public User Friend { get; set; }
    }
}
