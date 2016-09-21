// <auto-generated/>
using System;

namespace Telegram.Api.TL.Methods.Messages
{
	/// <summary>
	/// RCP method messages.editInlineBotMessage
	/// </summary>
	public partial class TLMessagesEditInlineBotMessage : TLObject
	{
		[Flags]
		public enum Flag : Int32
		{
			NoWebpage = (1 << 1),
			Message = (1 << 11),
			ReplyMarkup = (1 << 2),
			Entities = (1 << 3),
		}

		public bool IsNoWebpage { get { return Flags.HasFlag(Flag.NoWebpage); } set { Flags = value ? (Flags | Flag.NoWebpage) : (Flags & ~Flag.NoWebpage); } }
		public bool HasMessage { get { return Flags.HasFlag(Flag.Message); } set { Flags = value ? (Flags | Flag.Message) : (Flags & ~Flag.Message); } }
		public bool HasReplyMarkup { get { return Flags.HasFlag(Flag.ReplyMarkup); } set { Flags = value ? (Flags | Flag.ReplyMarkup) : (Flags & ~Flag.ReplyMarkup); } }
		public bool HasEntities { get { return Flags.HasFlag(Flag.Entities); } set { Flags = value ? (Flags | Flag.Entities) : (Flags & ~Flag.Entities); } }

		public Flag Flags { get; set; }
		public TLInputBotInlineMessageID Id { get; set; }
		public String Message { get; set; }
		public TLReplyMarkupBase ReplyMarkup { get; set; }
		public TLVector<TLMessageEntityBase> Entities { get; set; }

		public TLMessagesEditInlineBotMessage() { }
		public TLMessagesEditInlineBotMessage(TLBinaryReader from, bool cache = false)
		{
			Read(from, cache);
		}

		public override TLType TypeId { get { return TLType.MessagesEditInlineBotMessage; } }

		public override void Read(TLBinaryReader from, bool cache = false)
		{
			Flags = (Flag)from.ReadInt32();
			Id = TLFactory.Read<TLInputBotInlineMessageID>(from, cache);
			if (HasMessage) Message = from.ReadString();
			if (HasReplyMarkup) ReplyMarkup = TLFactory.Read<TLReplyMarkupBase>(from, cache);
			if (HasEntities) Entities = TLFactory.Read<TLVector<TLMessageEntityBase>>(from, cache);
			if (cache) ReadFromCache(from);
		}

		public override void Write(TLBinaryWriter to, bool cache = false)
		{
			UpdateFlags();

			to.Write(0x130C2C85);
			to.Write((Int32)Flags);
			to.WriteObject(Id, cache);
			if (HasMessage) to.Write(Message);
			if (HasReplyMarkup) to.WriteObject(ReplyMarkup, cache);
			if (HasEntities) to.WriteObject(Entities, cache);
			if (cache) WriteToCache(to);
		}

		private void UpdateFlags()
		{
			HasMessage = Message != null;
			HasReplyMarkup = ReplyMarkup != null;
			HasEntities = Entities != null;
		}
	}
}