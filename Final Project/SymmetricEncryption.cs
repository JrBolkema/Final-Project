using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project
{
	public class SymmetricEncryption
	{
		public byte[] EncryptData(SymmetricAlgorithm symAlg,string plainData)
		{
			byte[] inBlock = Encoding.Unicode.GetBytes(plainData);
			ICryptoTransform xfrm = symAlg.CreateEncryptor();
			byte[] outBlock = xfrm.TransformFinalBlock(inBlock, 0, inBlock.Length);
			return outBlock;
		}

		public string DecryptData(SymmetricAlgorithm symAlg, byte[] cipherData)
		{
			ICryptoTransform xfrm = symAlg.CreateDecryptor();
			byte[] outBlock = xfrm.TransformFinalBlock(cipherData, 0, cipherData.Length);
			return UnicodeEncoding.Unicode.GetString(outBlock);
		}

	}
}
