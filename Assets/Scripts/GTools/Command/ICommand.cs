namespace GTools.Command
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }

}
