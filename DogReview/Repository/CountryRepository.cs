using DogReview.Data;
using DogReview.Interfaces;
using DogReview.Models;

namespace DogReview.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;

        public CountryRepository(DataContext context)
        {
            _context = context;
        }
        public bool CountryExist(int id)
        {
            return _context.countries.Any(c => c.Id == id);
        }

        public bool CreateCountry(Country country)
        {
            _context.countries.Add(country);
            return Save();
        }

        public bool DeleteCountry(Country country)
        {
            _context.countries.Remove(country);
            return Save();
        }

        public ICollection<Country> GetCountries()
        {
            return _context.countries.ToList();
            // return _context.countries.OrdeBy(c => c.Id).ToList(); this is used for long list
        }

        public Country GetCountry(int id)
        {
            return _context.countries.Where(c => c.Id == id).FirstOrDefault();
        }

        public Country GetCountryOfOwner(int ownerId)
        {
            return _context.owners.Where(o => o.Id == ownerId).Select(c => c.Country).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnerByCountry(int countryId)
        {
            return _context.owners.Where(o => o.Country.Id == countryId).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCountry(Country country)
        {
            _context.countries.Update(country);
            return Save();
        }
    }
}
