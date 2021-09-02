using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using BakuchiApi.Models.Dtos.Validators;
using System.ComponentModel.DataAnnotations;

namespace BakuchiApi.Tests.UnitTests.Validators
{
    internal class DateIsNotMoreThanOneYearLaterTests
    {
        private DateIsNotMoreThanOneYearLater validator;

        [SetUp]
        public void Setup()
        {
            validator = new DateIsNotMoreThanOneYearLater();
        }

        [Test]
        public void AssertValidDateReturnsTrue()
        {
            var date = DateTime.Now.AddDays(1);
            var result = validator.IsValid(date);
            Assert.IsTrue(result);
        }

        [Test]
        public void AssertDatePastOneYearInTheFutureReturnsFalse()
        {
            var date = DateTime.Now.AddDays(366);
            var result = validator.IsValid(date);
            Assert.IsFalse(result);
        }

        [Test]
        public void AssertNullDateReturnsFalse()
        {
            var result = validator.IsValid(null);
            Assert.IsFalse(result);
        }
    }
}