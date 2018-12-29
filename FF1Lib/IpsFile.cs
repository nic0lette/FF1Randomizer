using System;
using System.Collections.Generic;
using System.Text;
using RomUtilities;

namespace FF1Lib
{
	public class IpsFile
	{
		private readonly byte[] IPS_MAGIC = Encoding.ASCII.GetBytes("PATCH");
		private const int IPS_EOF = 0x454f46;

		private Blob IpsBlob;
		private List<Tuple<int, Blob>> HunkList = new List<Tuple<int, Blob>>();

		public IpsFile(Blob inputBlob)
		{
			if (inputBlob.SubBlob(0, 5) != IPS_MAGIC)
			{
				throw new ArgumentException("Not an IPS blob");
			}
			IpsBlob = inputBlob;

			var start = 5;
			do
			{
				start = ReadHunk(start);
			} while (start > 0);
		}

		public void ApplyPatch(Rom rom)
		{
			foreach (var hunk in HunkList)
			{
				var start = (hunk.Item1 - rom.HeaderLength);
				if (start < 0)
				{
					//Console.WriteLine("Skipping hunk that starts inside the header.");
					continue;
				}

				var data = hunk.Item2;
				var length = data.Length;

				for (var i = 0; i < length; ++i)
				{
					rom[i + start] = data[i];
				}
			}
		}

		private int ReadHunk(int start)
		{
			int offset = (IpsBlob[start] << 16) | (IpsBlob[start + 1] << 8) | (IpsBlob[start + 2]);
			if (offset == IPS_EOF)
			{
				// EOF
				return - 1;
			}

			int length = (IpsBlob[start + 3] << 8) | (IpsBlob[start + 4]);
			if (length == 0)
			{
				// RLE Chunk -- 2 bytes of length + 1 byte of data
				int runLength = (IpsBlob[start + 5] << 8) | (IpsBlob[start + 6]);
				byte data = IpsBlob[start + 7];

				var patchData = new byte[runLength];
				for (var i = 0; i < runLength; ++i)
				{
					patchData[i] = data;
				}
				HunkList.Add(Tuple.Create<int, Blob>(offset, patchData));
				return start + 8;
			}
			else
			{
				var patchData = IpsBlob.SubBlob(start + 5, length);
				HunkList.Add(Tuple.Create<int, Blob>(offset, patchData));
				return start + length + 5;
			}
		}
	}
}
