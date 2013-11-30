using AnimalStore.Model;
using System.Data.Entity;

namespace AnimalStore.Data.Configuration
{
    public class CustomDatabaseInitialiser : 
        DropCreateDatabaseIfModelChanges<DataContext.AnimalsDataContext>
    {
        protected override void Seed(DataContext.AnimalsDataContext context)
        {
             #region "Add some dog story data to database"

            // species
            var dogSpecies = new Species { Name = "Dog" };
            context.Species.Add(dogSpecies);

            // breeds
            var bloodhound = new Breed { Name = "Blood-hound", Species = dogSpecies };
            var dalmatian = new Breed { Name = "Dalmatian", Species = dogSpecies };
            var goldenRetriever = new Breed { Name = "Golden Retriever", Species = dogSpecies };
            var germanSheperd = new Breed { Name = "Germand Sheperd", Species = dogSpecies };
            var mix = new Breed {Name = "Mix", Species = dogSpecies};

            context.Breeds.Add(bloodhound);
            context.Breeds.Add(dalmatian);
            context.Breeds.Add(goldenRetriever);

            // animals
            context.Dogs.Add(new Dog { AgeInYears = 4, Headline = "A well behaved dalmatian.", FullDescription = "cocco is a pure toy dlamatian bitch ,she is four half months old,is pedigree with 3 generation certificate.has been fully vet checked and had both vaccinations.flea treated and wormed on pancur.is use to other dogs and household noises.likes to go in the car and loves her walks.is part trainned and doing well on going outside.loves children and likes to be cuddled and played with.mum is a black toy poodle and can be seen .dad is a chocolate toy poodle with clear eye certificat a copy of eye certificate will go with cocco.i was keeping cocco but she isnt getting the attention she should be.so looking for loving caring home where she will get lots of love and attention.please ring for futher information,will only go to the right home thanks", Name = "Jessie", isLitter = false, isSold = false, isFemale = true, Breed = dalmatian });
            context.Dogs.Add(new Dog { AgeInYears = 1, Headline = "A young Golden Retriever. Well behaved and trained.", FullDescription = "XXXX STUNNING ENGLISH BULLDOG PUPPIES XXXXXXXXXX. 29 CHAMPIONS INCLUDING OCOBO & MY STYLE XXXX XXXX DONT MISSS OUT ON THESE GORGEOUS BABIES XXXXX XXXXX ONLY OUR PICK OF LITTER AVAILABLE XXXXX We have a litter of kc registered english bulldogs for sale 1 boy remaing stunning examples of this breed many champions in there pedigree large heads straight tails rose ears heavy boned lot of wrinkle come with 4 weeks free insurance 1st injections kc papers mum and dad can both be seen! They are our family pets well handled use to all household noises brought up with our children no breathing problems or health issues ready to go to there new homes for more information please call us ( reduced to £1650 with kc registration papers)",Name = "Goldie", isLitter = false, isSold = false, Breed = goldenRetriever });
            context.Dogs.Add(new Dog { AgeInYears = 0, Headline = "6 brand new dalmations. Pedigree standard, all healthy and fit.", FullDescription = "6 brand new dalmations. Pedigree standard, all healthy and fit. Ready in 5 weeks after wheaning", Name = null, isLitter = true, isSold = false, Breed = dalmatian });
            context.Dogs.Add(new Dog { AgeInYears = 2, Headline = "2 yr old Blood-hound. Well-behaved, likes children", FullDescription = "2 yr old Blood-hound. Well-behaved, likes children, no health problems and properly toilet trained. See for yourself", Name = "Spud", isLitter = false, isSold = false, Breed = bloodhound });
            context.Dogs.Add(new Dog { AgeInYears = 7, Headline = "Middle aged female Germen Sheperds - loves kids!", FullDescription = "A fairly old Alsatian requires a new home as we can't look after her anymore. Please get in touch. She likes walks and bones but other than that she's not high maintenance at all!", Name = "Spud", isLitter = false, isSold = false, isFemale = true, Breed = germanSheperd });
            context.Dogs.Add(new Dog { AgeInYears = 0, AgeInMonths = 3, Headline = "A litter of healthy pups - 2 males, 3 bitches!", FullDescription = "A litter of healthy pups - 2 males, 3 bitches! Dad is an Alsatian, mum is a Chihuhua so bit off there", Name = "Spud", isLitter = true, isSold = false, Breed = mix });
            
             #endregion

            base.Seed(context);
        }
    }
}
