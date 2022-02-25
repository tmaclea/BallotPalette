using BallotPalette.Core;
using BallotPalette.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace BallotPalette.Test
{
    public class BallotDataTest
    {
        private readonly IBallotData ballotData;

        public BallotDataTest()
        {
            ballotData = new InMemoryBallotData();
        }

        [Fact]
        public void GetBallotByIdTest()
        {
            //Arrange
            string ballotName = "test ballot one 561";
            int expectedId = ballotData.GetBallotsByName("").Max(b => b.Id) + 1;

            Ballot newBallot = new Ballot{ Name = ballotName};

            ballotData.Add(newBallot);
            ballotData.Commit();
            //Act
            Ballot resultBallot = ballotData.GetBallotById(expectedId);

            //Assert
            Assert.Equal("test ballot one 561", ballotData.GetBallotById(expectedId).Name);
        }

        [Fact]
        public void GetBallotsByNameTest()
        {
            //Arrange

            //make ballots with the same string of lorem ipsum
            Ballot ballot_1 = new Ballot { Name = "aaaaaaa maecenas felis aaaaaa" };
            Ballot ballot_2 = new Ballot { Name = "bbbbbbbb maecenas felis bbbbbbb" };
            Ballot ballot_3 = new Ballot { Name = "cccccccc maecenas felis cccccccc" };

            List<Ballot> expectedList = new List<Ballot>() { ballot_1, ballot_2, ballot_3 };

            ballotData.Add(ballot_1);
            ballotData.Add(ballot_2);
            ballotData.Add(ballot_3);
            ballotData.Commit();
            //Act
            List<Ballot> resultList = ballotData.GetBallotsByName("maecenas felis").ToList();
            //Assert
            Assert.Equal(expectedList, resultList);
        }

        [Fact]
        public void UpdateBallotTest()
        {
            //Arrange
            string newTitle = "Cras suscipit efficitur quam vel";
            Ballot toBeUpdated = new Ballot { Name = "Nam mollis finibus enim" };
            ballotData.Add(toBeUpdated);
            ballotData.Commit();
            
            //Act
            Ballot updatedBallot = toBeUpdated;
            updatedBallot.Name = newTitle;
            Ballot resultBallot = ballotData.Update(updatedBallot);
            ballotData.Commit();
            //Assert
            Assert.Equal(newTitle, resultBallot.Name);
            }

        [Fact]
        public void AddBallotTest()
        {
            //Arrange
            string newBallotName = "Donec id nulla at sem elementum";
            Ballot newBallot = new Ballot { Name = newBallotName };
            //Act
            ballotData.Add(newBallot);
            ballotData.Commit();

            Ballot resultBallot = ballotData.GetBallotsByName(newBallotName).FirstOrDefault();
            //Assert
            Assert.Equal(newBallot, resultBallot);
        }

        [Fact]
        public void DeleteBallotByIdTest()
        {
            //Arrange
            string newBallotName = "Ac commodo nisi imperdiet ut";
            Ballot newBallot = new Ballot { Name = newBallotName };

            ballotData.Add(newBallot);
            ballotData.Commit();

            Ballot ballotToDelete = ballotData.GetBallotsByName(newBallotName).FirstOrDefault();
            //Act
            ballotData.Delete(ballotToDelete.Id);
            ballotData.Commit();
            //Assert
            Assert.Null(ballotData.GetBallotsByName(newBallotName).FirstOrDefault());
        }

        [Fact]
        public void DeleteBallotByNameTest()
        {
            //Arrange
            string newBallotName = "Vivamus viverra viverra tellus";
            Ballot newBallot = new Ballot { Name = newBallotName };

            ballotData.Add(newBallot);
            ballotData.Commit();
            //Act
            ballotData.Delete(newBallotName);
            ballotData.Commit();
            //Assert
            Assert.Null(ballotData.GetBallotsByName(newBallotName).FirstOrDefault());
        }
    }
}
