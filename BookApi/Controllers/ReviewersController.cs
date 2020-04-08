using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApi.Dtos;
using BookApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewersController : ControllerBase
    {
        private IReviewerRepository reviewerRepository { get; set; }
        private IReviewRepository reviewRepository { get; set; }

        public ReviewersController(IReviewerRepository _reviewerRepository, IReviewRepository _reviewRepository)
        {
            reviewerRepository = _reviewerRepository;
            reviewRepository = _reviewRepository;
        }

        // api/Reviewers
        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewerDto>))]
        public IActionResult GetReviewers()
        {
            var reviewers = reviewerRepository.GetReviewers().ToList();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewerDtos = new List<ReviewerDto>();

            foreach (var reviewer in reviewers)
            {
                reviewerDtos.Add(new ReviewerDto
                {
                    Id = reviewer.Id,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName
                });
            }

            return Ok(reviewerDtos);
        }

        [HttpGet("{reviewerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(ReviewerDto))]
        public IActionResult GetReviewer(int reviewerId)
        {
            if (!reviewerRepository.IsReviewerIdExist(reviewerId))
                return NotFound();

            var reviewer = reviewerRepository.GetReviewer(reviewerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewerDto = new ReviewerDto()
            {
                Id = reviewer.Id,
                FirstName = reviewer.FirstName,
                LastName = reviewer.LastName
            };

            return Ok(reviewerDto);
        }


        [HttpGet("{reviewId}/reviewer")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(ReviewerDto))]
        public IActionResult GetReviewerFromReviews(int reviewId)
        {
            if (!reviewRepository.ReviewIdExist(reviewId))
                return NotFound();

            var reviewer = reviewerRepository.GetReviewerFromReviews(reviewId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewerDto = new ReviewerDto()
            {
                Id = reviewer.Id,
                FirstName = reviewer.FirstName,
                LastName = reviewer.LastName
            };

            return Ok(reviewerDto);
        }

        //Reviews
        [HttpGet("{reviewerId}/reviews")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
        public IActionResult GetReviewsFromReviewer(int reviewerId)
        {
            if (!reviewerRepository.IsReviewerIdExist(reviewerId))
                return NotFound();

            var reviews = reviewerRepository.GetReviewsFromReviewer(reviewerId).ToList();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewDtos = new List<ReviewDto>();

            foreach (var review in reviews)
            {
                reviewDtos.Add(new ReviewDto
                {
                    Id = review.Id,
                    HeadLine = review.HeadLine,
                    Rating = review.Rating,
                    ReviewText = review.ReviewText
                });
            }

            return Ok(reviewDtos);
        }
    }
}