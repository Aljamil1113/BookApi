using BookApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Services
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();
        Review GetReview(int reviewId);

        ICollection<Review> GetReviewsFromReviewer(int reviewerId);
        Reviewer GetReviewerFromReview(int reviewId);

        ICollection<Review> GetReviewsFromBook(int bookId);
        Book GetBookFromReview(int reviewId);

        bool ReviewIdExist(int reviewId);
    }
}
