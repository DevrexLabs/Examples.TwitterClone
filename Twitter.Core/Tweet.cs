using System;
using System.Collections.Generic;

namespace Twitter.Core
{
    [Serializable]
    public class Tweet
    {
        public readonly ulong Id;
        public readonly DateTime Posted;
        public readonly string Status;
        public readonly string UserId;
        
        
        public readonly ISet<string> Favoriteers = new HashSet<string>();
        public readonly ISet<string> Retweeters = new HashSet<string>();

        public Tweet(ulong id, string userId, string content, DateTime posted)
        {
            Status = content;
            Posted = posted;
            UserId = userId;
            Id = id;
        }
    }
}