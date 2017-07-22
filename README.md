# Redis with ASP.net MVC Core Web Application

This is an example application illustrating different methods to integrate Redis into ASP.net MVC core web application.

It is supplementary code for my [Redis InMemory Cache in ASP.net MVC Core](https://garywoodfine.com/redis-inmemory-c…asp-net-mvc-core/) blog post tutorial.

This application is developed using Microsoft .net core so you can run on it on any operating system of your choice as long as you have .net core installed.

It makes use of [Redis an open source](https://redis.io/) , in-memory data structure store, used as a database, cache and message broker. It supports data structures such as strings, hashes, lists, sets, sorted sets with range queries, bitmaps, hyperloglogs and geospatial indexes with radius queries. Redis has built-in replication, Lua scripting, LRU eviction, transactions and different levels of on-disk persistence, and provides high availability via Redis Sentinel and automatic partitioning with Redis Cluster.   

Check out the official [Github repo](https://github.com/antirez/redis)

This application will need Redis installed on your computer in order for it too work

## Redis Installation

There are anumber of ways to install Redis depending on your operating system or development environment. 

### Windows

The Microsoft Open Tech group develops and maintains Windows port targeting Win64 available. 
Download the stable release
[MicrosoftArchive/Redis](https://github.com/MicrosoftArchive/redis/releases)

### Linux (Ubuntu/Debian)

On a Ubuntu Desktop Redis can be installed via the apt repository
```bash
sudo apt install redis-server
```

### Docker

Check out the official [Redis repository on the Docker Store](https://store.docker.com/images/redis)

```bash
docker pull redis 
docker run --name some-redis -d redis
```
