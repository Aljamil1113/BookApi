using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApi.Model;

namespace BookApi.Services
{
    public class ReviewerRepository : IReviewerRepository
    {
        private BookDbContext ReviewerContext;

        public ReviewerRepository(BookDbContext _ReviewerContext)
        {
            ReviewerContext = _ReviewerContext;
        }

        public Reviewer GetReviewer(int reviewerId)
        {
            return ReviewerContext.Reviewers.Where(re => re.Id == reviewerId).FirstOrDefault();
        }

        public Reviewer GetReviewerFromReviews(int reviewId)
        {
            return ReviewerContext.Reviews.Where(r => r.Id == reviewId).Select(re => re.Reviewer).FirstOrDefault();         
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return ReviewerContext.Reviewers.OrderBy(re => re.LastName).ToList();
        }

        public ICollection<Review> GetReviewsFromReviewer(int reviewerId)
        {
            return ReviewerContext.Reviewers.Where(re => re.Id == reviewerId).SelectMany(r => r.Reviews).ToList();
        }

        public bool IsReviewerIdExist(int reviwerId)
        {
            return ReviewerContext.Reviewers.Any(re => re.Id == reviwerId);
        }
    }
}
