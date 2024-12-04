
namespace Core.Entity
{
    public interface IMoveState
    {
        void Enter(EMove data);
        void Update(EMove data);
        void Exit(EMove data);
    }

}

