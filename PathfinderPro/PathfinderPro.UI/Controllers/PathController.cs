using PathfinderPro.Bussiness.Interfaces;
using System.Web;
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
        public IHttpActionResult Get(string from, string to)
        {
            if (string.IsNullOrWhiteSpace(from) || string.IsNullOrWhiteSpace(to))
            {
                return BadRequest("Source and destination nodes cannot be empty.");
            }

            var server = HttpContext.Current.Server;
            string dataFilePath = server.MapPath("~/bin/Data/GraphData.json");
            var graph = _graphService.BuildGraph(dataFilePath);
            var bestPath = _pathfinderService.ShortestPath(from, to, graph);
            return Ok(bestPath);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }


    }
}