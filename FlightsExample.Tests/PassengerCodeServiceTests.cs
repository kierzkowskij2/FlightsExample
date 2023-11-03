using FlightsExample.Core.Dtos;
using FlightsExample.Core.Services;
using FlightsExample.Services.Services;

namespace FlightsExample.Tests
{
    public class PassengerCodeServiceTests
    {
        private IPassengerCodeService _passengerCodeService;
        private CreatePassengerCodeRequest _basicRequest;
        [SetUp]
        public void Setup()
        {
            _passengerCodeService = new PassengerCodeService();
            _basicRequest = new CreatePassengerCodeRequest()
            {
                Age = 20,
                FlightClass = FlightClass.Economy,
                Source = Destination.Asia,
                Destination = Destination.UK,
                StartTime = new DateTime(2023, 10, 1, 12, 0, 0),
                EndTime = new DateTime(2023, 10, 1, 16, 0, 0),
                Gender = Gender.Male,
                Meal = Meal.Asian
            };
        }

        [Test]
        public void AgeBelow12MonthsShouldReturnFalse()
        {
            var dto = new CreatePassengerCodeRequest()
            {
                Age = 0
            };
            var response = _passengerCodeService.Create(dto);
            Assert.Pass();
        }

        [Test]
        public void AgeAbove80MonthsShouldReturnFalse()
        {
            var dto = new CreatePassengerCodeRequest()
            {
                Age = 0
            };
            var response = _passengerCodeService.Create(dto);
            Assert.IsFalse(response.Success);
        }

        [Test]
        public void AdultOrderChildMealShouldReturnFalse()
        {
            var dto = new CreatePassengerCodeRequest()
            {
                Age = 81
            };
            var response = _passengerCodeService.Create(dto);
            Assert.IsFalse(response.Success);
        }

        [Test]
        public void FirstClassUKSourceShouldReturnFalse()
        {
            var dto = new CreatePassengerCodeRequest()
            {
                Age = 20,
                FlightClass = FlightClass.First,
                Source = Destination.UK,
                Destination = Destination.Asia
            };
            var response = _passengerCodeService.Create(dto);
            Assert.IsFalse(response.Success);
        }

        [Test]
        public void FirstClassUKDestinationShouldReturnFalse()
        {
            var dto = new CreatePassengerCodeRequest()
            {
                Age = 20,
                FlightClass = FlightClass.First,
                Source = Destination.Asia,
                Destination = Destination.UK
            };
            var response = _passengerCodeService.Create(dto);
            Assert.IsFalse(response.Success);
        }

        [Test]
        public void NewPassengerWithValidFieldsShouldReturnTrueAndHaveValidCode()
        {
            var response = _passengerCodeService.Create(_basicRequest);
            Assert.True(response.Success);
            Assert.AreEqual(response.PassengerCode, "UXHR-ZZ");
        }

        [Test]
        public void NewPassengerWithUKDestinationShouldReturnCodeWithU()
        {
            var dto = _basicRequest;
            dto.Destination = Destination.UK;
            var response = _passengerCodeService.Create(dto);
            Assert.AreEqual(response.PassengerCode.Substring(0, 1), "U");
        }

        [Test]
        public void NewPassengerWithEuropeDestinationShouldReturnCodeWithE()
        {
            var dto = _basicRequest;
            dto.Destination = Destination.Europe;
            var response = _passengerCodeService.Create(dto);
            Assert.AreEqual(response.PassengerCode.Substring(0, 1), "E");
        }


        [Test]
        public void NewPassengerWithAsiaDestinationShouldReturnCodeWithA()
        {
            var dto = _basicRequest;
            dto.Destination = Destination.Asia;
            var response = _passengerCodeService.Create(dto);
            Assert.AreEqual(response.PassengerCode.Substring(0, 1), "A");
        }

        [Test]
        public void NewPassengerWithAmericaDestinationShouldReturnCodeWithZ()
        {
            var dto = _basicRequest;
            dto.Destination = Destination.America;
            var response = _passengerCodeService.Create(dto);
            Assert.AreEqual(response.PassengerCode.Substring(0, 1), "Z");
        }

