using System;
using System.Collections.Generic;

namespace Generics.Robots
{
    public interface RobotAI<out T>
    {
        T GetCommand();
    }

    public class ShooterAI : RobotAI<ShooterCommand>
    {
        int counter = 1;

        public ShooterCommand GetCommand()
        {
            return ShooterCommand.ForCounter(counter++);
        }
    }

    public class BuilderAI : RobotAI<BuilderCommand>
    {
        int counter = 1;

        public BuilderCommand GetCommand()
        {
            return BuilderCommand.ForCounter(counter++);
        }
    }

    public interface Device<in T>
    {
        string ExecuteCommand(T command);
    }

    public class Mover : Device<IMoveCommand>
    {
        public string ExecuteCommand(IMoveCommand command)
        {
            if (command == null)
                throw new ArgumentException();
            return $"MOV {command.Destination.X}, {command.Destination.Y}";
        }
    }

    public class ShooterMover : Device<IMoveCommand>
    {
        public string ExecuteCommand(IMoveCommand _command)
        {
            var command = _command as IShooterMoveCommand;
            if (command == null)
                throw new ArgumentException();
            var hide = command.ShouldHide ? "YES" : "NO";
            return $"MOV {command.Destination.X}, {command.Destination.Y}, USE COVER {hide}";
        }
    }

    public class Robot
    {
        private readonly RobotAI<IMoveCommand> ai;
        private readonly Device<IMoveCommand> device;

        private Robot(RobotAI<IMoveCommand> ai, Device<IMoveCommand> executor)
        {
            this.ai = ai;
            this.device = executor;
        }

        public IEnumerable<string> Start(int steps)
        {
            for (int i = 0; i < steps; i++)
            {
                var command = ai.GetCommand();
                if (command == null)
                    break;
                yield return device.ExecuteCommand(command);
            }
        }

        public static Robot Create<TCommand>(RobotAI<TCommand> ai, Device<TCommand> executor) where TCommand: IMoveCommand
        {
            return new Robot(ai as RobotAI<IMoveCommand>, executor as Device<IMoveCommand>);
        }
    }
}