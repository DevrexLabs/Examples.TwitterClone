using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrigoDB.Core;
using Twitter.Core;

namespace Twitter.Test
{
    [TestClass]
    public class TwitterVerseTests
    {

        [TestMethod]
        public void Can_tweet()
        {
            var target = new TwitterVerse();
            var userId = "rob";
            var message = "Hello world!";
            target.AddUser(userId);
            target.Tweet(userId, message, DateTime.Now);
        }

        [TestMethod]
        public void Can_add_user()
        {
            var target = new TwitterVerse();
            string userId = "robtheslob";
            target.AddUser(userId);
        }

        [TestMethod]
        [ExpectedException(typeof(CommandFailedException))]
        public void Cant_add_duplicate_user()
        {
            var target = new TwitterVerse();
            string userId = "rob";
            target.AddUser(userId);
            target.AddUser(userId);
        }

        [TestMethod]
        public void Can_follow()
        {
            var target = new TwitterVerse();
            var user1 = "robtheslob";
            var user2 = "codinginsomnia";
            target.AddUser(user1);
            target.AddUser(user2);
            target.Follow(user1, user2);
        }

        [TestMethod]
        [ExpectedException(typeof (CommandFailedException))]
        public void Cant_follow_non_existing_user()
        {
            var target = new TwitterVerse();
            var user = "robtheslob";
            target.AddUser(user);
            target.Follow(user, "codinginsomnia");
        }

        [TestMethod]
        public void Tweets_are_returned()
        {
            var target = new TwitterVerse();
            var user = "robtheslob";
            var status = "Are we having fun now?";

            target.AddUser(user);
            var posted = DateTime.Now;
            ulong tweetId = target.Tweet(user, status, posted);

            var tweet = target.AllTweets().Single();
            
            Assert.AreEqual(tweetId, tweet.Id);
            Assert.AreEqual(user, tweet.UserId);
            Assert.AreEqual(posted, tweet.Posted);
            Assert.AreEqual(status, tweet.Status);
            Assert.AreEqual(0, tweet.Favoriteers.Count);
            Assert.AreEqual(0, tweet.Retweeters.Count);
        }


        [TestMethod]
        [ExpectedException(typeof(CommandFailedException))]
        public void Excessive_length_status_is_rejected()
        {
            var target = new TwitterVerse();
            var user = "robtheslob";
            target.AddUser(user);
            var status = string.Join(". ", Enumerable.Repeat("Say you, say me", 20));
            target.Tweet(user, status, DateTime.Now);
        }



    }
}
