using AnimalStore.Data.Repositories;
using AnimalStore.Model;
using AnimalStore.Web.API.Helpers;
using AnimalStore.Web.API.Strategies;
using AnimalStore.Web.API.Wrappers;
using NUnit.Framework;
using Rhino.Mocks;
using System.Collections.Generic;
using System.Linq;

namespace AnimalStore.Services.UnitTests
{
    [TestFixture]
    public class DogSearchHelperTests
    {
        public DogSearchHelperTests ()
	    {
	    }

        //[Test]        
        //public void GetDogsList_filters_dogs_by_breed()
        //{
        //    // act
        //    _dogSearchHelper.GetDogsList(1, null);

        //    // assert
        //    _dogBreedFilterStrategy.AssertWasCalled(x => x.Filter(1));
        //}


        //[Test]
        //public void ApplyDogLocationAndSortFiltering_filters_dogs_by_place()
        //{
        //    // act
        //    _dogSearchHelper.ApplyDogLocationAndSortFiltering(_dogs, 1, _sortColumn, 1);

        //    // assert
        //    _dogLocationFilterStrategy.AssertWasCalled(x => x.Filter(_dogs, 1));
        //}

        //[Test]
        //public void ApplyDogLocationAndSortFiltering_calls_sorting_strategy_when_not_filtering_on_place()
        //{
        //    // act
        //    _dogSearchHelper.ApplyDogLocationAndSortFiltering(_dogs, 1, _sortColumn, 0);

        //    // assert
        //    _dogCategoryFilterStrategy.AssertWasCalled(x => x.Sort(_dogs, _sortColumn));
        //}

        //[Test]
        //public void ApplyDogLocationAndSortFiltering_calls_sorting_strategy_when_filtering_on_place()
        //{
        //    // act
        //    _dogSearchHelper.ApplyDogLocationAndSortFiltering(_dogs, 1, _sortColumn, 1);

        //    // assert
        //    _dogLocationFilterStrategy.AssertWasCalled(x => x.Sort(Arg<IEnumerable<Dog>>.Is.Anything));
        //}
    }
}
