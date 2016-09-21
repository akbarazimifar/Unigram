// <auto-generated/>
using System;

namespace Telegram.Api.TL
{
	public partial class TLReplyKeyboardHide : TLReplyMarkupBase 
	{
		[Flags]
		public enum Flag : Int32
		{
			Selective = (1 << 2),
		}

		public bool IsSelective { get { return Flags.HasFlag(Flag.Selective); } set { Flags = value ? (Flags | Flag.Selective) : (Flags & ~Flag.Selective); } }

		public Flag Flags { get; set; }

		public TLReplyKeyboardHide() { }
		public TLReplyKeyboardHide(TLBinaryReader from, bool cache = false)
		{
			Read(from, cache);
		}

		public override TLType TypeId { get { return TLType.ReplyKeyboardHide; } }

		public override void Read(TLBinaryReader from, bool cache = false)
		{
			Flags = (Flag)from.ReadInt32();
			if (cache) ReadFromCache(from);
		}

		public override void Write(TLBinaryWriter to, bool cache = false)
		{
			UpdateFlags();

			to.Write(0xA03E5B85);
			to.Write((Int32)Flags);
			if (cache) WriteToCache(to);
		}

		private void UpdateFlags()
		{
		}
	}
}