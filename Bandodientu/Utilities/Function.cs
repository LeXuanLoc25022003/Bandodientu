using System.Security.Cryptography;
using System.Text;

namespace Bandodientu.Utilities
{
	public class Function
	{
		public static int _UserID = 0;
		public static int _CustomerID = 0;
		public static string _UserName = String.Empty;
        public static string _Email = String.Empty;
        public static string _Message = string.Empty;
        public static string _MessageEmail = string.Empty;
		public static string _MessageContact= string.Empty;
		public static string _Phone = String.Empty;
        public static string _About = String.Empty;
        public static string _Image = String.Empty;
        public static string _Company = String.Empty;
        public static string _Job = String.Empty;
        public static string _Country = String.Empty;
        public static string _Address = String.Empty;
        public static string Phone = String.Empty;
		public static string _Password = String.Empty;
        public static int _ProductID;
        public static int _PostID;
        public static string TitleSlugGeneration(string type,string productname,long id)
		{
			string sProduct = type + "-" + SlugGenerator.SlugGenerator.GenerateSlug(productname) + "-" + id.ToString() + ".html";
			return sProduct;
		}
		public static string getCurentDate()
		{
			return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
		}
		public static string MD5Hash(string text)
		{
			MD5 md5 = new MD5CryptoServiceProvider();
			md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
			byte[] result = md5.Hash;
			StringBuilder strBuilder = new StringBuilder();
			for(int i = 0; i < result.Length; i++)
			{
				strBuilder.Append(result[i].ToString("x2"));
			}
			return strBuilder.ToString();
		}
		public static string MD5Password(string? text)
		{
			string str = MD5Hash(text);
			for(int i = 0; i <= 5; i++)
				str = MD5Hash(str + "_" + str);
			return str;
		}

		public static bool IsLogin()
		{
			if(string.IsNullOrEmpty(Function._UserName) || string.IsNullOrEmpty(Function._Email) || (Function._UserID <= 0))
				return false;
			return true;
		}
		public static bool IsLoginCustomer()
		{
			if (string.IsNullOrEmpty(Function._UserName) || string.IsNullOrEmpty(Function._Email) || (Function._CustomerID <= 0))
				return false;
			return true;
		}
	}
}
