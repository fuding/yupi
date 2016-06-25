﻿using System;
using Yupi.Emulator.Game.Rooms.User;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;

namespace Yupi.Messages.Items
{
	public class PickUpBotMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			uint botId = request.GetUInt32();

			Room room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);
			RoomUser bot = room.GetRoomUserManager().GetBot(botId);

			if (session?.GetHabbo() == null || session.GetHabbo().GetInventoryComponent() == null || bot == null ||
				!room.CheckRights(session, true))
				return;

			session.GetHabbo().GetInventoryComponent().AddBot(bot.BotData);

			using (IQueryAdapter queryreactor2 = Yupi.GetDatabaseManager().GetQueryReactor()) {
				queryreactor2.SetQuery ("UPDATE bots_data SET room_id = '0' WHERE id = @id");
				queryreactor2.AddParameter ("id", botId);
				queryreactor2.RunQuery ();
			}
			room.GetRoomUserManager().RemoveBot(bot.VirtualId, false);
			bot.BotData.WasPicked = true;

			session.SendMessage(session.GetHabbo().GetInventoryComponent().SerializeBotInventory());
		}
	}
}
