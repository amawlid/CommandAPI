using System.Collections.Generic;
using CommandAPI.Models;

namespace CommandAPI.Data
{
    public class MockCommandAPIRepo : ICommandAPIRepo
    {
        public void CreateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>
            {
                new Command{
                    Id=0, HowTo="Learning to make API using C# .NET Core", 
                    CommandLine="It's going well for im done with chapter 6", 
                    Platform=".Net Core EF"},
                new Command{
                    Id=1, HowTo="Starting on chapter 7", 
                    CommandLine="chapter 7 is about database or something like that", 
                    Platform=".Net Core EF"},
                new Command{
                    Id=2, HowTo="List active migrations", 
                    CommandLine="dotnet ef migrations list", 
                    Platform=".Net Core EF"}
            };

            return commands;
        }

        public Command GetCommandById(int id)
        {
            return new Command{
                Id=0, HowTo="How to genrate a migration", 
                CommandLine="dotnet ef migrations add <Name of Migration>", 
                Platform=".Net Core EF"};
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }
    }
}