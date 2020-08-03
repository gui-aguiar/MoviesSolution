using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Movies.Models;

namespace Movies.Business.Tests
{
    /// <summary>
    /// Example of a Test Class to test the business level. In this class the GenderBusiness class is tested
    /// For the tests, a GenderListRepository instance is created and used by the GenderBusiness as repository
    /// </summary>

    [TestClass]
    public class GenderBusinessTests
    {
        #region Fields
        private GenderBusiness _genderBusiness;
        private GenderListRepository _repository;
        #endregion

        [TestInitialize]
        public void Setup()
        {
            _repository = new GenderListRepository();
            _genderBusiness = new GenderBusiness(_repository);
        }

        [TestMethod]
        public void AddGender()
        {
            Gender gender = new Gender
            {
                Enabled = false,
                Name = "gender test",
                CreationDateTime = DateTime.Now
            };
            _genderBusiness.Add(gender);

            Assert.AreNotEqual(gender.Id, -1);

            var repoGender = _genderBusiness.Get(gender.Id);
                                
            Assert.IsNotNull(repoGender);
            Assert.AreEqual(repoGender.Id, gender.Id);
            Assert.AreEqual(repoGender.Enabled, gender.Enabled);
            Assert.AreEqual(repoGender.Name, gender.Name);
            Assert.AreEqual(repoGender.CreationDateTime, gender.CreationDateTime);            
        }

        [TestMethod]
        public void GetGender()
        {
            Gender gender = new Gender
            {
                Enabled = false,
                Name = "gender test",
                CreationDateTime = DateTime.Now
            };
            _genderBusiness.Add(gender);

            Assert.AreNotEqual(gender.Id, -1);
            
            var genderId = gender.Id;
            var repoGender = _genderBusiness.Get(genderId);

            Assert.IsNotNull(repoGender);
            Assert.AreEqual(repoGender.Id, genderId);
        }

        [TestMethod]
        public void UpdateGender()
        {
            Gender gender = new Gender
            {
                Enabled = false,
                Name = "gender test",
                CreationDateTime = DateTime.Now
            };
            _genderBusiness.Add(gender);

            Assert.AreNotEqual(gender.Id, -1);
            var genderId = gender.Id;

            Gender genderNewValues = new Gender
            {
                Enabled = true,
                Name = "another name",
                CreationDateTime = DateTime.Now.AddMinutes(1)
            };

            _genderBusiness.Update(genderId, genderNewValues);
            var repoGender = _genderBusiness.Get(genderId);

            Assert.IsNotNull(repoGender);            
            Assert.AreEqual(repoGender.Enabled, genderNewValues.Enabled);
            Assert.AreEqual(repoGender.Name, genderNewValues.Name);
            Assert.AreEqual(repoGender.CreationDateTime, genderNewValues.CreationDateTime);
        }

        [TestMethod]
        public void DeleteGender()
        {
            Gender gender = new Gender
            {
                Enabled = false,
                Name = "gender test",
                CreationDateTime = DateTime.Now
            };
            _genderBusiness.Add(gender);

            Assert.AreNotEqual(gender.Id, -1);

            var genderId = gender.Id;
            _genderBusiness.Delete(genderId);

            var repoGender = _genderBusiness.Get(genderId);
            Assert.IsNull(repoGender);            
        }
    }
}
