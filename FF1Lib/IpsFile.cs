﻿using System;
using System.Collections.Generic;
using System.Text;
using RomUtilities;

namespace FF1Lib
{
	public class IpsFile
	{
		public const string FF_Grinch_Edition = "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z4+/f4/f5/9///fh9zl5ANuwAB/n5+fn5////5mYmJj4+P7/5+fn5+f///+ZmZmZn5//AOKyAB75+v/w++f//v/vh++cnv/Pj79/j9/n/3//9+H3OXkA47AAH+fn5+fn////mZiYmPj4/v/n5+fn5////5mZmZmfn/8A6rIAHvn6//D75//+/++H75ye/8+Pv3+P3+f/f//34fc5eQDrsAAf5+fn5+f///+ZmJiY+Pj+/+fn5+fn////mZmZmZ+f/wDsNAAAABz/AO0wAB7//////////P/////+/vz//////3///z///////38A8rIAHvn6//D75//+/++H75ye/8+Pv3+P3+f/f//34fc5eQDzsAAf5+fn5+f///+ZmJiY+Pj+/+fn5+fn////mZmZmZ+f/wD6sgAe+fr/8Pvn//7/74fvnJ7/z4+/f4/f5/9///fh9zl5APuwAB/n5+fn5////5mYmJj4+P7/5+fn5+f///+ZmZmZn5//AP4wACD//v78//j48//////8///8/39/vz8/3w////9//98//wD/MAAg/PDhxtjw///z7/755//+/B9nhw8zz///759/888/fz8BqZwAAeQBqbYABwRkEGQRZAQBqdAABOQDFeQBqegABuQCEAQR5AGqBQABZAGqJQAFZBIEFGQBqkYABWQSBBRkAhoxAAcAAAAAAAAAAhpBAAAAFAACGlUA2gEDAwAAAAAAAQMDAAwaPdby2OgADB4/9vLY6AAAAAAAAAEBAAAAAAAAAQEAADB8MyCh4wBwfn8zIKfvAwMDIchQ7PgDEzv9/P7+/NDQwPjg3HgA0PDw+ODceAAAAAADBw8GBgAHBw8PDwYG5gf/////9/7uH//////3/vjw2LCwMCMm+PjYsLAwIyYAAAAAAACAwAAAAAAAAIDABgEfAAYCAAA2MT9gdmLfvziAAAAAAAAAOIAAAAAAAAAsPwAAAAAAACw/AAAAAAAAQAAAAAAAAABAAAAAAAAAAhtwAAEAAht4AAEAAjkiAF4BAgEBAQMAAAAAAAEBAwCAQKBAgICAAAAAAAAAgIAAAAAAAQcFDQAAAAAABgcPCAjIyPz8dKQ8PDy8YCCg4AAAAAAAAQcMAAAAAAABBwwAAAEOOMAAAAAAAQ44wAAAAjmGAAKAQAI5jgOSgEADBwMBAQIODwMBDB8fDwcDgICAxhwYOQ2AgAABw+fG8As7tTkdHQgUD48LhgJid2vCwsKCg4MHAODAwAABAweAAAAAwIBgAAAAAADAgOBAABgwIGBAwMDAGDAgYEDAwMAgEBAICAQEBCAQEAgIBAQEHx8eHicDAycYHx8fHz88P8AwPDgw8pgIfMzAxu78ePAMK/P/Tw8de+/ODA4/d2MHkqLCwv7+/vztnX2+nqZ2dAMDAwMHAAAAAQEDAwAHDx/AwMDAgAADH8DBw8cPnzwgAAAAAAMDAODg+Pz+/PDgAAICAgQECAgQAgICBAQICBA/PzkwMDk/Pz8fBz8/Pw8nDJz9//Ph4fP8/P0fz////+YGzv7w4gbvn//3h8/f+fB8PGJCQ0NO/fbWnLy8vrIAAAAAAAAACEwAMHBg2KxmIwAAAAAAAAAAPnx4eHw+Hw8/PR4fHw8ndgMNDg4ODweG4MCAAIDAAOAAwIAAAMAA4CAgQEBAQCAgICBAQEBAICC///9/PwQDA7D//38/BAMD////+M84QRo/j////zjB+v//D3yAYADA/////IN////z2IAAA05xFPPfv3//zvH3IyFQUCAAYEBvb19fP38fP4DAwMDg4Nj4gMDAwODg+PgAAAAAAAAAAA8HAwMHCxA8cxweDgAAAMCD4ODw/v//D8AAAAAAAAAAwAAAAAAAgPAgEBAIBAMAACAQEAgEAwAADx88cODgwMAPHz9//////zdoWEBAYAAA9+/f39////9goGAAAQEDBn+////////+ECho2KFDB4f37+/fvnx4+WHxOYGDg8HAHw9Hf39/Pz/43MzMzNiYHPj8/Pz8+Pj8AAAAIHx/MzA/Pz8fAwAwMMAAAAAAmPhgB+v4+PBgAGAPDw8PAAAAAPf3NwcAAAAAAACAQEAgIBAAAAAAAAAAAAAAwDgGAQAAAADAOAYBAADAgICAwMDA4P//////////AAAAAAEDBhj////////++A0bN2/cOAD//fv3798/f/8f//+HBwMP//Py8vv5/P//4ODs/Pz48IIfHx8fnzzwghwYcHHDAxp5/Pjw8cMDG3s/Pvj4wIB4+D8//////////wMAAAAAAAD///////////D8fP//Hx8e8Pz8//////8AAAAAgMDwEAAAAACAwPDwCAgEBAIBAQAAAAAAAAAAAAAAQKCQ4BAQAHBAgJDg0BACPSgC6Gho4MCAAAAAcPwnEEyA0CAAAAAAAAAAAPd/PBAIBQMA/388EAgFAwDzgAM//////vOAAz/////+AHx8/PEGfOAA//788Qd//w6MLcyYGDRsDpw9/fv79+/g+MAAAAAAAP//////////gIAAAAMPD////////////wAADgkGAg9w////+f7+//AQEHj4+DiY2PDw+Pj4OJjYAAAGCxIUAAoABh8fPjw4O4BAQCgQEAgoAACAGAAAAOABAQMGJCwcCwAAAAAAAAAAAAQKAUBC5oYHCwWOh4IHZwF8eHx8fHx/AX9/f39/f38DAD1/////////////////4AEBAwYNDBj//////v39+9ygQACAAAAw379/f/////82Li4uL29vb/fv7+/v7ujj93T0+Pj7++H39/f7+/v74c+vzgEBDhn2z7/////++fcM39zcvFycHADc3Ny8fPz8CIh4BxAREAAbDwcAGB8eDgQ7ApvhHgHQ8Pn5+BwAwNwY+BjsLIejMQDg4ODgUDA8AAAQEAkG/PAAAAAAACAAAOOPv//4+HA/44+/////fz9/fn5/Hx8HB39/f38fHwcHwAABAwb8+fv//////vz5+xTomCBAgICA9++fP3/////v4AAAwPD/3//////////f79f3dXY3l5eLA6Qxxvf398PAwPA8z/Psf3///z/P8/w/0AAAAACAQD/Q//////9/DEAAAAAAAAAPf////////xwcHBwcOHj4/Pz8/Pz4+PgICDA8MCEiPA4OPDwwMDAw21wrNkeGXGDcWDMGDz58YEAAUgQw4xwHTj9T8Dj/HwfAAAAAAAH/////////////AAEDAwP///////////////jw8PHmyIAA+PDw8ODAgHg4IMAAAAAAAAAAAAAAAAAAYEBAcGA0AABgYGBweAAAAAoEAwAAAAAACgADAAAAAADYCIwGBQAAANgIjAYAAAAAHw8DAAAAAAAfDwMA//8AAAK5FQADwcHBAtQwALMREgATFAAAABUWRBcAABgZXhobHAAAHQAAHgAAHyAhIgAAACMkJQAmAAAnKCkqKwAALC0uAC8AADAxMjM0NQA2NzgAOQAAOjs8PT4/AEBBQkMAAABFRkdISUpLTE1OTwAAAFNUVFVWVldMWVpbXF1SAF9MYWJjTExlZmdoaWoAbG1uTHBxcnN0dXZ3UAAAa1hgZG94eXp7fH1+AAB/f39/f39/UQAAAACzoFBQe1qqmXdVVQLVxAArsZi3mL6YxZjNmNWY3JjkmOqY75j3mP+YCJkQmRWZGpkfmSSZK5kymTmZQALYuwCbm5KXjJEAkKS2/40Ai6+4qP+NAJa4p5CYlQCbsqaukJiVAJK1srGQmJUAi4qNloqXAI6fkpWWipcAipydmJwAloqQjgCPkpCRnY6bAJaKjZmYl6IAl5KdjpaKm44AoKS1lo6MkQCVkoyRAJWSjJEAlIqbogCUipuiAJSbipSOlwCUm4qUjpcAnZKKloqdAJ2SipaKnQCQm5KXjJEDD34AAigaAxApAAcgKBgPIBYaA3ZQAQAODw8fHz8/f/Hw8O7p0duyAMD4/Pz88OD/PwcDg+MvX3///38/Hw8HsCFljN7s9/jAgMDg8Pj48L9/v9+PBwcPDg8PHx8/P3/x8PDu6MPPvADA+Pz8/PDg/z8HA4PDD19///9/Px8PB7EHHrjD7/T4wIDA4PD4+PC/fz/fzxd3Dw4PDx8fPz9/8fDw7u/b0bsAwPj8/Pzw4P8/BwOD4+/ff///fz8fDwe/L0eu3O73+MCAwODw+Pjwv3+/32/39w8ODw8fHz8/f/Hw8O7v2dmwAMD4/Pz88OD/PwcDg+Pv33///38/Hw8HsGBgoNnp9/jAgMDg8Pj48L9/P1/v9/cPA38xAN2OMrVLoJGYZ0YpH/+gq7K5YUUB//9lrK5A/4yRm5KcnVKcIGWyt2kBi7ghHBqQm5KXjJG/M6suaDInrbg3Af+xNRw2VKCrsrlhRb9nrCeXmJ3EAQH/nR3/kJuSl4yRWTlA/4yRm5KcnVKcxAH/nR0zq7JF/4yRm5KcnVKcJCu2PMQBly48GrS4WxqusUYeHBojPzzAAQH//4u4IZIbPbGuG0EhHBqwsjcB////aK6or0sjPzw2VF+vAf+WpEtBMjHIKRw5WTBZK7W3Af//XR63ui62rL2oHigutrBfrwOtiwABFgO4zQABAUVPRg==";

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
