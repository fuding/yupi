﻿using System;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
	public class BuildersClubMembershipMessageComposer : AbstractComposer
	{
		public void Compose(GameClient session, int expire, int maxItems) {
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(expire);
				message.AppendInteger(maxItems);
				message.AppendInteger(2); // TODO Hardcoded
				session.Send(message);
			}
		}
	}
}
