using System;
using System.Collections.Generic;

namespace Twitter.Core
{
    [Serializable]
    public class User
    {
        public readonly string UserId;
        public readonly List<Tweet> Tweets = new List<Tweet>();
        public readonly ISet<string> Followers = new HashSet<string>();
        public readonly ISet<string> Followees = new HashSet<string>();

        public User(string userId)
        {
            UserId = userId;
        }

    }
}