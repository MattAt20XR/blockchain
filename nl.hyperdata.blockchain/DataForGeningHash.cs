using System;
namespace nl.hyperdata.blockchain
{
    public struct DataForGeningHash
    {
        public IBlock Block { get; set;  }
        public byte[] FailedHash { get; set;  }
        public byte[] MinersPublicKey { get; set;  }
    }
}
