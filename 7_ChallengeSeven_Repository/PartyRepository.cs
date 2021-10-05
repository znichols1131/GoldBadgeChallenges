using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7_ChallengeSeven_Repository
{
    public class PartyRepository
    {
        private List<Party> _listOfParties = new List<Party>();
        private int _nextPartyNumber = 0;

        // Create
        public bool CreateParty(Party party)
        {
            if (party is null)
            {
                return false;
            }

            // Keep IDs unique
            party.PartyID = _nextPartyNumber;
            _nextPartyNumber++;

            int before = _listOfParties.Count();
            _listOfParties.Add(party);
            int after = _listOfParties.Count();

            if (before < after)
            {
                return true;
            }

            return false;
        }

        // Read
        public List<Party> GetAllParties()
        {
            return _listOfParties;
        }

        public Party GetPartyForID(int partyID)
        {
            if (partyID < 0 || _listOfParties.Count == 0)
            {
                return null;
            }

            foreach (Party party in _listOfParties)
            {
                if (party.PartyID == partyID)
                {
                    return party;
                }
            }

            return null;
        }

        // Update
        public bool UpdatePartyForID(int partyID, Party newParty)
        {
            if (partyID < 0 || newParty is null || _listOfParties.Count == 0)
            {
                return false;
            }

            Party oldParty = GetPartyForID(partyID);
            if (oldParty is null)
            {
                return false;
            }

            oldParty.PartyID = newParty.PartyID;
            oldParty.Purpose = newParty.Purpose;
            oldParty.Date = newParty.Date;
            oldParty.Booths = newParty.Booths;

            return true;
        }

        // Delete
        public bool DeletePartyForID(int partyID)
        {
            Party party = GetPartyForID(partyID);
            if (party is null || _listOfParties.Count == 0)
            {
                return false;
            }

            int before = _listOfParties.Count();
            _listOfParties.Remove(party);
            int after = _listOfParties.Count();

            if (before > after)
            {
                return true;
            }

            return false;
        }

        // Helper methods (if any)
    }
}
