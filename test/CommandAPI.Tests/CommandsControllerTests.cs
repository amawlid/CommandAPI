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

        //Test 1.1 200 OK HTTP Response when DB Empty
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
            // Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<OkObjectResult> (result);
        }

        // Test 1.1 200 OK HTTP Response when DB Empty
        //Test 1.2
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

        //Test 1.2
        //Test 1.3 Check correct Object Type Returned
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

        //Test 1.3 Check correct Object Type Returned
        //Test 1.4 Check 404 Not Found HTTP Response
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

        //Test 1.4 Check 404 Not Found HTTP Response
        //Check 200 OK HTTP Response
        [Fact]
        public void GetCommandById_Returns200OK_WhenValidIDProvided()
        {
            //
            mockRepo
                .Setup(repo => repo.GetCommandById(1))
                .Returns(new Command {
                    Id = 1,
                    HowTo = "mock",
                    Platform = "Mock",
                    CommandLine = "Mock"
                });

            var controller = new CommandsController(mockRepo.Object, mapper);

            //
            var result = controller.GetCommandById(1);

            //
            Assert.IsType<OkObjectResult>(result.Result);
        }

        //Check 200 OK HTTP Response
        //Check the Correct Object Type Returned
        [Fact]
        public void GetCommandById_Return200OK_WhenValidIDProvided()
        {
            mockRepo
                .Setup(repo => repo.GetCommandById(1))
                .Returns(new Command {
                    Id = 1,
                    HowTo = "mock",
                    Platform = "Mock",
                    CommandLine = "Mock"
                });
            var controller = new CommandsController(mockRepo.Object, mapper);

            //
            var result = controller.GetCommandById(1);

            //
            Assert.IsType<ActionResult<CommandReadDto>> (result);
        }

        //Check the Correct Object Type Returned
        //Test 3.1 Check if the correct object type is returned
        [Fact]
        public void CreateCommand_ReturnsCorrectResourceType_WhenValidObjectSubmitted()
        {
            mockRepo
                .Setup(repo => repo.GetCommandById(1))
                .Returns(new Command {
                    Id = 1,
                    HowTo = "mock",
                    Platform = "Mock",
                    CommandLine = "Mock"
                });

            var controller = new CommandsController(mockRepo.Object, mapper);

            //
            var result = controller.CreateCommand(new CommandCreateDto { });

            //
            Assert.IsType<ActionResult<CommandReadDto>> (result);
        }

        //Test 3.1 Check if the correct object type is returned
        //Test 3.2 Check 201 HTTP RESPONSE
        [Fact]
        public void CreateCommand_Returns201Created_WhenValidObjectSubmitted()
        {
            mockRepo
                .Setup(repo => repo.GetCommandById(1))
                .Returns(new Command {
                    Id = 1,
                    HowTo = "mock",
                    Platform = "Mock",
                    CommandLine = "Mock"
                });
            var controller = new CommandsController(mockRepo.Object, mapper);

            //
            var result = controller.CreateCommand(new CommandCreateDto { });

            //
            Assert.IsType<CreatedAtRouteResult>(result.Result);
        }

        //Test 3.2 Check 201 HTTP RESPONSE
        //Test 4.1 Check 204 HTTP Respons
        [Fact]
        public void UpdateCommand_Returns204NoContent_WhenValidObjectSubmitted()
        {
            mockRepo
                .Setup(repo => repo.GetCommandById(1))
                .Returns(new Command {
                    Id = 1,
                    HowTo = "mock",
                    Platform = "Mock",
                    CommandLine = "Mock"
                });
            var controller = new CommandsController(mockRepo.Object, mapper);

            //
            var result = controller.UpdateCommand(1, new CommandUpdateDto { });

            //
            Assert.IsType<NoContentResult> (result);
        }

        //Test 4.1 Check 204 HTTP Respons
        //Test 4.2 Check 404 HTTP Respons
        [Fact]
        public void UpdateCommand_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            mockRepo.Setup(repo => repo.GetCommandById(1)).Returns(() => null);

            var controller = new CommandsController(mockRepo.Object, mapper);

            //
            var result = controller.UpdateCommand(0, new CommandUpdateDto { });

            //
            Assert.IsType<NotFoundResult> (result);
        }

        //Test 4.2 Check 404 HTTP ResponsE
        //Test 5.1 Check 404 HTTP Response
        [Fact]
        public void PartialCommandUpdate_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            mockRepo.Setup(repo => repo.GetCommandById(1)).Returns(() => null);
            var controller = new CommandsController(mockRepo.Object, mapper);

            //
            var result =
                controller
                    .PartialCommandUpdate(0,
                    new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<CommandUpdateDto
                    > { });

            //
            Assert.IsType<NotFoundResult> (result);
        }

        //Test 5.1 Check 404 HTTP Response
        //Test .61 Check 404 HTTP Response DeleteCommand Unit tests
        [Fact]
        public void DeleteCommand_Returns204NoContent_WhenValidResourceIDSubmitted()
        {
            mockRepo
                .Setup(repo => repo.GetCommandById(1))
                .Returns(new Command {
                    Id = 1,
                    HowTo = "mock",
                    Platform = "Mock",
                    CommandLine = "Mock"
                });
            var controller = new CommandsController(mockRepo.Object, mapper);

            //
            var result = controller.DeleteCommand(1);

            //
            Assert.IsType<NoContentResult> (result);
        }

        //Test 6.1 Check 404 HTTP Response DeleteCommand Unit tests
        //Test 6.2 Check 404 HTTP response not found
        [Fact]
        public void DeleteCommand_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            mockRepo.Setup(repo => repo.GetCommandById(0)).Returns(() => null);
            var controller = new CommandsController(mockRepo.Object, mapper);

            //
            var result = controller.DeleteCommand(0);

            //
            Assert.IsType<NotFoundResult> (result);
        }

        //Test 6.2 Check 404 HTTP response not found
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
