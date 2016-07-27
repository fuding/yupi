﻿using System;

namespace Yupi.Messages.User
{
	public class LoadPetInventoryMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			if (session.GetHabbo().GetInventoryComponent() == null)
				return;

			// TODO Refactor

			session.Send(session.GetHabbo().GetInventoryComponent().SerializePetInventory());
		}
	}
}

