﻿using System;
using Yupi.Emulator.Game.Groups.Structs;

namespace Yupi.Messages.Groups
{
	public class GetGroupMembersMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			uint groupId = request.GetUInt32();
			int page = request.GetInteger();
			string searchVal = request.GetString();
			uint reqType = request.GetUInt32();

			Group group = Yupi.GetGame().GetGroupManager().GetGroup(groupId);

			Response.Init(PacketLibraryManager.OutgoingHandler("GroupMembersMessageComposer"));

			Yupi.GetGame().GetGroupManager().SerializeGroupMembers(Response, group, reqType, Session, searchVal, page);
		}
	}
}
