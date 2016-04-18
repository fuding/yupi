﻿using System;
using System.Collections.Generic;
using Yupi.Emulator.Game.Rooms.Data.Models;
using Yupi.Emulator.Game.Groups.Structs;
using Yupi.Emulator.Game.Catalogs.Composers;

namespace Yupi.Messages.Groups
{
	public class CreateGuildMessageEvent : AbstractHandler
	{
		// TODO Refactor
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			if (session.GetHabbo().Credits < 10)
				return;


			List<int> gStates = new List<int>();
			string name = request.GetString();
			string description = request.GetString();
			uint roomid = request.GetUInt32();
			int color = request.GetInteger();
			int num3 = request.GetInteger();

			request.GetInteger();

			int guildBase = request.GetInteger();
			int guildBaseColor = request.GetInteger();
			int num6 = request.GetInteger();

			RoomData roomData = Yupi.GetGame().GetRoomManager().GenerateRoomData(roomid);

			if (roomData.Owner != session.GetHabbo().UserName)
				return;

			for (int i = 0; i < num6*3; i++)
				gStates.Add(request.GetInteger());

			string image = Yupi.GetGame().GetGroupManager().GenerateGuildImage(guildBase, guildBaseColor, gStates);

			Group theGroup;

			Yupi.GetGame()
				.GetGroupManager()
				.CreateGroup(name, description, roomid, image, session,
					!Yupi.GetGame().GetGroupManager().SymbolColours.Contains(color) ? 1 : color,
					!Yupi.GetGame().GetGroupManager().BackGroundColours.Contains(num3) ? 1 : num3, out theGroup);

			CatalogPageComposer.PurchaseOk (0u, "CREATE_GUILD", 10);

			router.GetComposer<GroupRoomMessageComposer> ().Compose (session, roomid, theGroup.Id);

			roomData.Group = theGroup;
			roomData.GroupId = theGroup.Id;
			roomData.SerializeRoomData(Response, session, true);

			if (!session.GetHabbo().InRoom || session.GetHabbo().CurrentRoom.RoomId != roomData.Id)
			{
				session.GetMessageHandler().PrepareRoomForUser(roomData.Id, roomData.PassWord);
				session.GetHabbo().CurrentRoomId = roomData.Id;
			}

			if (session.GetHabbo().CurrentRoom != null &&
				!session.GetHabbo().CurrentRoom.LoadedGroups.ContainsKey(theGroup.Id))
				session.GetHabbo().CurrentRoom.LoadedGroups.Add(theGroup.Id, theGroup.Badge);

			if (CurrentLoadingRoom != null && !CurrentLoadingRoom.LoadedGroups.ContainsKey(theGroup.Id))
				CurrentLoadingRoom.LoadedGroups.Add(theGroup.Id, theGroup.Badge);

			if (CurrentLoadingRoom != null)
			{
				router.GetComposer<RoomGroupMessageComposer> ().Compose (CurrentLoadingRoom, CurrentLoadingRoom.LoadedGroups);


				if (session.GetHabbo ().FavouriteGroup != theGroup.Id) {
					router.GetComposer<ChangeFavouriteGroupMessageComposer> ().Compose (session, theGroup, CurrentLoadingRoom.GetRoomUserManager ().GetRoomUserByHabbo (session.GetHabbo ().Id).VirtualId);
				}
			}
		}
	}
}
