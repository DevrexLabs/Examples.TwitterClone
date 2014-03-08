using System;
using System.Collections.Generic;
using System.Linq;
using OrigoDB.Core;
using OrigoDB.Core.Proxy;

namespace Twitter.Core
{

    [Serializable]
    public class TwitterVerse : Model
    {
        SortedDictionary<ulong,Tweet> _tweets = new SortedDictionary<ulong,Tweet>();
        Dictionary<string,User> _users = new Dictionary<string, User>(StringComparer.InvariantCultureIgnoreCase);
        ulong _nextTweetId = 1;

        public SortedDictionary<ulong, Tweet> Tweets
        {
            get { return _tweets; }
        }

        public Dictionary<string, User> Users
        {
            get { return _users; }
        } 

        public IList<String> AllUserIds()
        {
            return _users.Keys.ToList();
        }

        public IList<Tweet> AllTweets()
        {
            return _tweets.Values.ToList();
        }

        public void AddUser(string userId)
        {
            if (_users.ContainsKey(userId)) throw new CommandFailedException("User already exists");
            _users[userId] = new User(userId);
        }

        [ProxyMethod(OperationType = OperationType.Command)]
        public ulong Tweet(string userId, string message, DateTime posted)
        {
            AssertUserExists(userId);
            if (message.Length > 141) throw new CommandFailedException("Tweet too long");
            ulong id = _nextTweetId++;
            var tweet = new Tweet(id, userId, message, posted);
            _users[userId].Tweets.Add(tweet);
            _tweets.Add(id, tweet);
            return id;
        }

        private void AssertUserExists(string userId)
        {
            if (!_users.ContainsKey(userId)) 
                throw new CommandFailedException("No such user: " + userId);
        }

        public void Retweet(string userId, ulong tweetId)
        {
            AssertUserExists(userId);
            if (!_tweets.ContainsKey(tweetId)) throw new CommandFailedException();
            _tweets[tweetId].Retweeters.Add(userId);
        }

        public void Favorite(string userId, ulong tweetId)
        {
            AssertUserExists(userId);
            if (!_tweets.ContainsKey(tweetId)) throw new CommandFailedException();
            _tweets[tweetId].Favoriteers.Add(userId);
        }

        public void Follow(string follower, string followee)
        {
            AssertUserExists(follower);
            AssertUserExists(followee);
            _users[follower].Followees.Add(followee);
            _users[followee].Followers.Add(follower);
        }

        public void Unfollow(string follower, string followee)
        {
            AssertUserExists(follower);
            AssertUserExists(followee);
            _users[follower].Followees.Remove(followee);
            _users[followee].Followers.Remove(follower);
       }
    }
}
