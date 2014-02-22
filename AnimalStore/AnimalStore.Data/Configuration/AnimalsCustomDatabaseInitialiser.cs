using AnimalStore.Model;
using System.Configuration;
using System.Data.Entity;

namespace AnimalStore.Data.Configuration
{
    public class AnimalsCustomDatabaseInitialiser : 
        DropCreateDatabaseAlways<DataContext.AnimalsDataContext>
    {
        protected override void Seed(DataContext.AnimalsDataContext context)
        {
            if (IsDevelopmentEnvironment())
            {
                #region species

                var dogSpecies = new Species { Name = "Dog" };
                context.Species.Add(dogSpecies);

                #endregion

                #region categories

                var sporting = new Category { Name = "Sporting", Description = "Naturally active and alert, Sporting dogs make likeable, well-rounded companions. Members of the Group include pointers, retrievers, setters and spaniels. Remarkable for their instincts in water and woods, many of these breeds actively continue to participate in hunting and other field activities. Potential owners of Sporting dogs need to realize that most require regular, invigorating exercise." };
                var hound = new Category { Name = "Hound", Description = "Most hounds share the common ancestral trait of being used for hunting. Some use acute scenting powers to follow a trail. Others demonstrate a phenomenal gift of stamina as they relentlessly run down quarry. Beyond this, however, generalizations about hounds are hard to come by, since the Group encompasses quite a diverse lot. There are Pharaoh Hounds, Norwegian Elkhounds, Afghans and Beagles, among others. Some hounds share the distinct ability to produce a unique sound known as baying. You'd best sample this sound before you decide to get a hound of your own to be sure it's your cup of tea." };
                var working = new Category { Name = "Working", Description = "Dogs of the Working Group were bred to perform such jobs as guarding property, pulling sleds and performing water rescues. They have been invaluable assets to man throughout the ages. The Doberman Pinscher, Siberian Husky and Great Dane are included in this Group, to name just a few. Quick to learn, these intelligent, capable animals make solid companions. Their considerable dimensions and strength alone, however, make many working dogs unsuitable as pets for average families. And again, by virtue of their size alone, these dogs must be properly trained." };
                var terrier = new Category { Name = "Terrier", Description = "People familiar with this Group invariably comment on the distinctive terrier personality. These are feisty, energetic dogs whose sizes range from fairly small, as in the Norfolk, Cairn or West Highland White Terrier, to the grand Airedale Terrier. Terriers typically have little tolerance for other animals, including other dogs. Their ancestors were bred to hunt and kill vermin. Many continue to project the attitude that they're always eager for a spirited argument. Most terriers have wiry coats that require special grooming known as stripping in order to maintain a characteristic appearance. In general, they make engaging pets, but require owners with the determination to match their dogs' lively characters." };
                var toy = new Category { Name = "Toy", Description = "The diminutive size and winsome expressions of Toy dogs illustrate the main function of this Group: to embody sheer delight. Don't let their tiny stature fool you, though - - many Toys are tough as nails. If you haven't yet experienced the barking of an angry Chihuahua, for example, well, just wait. Toy dogs will always be popular with city dwellers and people without much living space. They make ideal apartment dogs and terrific lap warmers on nippy nights. (Incidentally, small breeds may be found in every Group, not just the Toy Group. We advise everyone to seriously consider getting a small breed, when appropriate, if for no other reason than to minimize some of the problems inherent in canines such as shedding, creating messes and cost of care. And training aside, it's still easier to control a ten-pound dog than it is one ten times that size.)" };
                var nonSporting = new Category { Name = "Non-sporting", Description = "Non-sporting dogs are a diverse group. Here are sturdy animals with as different personalities and appearances as the Chow Chow, Dalmatian, French Bulldog, and Keeshond. Talk about differences in size, coat, and visage! Some, like the Schipperke and Tibetan Spaniel are uncommon sights in the average neighborhood. Others, however, like the Poodle and Lhasa Apso, have quite a large following. The breeds in the Non-Sporting Group are a varied collection in terms of size, coat, personality and overall appearance." };
                var herding = new Category { Name = "Herding", Description = "All breeds in the Herding Group share the fabulous ability to control the movement of other animals. A remarkable example is the low-set Corgi, perhaps one foot tall at the shoulders, that can drive a herd of cows many times its size to pasture by leaping and nipping at their heels. The vast majority of Herding dogs, as household pets, never cross paths with a farm animal. Nevertheless, pure instinct prompts many of these dogs to gently herd their owners, especially the children of the family. In general, these intelligent dogs make excellent companions and respond beautifully to training exercises." };
                var miscellaneous = new Category { Name = "Miscellaneous", Description = "" };

                #endregion

                #region breeds

                var mixedBreedDog = new Breed { Species = dogSpecies, Name = "Mixed Breed", Category = miscellaneous };
                var affenpinscher = new Breed { Species = dogSpecies, Name = "Affenpinscher", Category = toy };
                var afghanHound = new Breed { Species = dogSpecies, Name = "Afghan Hound", Category = hound };
                var airedaleTerrier = new Breed { Species = dogSpecies, Name = "Airedale Terrier", Category = terrier };
                var akita = new Breed { Species = dogSpecies, Name = "Akita", Category = working };
                var alaskanMalamute = new Breed { Species = dogSpecies, Name = "Alaskan Malamute", Category = working };
                var americanEnglishCoonhound = new Breed { Species = dogSpecies, Name = "American English Coonhound", Category = hound };
                var americanEskimoDog = new Breed { Species = dogSpecies, Name = "American Eskimo Dog", Category = nonSporting };
                var americanFoxhound = new Breed { Species = dogSpecies, Name = "American Foxhound", Category = hound };
                var americanStaffordshireTerrier = new Breed { Species = dogSpecies, Name = "American Staffordshire Terrier", Category = terrier };
                var americanWaterSpaniel = new Breed { Species = dogSpecies, Name = "American Water Spaniel", Category = sporting };
                var anatolianShepherdDog = new Breed { Species = dogSpecies, Name = "Anatolian Shepherd Dog", Category = working };
                var australianCattleDog = new Breed { Species = dogSpecies, Name = "Australian Cattle Dog", Category = herding };
                var australianShepherd = new Breed { Species = dogSpecies, Name = "Australian Shepherd", Category = herding };
                var australianTerrier = new Breed { Species = dogSpecies, Name = "Australian Terrier", Category = terrier };
                var azawakh = new Breed { Species = dogSpecies, Name = "Azawakh", Category = miscellaneous };
                var basenji = new Breed { Species = dogSpecies, Name = "Basenji", Category = hound };
                var bassetHound = new Breed { Species = dogSpecies, Name = "Basset Hound", Category = hound };
                var beagle = new Breed { Species = dogSpecies, Name = "Beagle", Category = hound };
                var beardedCollie = new Breed { Species = dogSpecies, Name = "Bearded Collie", Category = herding };
                var beauceron = new Breed { Species = dogSpecies, Name = "Beauceron", Category = herding };
                var bedlingtonTerrier = new Breed { Species = dogSpecies, Name = "Bedlington Terrier", Category = terrier };
                var belgianLaekenois = new Breed { Species = dogSpecies, Name = "Belgian Laekenois", Category = miscellaneous };
                var belgianMalinois = new Breed { Species = dogSpecies, Name = "Belgian Malinois", Category = herding };
                var belgianSheepdog = new Breed { Species = dogSpecies, Name = "Belgian Sheepdog", Category = herding };
                var belgianTervuren = new Breed { Species = dogSpecies, Name = "Belgian Tervuren", Category = herding };
                var bergamasco = new Breed { Species = dogSpecies, Name = "Bergamasco", Category = miscellaneous };
                var bergerPicard = new Breed { Species = dogSpecies, Name = "Berger Picard", Category = miscellaneous };
                var berneseMountainDog = new Breed { Species = dogSpecies, Name = "Bernese Mountain Dog", Category = working };
                var bichonFrise = new Breed { Species = dogSpecies, Name = "Bichon Frise", Category = nonSporting };
                var blackRussianTerrier = new Breed { Species = dogSpecies, Name = "Black Russian Terrier", Category = working };
                var blackandTanCoonhound = new Breed { Species = dogSpecies, Name = "Black and Tan Coonhound", Category = hound };
                var bloodhound = new Breed { Species = dogSpecies, Name = "Bloodhound", Category = hound };
                var bluetickCoonhound = new Breed { Species = dogSpecies, Name = "Bluetick Coonhound" };
                var boerboel = new Breed { Species = dogSpecies, Name = "Boerboel", Category = miscellaneous };
                var borderCollie = new Breed { Species = dogSpecies, Name = "Border Collie", Category = herding };
                var borderTerrier = new Breed { Species = dogSpecies, Name = "Border Terrier", Category = terrier };
                var borzoi = new Breed { Species = dogSpecies, Name = "Borzoi", Category = hound };
                var bostonTerrier = new Breed { Species = dogSpecies, Name = "Boston Terrier", Category = terrier };
                var bouvierdesFlandres = new Breed { Species = dogSpecies, Name = "Bouvier des Flandres", Category = herding };
                var boxer = new Breed { Species = dogSpecies, Name = "Boxer", Category = working };
                var boykinSpaniel = new Breed { Species = dogSpecies, Name = "Boykin Spaniel", Category = sporting };
                var briard = new Breed { Species = dogSpecies, Name = "Briard", Category = herding };
                var brittany = new Breed { Species = dogSpecies, Name = "Brittany", Category = sporting };
                var brusselsGriffon = new Breed { Species = dogSpecies, Name = "Brussels Griffon", Category = toy };
                var bullTerrier = new Breed { Species = dogSpecies, Name = "Bull Terrier", Category = terrier };
                var bulldog = new Breed { Species = dogSpecies, Name = "Bulldog", Category = nonSporting };
                var bullmastiff = new Breed { Species = dogSpecies, Name = "Bullmastiff", Category = working };
                var cairnTerrier = new Breed { Species = dogSpecies, Name = "Cairn Terrier", Category = terrier };
                var canaanDog = new Breed { Species = dogSpecies, Name = "Canaan Dog", Category = herding };
                var caneCorso = new Breed { Species = dogSpecies, Name = "Cane Corso", Category = working };
                var cardiganWelshCorgi = new Breed { Species = dogSpecies, Name = "Cardigan Welsh Corgi", Category = herding };
                var cavalierKingCharlesSpaniel = new Breed { Species = dogSpecies, Name = "Cavalier King Charles Spaniel", Category = toy };
                var ceskyTerrier = new Breed { Species = dogSpecies, Name = "Cesky Terrier", Category = terrier };
                var chesapeakeBayRetriever = new Breed { Species = dogSpecies, Name = "Chesapeake Bay Retriever", Category = sporting };
                var chihuahua = new Breed { Species = dogSpecies, Name = "Chihuahua", Category = toy };
                var chineseCrested = new Breed { Species = dogSpecies, Name = "Chinese Crested", Category = toy };
                var chineseSharPei = new Breed { Species = dogSpecies, Name = "Chinese Shar-Pei", Category = nonSporting };
                var chinook = new Breed { Species = dogSpecies, Name = "Chinook", Category = working };
                var chowChow = new Breed { Species = dogSpecies, Name = "Chow Chow", Category = nonSporting };
                var cirnecodellEtna = new Breed { Species = dogSpecies, Name = "Cirneco dell'Etna", Category = miscellaneous };
                var clumberSpaniel = new Breed { Species = dogSpecies, Name = "Clumber Spaniel", Category = sporting };
                var cockerSpaniel = new Breed { Species = dogSpecies, Name = "Cocker Spaniel", Category = sporting };
                var collie = new Breed { Species = dogSpecies, Name = "Collie", Category = herding };
                var cotondeTulear = new Breed { Species = dogSpecies, Name = "Coton de Tulear", Category = miscellaneous };
                var curlyCoatedRetriever = new Breed { Species = dogSpecies, Name = "Curly-Coated Retriever", Category = sporting };
                var dachshund = new Breed { Species = dogSpecies, Name = "Dachshund", Category = hound };
                var dalmatian = new Breed { Species = dogSpecies, Name = "Dalmatian", Category = nonSporting };
                var dandieDinmontTerrier = new Breed { Species = dogSpecies, Name = "Dandie Dinmont Terrier", Category = terrier };
                var dobermanPinscher = new Breed { Species = dogSpecies, Name = "Doberman Pinscher", Category = working };
                var dogoArgentino = new Breed { Species = dogSpecies, Name = "Dogo Argentino", Category = miscellaneous };
                var doguedeBordeaux = new Breed { Species = dogSpecies, Name = "Dogue de Bordeaux", Category = working };
                var englishCockerSpaniel = new Breed { Species = dogSpecies, Name = "English Cocker Spaniel", Category = sporting };
                var englishFoxhound = new Breed { Species = dogSpecies, Name = "English Foxhound", Category = hound };
                var englishSetter = new Breed { Species = dogSpecies, Name = "English Setter", Category = sporting };
                var englishSpringerSpaniel = new Breed { Species = dogSpecies, Name = "English Springer Spaniel", Category = sporting };
                var englishToySpaniel = new Breed { Species = dogSpecies, Name = "English Toy Spaniel", Category = toy };
                var entlebucherMountainDog = new Breed { Species = dogSpecies, Name = "Entlebucher Mountain Dog", Category = herding };
                var fieldSpaniel = new Breed { Species = dogSpecies, Name = "Field Spaniel", Category = sporting };
                var finnishLapphund = new Breed { Species = dogSpecies, Name = "Finnish Lapphund", Category = herding };
                var finnishSpitz = new Breed { Species = dogSpecies, Name = "Finnish Spitz", Category = nonSporting };
                var flatCoatedRetriever = new Breed { Species = dogSpecies, Name = "Flat-Coated Retriever", Category = sporting };
                var frenchBulldog = new Breed { Species = dogSpecies, Name = "French Bulldog", Category = nonSporting };
                var germanPinscher = new Breed { Species = dogSpecies, Name = "German Pinscher", Category = working };
                var germanShepherd = new Breed { Species = dogSpecies, Name = "German Shepherd Dog", Category = herding };
                var germanShorthairedPointer = new Breed { Species = dogSpecies, Name = "German Shorthaired Pointer", Category = sporting };
                var germanWirehairedPointer = new Breed { Species = dogSpecies, Name = "German Wirehaired Pointer", Category = sporting };
                var giantSchnauzer = new Breed { Species = dogSpecies, Name = "Giant Schnauzer", Category = working };
                var glenofImaalTerrier = new Breed { Species = dogSpecies, Name = "Glen of Imaal Terrier", Category = terrier };
                var goldenRetriever = new Breed { Species = dogSpecies, Name = "Golden Retriever", Category = sporting };
                var gordonSetter = new Breed { Species = dogSpecies, Name = "Gordon Setter", Category = sporting };
                var greatDane = new Breed { Species = dogSpecies, Name = "Great Dane", Category = working };
                var greatPyrenees = new Breed { Species = dogSpecies, Name = "Great Pyrenees", Category = working };
                var greaterSwissMountainDog = new Breed { Species = dogSpecies, Name = "Greater Swiss Mountain Dog", Category = working };
                var greyhound = new Breed { Species = dogSpecies, Name = "Greyhound", Category = hound };
                var harrier = new Breed { Species = dogSpecies, Name = "Harrier", Category = hound };
                var havanese = new Breed { Species = dogSpecies, Name = "Havanese", Category = toy };
                var ibizanHound = new Breed { Species = dogSpecies, Name = "Ibizan Hound", Category = hound };
                var icelandicSheepdog = new Breed { Species = dogSpecies, Name = "Icelandic Sheepdog", Category = herding };
                var irishRedandWhiteSetter = new Breed { Species = dogSpecies, Name = "Irish Red and White Setter", Category = sporting };
                var irishSetter = new Breed { Species = dogSpecies, Name = "Irish Setter", Category = sporting };
                var irishTerrier = new Breed { Species = dogSpecies, Name = "Irish Terrier", Category = terrier };
                var irishWaterSpaniel = new Breed { Species = dogSpecies, Name = "Irish Water Spaniel", Category = sporting };
                var irishWolfhound = new Breed { Species = dogSpecies, Name = "Irish Wolfhound", Category = hound };
                var italianGreyhound = new Breed { Species = dogSpecies, Name = "Italian Greyhound", Category = toy };
                var japaneseChin = new Breed { Species = dogSpecies, Name = "Japanese Chin", Category = toy };
                var keeshond = new Breed { Species = dogSpecies, Name = "Keeshond", Category = nonSporting };
                var kerryBlueTerrier = new Breed { Species = dogSpecies, Name = "Kerry Blue Terrier", Category = terrier };
                var komondor = new Breed { Species = dogSpecies, Name = "Komondor", Category = working };
                var kuvasz = new Breed { Species = dogSpecies, Name = "Kuvasz", Category = working };
                var labradorRetriever = new Breed { Species = dogSpecies, Name = "Labrador Retriever", Category = sporting };
                var lagottoRomagnolo = new Breed { Species = dogSpecies, Name = "Lagotto Romagnolo", Category = miscellaneous };
                var lakelandTerrier = new Breed { Species = dogSpecies, Name = "Lakeland Terrier", Category = terrier };
                var leonberger = new Breed { Species = dogSpecies, Name = "Leonberger", Category = working };
                var lhasaApso = new Breed { Species = dogSpecies, Name = "Lhasa Apso", Category = nonSporting };
                var lowchen = new Breed { Species = dogSpecies, Name = "Lowchen", Category = nonSporting };
                var maltese = new Breed { Species = dogSpecies, Name = "Maltese", Category = toy };
                var manchesterTerrier = new Breed { Species = dogSpecies, Name = "Manchester Terrier", Category = terrier };
                var mastiff = new Breed { Species = dogSpecies, Name = "Mastiff", Category = working };
                var miniatureAmericanShepherd = new Breed { Species = dogSpecies, Name = "Miniature American Shepherd", Category = miscellaneous };
                var miniatureBullTerrier = new Breed { Species = dogSpecies, Name = "Miniature Bull Terrier", Category = terrier };
                var miniaturePinscher = new Breed { Species = dogSpecies, Name = "Miniature Pinscher", Category = toy };
                var miniatureSchnauzer = new Breed { Species = dogSpecies, Name = "Miniature Schnauzer", Category = terrier };
                var neapolitanMastiff = new Breed { Species = dogSpecies, Name = "Neapolitan Mastiff", Category = working };
                var newfoundland = new Breed { Species = dogSpecies, Name = "Newfoundland", Category = working };
                var norfolkTerrier = new Breed { Species = dogSpecies, Name = "Norfolk Terrier", Category = terrier };
                var norwegianBuhund = new Breed { Species = dogSpecies, Name = "Norwegian Buhund", Category = herding };
                var norwegianElkhound = new Breed { Species = dogSpecies, Name = "Norwegian Elkhound", Category = hound };
                var norwegianLundehund = new Breed { Species = dogSpecies, Name = "Norwegian Lundehund", Category = nonSporting };
                var norwichTerrier = new Breed { Species = dogSpecies, Name = "Norwich Terrier", Category = terrier };
                var novaScotiaDuckTollingRetriever = new Breed { Species = dogSpecies, Name = "Nova Scotia Duck Tolling Retriever", Category = sporting };
                var oldEnglishSheepdog = new Breed { Species = dogSpecies, Name = "Old English Sheepdog", Category = herding };
                var otterhound = new Breed { Species = dogSpecies, Name = "Otterhound", Category = hound };
                var papillon = new Breed { Species = dogSpecies, Name = "Papillon", Category = toy };
                var parsonRussellTerrier = new Breed { Species = dogSpecies, Name = "Parson Russell Terrier", Category = terrier };
                var pekingese = new Breed { Species = dogSpecies, Name = "Pekingese", Category = toy };
                var pembrokeWelshCorgi = new Breed { Species = dogSpecies, Name = "Pembroke Welsh Corgi", Category = herding };
                var peruvianIncaOrchid = new Breed { Species = dogSpecies, Name = "Peruvian Inca Orchid", Category = miscellaneous };
                var petitBassetGriffonVendeen = new Breed { Species = dogSpecies, Name = "Petit Basset Griffon Vendeen", Category = hound };
                var pharaohHound = new Breed { Species = dogSpecies, Name = "Pharaoh Hound", Category = hound };
                var plott = new Breed { Species = dogSpecies, Name = "Plott", Category = hound };
                var pointer = new Breed { Species = dogSpecies, Name = "Pointer", Category = sporting };
                var polishLowlandSheepdog = new Breed { Species = dogSpecies, Name = "Polish Lowland Sheepdog", Category = herding };
                var pomeranian = new Breed { Species = dogSpecies, Name = "Pomeranian", Category = toy };
                var poodle = new Breed { Species = dogSpecies, Name = "Poodle", Category = toy };
                var portuguesePodengoPequeno = new Breed { Species = dogSpecies, Name = "Portuguese Podengo Pequeno", Category = hound };
                var portugueseWaterDgo = new Breed { Species = dogSpecies, Name = "Portuguese Water Dog", Category = working };
                var pug = new Breed { Species = dogSpecies, Name = "Pug", Category = toy };
                var puli = new Breed { Species = dogSpecies, Name = "Puli", Category = herding };
                var pumi = new Breed { Species = dogSpecies, Name = "Pumi", Category = miscellaneous };
                var pyreneanShepherd = new Breed { Species = dogSpecies, Name = "Pyrenean Shepherd", Category = herding };
                var ratTerrier = new Breed { Species = dogSpecies, Name = "Rat Terrier", Category = terrier };
                var redboneCoonhound = new Breed { Species = dogSpecies, Name = "Redbone Coonhound", Category = hound };
                var rhodesianRidgeback = new Breed { Species = dogSpecies, Name = "Rhodesian Ridgeback", Category = hound };
                var rottweiler = new Breed { Species = dogSpecies, Name = "Rottweiler", Category = working };
                var russellTerrier = new Breed { Species = dogSpecies, Name = "Russell Terrier", Category = terrier };
                var saluki = new Breed { Species = dogSpecies, Name = "Saluki", Category = hound };
                var samoyed = new Breed { Species = dogSpecies, Name = "Samoyed", Category = working };
                var schipperke = new Breed { Species = dogSpecies, Name = "Schipperke", Category = nonSporting };
                var scottishDeerhound = new Breed { Species = dogSpecies, Name = "Scottish Deerhound", Category = hound };
                var scottishTerrie = new Breed { Species = dogSpecies, Name = "Scottish Terrier", Category = terrier };
                var sealyhamTerrier = new Breed { Species = dogSpecies, Name = "Sealyham Terrier", Category = terrier };
                var shetlandSheepdog = new Breed { Species = dogSpecies, Name = "Shetland Sheepdog", Category = herding };
                var shibaInu = new Breed { Species = dogSpecies, Name = "Shiba Inu", Category = nonSporting };
                var shihTzu = new Breed { Species = dogSpecies, Name = "Shih Tzu", Category = toy };
                var siberianHusky = new Breed { Species = dogSpecies, Name = "Siberian Husky", Category = working };
                var silkyTerrier = new Breed { Species = dogSpecies, Name = "Silky Terrier", Category = toy };
                var skyeTerrier = new Breed { Species = dogSpecies, Name = "Skye Terrier", Category = terrier };
                var smoothFoxTerrier = new Breed { Species = dogSpecies, Name = "Smooth Fox Terrier", Category = terrier };
                var softCoatedWheatenTerrier = new Breed { Species = dogSpecies, Name = "Soft Coated Wheaten Terrier", Category = terrier };
                var sloughi = new Breed { Species = dogSpecies, Name = "Sloughi", Category = miscellaneous };
                var spanishWaterDog = new Breed { Species = dogSpecies, Name = "Spanish Water Dog", Category = miscellaneous };
                var spinoneItaliano = new Breed { Species = dogSpecies, Name = "Spinone Italiano", Category = sporting };
                var stBernard = new Breed { Species = dogSpecies, Name = "St. Bernard", Category = working };
                var staffordshireBullTerrier = new Breed { Species = dogSpecies, Name = "Staffordshire Bull Terrier", Category = terrier };
                var standardSchnauzer = new Breed { Species = dogSpecies, Name = "Standard Schnauzer", Category = working };
                var sussexSpaniel = new Breed { Species = dogSpecies, Name = "Sussex Spaniel", Category = sporting };
                var swedishVallhund = new Breed { Species = dogSpecies, Name = "Swedish Vallhund", Category = herding };
                var tibetanMastiff = new Breed { Species = dogSpecies, Name = "Tibetan Mastiff", Category = working };
                var tibetanSpaniel = new Breed { Species = dogSpecies, Name = "Tibetan Spaniel", Category = nonSporting };
                var tibetanTerrier = new Breed { Species = dogSpecies, Name = "Tibetan Terrier", Category = nonSporting };
                var toyFoxTerrier = new Breed { Species = dogSpecies, Name = "Toy Fox Terrier", Category = toy };
                var treeingWalkerCoonhound = new Breed { Species = dogSpecies, Name = "Treeing Walker Coonhound", Category = hound };
                var vizsla = new Breed { Species = dogSpecies, Name = "Vizsla", Category = sporting };
                var weimaraner = new Breed { Species = dogSpecies, Name = "Weimaraner", Category = sporting };
                var welshSpringeSpaniel = new Breed { Species = dogSpecies, Name = "Welsh Springer Spaniel", Category = sporting };
                var welshTerrier = new Breed { Species = dogSpecies, Name = "Welsh Terrier", Category = terrier };
                var westHighlandWhiteTerrier = new Breed { Species = dogSpecies, Name = "West Highland White Terrier", Category = terrier };
                var whippet = new Breed { Species = dogSpecies, Name = "Whippet", Category = hound };
                var wireFoxTerrier = new Breed { Species = dogSpecies, Name = "Wire Fox Terrier", Category = terrier };
                var wirehairedPointingGriffon = new Breed { Species = dogSpecies, Name = "Wirehaired Pointing Griffon" };
                var wirehairedVizsla = new Breed { Species = dogSpecies, Name = "Wirehaired Vizsla", Category = miscellaneous };
                var xoloitzcuintli = new Breed { Species = dogSpecies, Name = "Xoloitzcuintli", Category = nonSporting };
                var yorkshireTerrier = new Breed { Species = dogSpecies, Name = "Yorkshire Terrier", Category = toy };

                context.Breeds.Add(affenpinscher);
                context.Breeds.Add(afghanHound);
                context.Breeds.Add(airedaleTerrier);
                context.Breeds.Add(akita);
                context.Breeds.Add(alaskanMalamute);
                context.Breeds.Add(americanEnglishCoonhound);
                context.Breeds.Add(americanEskimoDog);
                context.Breeds.Add(americanFoxhound);
                context.Breeds.Add(americanStaffordshireTerrier);
                context.Breeds.Add(americanWaterSpaniel);
                context.Breeds.Add(anatolianShepherdDog);
                context.Breeds.Add(australianCattleDog);
                context.Breeds.Add(australianShepherd);
                context.Breeds.Add(australianTerrier);
                context.Breeds.Add(azawakh);
                context.Breeds.Add(basenji);
                context.Breeds.Add(bassetHound);
                context.Breeds.Add(beagle);
                context.Breeds.Add(beardedCollie);
                context.Breeds.Add(beauceron);
                context.Breeds.Add(bedlingtonTerrier);
                context.Breeds.Add(belgianLaekenois);
                context.Breeds.Add(belgianMalinois);
                context.Breeds.Add(belgianSheepdog);
                context.Breeds.Add(belgianTervuren);
                context.Breeds.Add(bergamasco);
                context.Breeds.Add(bergerPicard);
                context.Breeds.Add(berneseMountainDog);
                context.Breeds.Add(bichonFrise);
                context.Breeds.Add(blackRussianTerrier);
                context.Breeds.Add(blackandTanCoonhound);
                context.Breeds.Add(bloodhound);
                context.Breeds.Add(bluetickCoonhound);
                context.Breeds.Add(boerboel);
                context.Breeds.Add(borderCollie);
                context.Breeds.Add(borderTerrier);
                context.Breeds.Add(borzoi);
                context.Breeds.Add(bostonTerrier);
                context.Breeds.Add(bouvierdesFlandres);
                context.Breeds.Add(boxer);
                context.Breeds.Add(boykinSpaniel);
                context.Breeds.Add(briard);
                context.Breeds.Add(brittany);
                context.Breeds.Add(brusselsGriffon);
                context.Breeds.Add(bullTerrier);
                context.Breeds.Add(bulldog);
                context.Breeds.Add(bullmastiff);
                context.Breeds.Add(cairnTerrier);
                context.Breeds.Add(canaanDog);
                context.Breeds.Add(caneCorso);
                context.Breeds.Add(cardiganWelshCorgi);
                context.Breeds.Add(cavalierKingCharlesSpaniel);
                context.Breeds.Add(ceskyTerrier);
                context.Breeds.Add(chesapeakeBayRetriever);
                context.Breeds.Add(chihuahua);
                context.Breeds.Add(chineseCrested);
                context.Breeds.Add(chineseSharPei);
                context.Breeds.Add(chinook);
                context.Breeds.Add(chowChow);
                context.Breeds.Add(cirnecodellEtna);
                context.Breeds.Add(clumberSpaniel);
                context.Breeds.Add(cockerSpaniel);
                context.Breeds.Add(collie);
                context.Breeds.Add(cotondeTulear);
                context.Breeds.Add(curlyCoatedRetriever);
                context.Breeds.Add(dachshund);
                context.Breeds.Add(dalmatian);
                context.Breeds.Add(dandieDinmontTerrier);
                context.Breeds.Add(dobermanPinscher);
                context.Breeds.Add(dogoArgentino);
                context.Breeds.Add(doguedeBordeaux);
                context.Breeds.Add(englishCockerSpaniel);
                context.Breeds.Add(englishFoxhound);
                context.Breeds.Add(englishSetter);
                context.Breeds.Add(englishSpringerSpaniel);
                context.Breeds.Add(englishToySpaniel);
                context.Breeds.Add(entlebucherMountainDog);
                context.Breeds.Add(fieldSpaniel);
                context.Breeds.Add(finnishLapphund);
                context.Breeds.Add(finnishSpitz);
                context.Breeds.Add(flatCoatedRetriever);
                context.Breeds.Add(frenchBulldog);
                context.Breeds.Add(germanPinscher);
                context.Breeds.Add(germanShepherd);
                context.Breeds.Add(germanShorthairedPointer);
                context.Breeds.Add(germanWirehairedPointer);
                context.Breeds.Add(giantSchnauzer);
                context.Breeds.Add(glenofImaalTerrier);
                context.Breeds.Add(goldenRetriever);
                context.Breeds.Add(gordonSetter);
                context.Breeds.Add(greatDane);
                context.Breeds.Add(greatPyrenees);
                context.Breeds.Add(greaterSwissMountainDog);
                context.Breeds.Add(greyhound);
                context.Breeds.Add(harrier);
                context.Breeds.Add(havanese);
                context.Breeds.Add(ibizanHound);
                context.Breeds.Add(icelandicSheepdog);
                context.Breeds.Add(irishRedandWhiteSetter);
                context.Breeds.Add(irishSetter);
                context.Breeds.Add(irishTerrier);
                context.Breeds.Add(irishWaterSpaniel);
                context.Breeds.Add(irishWolfhound);
                context.Breeds.Add(italianGreyhound);
                context.Breeds.Add(japaneseChin);
                context.Breeds.Add(keeshond);
                context.Breeds.Add(kerryBlueTerrier);
                context.Breeds.Add(komondor);
                context.Breeds.Add(kuvasz);
                context.Breeds.Add(labradorRetriever);
                context.Breeds.Add(lagottoRomagnolo);
                context.Breeds.Add(lakelandTerrier);
                context.Breeds.Add(leonberger);
                context.Breeds.Add(lhasaApso);
                context.Breeds.Add(lowchen);
                context.Breeds.Add(maltese);
                context.Breeds.Add(manchesterTerrier);
                context.Breeds.Add(mastiff);
                context.Breeds.Add(miniatureAmericanShepherd);
                context.Breeds.Add(miniatureBullTerrier);
                context.Breeds.Add(miniaturePinscher);
                context.Breeds.Add(miniatureSchnauzer);
                context.Breeds.Add(neapolitanMastiff);
                context.Breeds.Add(newfoundland);
                context.Breeds.Add(norfolkTerrier);
                context.Breeds.Add(norwegianBuhund);
                context.Breeds.Add(norwegianElkhound);
                context.Breeds.Add(norwegianLundehund);
                context.Breeds.Add(norwichTerrier);
                context.Breeds.Add(novaScotiaDuckTollingRetriever);
                context.Breeds.Add(oldEnglishSheepdog);
                context.Breeds.Add(otterhound);
                context.Breeds.Add(papillon);
                context.Breeds.Add(parsonRussellTerrier);
                context.Breeds.Add(pekingese);
                context.Breeds.Add(pembrokeWelshCorgi);
                context.Breeds.Add(peruvianIncaOrchid);
                context.Breeds.Add(petitBassetGriffonVendeen);
                context.Breeds.Add(pharaohHound);
                context.Breeds.Add(plott);
                context.Breeds.Add(pointer);
                context.Breeds.Add(polishLowlandSheepdog);
                context.Breeds.Add(pomeranian);
                context.Breeds.Add(poodle);
                context.Breeds.Add(portuguesePodengoPequeno);
                context.Breeds.Add(portugueseWaterDgo);
                context.Breeds.Add(pug);
                context.Breeds.Add(puli);
                context.Breeds.Add(pumi);
                context.Breeds.Add(pyreneanShepherd);
                context.Breeds.Add(ratTerrier);
                context.Breeds.Add(redboneCoonhound);
                context.Breeds.Add(rhodesianRidgeback);
                context.Breeds.Add(rottweiler);
                context.Breeds.Add(russellTerrier);
                context.Breeds.Add(saluki);
                context.Breeds.Add(samoyed);
                context.Breeds.Add(schipperke);
                context.Breeds.Add(scottishDeerhound);
                context.Breeds.Add(scottishTerrie);
                context.Breeds.Add(sealyhamTerrier);
                context.Breeds.Add(shetlandSheepdog);
                context.Breeds.Add(shibaInu);
                context.Breeds.Add(shihTzu);
                context.Breeds.Add(siberianHusky);
                context.Breeds.Add(silkyTerrier);
                context.Breeds.Add(skyeTerrier);
                context.Breeds.Add(smoothFoxTerrier);
                context.Breeds.Add(softCoatedWheatenTerrier);
                context.Breeds.Add(sloughi);
                context.Breeds.Add(spanishWaterDog);
                context.Breeds.Add(spinoneItaliano);
                context.Breeds.Add(stBernard);
                context.Breeds.Add(staffordshireBullTerrier);
                context.Breeds.Add(standardSchnauzer);
                context.Breeds.Add(sussexSpaniel);
                context.Breeds.Add(swedishVallhund);
                context.Breeds.Add(tibetanMastiff);
                context.Breeds.Add(tibetanSpaniel);
                context.Breeds.Add(tibetanTerrier);
                context.Breeds.Add(toyFoxTerrier);
                context.Breeds.Add(treeingWalkerCoonhound);
                context.Breeds.Add(vizsla);
                context.Breeds.Add(weimaraner);
                context.Breeds.Add(welshSpringeSpaniel);
                context.Breeds.Add(welshTerrier);
                context.Breeds.Add(westHighlandWhiteTerrier);
                context.Breeds.Add(whippet);
                context.Breeds.Add(wireFoxTerrier);
                context.Breeds.Add(wirehairedPointingGriffon);
                context.Breeds.Add(wirehairedVizsla);
                context.Breeds.Add(xoloitzcuintli);
                context.Breeds.Add(yorkshireTerrier);

                #endregion

                #region dogs

                #region affenpinschers
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 2,
                    Headline = "2 yr old Affen Pinscher. Well-behaved, likes children",
                    FullDescription = "2 yr old. Well-behaved, likes children, no health problems and properly toilet trained. See for yourself",
                    Name = "Spud",
                    IsLitter = false,
                    IsSold = false,
                    Price = 250,
                    Breed = affenpinscher,
                    CreatedByUsedId = 1,
                    PlaceId = 1
                });
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 5,
                    Headline = "im selling my little affenpincher girl",
                    FullDescription = "im having to sell my scuffy little friend she is perfect in everyway she is house trained can be left a sensible lenth of time gets on with all different animals not nasty in any way adores children and is a perfect lapdog will sit on your kn....",
                    Name = "Scuffy",
                    IsLitter = false,
                    IsSold = false,
                    Price = 125,
                    Breed = affenpinscher,
                    CreatedByUsedId = 1,
                    PlaceId = 8297
                });
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 2,
                    Headline = "2 yr old Affen Pinscher. Needs home",
                    FullDescription = "2 yr old. Well-behaved, good guard dog",
                    Name = null,
                    IsLitter = false,
                    IsSold = false,
                    Price = 250,
                    Breed = affenpinscher,
                    CreatedByUsedId = 1,
                    PlaceId = 1
                });
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 3,
                    Headline = "3yr old Affen Pinscher. good home needed",
                    FullDescription = "3 yr old. Well-behaved, good guard dog good home needed",
                    Name = null,
                    IsLitter = false,
                    IsSold = false,
                    Price = 190,
                    Breed = affenpinscher,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 0,
                    Headline = "0 yr old Affen Pinscher. Needs home",
                    FullDescription = "puppy dog - really well behaved",
                    Name = null,
                    IsLitter = false,
                    IsSold = false,
                    Price = 250,
                    Breed = affenpinscher,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 0,
                    Headline = "Litter of affenpinschers great",
                    FullDescription = "Brand new litter from award winning stud and bitch. Perfect lineage, real crufts quality",
                    Name = null,
                    IsLitter = true,
                    IsSold = false,
                    Price = 1200,
                    Breed = affenpinscher,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 2,
                    Headline = "2 yr old Affen Pinscher. Needs home",
                    FullDescription = "2 yr old. Well-behaved, good guard dog",
                    Name = null,
                    IsLitter = false,
                    IsSold = false,
                    Price = 600,
                    Breed = affenpinscher,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 0,
                    Headline = "Litter of affenpinschers great",
                    FullDescription = "litter of Well-behaved, good guard dogs",
                    Name = null,
                    IsLitter = false,
                    IsSold = false,
                    Price = 400,
                    Breed = affenpinscher,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 2,
                    Headline = "2 yr old Affen Pinscher. Needs home",
                    FullDescription = "2 yr old. Well-behaved, good guard dog",
                    Name = null,
                    IsLitter = false,
                    IsSold = false,
                    Price = 350,
                    Breed = affenpinscher,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 2,
                    Headline = "2 yr old Affen Pinscher. Well-behaved, likes children",
                    FullDescription = "2 yr old. Well-behaved, likes children, no health problems and properly toilet trained. See for yourself",
                    Name = "Spud",
                    IsLitter = false,
                    IsSold = false,
                    Price = 500,
                    Breed = affenpinscher,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 5,
                    Headline = "im selling my little affenpincher girl",
                    FullDescription = "im having to sell my scuffy little friend she is perfect in everyway she is house trained can be left a sensible lenth of time gets on with all different animals not nasty in any way adores children and is a perfect lapdog will sit on your kn....",
                    Name = "Scuffy",
                    IsLitter = false,
                    IsSold = false,
                    Price = 600,
                    Breed = affenpinscher,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 2,
                    Headline = "2 yr old Affen Pinscher. Needs home",
                    FullDescription = "2 yr old. Well-behaved, good guard dog",
                    Name = null,
                    IsLitter = false,
                    IsSold = false,
                    Price = 980,
                    Breed = affenpinscher,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 3,
                    Headline = "3yr old Affen Pinscher. good home needed",
                    FullDescription = "3 yr old. Well-behaved, good guard dog good home needed",
                    Name = null,
                    IsLitter = false,
                    IsSold = false,
                    Price = 80,
                    Breed = affenpinscher,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 0,
                    Headline = "0 yr old Affen Pinscher. Needs home",
                    FullDescription = "puppy dog - really well behaved",
                    Name = null,
                    IsLitter = false,
                    IsSold = false,
                    Price = 700,
                    Breed = affenpinscher,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 0,
                    Headline = "Litter of affenpinschers great",
                    FullDescription = "Brand new litter from award winning stud and bitch. Perfect lineage, real crufts quality",
                    Name = null,
                    IsLitter = true,
                    IsSold = false,
                    Price = 600,
                    Breed = affenpinscher,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 3,
                    Headline = "3yr old Affen Pinscher. good home needed",
                    FullDescription = "3 yr old. Well-behaved, good guard dog good home needed",
                    Name = null,
                    IsLitter = false,
                    IsSold = false,
                    Price = 400,
                    Breed = affenpinscher,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 0,
                    Headline = "0 yr old Affen Pinscher. Needs home",
                    FullDescription = "puppy dog - really well behaved",
                    Name = null,
                    IsLitter = false,
                    IsSold = false,
                    Price = 300,
                    Breed = affenpinscher,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 0,
                    Headline = "Litter of affenpinschers great",
                    FullDescription = "Brand new litter from award winning stud and bitch. Perfect lineage, real crufts quality",
                    Name = null,
                    IsLitter = true,
                    IsSold = false,
                    Price = 300,
                    Breed = affenpinscher,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 2,
                    Headline = "2 yr old Affen Pinscher. Needs home",
                    FullDescription = "2 yr old. Well-behaved, good guard dog",
                    Name = null,
                    IsLitter = false,
                    IsSold = false,
                    Price = 600,
                    Breed = affenpinscher,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 0,
                    Headline = "Litter of affenpinschers great",
                    FullDescription = "litter of Well-behaved, good guard dogs",
                    Name = null,
                    IsLitter = false,
                    IsSold = false,
                    Price = 700,
                    Breed = affenpinscher,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 2,
                    Headline = "2 yr old Affen Pinscher. Needs home",
                    FullDescription = "2 yr old. Well-behaved, good guard dog",
                    Name = null,
                    IsLitter = false,
                    IsSold = false,
                    Price = 300,
                    Breed = affenpinscher,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 2,
                    Headline = "2 yr old Affen Pinscher. Well-behaved, likes children",
                    FullDescription = "2 yr old. Well-behaved, likes children, no health problems and properly toilet trained. See for yourself",
                    Name = "Spud",
                    IsLitter = false,
                    IsSold = false,
                    Price = 400,
                    Breed = affenpinscher,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 5,
                    Headline = "im selling my little affenpincher girl",
                    FullDescription = "im having to sell my scuffy little friend she is perfect in everyway she is house trained can be left a sensible lenth of time gets on with all different animals not nasty in any way adores children and is a perfect lapdog will sit on your kn....",
                    Name = "Scuffy",
                    IsLitter = false,
                    IsSold = false,
                    Price = 100,
                    Breed = affenpinscher,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 0,
                    Headline = "0 yr old Affen Pinscher. Needs home",
                    FullDescription = "puppy dog - really well behaved",
                    Name = null,
                    IsLitter = false,
                    IsSold = false,
                    Price = 300,
                    Breed = affenpinscher,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 0,
                    Headline = "Litter of affenpinschers great",
                    FullDescription = "Brand new litter from award winning stud and bitch. Perfect lineage, real crufts quality",
                    Name = null,
                    IsLitter = true,
                    IsSold = false,
                    Price = 300,
                    Breed = affenpinscher,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 2,
                    Headline = "2 yr old Affen Pinscher. Needs home",
                    FullDescription = "2 yr old. Well-behaved, good guard dog",
                    Name = null,
                    IsLitter = false,
                    IsSold = false,
                    Price = 600,
                    Breed = affenpinscher,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 0,
                    Headline = "Litter of affenpinschers great",
                    FullDescription = "litter of Well-behaved, good guard dogs",
                    Name = null,
                    IsLitter = false,
                    IsSold = false,
                    Price = 700,
                    Breed = affenpinscher,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 3,
                    Headline = "3yr old Affen Pinscher. good home needed",
                    FullDescription = "3 yr old. Well-behaved, good guard dog good home needed",
                    Name = null,
                    IsLitter = false,
                    IsSold = false,
                    Price = 80,
                    Breed = affenpinscher,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 0,
                    Headline = "0 yr old Affen Pinscher. Needs home",
                    FullDescription = "puppy dog - really well behaved",
                    Name = null,
                    IsLitter = false,
                    IsSold = false,
                    Price = 700,
                    Breed = affenpinscher,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 0,
                    Headline = "Litter of affenpinschers great",
                    FullDescription = "Brand new litter from award winning stud and bitch. Perfect lineage, real crufts quality",
                    Name = null,
                    IsLitter = true,
                    IsSold = false,
                    Price = 600,
                    Breed = affenpinscher,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 3,
                    Headline = "3yr old Affen Pinscher. good home needed",
                    FullDescription = "3 yr old. Well-behaved, good guard dog good home needed",
                    Name = null,
                    IsLitter = false,
                    IsSold = false,
                    Price = 400,
                    Breed = affenpinscher,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });
                #endregion

                #region akitas
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 1,
                    Headline = "4 female akita puppies!",
                    FullDescription = "4 female akita puppies, Ready to go NOW!! 10 weeks old 300 Ono call or email . ",
                    Name = "",
                    IsLitter = true,
                    IsSold = false,
                    Breed = akita,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 0,
                    AgeInMonths = 2,
                    Headline = "Gorgeous Pedigree Akitas 7 Week Old Pups",
                    FullDescription = "We have adorable bear headed black and white girls available. The puppies will be fleaed, wormed and vaccinated as well as vet checked. Your puppy will also have 4 weeks free insurance. They will be ready to leave us on 8th December, however....",
                    Price = 700,
                    Name = "",
                    IsLitter = false,
                    IsSold = false,
                    Breed = akita,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 1,
                    Headline = "beautiful Akita pup",
                    FullDescription = "beautiful Akita pup She born on 16/09/2013 she was warm And fleat she is full pedigree akita but she don't come with the KC registrert. reason for sale landlord don't accept dog. Please any more information call on 07587916471",
                    Name = "lynn",
                    IsLitter = false,
                    IsSold = false,
                    IsFemale = true,
                    Breed = akita,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 7,
                    Headline = "Japanese Akita Pups Ready To Go",
                    FullDescription = "I have a lovely litter of 7 there is 6 left 3 girls 3boys looking for forever homes handled all the time be myself and my young children very playful ready to go they are eight week I also have mum and dad as there my pets more than welcome t....",
                    Name = "Spud",
                    IsLitter = false,
                    IsSold = false,
                    IsFemale = true,
                    Price = 300,
                    Breed = akita,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 1,
                    FullDescription = "WE ARE PROUD TO OFFER TWO EXCEPTIONAL MALE, AMERICAN AKITA PUPPIES. PUPPIES ARE KC REGISTERED AND HAVE BEEN FLEAD AND WORMED UP TO DATE.PUPPIES HAVE OUTSTANDING BONE STRUCTURE AND SUBSTANCE,COMPLIMENTED WITH A HEALTHY COAT.PUPPIES ARE 5 WEEKS...",
                    Headline = " AMERICAN AKITA PUPPIES  .",
                    Name = null,
                    IsLitter = false,
                    IsSold = false,
                    Breed = akita,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });
                #endregion

                #region blood hounds

                context.Dogs.Add(new Dog
                {
                    AgeInYears = 2,
                    Headline = "2 yr old Blood-hound. Well-behaved, likes children",
                    FullDescription = "2 yr old Blood-hound. Well-behaved, likes children, no health problems and properly toilet trained. See for yourself",
                    Name = "Spud",
                    IsLitter = false,
                    IsSold = false,
                    Price = 500,
                    Breed = bloodhound,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });

                #endregion

                #region bulldogs

                context.Dogs.Add(new Dog
                {
                    AgeInYears = 4,
                    Headline = "A dangerously behaved Bulldog.",
                    FullDescription = "A tough stocky bulldog",
                    Name = "Stocky",
                    IsLitter = false,
                    IsSold = false,
                    IsFemale = true,
                    Price = 400,
                    Breed = bulldog,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });

                #endregion

                #region dalmatians
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 4,
                    Headline = "A well behaved Dalmatian.",
                    FullDescription = "cocco is a pure toy Dalmatian bitch ,she is four half months old,is pedigree with 3 generation certificate.has been fully vet checked and had both vaccinations.flea treated and wormed on pancur.is use to other dogs and household noises.likes to go in the car and loves her walks.is part trainned and doing well on going outside.loves children and likes to be cuddled and played with.mum is a black toy poodle and can be seen .dad is a chocolate toy poodle with clear eye certificat a copy of eye certificate will go with cocco.i was keeping cocco but she isnt getting the attention she should be.so looking for loving caring home where she will get lots of love and attention.please ring for futher information,will only go to the right home thanks",
                    Name = "Cocco",
                    IsLitter = false,
                    IsSold = false,
                    IsFemale = true,
                    Price = 300,
                    Breed = dalmatian,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 0,
                    Headline = "6 brand new dalmations. Pedigree standard, all healthy and fit.",
                    FullDescription = "6 brand new dalmations. Pedigree standard, all healthy and fit. Ready in 5 weeks after wheaning",
                    Name = null,
                    IsLitter = true,
                    IsSold = false,
                    Price = 550,
                    Breed = dalmatian,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 5,
                    Headline = "A well behaved dalmatian.",
                    FullDescription = "Rocky is a pure toy dlamatian bitch ,she is four half months old,is pedigree with 3 generation certificate.has been fully vet checked and had both vaccinations.flea treated and wormed on pancur.is use to other dogs and household noises.likes to go in the car and loves her walks.is part trainned and doing well on going outside.loves children and likes to be cuddled and played with.mum is a black toy poodle and can be seen .dad is a chocolate toy poodle with clear eye certificat a copy of eye certificate will go with cocco.i was keeping cocco but she isnt getting the attention she should be.so looking for loving caring home where she will get lots of love and attention.please ring for futher information,will only go to the right home thanks",
                    Name = "Rocky",
                    IsLitter = false,
                    IsSold = false,
                    IsFemale = true,
                    Breed = dalmatian,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 0,
                    Headline = "6 brand new dalmations. Pedigree standard, all healthy and fit.",
                    FullDescription = "6 brand new dalmations. Pedigree standard, all healthy and fit. Ready in 3 weeks after wheaning",
                    Name = null,
                    IsLitter = true,
                    IsSold = false,
                    Price = 800,
                    Breed = dalmatian,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 0,
                    AgeInMonths = 1,
                    Headline = "6 brand new dalmations. Pedigree standard, all healthy and fit.",
                    FullDescription = "6 brand new dalmations. Pedigree standard, all healthy and fit. Ready in 3 weeks after wheaning",
                    Name = null,
                    IsLitter = true,
                    IsSold = true,
                    Breed = dalmatian,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 0,
                    Headline = "6 brand new dalmations. Pedigree standard, all healthy and fit.",
                    FullDescription = "6 brand new dalmations. Pedigree standard, all healthy and fit. Ready in 3 weeks after wheaning",
                    Name = null,
                    IsLitter = true,
                    IsSold = true,
                    Breed = dalmatian,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });
                #endregion

                #region golden retrievers
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 1,
                    Headline = "A young Golden Retriever. Well behaved and trained.",
                    FullDescription = "XXXX STUNNING ENGLISH BULLDOG PUPPIES XXXXXXXXXX. 29 CHAMPIONS INCLUDING OCOBO & MY STYLE XXXX XXXX DONT MISSS OUT ON THESE GORGEOUS BABIES XXXXX XXXXX ONLY OUR PICK OF LITTER AVAILABLE XXXXX We have a litter of kc registered english bulldogs for sale 1 boy remaing stunning examples of this breed many champions in there pedigree large heads straight tails rose ears heavy boned lot of wrinkle come with 4 weeks free insurance 1st injections kc papers mum and dad can both be seen! They are our family pets well handled use to all household noises brought up with our children no breathing problems or health issues ready to go to there new homes for more information please call us ( reduced to £1650 with kc registration papers)",
                    Name = "Goldie",
                    IsLitter = false,
                    IsSold = false,
                    Price = 325,
                    Breed = goldenRetriever,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });
                #endregion

                #region german sheperds
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 7,
                    Headline = "Middle aged female Germen Sheperds - loves kids!",
                    FullDescription = "A fairly old Alsatian requires a new home as we can't look after her anymore. Please get in touch. She likes walks and bones but other than that she's not high maintenance at all!",
                    Name = "Spud",
                    IsLitter = false,
                    IsSold = false,
                    IsFemale = true,
                    Breed = germanShepherd,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });

                #endregion

                #region mixed breed
                context.Dogs.Add(new Dog
                {
                    AgeInYears = 0,
                    AgeInMonths = 3,
                    Headline = "A litter of healthy pups - 2 males, 3 bitches!",
                    FullDescription = "A litter of healthy pups - 2 males, 3 bitches! Dad is an Alsatian, mum is a Chihuhua so bit off there",
                    Name = "Spud",
                    IsLitter = true,
                    IsSold = false,
                    Breed = mixedBreedDog,
                    CreatedByUsedId = 1,
                    PlaceId = 12472
                });
                #endregion

                #endregion

            }

            base.Seed(context);
        }

        // TODO: Is this the responsibility of this class? No - sort it out Joe
        private static bool IsDevelopmentEnvironment()
        {
            if (ConfigurationManager.AppSettings["Environment"] == "Development")
                return true;
            return false;
        }
    }
}
