﻿using System;


namespace Yupi.Messages.Support
{
	public class ModerationLockTradeMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			if (!session.GetHabbo().HasFuse("fuse_lock_trade"))
				return;

			uint userId = request.GetUInt32();
			string message = request.GetString();
			int length = request.GetInteger()*3600;

			ModerationTool.LockTrade(session, userId, message, length);
		}
	}
}

