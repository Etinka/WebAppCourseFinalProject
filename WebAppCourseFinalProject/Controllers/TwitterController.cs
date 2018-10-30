using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tweetinvi;

namespace WebAppCourseFinalProject.Controllers
{
    public class TwitterController : Controller
    {

        public TwitterController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        private void initTwitter()
        {
            Auth.SetUserCredentials(Consts.TWITTER_CONSUMER_KEY,
                Consts.TWITTER_CONSUMER_SECRET,
                Consts.TWITTER_ACCESS_TOKEN,
                Consts.TWITTER_ACCESS_TOKEN_SECRET
                );

        }

        public void publishTweet(String tweetContent)
        {
            initTwitter();
            Tweet.PublishTweet(tweetContent);
        }


    }
}