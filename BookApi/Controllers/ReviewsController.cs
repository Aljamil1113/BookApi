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
    public class ReviewsController : ControllerBase
    {
        private IReviewRepository reviewRepository { get; set; }

        public ReviewsController(IReviewRepository _reviewRepository)
        {
            reviewRepository = _reviewRepository;
        }

        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
        public IActionResult GetReviews()
        {
            var reviews = reviewRepository.GetReviews().ToList();

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

        [HttpGet("{reviewId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(ReviewDto))]
        public IActionResult GetReview(int reviewId)
        {
            if (!reviewRepository.ReviewIdExist(reviewId))
                return NotFound();

            var review = reviewRepository.GetReview(reviewId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewDto = new ReviewDto()
            {
                Id = review.Id,
                HeadLine = review.HeadLine,
                Rating = review.Rating,
                ReviewText = review.ReviewText
            };

            return Ok(reviewDto);
        }

        [HttpGet("books/{bookId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
        public IActionResult GetReviewsFromBook(int bookId)
        {
            
            var reviews = reviewRepository.GetReviewsFromBook(bookId).ToList();

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


        [HttpGet("reviewers/{reviewerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
        public IActionResult GetReviewsFromReviewer(int reviewerId)
        {

            var reviews = reviewRepository.GetReviewsFromReviewer(reviewerId).ToList();

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


        //Book
        [HttpGet("{reviewId}/book")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(BookDto))]
        public IActionResult GetBookFromReview(int reviewId)
        {
            if(!reviewRepository.ReviewIdExist(reviewId))
                return Ok();

            var book = reviewRepository.GetBookFromReview(reviewId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bookDto = new BookDto()
            {
                Id = book.Id,
                Isbn = book.Isbn,
                DatePublish = book.DatePublish,
                Title = book.Title
            };

            return Ok(bookDto);
        }

        //Reviewer
        [HttpGet("{reviewId}/reviewer")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(BookDto))]
        public IActionResult GetReviewerFromReviews(int reviewId)
        {
            if (!reviewRepository.ReviewIdExist(reviewId))
                return NotFound();

            var reviewer = reviewRepository.GetReviewerFromReview(reviewId);

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
    }
}