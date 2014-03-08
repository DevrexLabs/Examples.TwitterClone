using System;
using OrigoDB.Core;
using Twitter.Core;

namespace Twitter.Ui
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = Db.For<TwitterVerse>();
           
            //Add some data if the db is empty
            if (db.AllUserIds().Count == 0) Setup(db);

            Console.WriteLine("Users:");
            foreach (var user in db.AllUserIds())
            {
                Console.WriteLine(user);
            }

            Console.WriteLine("Tweets:");
            foreach (var tweet in db.AllTweets())
            {
                Console.WriteLine("Id: " + tweet.Id);
                Console.WriteLine("By: " + tweet.UserId);
                Console.WriteLine("At: " + tweet.Posted);
                Console.WriteLine("Status: " + tweet.Status);
                Console.WriteLine("Favorites:" + String.Join(", ", tweet.Favoriteers));
                Console.WriteLine("Retweets: " + String.Join(", ", tweet.Retweeters));
            }

            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }

        private static void Setup(TwitterVerse db)
        {
            db.AddUser("batman");
            db.AddUser("joker");
            db.AddUser("robin");

            db.Follow("joker", "batman");
            db.Follow("batman", "robin");

            var tweetId = db.Tweet("batman", "Holy batwings, this bacon is delicious!", DateTime.Now);
            db.Retweet("robin", tweetId);
            db.Tweet("robin", "Did you see that @batman?", DateTime.Now);
            db.Tweet("batman", "I feel silly wearing this outfit", DateTime.Now);
            tweetId = db.Tweet("joker", "You won't escape this time @batman", DateTime.Now);
            db.Favorite("batman", tweetId);
        }

    }
}
