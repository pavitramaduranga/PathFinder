using PathfinderPro.Bussiness.Interfaces;
using System.Web.Mvc;

namespace PathfinderPro.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGraphService _graphService;
        private readonly IPathfinderService _pathfinderService;

        public HomeController(IGraphService graphService, IPathfinderService pathfinderService)
        {
            _graphService = graphService;
            _pathfinderService = pathfinderService;
        }

        public ActionResult Index()
        {
            var graph = _graphService.BuildGraph();
            _pathfinderService.ShortestPath("A", "E", graph);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}