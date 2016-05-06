using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;
using System.IO;
using System.Security.Cryptography;
namespace openVisio
{
    class MonitorLicenseProvider:LicenseProvider
    {
        class MonitorLicence :License
        {
            private Type _Type;
            public MonitorLicence(Type type)
            {
                if(type==null)
                {
                    throw(new NullReferenceException());
                }
                _Type = type;
            }
            public override void Dispose()
            {
            // TODO: 根据需要插入垃圾回收的代码
            }
            public override string LicenseKey
            {
            get { return (_Type.GUID.ToString()); }
            }
        }
       
        public override License  GetLicense(LicenseContext context, Type type, object instance, bool allowExceptions)//具体的验证函数
        {
            if(context.UsageMode == LicenseUsageMode.Runtime)
            {
                if(ValideLicence())//达到验证条件
                {
                    return (new MonitorLicence(type));
                }
                else
                {
                    throw(new LicenseException(type));
                }
            }
            else if(context.UsageMode == LicenseUsageMode.Designtime)//编辑模式
            {
                return (new MonitorLicence(type));//编辑模式中的验证条件
            }
 	        return null;
        }
        public Boolean ValideLicence()
        {
            //私钥
            string publicRsa = "<RSAKeyValue><Modulus>5TUmLvKQPnvrO54M0cNlYGXA0iCtQsLVah8gBB5TVow90xu4bJMSDCEsyUdwRt6P69U6O4wu3j/f+9yvljI2uitSP4U2OLerABkitzQcwFDAD1yTx3pDaL5W00W5jq9XLWhyqsZcf04Z/Adjb6uE7STlCywJJnyS9lq+5kqoSVs=</Modulus><Exponent>AQAB</Exponent><P>+HG+fNl7MUT/kL/016VWadnXYrGBnzdEy3Rgln2rj36H+wZ5h2756srUiFDJoTNrfDcsJVybhuCmqRT6BwhscQ==</P><Q>7C2jaAO91lUx1TSPv8yolWpZjsk5HgtruO3hlX5k2vmyZ1ySSi+gmxNzC4kroakvSM8WKp3sV2f6QtE0Fz/oiw==</Q><DP>31oyrD9At0yWjWVlErF7fHHxZrK6G48uc9JOOz0DjV9tlEl/3DfyyhhK9bnbaua1TNi1AWm+EIXKjtti5Au4oQ==</DP><DQ>w58tqProY75fL1SA+IHA5TmYgn9DcxJMoHlQjy2IxiNqVNjFRvNaGPisItL0M8vZoXn9x0DVQLScAnobpmJF7w==</DQ><InverseQ>4tIZjC4wZGSb7n6QRG4SVHtlAVGyga2jBbcZi7hJtA7g0L2djG8FVkKGmN+wisozJF6h4mgDsgtbKJKwpMY5dw==</InverseQ><D>WKJP7LGyI+eJlJlOvcMAreFLSjaGqZ1sJ8h3egA8G+jJc5xxmdLZlMUza3ZrrYwdPYZMKYDREvLCa8PwRnWOgX4dZuS95coxBa12fO3mfjFDZEQoAKwJ3jPTJlU82NpLGNFMMxzTXegsZmCTVjmodYpLPYMTSDVP62+c8MOAK6E=</D></RSAKeyValue>";
            //获得本机mac
            List<string> macs = GetMacByIPConfig();
            string mac = macs.ElementAtOrDefault(0);
            string[] messageMac = mac.Split(':');
            mac = messageMac[1];
            
            //读取秘钥文件
            try
            {
                FileStream licensefile = new FileStream("LICENSE.INL", FileMode.Open);
                licensefile.Seek(0, SeekOrigin.Begin);
                //licensefile.l
                byte[] fileStream = new byte[licensefile.Length];
                licensefile.Read(fileStream, 0, (int)licensefile.Length);
                String encodeFileStream = Encoding.UTF8.GetString(fileStream);
                string fileMac = RsaDecrypt(publicRsa, encodeFileStream);

                if (mac.Contains(fileMac))//验证条件
                {
                    return true;
                }
                return false;
            }
            catch (Exception) 
            {
                return false;
            }
        }
        //获取MAC
        public static List<string> GetMacByIPConfig()
        {
          List<string> macs =new List<string>();
          ProcessStartInfo startInfo = new ProcessStartInfo("ipconfig", "/all");
          startInfo.UseShellExecute = false;
          startInfo.RedirectStandardInput = true;
          startInfo.RedirectStandardOutput = true;
          startInfo.RedirectStandardError = true;
          startInfo.CreateNoWindow = true;
          Process p = Process.Start(startInfo);
          //截取输出流
          StreamReader reader = p.StandardOutput;
          string line = reader.ReadLine();

          while (!reader.EndOfStream)
          {
            if (!string.IsNullOrEmpty(line))
            {
              line = line.Trim();

              if (line.StartsWith("Physical Address") || line.StartsWith("物理地址"))
              {
                macs.Add(line);
              }
            }

            line = reader.ReadLine();
          }  //等待程序执行完退出进程
          p.WaitForExit();
          p.Close();
          reader.Close();
 
          return macs;
        }
        public static string RsaDecrypt(string publickey, string content)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publickey);
            byte [] cipherBypes = new byte[content.Length/2];
            for (int x = 0; x < content.Length / 2; x++)
            {
                int g = Convert.ToInt32("C6", 16);
                int i = (Convert.ToInt32(content.Substring(x * 2, 2), 16));
                cipherBypes[x] = (byte)i;
            }
            byte[] source;    //原文byte数组
            source = rsa.Decrypt(cipherBypes, false);
            return Encoding.UTF8.GetString(source);
        }
    }
}
