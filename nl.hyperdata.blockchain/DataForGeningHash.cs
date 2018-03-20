using System;

namespace jewelzcoin.blockchain
{
    public struct DataForGeningHash
    {
        public IBlock Block { get; set; }
        public byte[] FailedHash { get; set; }
        public byte[] MinersPublicKey { get; set; }
    }
}