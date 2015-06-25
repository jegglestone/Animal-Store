namespace AnimalStore.Services.UnitTests
{
  using System.Configuration;
  using Common.Configuration;
  using Common.Constants;
  using Data.Repositories.Animals;
  using Data.Repositories.Places;
  using Model.Settings;
  using Web.API.Controllers;
  using NUnit.Framework;
  using Rhino.Mocks;
  using System;
  using System.Linq;
  using Data.UnitsOfWork;
  using Model;
  using Web.API.Services;

  [TestFixture]
  public class DogsControllerTests
  {
    private IRepository<Dog> _dogsRepository;
    private IRepository<Breed> _breedsRepository;
    private IPlacesRepository _placesRepository;
    private IDogSearchService _dogSearchhelper;
    private IUnitOfWork _unitofWork;
    private DogsController _dogsController;
    private IConfiguration _configuration;

    [TestFixtureSetUp]
    public void DogsControllerTestsSetup()
    {
      _dogsRepository = MockRepository.GenerateMock<IRepository<Dog>>();
      _breedsRepository = MockRepository.GenerateMock<IRepository<Breed>>();
      _placesRepository = MockRepository.GenerateMock<IPlacesRepository>();
      _unitofWork = MockRepository.GenerateMock<IUnitOfWork>();
      _dogSearchhelper = MockRepository.GenerateMock<IDogSearchService>();
      _configuration = MockRepository.GenerateMock<IConfiguration>();

      _breedsRepository.Stub(x => x.GetById(Arg<int>.Is.Anything)).Return(
        new Breed {Name = "Beagel"});

      _configuration.Stub(x => x.GetNationwideSearchResultsDescriptionMessageForAllBreeds())
        .Return("Search results {0} to {1} out of {2} results for all breeds nationwide.");
      _configuration.Stub(x => x.GetNationwideSearchResultsDescriptionMessageForSpecificBreed())
        .Return("Showing results {0} to {1} out of {2} results for {3} nationwide");
      _configuration.Stub(x => x.GetLocalSearchResultsDescriptionMessageForAllBreeds())
        .Return("Search results {0} to {1} out of {2} results for all breeds in {3}");

      _dogsController = new DogsController(_dogsRepository, _breedsRepository, _unitofWork, _dogSearchhelper, _configuration, _placesRepository);

      StubDogsRepository();
    }

    private void StubDogsRepository()
    {
      _dogsRepository.Stub(x => x.GetById(4)).Return(new Dog() {Name = "dog", Id = 4});
      _dogsRepository.Stub(x => x.GetAll()).Return(
        new DogSearchResultsListBuilder().ListWith30Dogs().Build().AsQueryable());
    }

    [Test]
    public void Get_Paged_With_Breed_Returns_Correct_Search_Description()
    {
      // arrange
      const int breedId = 3;
      const int page = 2;
      const int pageSize = 5;

      var dogsList = new DogSearchResultsListBuilder().ListOf14Beagels().Build();

      _dogSearchhelper.Stub(x => x.GetDogsByBreed(breedId)).Return(dogsList);

      _dogSearchhelper.Stub(x => x.ApplyDogLocationFilteringAndSorting(Arg<IQueryable<Dog>>.Is.Anything, Arg<int>.Is.Anything,
        Arg<string>.Is.Anything, Arg<int>.Is.Anything)).Return(dogsList);

      var dogsController = new DogsController(_dogsRepository, _breedsRepository, _unitofWork, _dogSearchhelper, _configuration, _placesRepository);

      //act
      var result = dogsController.GetPagedByBreed(page, pageSize, breedId, SearchSortOptions.PRICE_HIGHEST);

      Assert.That(result.SearchDescription, Is.EqualTo("Showing results 6 to 10 out of 14 results for Beagel nationwide"));
    }

    [Test]
    public void Get_Paged_With_Place_Returns_Dogs_matching_Place()
    {
      // arrange
      const int placeId = 3;
      const string placeName = "Wallsend";
      const int page = 1;
      const int pageSize = 5;

      _dogsRepository.Stub(x => x.GetAll()).Return(
        new DogSearchResultsListBuilder().ListWith30Dogs().Build().AsQueryable());

      ConfigurationManager.AppSettings[AppSettingKeys.BaseUrlPagedDogs] 
        = "localhost";

      var dogsController = new DogsController(
        _dogsRepository, 
        _breedsRepository, 
        _unitofWork, 
        _dogSearchhelper, 
        _configuration, 
        _placesRepository);

      _placesRepository.Stub(x => x.GetById(placeId)).Return(
        new Place
        {
          Name = placeName
        });

      //act
      var result = dogsController.GetPaged(page, pageSize, placeId);

      Assert.That(result.TotalCount.Equals(3));
      Assert.That(result.SearchDescription, Is.EqualTo(
        string.Format("Search results 1 to 3 out of 3 results for all breeds in {0}", 
          placeName)));
    }

    [Test]
    public void Get_CallRepositoryGetAllMethod()
    {
      // act
      _dogsController.GetPaged();

      // assert
      _dogsRepository.AssertWasCalled(X => X.GetAll());
    }

    [Test]
    public void Get_ReturnsUpTo25Items_WithNoPageLimitSpecified()
    {
      // act
      var result = _dogsController.GetPaged();

      // assert
      Assert.That(result.Data.Count(), Is.EqualTo(25));
    }

    [Test]
    public void Get_ReturnsSpecifiedNumberOfResultSetWhenPaging()
    {
      // act
      var result = _dogsController.GetPaged(1, 10);

      // assert
      Assert.That(result.Data.Count(), Is.EqualTo(10));
    }

    [TestCase(1, 10, "Flossie", Description = "we expect the first page to have this dog")]
    [TestCase(2, 10, "Rex", Description = "we expect the second page to have this dog")]
    [TestCase(3, 10, "Tip", Description = "we expect the third page to have this dog")]
    public void get_ReturnsTheSpecifiedPage(int pageNumber, int pageSize, string expectedDogName)
    {
      // act
      var result = _dogsController.GetPaged(pageNumber, pageSize);

      // assert
      Assert.That(result.Data.ToList().First(dog => dog.Name == expectedDogName), Is.Not.Null);
    }

    [Test]
    public void GetPaged_ReturnsTheCorrectTotalCount()
    {
      // act
      var result = _dogsController.GetPaged(1, 10);

      // assert
      Assert.That(result.TotalCount, Is.EqualTo(30));
    }

    [Test]
    public void GetPaged_ReturnsTheCorrectPageCount()
    {
      // act
      var result = _dogsController.GetPaged(1, 9);

      // assert
      Assert.That(result.TotalPages, Is.EqualTo(4));
    }

    [Test]
    public void GetPaged_ReturnsTheCurrentPage()
    {
      // arrange
      const int page = 2;

      // act
      var result = _dogsController.GetPaged(page, 4);

      // assert
      Assert.That(result.CurrentPageNumber, Is.EqualTo(page));
    }

    [Test]
    public void GetPaged_ReturnsTheCorrectNextPageUrl()
    {
      // act
      var result = _dogsController.GetPaged(1, 10);

      // assert
      Assert.That(result.NextPage.Contains("?page=2"));
    }

    [Test]
    public void GetPaged_ReturnsTheCorrectPrevPageUrl()
    {
      // act
      var result = _dogsController.GetPaged(2, 10);

      // assert
      Assert.That(result.PrevPage.Contains("?page=1"));
    }

    [Test]
    public void GetPaged_ReturnsItemsOrderedByDateCreatedDescending()
    {
      // act
      var result = _dogsController.GetPaged(0, 20);

      // assert
      Assert.That(result.Data.ToList()[0].CreatedOn, Is.EqualTo(DateTime.Today));
    }

    [Test]
    public void Get_ById_ReturnsSingleItemWithMatchingIdAndCallsRepositoryGetById()
    {
      // act
      var result = _dogsController.Get(4);

      // assert
      _dogsRepository.AssertWasCalled(x => x.GetById(4));
      Assert.That(result.Id, Is.EqualTo(4));
    }

    [TearDown]
    public void TearDown()
    {
      _dogsRepository.Dispose();
      _unitofWork.Dispose();
    }
  }
}