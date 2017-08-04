using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using RedisConfig;

namespace RedisExample.Controllers
{
    using Microsoft.Net.Http.Headers;

    using RedisExample.Models;

    public class HomeController : Controller
    {
        private readonly RedisConfiguration _redis;
        private readonly IDistributedCache _cache;
        private readonly IRedisConnectionFactory _fact;
        public HomeController(IOptions<RedisConfiguration> redis, IDistributedCache cache,IRedisConnectionFactory factory)
        {   
            _redis = redis.Value;
            _cache = cache;
            _fact = factory;
        }
        public IActionResult Index()
        {
            var helloRedis = Encoding.UTF8.GetBytes("Hello Redis");
            HttpContext.Session.Set("hellokey", helloRedis);

            var getHello = default(byte[]);
            HttpContext.Session.TryGetValue("hellokey", out getHello);
            ViewData["Hello"] = Encoding.UTF8.GetString(getHello);
            ViewData["SessionID"] = HttpContext.Session.Id;
           
            _cache.SetString("CacheTest", "Redis is awesome");

            ViewData["Host"] =_redis.Host;
            ViewData["Port"] = _redis.Port;
            ViewData["Name"] = _redis.Name;


            ViewData["DistCache"] = _cache.GetString("CacheTest");

            var db = _fact.Connection().GetDatabase();
            db.StringSet("StackExchange.Redis.Key", "Stack Exchange Redis is Awesome");
            ViewData["StackExchange"] = db.StringGet("StackExchange.Redis.Key");

            return View();
        }

        public PartialViewResult Vote (string value)
        {
            var redis = new RedisVoteService<Vote>(this._fact);
            var theVote = new Vote();
            switch (value)
            {
                case "Y":
                    theVote.Yes = 1;
                    break;
                case "N":
                    theVote.No = 1;
                    break;
                case "U":
                    theVote.Undecided = 1;
                    break;
                default: break;
            }
            redis.Save("RedisVote", theVote);

            var model = redis.Get("RedisVote");
           return this.PartialView("~/Views/Home/Vote.cshtml", model);
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
