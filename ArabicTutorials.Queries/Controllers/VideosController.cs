using System.Collections.Generic;
using ArabicTutorials.Common;
using ArabicTutorials.Data.Infrastructure;
using ArabicTutorials.Data.Models;
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
        private readonly IRepository<Video> _repository;

        public VideosController(ILogger logger, IRepository<Video> repository)
        {
            _logger = logger;
            _repository = repository;
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
