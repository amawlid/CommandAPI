using System;
using CommandAPI.Models;
using Xunit;

namespace CommandAPI.Tests
{
    public class CommandTests : IDisposable
    {
        Command testCommand;

        //Arrange
        public CommandTests()
        {
            testCommand =
                new Command {
                    HowTo = "Do something",
                    Platform = "some platform",
                    CommandLine = "some commandline"
                };
        }

        public void Dispose()
        {
            testCommand = null;
        }

        [Fact]
        public void CanChangeHowTo()
        {
            //Act
            testCommand.HowTo = "Execute Unit Tests";

            //Assert
            Assert.Equal("Execute Unit Tests", testCommand.HowTo);
        }

        [Fact]
        public void CanChangePlatform()
        {
            //Act
            testCommand.Platform = "New Platform test";

            //Assert
            Assert.Equal("New Platform test", testCommand.Platform);
        }

        [Fact]
        public void CanChangeCommandLine()
        {
            //Act
            testCommand.CommandLine = "New Command Line";

            //Assert
            Assert.Equal("New Command Line", testCommand.CommandLine);
        }
    }
}
