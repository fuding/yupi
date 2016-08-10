﻿using System;
using System.Linq;
using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using Yupi.Model.Domain;


namespace Yupi.Messages.Catalog
{
	public class CatalogueIndexMessageComposer : Yupi.Messages.Contracts.CatalogueIndexMessageComposer
	// TODO Refactor
	{
		public override void Compose ( Yupi.Protocol.ISender session, IList<CatalogPage> sortedPages, string type)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendBool(true);
				message.AppendInteger(0);
				message.AppendInteger(-1);
				message.AppendString("root");
				message.AppendString(string.Empty);
				message.AppendInteger(0);
				message.AppendInteger(sortedPages.Count());

				foreach (CatalogPage cat in sortedPages)
				{
					message.AppendBool(cat.Visible);
					message.AppendInteger(cat.IconImage);
					message.AppendInteger(cat.Id);
					message.AppendString(cat.CodeName);
					message.AppendString(cat.Caption);
					message.AppendInteger(cat.FlatOffers.Count);

					foreach (uint i in cat.FlatOffers.Keys)
						message.AppendInteger(i);
					// TODO Refactor pages to TREE
					IOrderedEnumerable<CatalogPage> sortedSubPages =
						pages.Where(x => x.ParentId == cat.PageId && x.MinRank <= rank).OrderBy(x => x.OrderNum);

					message.AppendInteger(sortedSubPages.Count());

					foreach (CatalogPage subCat in sortedSubPages)
					{
						message.AppendBool(subCat.Visible);
						message.AppendInteger(subCat.IconImage);
						message.AppendInteger(subCat.Id);
						message.AppendString(subCat.CodeName);
						message.AppendString(subCat.Caption);
						message.AppendInteger(subCat.FlatOffers.Count);

						foreach (uint i2 in subCat.FlatOffers.Keys)
							message.AppendInteger(i2);

						message.AppendInteger(0);
					}
				}

				message.AppendBool(false);
				message.AppendString(type);

				session.Send (message);
			}
		}
	}
}
