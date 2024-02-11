using PathfinderPro.Bussiness.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PathfinderPro.UI.Controllers
{
    public class PathController : ApiController
    {
        private readonly IGraphService _graphService;
        private readonly IPathfinderService _pathfinderService;

        public PathController(IGraphService graphService, IPathfinderService pathfinderService)
        {
            _graphService = graphService;
            _pathfinderService = pathfinderService;
        }

        // GET api/<controller>
        public IHttpActionResult Get(string from,string to)
        {
            var graph = _graphService.BuildGraph();
            var bestPath = _pathfinderService.ShortestPath(from, to, graph);
            return Ok(bestPath);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}