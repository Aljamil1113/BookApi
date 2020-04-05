using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApi.Model;

namespace BookApi.Services
{
    public class CountryRepository : ICountryRepository
    {
        private BookDbContext countryContext;

        public CountryRepository(BookDbContext _countryContext)
        {
            countryContext = _countryContext;
        }

        public bool CountryExist(int countryId)
        {
            return countryContext.Countries.Any(c => c.Id == countryId);
        }

        public ICollection<Author> GetAuthorsFromCountry(int countryId)
        {
            return countryContext.Authors.Where(c => c.Id == countryId).ToList();
        }

        public ICollection<Country> GetCountries()
        {
            return countryContext.Countries.OrderBy(c => c.Name).ToList();
        }

        public Country GetCountry(int countryId)
        {
            return countryContext.Countries.Where(c => c.Id == countryId).FirstOrDefault();
        }

        public Country GetCountryOfAnAuthor(int authorId)
        {
            return countryContext.Authors.Where(a => a.Id == authorId).Select(c => c.Country).FirstOrDefault();
        }
    }
}
