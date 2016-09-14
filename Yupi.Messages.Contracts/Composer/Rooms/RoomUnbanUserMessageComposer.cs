using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
    public abstract class RoomUnbanUserMessageComposer : AbstractComposer<uint, uint>
    {
        public override void Compose(Yupi.Protocol.ISender session, uint roomId, uint userId)
        {
            // Do nothing by default.
        }
    }
}