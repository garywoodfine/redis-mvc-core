using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

using RedisConfig;

namespace RedisExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly RedisConfiguration _redis;
        private readonly IDistributedCache _cache;
        public HomeController(IOptions<RedisConfiguration> redis, IDistributedCache cache)
        {   
            _redis = redis.Value;
            _cache = cache;
        }
        public IActionResult Index()
        {
            var helloRedis = Encoding.UTF8.GetBytes("Hello Redis");
            HttpContext.Session.Set("hellokey", helloRedis);

            var getHello = default(byte[]);
            HttpContext.Session.TryGetValue("hellokey", out getHello);
            ViewData["Hello"] = Encoding.UTF8.GetString(getHello);
            ViewData["SessionID"] = HttpContext.Session.Id;
           
            _cache.SetString("CacheTest", "Gary is awesome");

            ViewData["Host"] =_redis.Host;
            ViewData["Port"] = _redis.Port;
            ViewData["Name"] = _redis.Name;


            ViewData["DistCache"] = _cache.GetString("CacheTest");


            return View();
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
