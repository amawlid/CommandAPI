using System;
using System.Collections.Generic;
using AutoMapper;
using CommandAPI.Controllers;
using CommandAPI.Data;
using CommandAPI.Dtos;
using CommandAPI.Models;
using CommandAPI.Profiles;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CommandAPI.ControllerTests
{
    public class CommandsControllerTests : IDisposable
    {
        Mock<ICommandAPIRepo> mockRepo;

        CommandsProfile realProfile;

        MapperConfiguration configuration;

        IMapper mapper;

        public CommandsControllerTests()
        {
            mockRepo = new Mock<ICommandAPIRepo>();
            realProfile = new CommandsProfile();
            configuration =
                new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
            mapper = new Mapper(configuration);
        }

        public void Dispose()
        {
            mockRepo = null;
            mapper = null;
            configuration = null;
            realProfile = null;
        }

        //Testing 200 OK HTTP Response when DB Empty
        [Fact]
        public void GetCommandItem_ReturnsZeroItems_whenDBIsempty()
        {
            //Arrange
            var mockRepo = new Mock<ICommandAPIRepo>();

            mockRepo
                .Setup(repo => repo.GetAllCommands())
                .Returns(GetCommands(0));

            var realProfile = new CommandsProfile();
            var configuration =
                new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
            IMapper mapper = new Mapper(configuration);

            var controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetAllCommands();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        // Testing 200 OK HTTP Response when DB Empty
        //Return http response 200
        [Fact]
        public void GetAllCommands_Return200OK_WhenDBHasOneResource()
        {
            //Arrange
            mockRepo
                .Setup(repo => repo.GetAllCommands())
                .Returns(GetCommands(1));
            var controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetAllCommands();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        //Return http response 200
        //Check correct Object Type Returned
        [Fact]
        public void GetAllCommands_ReturnCorrectType_WhenDBHasOneResource()
        {
            //Arrange
            mockRepo
                .Setup(repo => repo.GetAllCommands())
                .Returns(GetCommands(1));
            var controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetAllCommands();

            //Assert
            Assert.IsType<ActionResult<IEnumerable<CommandReadDto>>> (result);
        }

        //Check correct Object Type Returned
        //Check 404 Not Found HTTP Response
        [Fact]
        public void GetCommandById_Returns404NotFound_WhenNonExistentIdProvided()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetCommandById(0)).Returns(() => null);
            var controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetCommandById(1);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        //Check 404 Not Found HTTP Response
        private List<Command> GetCommands(int num)
        {
            var commands = new List<Command>();
            if (num > 0)
            {
                commands
                    .Add(new Command {
                        Id = 0,
                        HowTo = "How to generate a migration",
                        CommandLine =
                            "dotnet ef migrations add <Name of Migration>",
                        Platform = ".Net Core EF"
                    });
            }
            return commands;
        }
    }
}
