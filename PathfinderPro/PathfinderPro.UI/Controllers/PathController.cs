using PathfinderPro.Business;
using PathfinderPro.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Http;

namespace PathfinderPro.UI.Controllers
{
    public class PathController : ApiController
    {
        private readonly IGraphService _graphService;
        private readonly IPathfinderService _pathfinderService;
        private List<Node> _graph;

        public PathController(IGraphService graphService, IPathfinderService pathfinderService)
        {
            _graphService = graphService;
            _pathfinderService = pathfinderService;

            var server = HttpContext.Current.Server;
            string dataFilePath = server.MapPath("~/bin/Data/GraphData.json");
            _graph = _graphService.GetGraph(dataFilePath);
        }

        public IHttpActionResult Get(string from, string to)
        {
            if (string.IsNullOrWhiteSpace(from) || string.IsNullOrWhiteSpace(to))
            {
                return BadRequest("Source and destination nodes cannot be empty.");
            }

            var bestPath = _pathfinderService.ShortestPath(from, to, _graph);
            return Ok(bestPath);
        }

        public IHttpActionResult GetNodeList()
        {
            var nodesInGraph = _graphService.GetNodesInGraph();
            return Ok(nodesInGraph);
        }


    }
}