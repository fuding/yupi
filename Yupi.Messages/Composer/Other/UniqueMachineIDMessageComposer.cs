﻿using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Other
{
	public class UniqueMachineIDMessageComposer : Yupi.Messages.Contracts.UniqueMachineIDMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, string machineId)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString(machineId);
				session.Send (message);
			}
		}
	}
}
