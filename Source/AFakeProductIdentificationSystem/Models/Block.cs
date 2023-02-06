using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using static AFakeProductIdentificationSystem.Models.Transaction;

namespace AFakeProductIdentificationSystem.Models
{
    public class Block
    {
        private readonly DateTime _timeStamp;
        private long _nonce;
        public string PreviousHash { get; set; }
        public List<Transactions> transactions { get; set; }
        public string Hash { get; private set; }
        public string productId { get; set; }


        public Block(string productId, DateTime timeStamp, List<Transactions> transactions, string previousHash = "")
        {
            this.productId = productId;
            _timeStamp = timeStamp;
            _nonce = 0;
            this.transactions = transactions;
            PreviousHash = previousHash;

            Hash = CreateHash(getProductString(this.productId));
        }
        public void MineBlock(int proofOfWorkDifficulty)
        {
            var productInfor = getProductString(this.productId);

            string hashValidationTemplate = new String('0', proofOfWorkDifficulty);

            while (Hash.Substring(0, proofOfWorkDifficulty) != hashValidationTemplate)
            {
                _nonce++;

                Hash = CreateHash(productInfor);
            }

        }
        public string CreateHash()
        {
            using (SHA512 sha512 = SHA512.Create())
            {

                string rawData = productId + _timeStamp.ToString() + _nonce.ToString();


                byte[] bytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public string CreateHash(string _productInfor_)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                string rawData = _productInfor_ + _timeStamp.ToString() + _nonce.ToString();

                byte[] bytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(rawData));
   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private string getProductString(string id)
        {
            if (id == "") return "";
            using (var db = new FakeRealProductSystemEntities())
            {
                var _product = db.Products.Find(id);
                return _product.pr_id + "-" + _product.pr_name + "-" + _product.pr_branch + "-" + _product.pr_type + "-" + _product.pr_origin + "-" + _product.pr_price.ToString();
            }
        }
    }
}