﻿using System;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Guides
{
	public class HelperToolConfigurationMessageComposer : AbstractComposer
	{
		public void Compose(GameClient session, bool onDuty, int guideCount, int helperCount, int guardianCount) {
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendBool(onDuty);
				message.AppendInteger(guideCount);
				message.AppendInteger(helperCount);
				message.AppendInteger(guardianCount);
				session.Send (message);
			}
		}
	}
}
