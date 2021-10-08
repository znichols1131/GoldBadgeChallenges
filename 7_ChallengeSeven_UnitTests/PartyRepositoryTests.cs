using _7_ChallengeSeven_Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace _7_ChallengeSeven_UnitTests
{
    [TestClass]
    public class PartyRepositoryTests
    {
        private PartyRepository _repo = new PartyRepository();

        [TestInitialize]
        public void Arrange()
        {
            // Need some test data
            Party partyOne = new Party("Zach's birthday", DateTime.Today);
            _repo.CreateParty(partyOne);
        }

        // Create
        [TestMethod]
        public void CreateParty_PartyIsNull_ReturnFalse()
        {
            // Arrange
            Party party = null;

            // Act
            bool success = _repo.CreateParty(party);

            // Assert
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void CreateParty_PartyExists_ReturnTrue()
        {
            // Arrange
            Party party = new Party("Molly's graduation", DateTime.Now);

            // Act
            bool success = _repo.CreateParty(party);

            // Assert
            Assert.IsTrue(success);
        }


        // Read
        [TestMethod]
        public void GetAllParties_ListExists_ReturnList()
        {
            // Arrange (nothing on this one)

            // Act
            List<Party> list = _repo.GetAllParties();

            // Assert
            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void GetPartyForID_PartyDoesNotExist_ReturnNull()
        {
            // Arrange
            int partyID = 1;

            // Act
            Party party = _repo.GetPartyForID(partyID);

            // Assert
            Assert.IsNull(party);
        }

        [TestMethod]
        public void GetPartyForID_PartyExists_ReturnItem()
        {
            // Arrange
            int partyID = 0;

            // Act
            Party party = _repo.GetPartyForID(partyID);

            // Assert
            Assert.IsNotNull(party);
        }


        // Update
        [TestMethod]
        public void UpdatePartyForID_PartyDoesNotExist_ReturnFalse()
        {
            // Arrange
            int partyID = 1;  // Out of range
            Party newParty = new Party("Molly's graduation", DateTime.Now);

            // Act
            bool success = _repo.UpdatePartyForID(partyID, newParty);

            // Assert
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void UpdatePartyForID_NewPartyIsNull_ReturnFalse()
        {
            // Arrange
            int partyID = 0;
            Party newParty = null;

            // Act
            bool success = _repo.UpdatePartyForID(partyID, newParty);

            // Assert
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void UpdatePartyForID_PartyIsUpdated_ReturnTrue()
        {
            // Arrange
            int partyID = 0;
            Party newParty = new Party("Molly's graduation", DateTime.Now);

            // Act
            bool success = _repo.UpdatePartyForID(partyID, newParty);

            // Assert
            Assert.IsTrue(success);
        }


        // Delete
        [TestMethod]
        public void DeletePartyForID_PartyDoesNotExist_ReturnFalse()
        {
            // Arrange
            int partyID = 1;  // Out of range

            // Act
            bool success = _repo.DeletePartyForID(partyID);

            // Assert
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void DeletePartyForID_PartyExists_ReturnTrue()
        {
            // Arrange
            int partyID = 0;

            // Act
            bool success = _repo.DeletePartyForID(partyID);

            // Assert
            Assert.IsTrue(success);
        }
    }
}
