using System;
using System.Collections.Generic;
using System.Text;

namespace Software.Domain.Entities
{
    public class Document
    {
        public int DocumentId { get; set; }

        public string Name { get; set; }

        public byte[] Content { get; set; }

        public long FileSize { get; set; }
    }
}
