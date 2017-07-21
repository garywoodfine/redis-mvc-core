using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RedisConfiguration;

namespace RedisExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly RedisConfigurationOptions _redis;
        public HomeController(IOptions<RedisConfigurationOptions> redis)
        {
            _redis = redis.Value;
        }
        public IActionResult Index()
        {
            ViewData["Host"] =_redis.Host;
            ViewData["Port"] = _redis.Port;
            ViewData["Name"] = _redis.Name;

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
