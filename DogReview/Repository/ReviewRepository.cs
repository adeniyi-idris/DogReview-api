using AutoMapper;
using DogReview.Data;
using DogReview.Interfaces;
using DogReview.Models;

namespace DogReview.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ReviewRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool CreateReview(Review review)
        {
            _context.reviews.Add(review);
            return Saved();
        }

        public bool DeleteReview(Review review)
        {
            _context.reviews.Remove(review);
            return Saved();
        }

        public bool DeleteReviews(List<Review> reviews)// to delete a range of values
        {
            _context.RemoveRange(reviews);
            return Saved();
        }

        public Review GetReview(int ReviewId)
        {
            return _context.reviews.Where(r => r.Id == ReviewId).FirstOrDefault();
        }

        public ICollection<Review> GetReviews()
        {
            return _context.reviews.ToList();
        }

        public ICollection<Review> GetReviewsOfAPokemon(int pokeId)
        {
            return _context.reviews.Where(r => r.Pokemon.Id == pokeId).ToList(); //one to many relationship
        }

        public bool ReviewExists(int reviewId)
        {
            return _context.reviews.Any(r => r.Id == reviewId);
        }

        public bool Saved()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateReview(Review review)
        {
            _context.reviews.Update(review);
            return Saved();
        }
    }
}
