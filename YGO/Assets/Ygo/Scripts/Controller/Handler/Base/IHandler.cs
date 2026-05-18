using Cysharp.Threading.Tasks;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;

namespace Ygo.Controller.Handler.Base
{
    public interface IHandler
    {
        UniTask HandleMessage(IDuelMessage message);
    }
    
    public interface IHandler<in T> : IHandler where T : IDuelMessage
    {
        UniTask HandleMessage(T message);
    }
}