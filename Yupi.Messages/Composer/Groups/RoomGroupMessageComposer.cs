﻿using System;
using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using Yupi.Emulator.Game.Rooms;

namespace Yupi.Messages.Groups
{
	public class RoomGroupMessageComposer : AbstractComposer
	{
		public override void Compose (Yupi.Protocol.ISender room)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(room.LoadedGroups.Count);

				foreach (KeyValuePair<uint, string> current in room.LoadedGroups)
				{
					message.AppendInteger(current.Key);
					message.AppendString(current.Value);
				}

				room.Send (message);
			}
		}
	}
}

