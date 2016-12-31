// <auto-generated/>
using System;

namespace Telegram.Api.TL
{
	public partial class TLPageBlockCollage : TLPageBlockBase 
	{
		public TLVector<TLPageBlockBase> Items { get; set; }
		public TLRichTextBase Caption { get; set; }

		public TLPageBlockCollage() { }
		public TLPageBlockCollage(TLBinaryReader from, bool cache = false)
		{
			Read(from, cache);
		}

		public override TLType TypeId { get { return TLType.PageBlockCollage; } }

		public override void Read(TLBinaryReader from, bool cache = false)
		{
			Items = TLFactory.Read<TLVector<TLPageBlockBase>>(from, cache);
			Caption = TLFactory.Read<TLRichTextBase>(from, cache);
			if (cache) ReadFromCache(from);
		}

		public override void Write(TLBinaryWriter to, bool cache = false)
		{
			to.Write(0x8B31C4F);
			to.WriteObject(Items, cache);
			to.WriteObject(Caption, cache);
			if (cache) WriteToCache(to);
		}
	}
}