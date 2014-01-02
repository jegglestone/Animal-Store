using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using AnimalStore.Common.Helpers;
using AnimalStore.Model;
using AnimalStore.Web.Wrappers.Interfaces;
using AnimalStore.Web.Helpers.Interfaces;
using AnimalStore.Web.Repository;
using NUnit.Framework;
using Rhino.Mocks;

namespace AnimalStore.Web.UnitTests.Repositories
{
    [TestFixture]
    public class SearchAPIFacadeTests
    {
        IExceptionHelper _exceptionHandler;
        IWebAPIRequestWrapper _webAPIRequestWrapper;
        IConfiguration _configMgr;
        IResponseStreamHelper _responseStreamHelper;

        private readonly List<Breed> _breedsList = new List<Breed>()
        {
            new Breed() { Id = 1, Name = "Dalmatian" },
            new Breed() { Id = 2, Name = "Afghan Hound" },
            new Breed() { Id = 3, Name = "Rottweiler" },
            new Breed() { Id = 4, Name = "Whippet" },
            new Breed() { Id = 5, Name = "Blood Hound" },
        };

        private readonly List<Dog> _dogsList = new List<Dog>()
        {
            new Dog() { Id = 1, AgeInYears = 2, isSold = false},
            new Dog() { Id = 2, AgeInYears = 5, isSold = false},
        };

        [TestFixtureSetUp]
        public void SetupTests()
        {
            _exceptionHandler = MockRepository.GenerateMock<IExceptionHelper>();
            _webAPIRequestWrapper = MockRepository.GenerateMock<IWebAPIRequestWrapper>();
            _configMgr = MockRepository.GenerateMock<IConfiguration>();
            _responseStreamHelper = MockRepository.GenerateMock<IResponseStreamHelper>();
            _configMgr.Stub(x => x.GetWebAPIUrl()).Return("http://www.someAPI.com");
        }

        [Test]
        public void GetBreeds_Returns_List_Breeds()
        {
            // arrange         
            var stubJsonSerializerWrapper = MockRepository.GenerateMock<IDataContractJsonSerializerWrapper>();
            stubJsonSerializerWrapper.Stub(x => x.ReadObject(Arg<Stream>.Is.Anything, Arg<DataContractJsonSerializer>.Is.Anything)).Return(_breedsList);

            var searchRepository = new SearchAPIFacade(stubJsonSerializerWrapper, _exceptionHandler, _configMgr,
                _webAPIRequestWrapper, _responseStreamHelper);

            // act
            var result = searchRepository.GetBreeds().ToList();

            // assert
            Assert.That(result.First().Id == 1 && result.First().Name == "Dalmatian");
            Assert.That(result.Count == _breedsList.Count);
        }

        [Test]
        public void GetBreeds_Returns_EmptyList_When_No_Data_Returned()
        {
            // arrange         
            var stubJsonSerializerWrapper = MockRepository.GenerateMock<IDataContractJsonSerializerWrapper>();
            stubJsonSerializerWrapper.Stub(x => x.ReadObject(Arg<Stream>.Is.Anything, Arg<DataContractJsonSerializer>.Is.Anything)).Return(null);

            var searchRepository = new SearchAPIFacade(stubJsonSerializerWrapper, _exceptionHandler, _configMgr,
                _webAPIRequestWrapper, _responseStreamHelper);

            // act
            var result = searchRepository.GetBreeds().ToList();

            // assert
            Assert.That(result.Count == 0);
        }

        [Test]
        public void GetDogs_Returns_PageableResult_Dogs()
        {
            // arrange         
            var stubJsonSerializerWrapper = MockRepository.GenerateMock<IDataContractJsonSerializerWrapper>();
            stubJsonSerializerWrapper.Stub(x => x.ReadObject(Arg<Stream>.Is.Anything, Arg<DataContractJsonSerializer>.Is.Anything)).Return(
                new PageableResults<Dog>() { Data = _dogsList });

            var searchRepository = new SearchAPIFacade(stubJsonSerializerWrapper, _exceptionHandler, _configMgr,
                _webAPIRequestWrapper, _responseStreamHelper);

            // act
            var result = searchRepository.GetDogs(1, 20);

            // assert
            Assert.That(result.Data.Count() == _dogsList.Count);
        }

        [Test]
        public void GetDogs_With_Breed_Returns_PageableResult_Dogs()
        {
            // arrange         
            var stubJsonSerializerWrapper = MockRepository.GenerateMock<IDataContractJsonSerializerWrapper>();
            stubJsonSerializerWrapper.Stub(x => x.ReadObject(Arg<Stream>.Is.Anything, Arg<DataContractJsonSerializer>.Is.Anything)).Return(
                new PageableResults<Dog>() { Data = _dogsList });

            var searchRepository = new SearchAPIFacade(stubJsonSerializerWrapper, _exceptionHandler, _configMgr,
                _webAPIRequestWrapper, _responseStreamHelper);

            // act
            var result = searchRepository.GetDogs(1, 20, 4, "dalmatian");

            // assert
            Assert.That(result.Data.Count() == _dogsList.Count);
        }
    }
}
