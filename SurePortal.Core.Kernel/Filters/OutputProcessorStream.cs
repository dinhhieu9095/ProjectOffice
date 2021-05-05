using System;
using System.IO;
using System.Text;

namespace DaiPhatDat.Core.Kernel.Filters
{
    internal class OutputProcessorStream : Stream
    {
        private readonly StringBuilder _data = new StringBuilder();

        private readonly Encoding _inputEncoding;
        private readonly Encoding _outputEncoding;
        private readonly Func<string, string> _processor;

        private readonly Stream _stream;

        public OutputProcessorStream(Stream stream, Encoding inputEncoding, Encoding outputEncoding,
            Func<string, string> processor)
        {
            _stream = stream;
            _processor = processor;
            _inputEncoding = inputEncoding;
            _outputEncoding = outputEncoding;
        }

        public override bool CanRead => true;

        public override bool CanSeek => true;

        public override bool CanWrite => true;

        public override long Length => 0;

        public override long Position { get; set; }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _data.Append(_inputEncoding.GetString(buffer, offset, count));
        }

        /// <exception cref="IOException">An I/O error has occurred.</exception>
        /// <exception cref="Exception">A delegate callback throws an exception.</exception>
        public override void Close()
        {
            var output = _outputEncoding.GetBytes(_processor(_data.ToString()));
            _stream.Write(output, 0, output.Length);
            _stream.Flush();
            _data.Clear();
        }

        public override void Flush()
        {
        }

        /// <exception cref="IOException">An I/O error occurs. </exception>
        public override int Read(byte[] buffer, int offset, int count)
        {
            return _stream.Read(buffer, offset, count);
        }

        /// <exception cref="IOException">An I/O error occurs. </exception>
        public override long Seek(long offset, SeekOrigin origin)
        {
            return _stream.Seek(offset, origin);
        }

        /// <exception cref="IOException">An I/O error occurs. </exception>
        public override void SetLength(long value)
        {
            _stream.SetLength(value);
        }
    }
}