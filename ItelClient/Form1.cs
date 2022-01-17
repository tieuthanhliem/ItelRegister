using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VTPRequest.Request.API8;

namespace ItelClient
{
    public partial class Form1 : Form
    {
        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; // 0123456789
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public Form1()
        {
            InitializeComponent();

            //var tokenId = Guid.NewGuid().ToString();
            var tokenId = "c2aa92b4-5cc7-d6f7-e053-604fc10a04d2";


            AddFileRequest request = new AddFileRequest(tokenId);
            request.CreateRequest();

            //string token64 = RandomString(64); 
            string token64 = "FCXVJZINZDMHCAHRHGLYLMAVOTGZEOPZEIWXJCMRIDBYKMSEYFWNYVITGBEPLTTH";

             LivenessRequest livenessRequest = new LivenessRequest(request.GetImgHash(), token64, tokenId);
            livenessRequest.CreateRequest();
        }
    }
}
