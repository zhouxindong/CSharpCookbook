using System;
using System.Security.Cryptography;
using System.Text;

namespace CSharpCookbook
{
    public class HashCodeGenerator
    {
        public int SimpleHash(params int[] values)
        {
            int hash_code = 0;
            if (values != null)
            {
                foreach (var val in values)
                {
                    hash_code ^= val;
                }
            }

            return hash_code;
        }

        public int FoldingHash(params long[] values)
        {
            int hash_code = 0;
            if (values != null)
            {
                int lower = 0;
                int upper = 0;
                foreach (var val in values)
                {
                    lower = (int) (val & 0x000000007FFFFFFF);
                    upper = (int) ((val >> 32) & 0xFFFFFFFF);
                    hash_code ^= lower ^ upper;
                }
            }
            return hash_code;
        }

        public int ContainedObjsHash(params object[] values)
        {
            int hash_code = 0;
            if (values != null)
            {
                foreach (var val in values)
                {
                    hash_code ^= val.GetHashCode();
                }
            }

            return hash_code;
        }

        private readonly byte[] _keys = {1, 122, 3, 11, 65, 7, 9, 45, 42, 98, 77, 34, 99, 45, 167, 211};

        public int CryptoHash(string value)
        {
            int hash_code = 0;
            if (value != null)
            {
                byte[] value_bytes = Encoding.Unicode.GetBytes(value);
                MACTripleDES hashing_obj = new MACTripleDES(_keys);
                byte[] code = hashing_obj.ComputeHash(value_bytes);
                int start = BitConverter.ToInt32(code, 0);
                int end = BitConverter.ToInt32(code, 4);
                hash_code = start ^ end;
            }
            return hash_code;
        }

        public int CryptoHash(long value)
        {
            int hash_code = 0;
            byte[] value_bytes = Encoding.Unicode.GetBytes(value.ToString());
            MACTripleDES hashing_obj = new MACTripleDES(_keys);
            byte[] code = hashing_obj.ComputeHash(value_bytes);
            int start = BitConverter.ToInt32(code, 0);
            int end = BitConverter.ToInt32(code, 4);
            hash_code = start ^ end;
            return hash_code;
        }

        public int ShiftAddHash(string value)
        {
            int hash_code = 0;
            long work = 0;
            if (value != null)
            {
                for (int counter = 0; counter < value.Length; counter++)
                {
                    work = (work << (counter%4)) + (int) value[counter];
                }
                work = work%127;
            }
            hash_code = (int) work;
            return hash_code;
        }
    }
}