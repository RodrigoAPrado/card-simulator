using System;

namespace YgoSoul.RapTech.Lib.YgoEdo.Util
{
    public class PacketReader
    {
        private readonly byte[] _buffer;

        public PacketReader(byte[] buffer)
        {
            _buffer = buffer;
            Position = 0;
        }

        public int Position { get; private set; }

        public int Length => _buffer.Length;
        public bool EndOfBuffer => Position >= _buffer.Length;

        public byte ReadByte()
        {
            return _buffer[Position++];
        }

        public ushort ReadUInt16()
        {
            var value = BitConverter.ToUInt16(_buffer, Position);
            Position += 2;
            return value;
        }

        public uint ReadUInt32()
        {
            var value = BitConverter.ToUInt32(_buffer, Position);
            Position += 4;
            return value;
        }

        public int ReadInt32()
        {
            var value = BitConverter.ToInt32(_buffer, Position);
            Position += 4;
            return value;
        }

        public ulong ReadULong64()
        {
            var value = BitConverter.ToUInt64(_buffer, Position);
            Position += 8;
            return value;
        }

        public byte[] ReadBytes(int count)
        {
            var result = new byte[count];
            Buffer.BlockCopy(_buffer, Position, result, 0, count);
            Position += count;
            return result;
        }

        public void Skip(int count)
        {
            Position += count;
        }

        public byte PeekByte()
        {
            return _buffer[Position];
        }
    }
}