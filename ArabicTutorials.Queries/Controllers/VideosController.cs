using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArabicTutorials.Common;
using ArabicTutorials.Queries.Projections;
using Microsoft.AspNetCore.Mvc;

namespace ArabicTutorials.Queries.Controllers
{
    //IConferenceCommandService
    //IConferenceQueryService
    [Route("api/[controller]")]
    public class VideosController : Controller
    {
        private ILogger _logger;

        public VideosController(ILogger logger)
        {
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public IEnumerable<VideoProjection> GetAll()
        {
            return new[] {
                new VideoProjection
            {
                Name="Video 1"
            }, new VideoProjection
            {
                Name="Video 2"
            }};
        }
    }
}
