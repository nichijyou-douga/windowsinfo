using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using Microsoft.Win32;

namespace WindowsFormsApp1
{
	static class Program
	{
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            try
            {
                //Application.Run(new Form1());
                //OSの情報(エディションまで)を取得する
                object registry = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full", "Release", "Return this default if NoSuchName does not exist.");
                string osinfo = getOsinfo();
                if (osinfo == null)
                {
                    return;
                }
                //OSの情報を取得する
                else
                {
                    System.OperatingSystem os = System.Environment.OSVersion;
                    if (registry == null)
                    {
                        MessageBox.Show("バージョン4.5以上5未満が存在しません", "フレームワーク情報エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        string registry_str = "";
                        if ((int)registry >= 528040)
                        {
                            registry_str = "4.8以上5.0未満";
                        }
                        else if ((int)registry >= 461808)
                        {
                            registry_str = "4.7.2以上";
                        }
                        else if ((int)registry >= 461308)
                        {
                            registry_str = "4.7.1以上";
                        }
                        else if ((int)registry >= 460798)
                        {
                            registry_str = "4.7以上";
                        }
                        else if ((int)registry >= 394802)
                        {
                            registry_str = "4.6.2以上";
                        }
                        else if ((int)registry >= 394254)
                        {
                            registry_str = "4.6.1以上";
                        }
                        else if ((int)registry >= 393295)
                        {
                            registry_str = "4.6以上";
                        }
                        else if ((int)registry >= 393273)
                        {
                            registry_str = "4.6 RC以上";
                        }
                        else if (((int)registry >= 379893))
                        {
                            registry_str = "4.5.2以上";
                        }
                        else if (((int)registry >= 378675))
                        {
                            registry_str = "4.5.1以上";
                        }
                        else if (((int)registry >= 378389))
                        {
                            registry_str = "4.5以上";
                        }
                        else
                        {
                            registry_str = "No 4.5 or later version detected";
                        }

                        MessageBox.Show("OSの情報↓\n" + os.ToString() + "\n" + osinfo + "\nOSのバージョン↓\n" + os.Version.ToString() + "\nフレームワークのバージョン↓(バージョンは4.5以上です)\n" + registry_str + "\n" + "共通言語ランタイムのバージョン↓\n" + Environment.Version.ToString(),
                                        "OS情報など", MessageBoxButtons.OK);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("例外発生：" + e.Message, "例外", MessageBoxButtons.OK);
            }

        }

        /// <summary>
        /// OSの情報を取得する
        /// </summary>
        /// <returns>エディションを含むOSの情報</returns>
        static string getOsinfo()
        {
            var name = (from x in new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem").Get().Cast<ManagementObject>()
                        select x.GetPropertyValue("Caption")).FirstOrDefault();
            if (name == null)
            {
                MessageBox.Show("OSの詳細情報（エディション含む）が取得できませんでした", "エラー", MessageBoxButtons.OK);
                return null;
            }
            else
                return name.ToString();
        }

    }
}
