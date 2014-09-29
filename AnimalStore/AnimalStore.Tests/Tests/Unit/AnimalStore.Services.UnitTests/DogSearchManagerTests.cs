using System.Collections.Generic;
using AnimalStore.Common.Configuration;
using AnimalStore.Model;
using AnimalStore.Web.API.Strategies;
using NUnit.Framework;
using Rhino.Mocks;
using System.Linq;

namespace AnimalStore.Services.UnitTests
{
	using Web.API.Services;

	[TestFixture]
		public class DogSearchManagerTests
		{
				private readonly IDogBreedFilterStrategy _dogBreedFilterStrategy;
				private readonly IDogCategoryFilterStrategy _dogCategoryFilterStrategy;
				private readonly IDogCategoryService _dogCategoryService;
				private readonly IDogLocationFilter _dogLocationFilterStrategy;
				private readonly IConfiguration _configuration;
				private readonly DogSearchResultsListBuilder _dogSearchResultsListBuilder;
				private const string _sortColumn = "Name";
				private DogSearchService _dogSearchManager;

				public DogSearchManagerTests ()
				{
							_dogBreedFilterStrategy = MockRepository.GenerateMock<IDogBreedFilterStrategy>();
							_dogCategoryFilterStrategy = MockRepository.GenerateMock<IDogCategoryFilterStrategy>();
							_dogCategoryService = MockRepository.GenerateMock<IDogCategoryService>();
							_dogLocationFilterStrategy = MockRepository.GenerateMock<IDogLocationFilter>();
							_configuration = MockRepository.GenerateMock<IConfiguration>();
							_dogSearchResultsListBuilder = new DogSearchResultsListBuilder();
				}

				[Test]
				public void GetDogsByBreed_filters_dogs_by_breed()
				{
						// arrange
						var dogBreedFilterStrategy = MockRepository.GenerateMock<IDogBreedFilterStrategy>();
						var dogSearchResultsListBuilder = new DogSearchResultsListBuilder();

						dogBreedFilterStrategy.Stub(x => x.Filter(Arg<int>.Is.Anything)).Return(
								dogSearchResultsListBuilder.ListOf14Beagels().Build().AsQueryable()
								);

						var dogSearchManager = new DogSearchService(dogBreedFilterStrategy
								, _dogCategoryFilterStrategy
								, _dogCategoryService
								, _dogLocationFilterStrategy
								, _configuration);

						// act
						var result = dogSearchManager.GetDogsByBreed(1);

						// assert
						dogBreedFilterStrategy.AssertWasCalled(x => x.Filter(1));
						Assert.That(result.Count(), Is.EqualTo(14));
				}

				[Test]
				public void ApplyDogLocationFilteringAndSorting_filters_dogs_by_place()
				{
						// arrange
						const int breedId = 3;
						const int placeId = 1;

						var dogBreedFilterStrategy = MockRepository.GenerateMock<IDogBreedFilterStrategy>();
						var dogSearchResultsListBuilder = new DogSearchResultsListBuilder();
						var dogs = dogSearchResultsListBuilder.ListOf14Beagels().Build().AsQueryable();
						dogBreedFilterStrategy.Stub(x => x.Filter(breedId)).Return(dogs);

						_dogCategoryService.Stub(x => x.AddDogsInSameCategoryToDogsCollection(dogs, breedId)).Return(dogs);
						_dogLocationFilterStrategy.Stub(x => x.Filter(dogs, placeId)).Return(dogs);

						var dogSearchManager = new DogSearchService(dogBreedFilterStrategy
								, _dogCategoryFilterStrategy
								, _dogCategoryService
								, _dogLocationFilterStrategy
								, _configuration);

						// act
						dogSearchManager.ApplyDogLocationFilteringAndSorting(dogs, breedId, _sortColumn, placeId);

						// assert
						_dogLocationFilterStrategy.AssertWasCalled(x => x.Filter(dogs, placeId));
				}

				[Test]
				public void ApplyDogLocationFilteringAndSorting_calls_sorting_strategy_when_not_filtering_on_place()
				{
						// arrange
						const int breedId = 1;

						_dogBreedFilterStrategy.Stub(x => x.Filter(Arg<int>.Is.Anything)).Return(
								_dogSearchResultsListBuilder.ListOf14Beagels().Build().AsQueryable()
								);

						var dogs = _dogSearchResultsListBuilder.ListWith30Dogs().Build().AsQueryable();
						_dogCategoryService.Stub(x => x.AddDogsInSameCategoryToDogsCollection(dogs, breedId)).Return(dogs);

						_dogSearchManager = new DogSearchService(_dogBreedFilterStrategy
								, _dogCategoryFilterStrategy
								, _dogCategoryService
								, _dogLocationFilterStrategy
								, _configuration);


						// act
						_dogSearchManager.ApplyDogLocationFilteringAndSorting(dogs, breedId, _sortColumn, 0);

						// assert
						_dogCategoryFilterStrategy.AssertWasCalled(x => x.Sort(dogs, _sortColumn));
				}

				[Test]
				public void ApplyDogLocationFilteringAndSorting_calls_sorting_strategy_when_filtering_on_place()
				{
						// arrange
						const int placeId = 1;
						const int breedId = 4;

						var dogs = _dogSearchResultsListBuilder.ListOf3DalmatiansByCategory(1, breedId).Build().AsQueryable();

						_dogBreedFilterStrategy.Stub(x => x.Filter(breedId)).Return(dogs);
						_dogCategoryService.Stub(x => x.AddDogsInSameCategoryToDogsCollection(dogs, breedId)).Return(dogs);
						_dogLocationFilterStrategy.Stub(x => x.Filter(dogs, placeId)).Return(dogs);

						_dogSearchManager = new DogSearchService(_dogBreedFilterStrategy
								, _dogCategoryFilterStrategy
								, _dogCategoryService
								, _dogLocationFilterStrategy
								, _configuration);

						// act
						_dogSearchManager.ApplyDogLocationFilteringAndSorting(dogs, breedId, null, placeId);

						// assert
						_dogLocationFilterStrategy.AssertWasCalled(x => x.Sort(Arg<IEnumerable<Dog>>.Is.Anything));
				}
		}
}
