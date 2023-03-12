using System.Collections.Generic;
using UnityEngine;

namespace GTools.Command
{
    public class CommandList
    {
        private List<ICommand> commandList = new List<ICommand>();
        private int currentIndex = -1;

        public void AddCommand(ICommand command)
        {
            if (currentIndex < commandList.Count - 1)
            {
                commandList.RemoveRange(currentIndex + 1, commandList.Count - currentIndex - 1);
            }
            commandList.Add(command);
            currentIndex++;
        }

        public void ExecuteCommand()
        {
            if (currentIndex >= 0 && currentIndex < commandList.Count)
            {
                commandList[currentIndex].Execute();
            }
        }

        public void UndoCommand()
        {
            if (currentIndex >= 0 && currentIndex < commandList.Count)
            {
                commandList[currentIndex].Undo();
                currentIndex--;
            }
        }
    }
}


