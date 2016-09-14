﻿using System;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;
using Yupi.Controller;
using Yupi.Model;


namespace Yupi.Messages.Support
{
    public class ModerationRoomToolMessageComposer : Yupi.Messages.Contracts.ModerationRoomToolMessageComposer
    {
        private RoomManager RoomManager;

        public ModerationRoomToolMessageComposer()
        {
            RoomManager = DependencyFactory.Resolve<RoomManager>();
        }

        // TODO Refactor
        public override void Compose(Yupi.Protocol.ISender session, RoomData data, bool isLoaded)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(data.Id);
                message.AppendInteger(RoomManager.UsersNow(data));

                message.AppendBool(false); // TODO Meaning? (isOwnerInRoom?)

                message.AppendInteger(data.Owner.Id);
                message.AppendString(data.Owner.Name);
                message.AppendBool(isLoaded);
                message.AppendString(data.Name);
                message.AppendString(data.Description);
                message.AppendInteger(data.Tags.Count);

                foreach (string current in data.Tags)
                    message.AppendString(current);

                message.AppendBool(false);

                session.Send(message);
            }
        }
    }
}