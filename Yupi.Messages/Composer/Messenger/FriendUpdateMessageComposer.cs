﻿// ---------------------------------------------------------------------------------
// <copyright file="FriendUpdateMessageComposer.cs" company="https://github.com/sant0ro/Yupi">
//   Copyright (c) 2016 Claudio Santoro, TheDoctor
// </copyright>
// <license>
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.
// </license>
// ---------------------------------------------------------------------------------
namespace Yupi.Messages.Messenger
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class FriendUpdateMessageComposer : Yupi.Messages.Contracts.FriendUpdateMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, Relationship relationship)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(0);
                message.AppendInteger(1); // TODO Refactor!

                if (relationship.Deleted)
                {
                    message.AppendInteger(-1); // DELETED
                    message.AppendInteger(relationship.Friend.Id);
                }
                else
                {
                    message.AppendInteger(0);
                    message.AppendInteger(relationship.Friend.Id);
                    message.AppendString(relationship.Friend.Name);
                    /*
                    message.AppendInteger (relationship.Friend.IsOnline);
                    message.AppendBool (!relationship.Friend.AppearOffline && relationship.Friend.IsOnline);
                    message.AppendBool (!relationship.Friend.HideInRoom && relationship.Friend.InRoom);
                    */
                    throw new NotImplementedException();
                    message.AppendString(relationship.Friend.Look);
                    message.AppendInteger(0);
                    message.AppendString(relationship.Friend.Motto);
                    message.AppendString(string.Empty);
                    message.AppendString(string.Empty);
                    message.AppendBool(true);
                    message.AppendBool(false);
                    message.AppendBool(false);
                    message.AppendShort((short) relationship.Type);
                    message.AppendBool(false);
                    session.Send(message);
                }
            }
        }

        #endregion Methods
    }
}