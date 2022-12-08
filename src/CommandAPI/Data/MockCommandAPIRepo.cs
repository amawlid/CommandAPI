using System.Collections.Generic;
using CommandAPI.Models;
namespace CommandAPI.Data
{
     public class MockCommandAPIRepo : ICommandAPIRepo
     {
        public void CreateCommand(Command cmd){
            throw new System.NotImplementedException();
        }

        public void DeleteCommand(Command cmd){
            throw new System.NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands(){
            var commands = new List<CommandAPI>
            {ø
            new Command{
                Id=0, HowTo="How to generate a migration writen by Ahmed",
                CommandLine="dotnet ef migrations add <Name of Migration>",
                Platform=".Net Core EF"},
            new Command{
                Id=1, HowTo="Run Migration",
                CommandLine="dotnet ef database update",
                Platform=".Net Core EF"},
            new Command{
                Id=0, HowTo="List active migrations",
                CommandLine="dotnet ef migrations list",
                Platform=".Net Core EF"}
            };
            return commands;
        }

        public Command GetCommandById(int id){
            return new Command{
                Id=0, HowTo="How to generate a migration",
                CommandLine="dotnet ef migration add <Name of Migration>",
                Platform=".Net Core EF"
            }
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCommand(Command cmd)
        {
            throw new System.NotImplementedException
        }
     }
}