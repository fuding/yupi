﻿using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Other
{
    public class SecretKeyMessageComposer : Yupi.Messages.Contracts.SecretKeyMessageComposer
    {
        public override void Compose(Yupi.Protocol.ISender session)
        {
            // TODO Public networks???

            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString("Crypto disabled");
                message.AppendBool(false);
                session.Send(message);
            }
        }
    }
}