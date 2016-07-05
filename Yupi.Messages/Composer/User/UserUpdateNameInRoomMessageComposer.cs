﻿using System;

using Yupi.Protocol.Buffers;


namespace Yupi.Messages.User
{
	public class UserUpdateNameInRoomMessageComposer : AbstractComposer
	{
		public override void Compose (Yupi.Protocol.ISender room, Habbo habbo, string newName)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (habbo.Id);
				message.AppendInteger (habbo.CurrentRoom.RoomId);
				message.AppendString (newName);
				room.Send (message);
			}
		}
	}
}