        [Test]
        public void NewPassengerWithNightFlightReturnCodeWithFirstSmallLetter()
        {
            var dto = _basicRequest;
            dto.StartTime = new DateTime(2023, 10, 1, 23, 0, 0);
            dto.EndTime = new DateTime(2023, 10, 2, 5, 0, 0);
            var response = _passengerCodeService.Create(dto);
            Assert.True(Char.IsLower(response.PassengerCode.Substring(0, 1).First()));
        }

        [Test]
        public void NewPassengerMaleShouldReturnCodeWithX()
        {
            var dto = _basicRequest;
            dto.Gender = Gender.Male;
            var response = _passengerCodeService.Create(dto);
            Assert.AreEqual(response.PassengerCode.Substring(1, 1), "X");
        }

        [Test]
        public void NewPassengerFemaleShouldReturnCodeWithY()
        {
            var dto = _basicRequest;
            dto.Gender = Gender.Female;
            var response = _passengerCodeService.Create(dto);
            Assert.AreEqual(response.PassengerCode.Substring(1, 1), "Y");
        }

        [Test]
        public void NewPassengerFemaleChildShouldReturnCodeWithLowerY()
        {
            var dto = _basicRequest;
            dto.Gender = Gender.Female;
            dto.Age = 11;
            var response = _passengerCodeService.Create(dto);
            Assert.AreEqual(response.PassengerCode.Substring(1, 1), "y");
        }

        [Test]
        public void NewPassengerMealEuropeanShouldReturnCodeWithG()
        {
            var dto = _basicRequest;
            dto.Meal = Meal.European;
            var response = _passengerCodeService.Create(dto);
            Assert.AreEqual(response.PassengerCode.Substring(2, 1), "G");
        }

        [Test]
        public void NewPassengerMealAsianShouldReturnCodeWithH()
        {
            var dto = _basicRequest;
            dto.Meal = Meal.Asian;
            var response = _passengerCodeService.Create(dto);
            Assert.AreEqual(response.PassengerCode.Substring(2, 1), "H");
        }

        [Test]
        public void NewPassengerMealVegeterianShouldReturnCodeWithK()
        {
            var dto = _basicRequest;
            dto.Meal = Meal.Vegeterian;
            var response = _passengerCodeService.Create(dto);
            Assert.AreEqual(response.PassengerCode.Substring(2, 1), "K");
        }

        [Test]
        public void NewPassengerChildMealVegeterianShouldReturnCodeWithLowerK()
        {
            var dto = _basicRequest;
            dto.Meal = Meal.VegeterianChild;
            dto.Age = 11;
            var response = _passengerCodeService.Create(dto);
            Assert.AreEqual(response.PassengerCode.Substring(2, 1), "k");
        }

        [Test]
        public void NewPassengerFirstClassShouldReturnCodeWithP()
        {
            var dto = _basicRequest;
            dto.Destination = Destination.Asia;
            dto.Source = Destination.Asia;
            dto.FlightClass = FlightClass.First;
            var response = _passengerCodeService.Create(dto);
            Assert.AreEqual(response.PassengerCode.Substring(3, 1), "P");
        }

        [Test]
        public void NewPassengerBusinessClassShouldReturnCodeWithQ()
        {
            var dto = _basicRequest;
            dto.FlightClass = FlightClass.Business;
            var response = _passengerCodeService.Create(dto);
            Assert.AreEqual(response.PassengerCode.Substring(3, 1), "Q");
        }

        [Test]
        public void NewPassengerEconomyClassShouldReturnCodeWithR()
        {
            var dto = _basicRequest;
            dto.FlightClass = FlightClass.Economy;
            var response = _passengerCodeService.Create(dto);
            Assert.AreEqual(response.PassengerCode.Substring(3, 1), "R");
        }

        [Test]
        public void NewPassengerFromEuropeanCountryShouldReturnCodeWithEUAppended()
        {
            var dto = _basicRequest;
            dto.Source = Destination.Europe;
            var response = _passengerCodeService.Create(dto);
            Assert.AreEqual(response.PassengerCode.Substring(4, 3), "-EU");
        }

        [Test]
        public void NewPassengerNotFromEuropeanCountryShouldReturnCodeWithZZAppended()
        {
            var dto = _basicRequest;
            dto.Source = Destination.America;
            var response = _passengerCodeService.Create(dto);
            Assert.AreEqual(response.PassengerCode.Substring(4, 3), "-ZZ");
        }
    }
}